using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono
{
    public class PlatformGroundReverse: GameObject
    {
        private static Texture2D Ground;
        public PlatformGroundReverse(int x, int y)
        {
            TextureActive = Ground;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 64);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32, 64);
        }

        public static void LoadContent(ContentManager content)
        {
            Ground = content.Load<Texture2D>("PlatformGroundReverse");
        }
        
    }
}
