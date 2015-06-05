using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Floppy : RemoveGameObject
    {
        private static Texture2D _picFloppy;

        public Floppy(int x, int y)
        {
            TextureActive = _picFloppy;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 28);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32, 28);
        }

        public static void LoadContent(ContentManager content)
        {
            _picFloppy = content.Load<Texture2D>("Floppy");
        }
        
    }
}
