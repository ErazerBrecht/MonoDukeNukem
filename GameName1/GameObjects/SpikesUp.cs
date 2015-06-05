using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class SpikesUp : GameObject
    {
        private static Texture2D _picSpikesUp;
        public SpikesUp(int x, int y)
        {
            TextureActive = _picSpikesUp;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, 32, 32);
            Damage = true;
        }

        public static void LoadContent(ContentManager content)
        {
            _picSpikesUp = content.Load<Texture2D>("SpikesUp");
        }
        
    }
}
