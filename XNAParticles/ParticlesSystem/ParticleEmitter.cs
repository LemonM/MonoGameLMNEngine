using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace LemonParticlesSystem.ParticlesSystem
{
    /// <summary>
    /// Particle emitter class
    /// </summary>
    [Serializable]
    public class ParticleEmitter : IDisposable
    {
        [NonSerialized]
        private Random random;
        private Vector2 EmitterSize;       
        [NonSerialized]
        private Texture2D ParticlesTexture;
        private int _particlesPerEmit;        
        private float TimePerEmit;
        [NonSerialized]
        private float Timer;
        private EventArgs evtArgs;
        private Color ParticleColor;   
        private Vector2 _particlesVelocity;
        private float _particleLifeTime;
        

        [NonSerialized]
        [XmlIgnore]
        public Particle[] ParticlesM; //Particles array
        [XmlElement("Position")]
        public Vector2 EmitterPosition;
        [XmlElement("Path")]
        public string PathToTexture;
        public int MaxParticlesCount;
        [XmlElement("RandomColor")]
        public bool IsRandomColor;
        [XmlElement("RandomDirection")]
        public bool IsRandomDirection;

        public float ParticleAngularVelocity { get; set; }

        public event EventHandler OnEmitterLoad
        {
            add
            {
                new NotImplementedException();
            }

            remove
            {
                new NotImplementedException();
            }
        }
       
        public event EventHandler OnEmitterCreate;



        

        public bool Enabled { get; set; }
        [XmlIgnore]
        public int CurrentParticlesCount { get; set; }

        [XmlElement("PerEmit")]
        public int ParticlesPerEmit
        {
            get { return _particlesPerEmit; }
            set
            {
                if (value < 0)
                    _particlesPerEmit = 0;
                else
                    _particlesPerEmit = value;
            }
        }

        public Vector2 Size
        {
            get { return EmitterSize; }
            set { EmitterSize.X = value.X < 0 ? 0 : value.X; EmitterSize.Y = value.Y < 0 ? 0 : value.Y; }
        }

        public Color ParticlesColor
        {
            get { return ParticleColor; }
            set { 
                    ParticleColor.A = Math.Max((byte)0, (byte)value.A); 
                    ParticleColor.R = Math.Max((byte)0, (byte)value.R);
                    ParticleColor.G = Math.Max((byte)0, (byte)value.G);
                    ParticleColor.B = Math.Max((byte)0, (byte)value.B);
                }
        }

        public Vector2 ParticlesVelocity
        {
            get { return _particlesVelocity; }
            set { _particlesVelocity = value; }
        }

        public float ParticleLifeTime
        {
            get { return _particleLifeTime; }
            set { _particleLifeTime = Math.Max(0, value); }
        }

        public ParticleEmitter()
        {
            random = new Random();
            IsRandomDirection = false;
        }


        public void Update(GameTime gameTime)
        {
            if (Enabled)
            {   
                Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if ((Timer > TimePerEmit) && (MaxParticlesCount > CurrentParticlesCount))
                {
                    for (int i = 0; ParticlesPerEmit > i; i++)
                        Emit();                                     //Particle creation function
                    Timer = 0f;
                }
                
                for (int i = 0; i < MaxParticlesCount; i++)
                {
                    if (ParticlesM[i] != null)
                        ParticlesM[i].Update(gameTime);
                }                
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MaxParticlesCount; i++) 
                {
                     if (ParticlesM[i] != null)
                         if (ParticlesM[i].texture != null)
                            ParticlesM[i].Draw(spriteBatch);
                }   
             
        }

        public void LoadContent(ContentManager contentManager)
        { 
            try
            {
                ParticlesTexture = contentManager.Load<Texture2D>(PathToTexture);
            }
            catch (FileNotFoundException)
            {
                ParticlesTexture = contentManager.Load<Texture2D>(@"Texture\Particles\DefaultTexture");
            }
            ParticlesM = new Particle[MaxParticlesCount];
        }

        public void Emit()
        {
                if (MaxParticlesCount > CurrentParticlesCount)
                {
                    Particle particle = new Particle(new Vector2(random.Next((int)EmitterPosition.X, (int)EmitterPosition.X + (int)EmitterSize.X),
                        random.Next((int)EmitterPosition.Y, (int)EmitterPosition.Y + (int)EmitterSize.Y)), 0f, ParticlesTexture, 0.1f, this, IsRandomColor);
                   // particle.texture = ParticlesTexture;
                    for (int i = 0; i < ParticlesM.Length; i++)
                    {
                        if (ParticlesM[i] == null)
                        {
                            ParticlesM[i] = particle;
                            break;
                        }
                    }
                }
        }

        public void Dispose()
        {
            
            Parallel.For(0, ParticlesM.Length, (i) =>
                {
                    if (ParticlesM[i] != null)
                    {
                        ParticlesM[i].Dispose();
                        ParticlesM[i].Destroy(this, EventArgs.Empty);
                    }
                    i++;
                });
             
            ParticlesTexture.Dispose();
        }
    }
}
