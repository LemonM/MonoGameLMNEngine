using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using LemonParticlesSystem.GUI.Interfaces;

namespace LemonParticlesSystem.GUI
{
    [Serializable]
    public class Cursor : IDrawableGUIObject
    {
        Rectangle rect;
        [XmlIgnore]
        public Rectangle rectangle { get { return rect; } }
        [XmlIgnore]
        Texture2D Texture;
        [XmlIgnore]
        public Vector2 CurrentPos
        {
            get
            {
                return InputController.Input.Instance.GetMousePosition().ToVector2();
            }
            private set
            {
                throw new Exception("You cant change curosr position");
            }
        }

        public event EventHandler OnHide;
        public event EventHandler OnShow;

        public string PathToTexture { get; set; }

        public Cursor()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>(PathToTexture);
            rect = new Rectangle(0, 0, 16, 16);
        }

        public void Update(GameTime gameTime)
        {
            rect.X = (int)CurrentPos.X;
            rect.Y = (int)CurrentPos.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)CurrentPos.X, (int)CurrentPos.Y, 16, 16), Color.White); 
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
