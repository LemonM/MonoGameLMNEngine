using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LemonParticlesSystem.GUI
{
    public class Text : GUIObject
    {
        Vector2 size;

        Color color;

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

        public Text() { }

        public void LoadContent(ContentManager content)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ParentGUI.Font, TextString, Position, Color.White);
        }
    }
}
