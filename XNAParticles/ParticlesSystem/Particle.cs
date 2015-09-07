using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using LemonParticlesSystem.ParticlesSystem;
using LemonParticlesSystem.Screen;

namespace LemonParticlesSystem.ParticlesSystem
{
    /// <summary>
    /// Particle class
    /// </summary>
    [Serializable]
    public sealed class Particle : IDisposable
    {
        float _sizeX;
        float _sizeY;
        float _positionX;
        float _positionY;
        float Rotation;
        float _scale;
        float _lifeTime;
        float alphaDecreaseTime;
        float alphaDecreaseTimer;
        [NonSerialized]
        float CurrentLifeTime;
        [NonSerialized]
        Rectangle rect;
        EventArgs EvtArgs;
        ParticleEmitter parentEmitter;
        [NonSerialized]
        Random random;
        [NonSerialized]
        Vector2 _velocity;

        [NonSerialized]
        public Texture2D texture;
        /// <summary>
        /// Particle current speed
        /// </summary>
        public Vector2 Speed { get; private set; }
        
        public float AngularVelocity { get; set; }
        /// <summary>
        /// Current angular speed
        /// </summary>
        public Vector2 AngularSpeed { get; private set; }
        public Color color { get; set; }


        /// <summary>
        /// Particle center
        /// </summary>

        public float PositionX
        {
            get { return _positionX; }
            set
            {
                _positionX = value;
            }
        }

        public float PositionY
        {
            get { return _positionY; }
            set
            {
                _positionY = value;
            }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(texture.Width / 2, texture.Height / 2);
            }
            private set { }
        }

        public float Scale
        {
            get { return _scale; }
            set
            {
                if (value < 0)
                    _scale = 1;
                else
                    _scale = value;
            }
        }

        public float Width
        {
            get
            {
                if (texture != null)
                    return texture.Width * Scale;
                else
                    return 0;

            }
            private set { _sizeX = value; }
        }

        public float Height
        {
            get
            {
                if (texture != null)
                    return texture.Width * Scale;
                else
                    return 0;
            }
            private set { _sizeY = value; }
        }

        /// <summary>
        /// One particle life time
        /// </summary>
        public float LifeTime
        {
            get { return _lifeTime; }
            set
            {
                if (value < 0)
                {
                    _lifeTime = 0;
                }
                else
                {
                    _lifeTime = value;
                }
            }
        }

        /// <summary>
        /// When particle life time ends OnDeath event calling.
        /// </summary>
        public event EventHandler OnDeath;
        /// <summary>
        /// When particle appears OnBirth event calling.
        /// </summary>
        public event EventHandler OnBirth;

        public Particle(Vector2 position, float angularVelocity, Texture2D textre, float scale, ParticleEmitter parentEmit, bool RandomColor = false)
        {
            parentEmitter = parentEmit;
            random = new Random();
            texture = textre;
            Scale = (float)random.NextDouble() / 2.5f;
            PositionX = position.X;
            PositionY = position.Y;
            Velocity = parentEmitter.ParticlesVelocity;
            LifeTime = parentEmitter.ParticleLifeTime + random.Next(500);
            Width = textre.Width;
            Height = textre.Height;
            AngularVelocity = parentEmitter.ParticleAngularVelocity;


            if (RandomColor)
                color = new Color(random.Next(255), random.Next(255), random.Next(255), random.Next(240, 255));
            else
                color = parentEmit.ParticlesColor;
            Rotation = 0f;
            Origin = new Vector2((PositionX), (PositionY));


            if (OnBirth != null)
                OnBirth(this, EvtArgs);

            OnDeath += Destroy;
            parentEmitter.CurrentParticlesCount++;
            //System.Diagnostics.Debug.Print(this.ToString() + " created");
            alphaDecreaseTime = LifeTime / color.A;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(PositionX, PositionY), null, color, Rotation, new Vector2(texture.Width / 2, texture.Height / 2), Scale, SpriteEffects.None, Scale);
        }

        public void Update(GameTime gameTime)
        {
            CurrentLifeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            alphaDecreaseTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (alphaDecreaseTimer > alphaDecreaseTime)
            {
                color = new Color((byte)color.R, (byte)color.G, (byte)color.B, ((byte)Math.Max(0, (color.A - 1.5f))));
                alphaDecreaseTimer = 0f;
            }
            
            if (CurrentLifeTime > LifeTime)
            {
                if (OnDeath != null)
                    OnDeath(this, EvtArgs);
                //System.Diagnostics.Debug.Print("Particle timer = " + Timer.ToString());
                CurrentLifeTime = 0f;
            }


            if (PositionX > ScreenManager.instance.ScreenSize.X || PositionY > ScreenManager.instance.ScreenSize.Y ||
                PositionX < 0 || PositionY < 0)
            {
                if (OnDeath != null)
                    OnDeath(this, EvtArgs);
            }
            PositionX += Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds * Scale;
            PositionY += Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * Scale;
            Rotation += AngularVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;



        }

        public void Destroy(object Sender, EventArgs eArgs)
        {

            // System.Diagnostics.Debug.Print(Sender.ToString() + " destroyed");
            Parallel.For(0, parentEmitter.ParticlesM.Length, (i, loopState) =>
                {
                    if (parentEmitter.ParticlesM[i] != null)
                    {
                        if (parentEmitter.ParticlesM[i].Equals(Sender))
                        {
                            //parentEmitter.ParticlesM[i].texture.Dispose();
                            parentEmitter.ParticlesM[i] = null;

                            // Sender = null;
                            parentEmitter.CurrentParticlesCount--;
                            loopState.Stop();
                        }
                    }
                });
            /*
            for (int i = 0; i < parentEmitter.ParticlesM.Length; i++)
            {
                if (parentEmitter.ParticlesM[i] != null)
                {
                    if (parentEmitter.ParticlesM[i].Equals(Sender))
                    {
                        //parentEmitter.ParticlesM[i].texture.Dispose();
                        parentEmitter.ParticlesM[i] = null;

                        // Sender = null;
                        parentEmitter.CurrentParticlesCount--;
                        break;
                    }
                }
            }
             */ 
        }

        public void Dispose()
        {

        }
    }
}
