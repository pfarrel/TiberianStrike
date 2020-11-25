using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiberianStrike.Input;

namespace TiberianStrike
{
    class PlayerInput
    {
        public bool Debug { get { return keyboard.LeadingEdge(Keys.F1); } }
        public bool Pause { get { return keyboard.LeadingEdge(Keys.P); } }
        public bool Quit { get { return keyboard.LeadingEdge(Keys.Escape); } }
        public bool Left { get { return keyboard.IsKeyDown(Keys.A); } }
        public bool Right { get { return keyboard.IsKeyDown(Keys.D); } }
        public bool Up { get { return keyboard.IsKeyDown(Keys.W); } }
        public bool Down { get { return keyboard.IsKeyDown(Keys.S); } }
        public bool MainWeapon { get { return keyboard.IsKeyDown(Keys.Space); } }
        public bool Bomb { get { return keyboard.IsKeyDown(Keys.B); } }
        public bool Rocket { get { return keyboard.IsKeyDown(Keys.R); } }

        private readonly KeyboardStateWrapper keyboard;

        public PlayerInput(KeyboardStateWrapper keyboard)
        {
            this.keyboard = keyboard;
        }


    }
}
