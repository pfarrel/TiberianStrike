using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CncTd
{
    class SpriteFrameSet
    {
        public string Name { get; }
        public int Offset { get; }
        public int Count { get; }

        public SpriteFrameSet(string name, int offset, int count)
        {
            Name = name;
            Offset = offset;
            Count = count;
        }
    }
}
