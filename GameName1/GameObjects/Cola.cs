using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Cola : RemoveGameObject
    {
        private static Texture2D _picCola;

        public Cola(int x, int y)
        {
            TextureActive = _picCola;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 20, 28);
            RectangleCollision = new Rectangle(LocationX, LocationY, 20, 28);
        }

        public static void LoadContent(ContentManager content)
        {
            _picCola = content.Load<Texture2D>("Cola");
        }
        
    }
}
