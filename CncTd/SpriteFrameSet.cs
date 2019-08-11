using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CncTd
{
    class SpriteFrameSet
    {
        public string Name { get; }
        public int Start { get; }
        public int Length { get; set; }
        public int Facings { get; set; }

        public SpriteFrameSet(string name, int offset)
        {
            Name = name;
            Start = offset;
            Length = 1;
            Facings = 1;
        }
    }
}
