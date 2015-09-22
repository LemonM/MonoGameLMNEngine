using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LemonParticlesSystem.InputController
{
    public class Input
    {
        KeyboardState kbState;
        MouseState mouseState;

        public List<Keys> PressedKeys
        {
            get { kbState = Keyboard.GetState(); return kbState.GetPressedKeys().ToList(); }
        }

        static Input instance;

        public static Input Instance
        {
            get
            {
                if (instance == null)
                    instance = new Input();
                    return instance;
            }
        }

        private Input()
        { }

        public bool IsKeyPressed(Keys key)
        {
            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public bool IsLMBPressed()
        {
            UpdateMouseState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool IsRMBPressed()
        {
            UpdateMouseState();
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public bool IsCMBPressed()
        {
            UpdateMouseState();
            if (mouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public Point GetMousePosition()
        {
            UpdateMouseState();
            return mouseState.Position;
        }

        private void UpdateMouseState()
        {
            mouseState = Mouse.GetState();
        }
    }
}
