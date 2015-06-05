using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Box : GameObject
    {
        private static Texture2D _picBox;
        public Box(int x, int y)
        {
            TextureActive = _picBox;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 64, 64);
            RectangleCollision = new Rectangle(LocationX, LocationY, 64, 64);
        }

        public static void LoadContent(ContentManager content)
        {
            _picBox = content.Load<Texture2D>("Box");
        }
        
    }
}
