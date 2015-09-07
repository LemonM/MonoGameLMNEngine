using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LemonParticlesSystem.Screen
{
    public class GameScreen
    {
        protected ContentManager Content;

        [XmlIgnore]
        public Type type;

        public GameScreen()
        {
            type = this.GetType();
        }

        public virtual void LoadContent(ContentManager content) 
        {
            Content = new ContentManager(content.ServiceProvider, content.RootDirectory);
        }
        public virtual void UnloadContent() 
        {
            Content.Unload();
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
