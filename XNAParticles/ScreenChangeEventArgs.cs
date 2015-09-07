using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemonParticlesSystem.Screen
{
    public class ScreenChangeEventArgs : EventArgs
    {
        public GameScreen currentScreen { get; private set; }
        public GameScreen newScreen { get; private set; }

        public ScreenChangeEventArgs(GameScreen currentScr, GameScreen newScr)
        {
            currentScreen = currentScr;
            newScreen = newScr;
        }
    }
}
