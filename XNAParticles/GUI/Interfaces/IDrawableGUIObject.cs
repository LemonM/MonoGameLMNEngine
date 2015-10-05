using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using LPS = LemonParticlesSystem.Interfaces;

namespace LemonParticlesSystem.GUI.Interfaces
{
    interface IDrawableGUIObject : IGUIObject, LPS.IDrawable, LPS.IUpdatable
    {
        event EventHandler OnHide;
        event EventHandler OnShow;

        void Hide();

        void Show();

    }
}
