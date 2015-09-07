using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using LemonParticlesSystem.ParticlesSystem;

namespace LemonParticlesSystem
{
    [Serializable]
    public class Scene
    {
        ContentManager Content;
        Texture2D background;

        [XmlArray]
        public List<ParticleEmitter> Emitters;
        [XmlElement("BackgroundPath")]
        public string backgroundPath;

        [XmlElement("Sound")]
        public Sound.SoundSys sound { get; set; }

        XmlManager<ParticleEmitter> xmlEmitterManager;

        public Scene()
        {
            Emitters = new List<ParticleEmitter>();
            sound = new Sound.SoundSys();
            xmlEmitterManager = new XmlManager<ParticleEmitter>();
        }

        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, content.RootDirectory);
            foreach (ParticleEmitter emitter in Emitters)
            {
                emitter.LoadContent(content);
            }
            sound.LoadContent(Content);
            background = Content.Load<Texture2D>(backgroundPath);
            
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var emitter in Emitters)
            {
                emitter.Update(gameTime);
            }

            sound.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (background != null)
            spriteBatch.Draw(background, new Rectangle(0,0, (int)Screen.ScreenManager.instance.ScreenSize.X,
                                                (int)Screen.ScreenManager.instance.ScreenSize.Y), Color.White);

            foreach (var emitter in Emitters)
            {   
                emitter.Draw(spriteBatch);
            }
        }
    }
}
