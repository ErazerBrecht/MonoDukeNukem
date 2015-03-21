using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class PlatformMiddle : GameObject
    {
        private static Texture2D _picPlatFormMiddle;
        public PlatformMiddle(int x, int y)
        {
            TextureActive = _picPlatFormMiddle;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32, 32);
        }

        public static void LoadContent(ContentManager content)
        {
            _picPlatFormMiddle = content.Load<Texture2D>("PlatformMiddle");
        }


    }
}