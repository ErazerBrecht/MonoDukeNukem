using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class KeyRed : RemoveGameObject
    {
        private static Texture2D _picKeyRed;

        public KeyRed(int x, int y)
        {
            TextureActive = _picKeyRed;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, _picKeyRed.Width, _picKeyRed.Height);
            RectangleCollision = new Rectangle(LocationX, LocationY, RectangleActive.Width, RectangleActive.Height);
        }

        public static void LoadContent(ContentManager content)
        {
            _picKeyRed = content.Load<Texture2D>("KeyRed");
        }
        
    }
}
