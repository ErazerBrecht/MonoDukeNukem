using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class SpikesDown : GameObject
    {
        private static Texture2D _picSpikesDown;
        public SpikesDown(int x, int y)
        {
            TextureActive = _picSpikesDown;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32, 32);
            Damage = true;
        }

        public static void LoadContent(ContentManager content)
        {
            _picSpikesDown = content.Load<Texture2D>("SpikesDown");
        }
        
    }
}
