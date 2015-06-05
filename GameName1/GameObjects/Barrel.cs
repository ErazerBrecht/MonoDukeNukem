using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Barrel : GameObject
    {
        private static Texture2D _picBarrel;
        public Barrel(int x, int y)
        {
            TextureActive = _picBarrel;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32, 32);
        }

        public static void LoadContent(ContentManager content)
        {
            _picBarrel = content.Load<Texture2D>("Barrel");
        }
        
    }
}
