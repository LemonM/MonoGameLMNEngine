using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using LemonParticlesSystem.GUI.Interfaces;

namespace LemonParticlesSystem.GUI
{
    public class Text : IInteractiveGUIObject
    {
        bool isVisible;

        Vector2 size;
        Color color;
        Rectangle rect;

        public event EventHandler OnShow;
        public event EventHandler OnHide;
        public event EventHandler OnClick;
        public event EventHandler OnMouseOver;

        [XmlIgnore]
        public GUI ParentGUI { get; set; }
        
        public string TextString { get; set; }
        public Vector2 Position { get; set; }

        [XmlIgnore]
        public Vector2 Size
        {
            get { return size; }
            set { 
                    size.X = value.X > 0 ? value.X : 0;
                    size.Y = value.Y > 0 ? value.Y : 0;
                }
        }

        public Text()
        {
            rect = new Rectangle(Position.ToPoint(), ParentGUI.Font.MeasureString(TextString).ToPoint());
        }

        public void Hide()
        {
            isVisible = false;
            if (OnHide != null)
                OnHide(this, null);
        }

        public void Show()
        {
            isVisible = true;
            if (OnShow != null)
                OnShow(this, null);
        }

        public void LoadContent(ContentManager content)
        {
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
                spriteBatch.DrawString(ParentGUI.Font, TextString, Position, Color.White);
        }
    }
}
