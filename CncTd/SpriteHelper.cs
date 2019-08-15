using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike
{
    class SpriteHelper
    {
        // unused manual remap of turret sprite pixels.
        // need to replace with palette based recolor.
        public void recolor(ContentManager content)
        {
            Texture2D refinerySprite = content.Load<Texture2D>("gun-turret");
            Color[] data = new Color[refinerySprite.Width * refinerySprite.Height];
            refinerySprite.GetData(data);
            Color source = new Color(246, 214, 121);
            Color source2 = new Color(222, 190, 105);
            Color source3 = new Color(178, 149, 80);
            Color source4 = new Color(170, 153, 85);
            Color source5 = new Color(194, 174, 97);
            Color source6 = new Color(198, 170, 93);
            Color source7 = new Color(145, 137, 76);
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == source || data[i] == source2 || data[i] == source3 || data[i] == source4 || data[i] == source5 || data[i] == source6 || data[i] == source7)
                {
                    data[i] = Color.Red;
                }
            }
            refinerySprite.SetData(data);
        }
    }
}
