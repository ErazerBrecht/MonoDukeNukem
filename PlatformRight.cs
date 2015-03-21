using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono
{
    class PlatformRight : GameObject
    {
        private static Texture2D _picPlatFormRight;
        public PlatformRight(int x, int y)
        {
            TextureActive = _picPlatFormRight;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX,LocationY,32, 32);
        }

        public static void LoadContent(ContentManager content)
        {
            _picPlatFormRight = content.Load<Texture2D>("PlatformRight");
        }
    }
}