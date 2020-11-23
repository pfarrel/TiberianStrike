using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TiberianStrike
{
    class SpriteSequence
    {
        public string Name { get; }
        public int Start { get; }
        public int Length { get; set; }
        public int Facings { get; set; }

        public SpriteSequence(string name, int offset)
        {
            Name = name;
            Start = offset;
            Length = 1;
            Facings = 1;
        }
    }
}
