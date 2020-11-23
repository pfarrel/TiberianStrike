using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Input
{
    class InputManager
    {
        private GamePadState previousGamePadState;
        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;

        public PlayerInput Process()
        {
            KeyboardStateWrapper keyboard = new KeyboardStateWrapper(Keyboard.GetState(), previousKeyboardState);

            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();

            return new PlayerInput(keyboard);
        }
    }
}
