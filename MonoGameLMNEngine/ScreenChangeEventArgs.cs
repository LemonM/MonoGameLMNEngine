using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemonParticlesSystem.Screen
{
    public class ScreenChangeEventArgs : EventArgs
    {
        public Screen currentScreen { get; private set; }
        public Screen newScreen { get; private set; }

        public ScreenChangeEventArgs(Screen currentScr, Screen newScr)
        {
            currentScreen = currentScr;
            newScreen = newScr;
        }
    }
}
