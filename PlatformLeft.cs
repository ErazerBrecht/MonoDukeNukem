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
    class PlatformLeft:GameObject
    {
        private static Texture2D _picPlatFormLeft;
        public PlatformLeft(int x, int y)
        {
            TextureActive = _picPlatFormLeft;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32,32);
        }

        public static void LoadContent(ContentManager content)
        {
            _picPlatFormLeft = content.Load<Texture2D>("PLatformLeft");
        }
    }
}
