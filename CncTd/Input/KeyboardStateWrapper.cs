using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike.Input
{
    class KeyboardStateWrapper
    {
        public KeyboardState Current { get; private set; }
        public KeyboardState Previous { get; private set; }

        public KeyboardStateWrapper(KeyboardState current, KeyboardState previous)
        {
            Current = current;
            Previous = previous;
        }

        public bool IsKeyDown(Keys key)
        {
            return Current.IsKeyDown(key);
        }

        public bool LeadingEdge(Keys key)
        {
            return Current.IsKeyDown(key) && !Previous.IsKeyDown(key);
        }

        public bool TrailingEdge(Keys key)
        {
            return !Current.IsKeyDown(key) && Previous.IsKeyDown(key);
        }
    }
}
