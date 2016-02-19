using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemonParticlesSystem.GUI.Interfaces
{
    interface IInteractiveGUIObject : IDrawableGUIObject
    {
        event EventHandler OnClick;
        event EventHandler OnMouseOver;
    }
}
