using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Door : GameObject
    {
        private static Texture2D _picDoor;
        public Door(int x, int y)
        {
            TextureActive = _picDoor;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 64, 64);
            RectangleCollision = new Rectangle(LocationX, LocationY, 64, 64);
        }

        public static void LoadContent(ContentManager content)
        {
            _picDoor = content.Load<Texture2D>("Door");
        }
    }
}
