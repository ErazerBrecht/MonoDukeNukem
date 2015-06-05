using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Collisionbox : GameObject
    {
        public Collisionbox(int x, int y)
        {
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, RectangleActive.Width, RectangleActive.Height);
        }

        public override void Draw(SpriteBatch spriteBatchsp)
        {
            //DO NOTHING!
        }
    }
}
