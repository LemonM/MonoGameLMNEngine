using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LemonParticlesSystem.GUI
{
    [Serializable]
    public class Cursor : GUIObject
    {
        Rectangle rect;
        [XmlIgnore]
        public Rectangle rectangle { get { return rect; } }
        [XmlIgnore]
        Texture2D Texture;
        [XmlIgnore]
        public Vector2 CurrentPos { get; private set; }
        [XmlIgnore]
        public MouseState state;

        public string PathToTexture { get; set; }

        public Cursor()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            state = new MouseState(0, 0, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            Texture = content.Load<Texture2D>(PathToTexture);
            rect = new Rectangle(0, 0, 16, 16);
        }

        public void Update(GameTime gameTime)
        {
            state = Mouse.GetState();
            CurrentPos = new Vector2(state.X, state.Y);
            rect.X = (int)CurrentPos.X;
            rect.Y = (int)CurrentPos.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)CurrentPos.X, (int)CurrentPos.Y, 16, 16), Color.White); 
        }
    }
}
