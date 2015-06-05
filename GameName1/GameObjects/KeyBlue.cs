using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class KeyBlue : RemoveGameObject
    {
        private static Texture2D _picKeyBlue;

        public KeyBlue(int x, int y)
        {
            TextureActive = _picKeyBlue;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, _picKeyBlue.Width, _picKeyBlue.Height);
            RectangleCollision = new Rectangle(LocationX, LocationY, RectangleActive.Width, RectangleActive.Width);
        }

        public static void LoadContent(ContentManager content)
        {
            _picKeyBlue = content.Load<Texture2D>("KeyBlue");
        }
        
    }
}
