using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LPS = LemonParticlesSystem.Interfaces;

namespace LemonParticlesSystem.Screen
{
    public abstract class Screen : LPS.IDrawable, LPS.IUpdatable
    {
        protected ContentManager Content;

        [XmlIgnore]
        public Type type;

        public Screen()
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
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
