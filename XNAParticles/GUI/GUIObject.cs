using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace LemonParticlesSystem.GUI
{
    public virtual class GUIObject
    {
        string name;
        GUI ParentGUI { get; set; }
        public string Name
        {
            get;
            set
            {
                if (ParentGUI.GuiObjects.Find((obj) => obj.Name == value) != null)
                    name = string.Format(value);
            }
        }
        void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
