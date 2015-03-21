using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class MonsterRight:Enemy
    {
        private static Texture2D _textureMonster;
        public MonsterRight(int x, int y)
        {
            TextureActive = _textureMonster;
            Positie = new Vector2(x, y);
            _rectangleMove = new Rectangle(0, 0, TextureActive.Width / 4, TextureActive.Height);
            RectangleActive = _rectangleMove;
            RectangleCollision = new Rectangle(LocationX, LocationY, TextureActive.Width / 4, TextureActive.Height);
            Direction = 1;
            BewegingsRichting = Beweging.Verticaal;
        }

        public override void Update(GameTime g)
        {
            Ticks += g.ElapsedGameTime.Milliseconds;

            if (Ticks >= 66)
            {
                RectangleActive.X += 32;
                Ticks = 0;
            }
            
            if (this.Die == false)
            {
                Positie.Y += 20 * Direction * (float)g.ElapsedGameTime.TotalSeconds;

                if (RectangleActive.X >= 128)
                    RectangleActive.X = 0;

                UpdateCollisionRectangles();
            }

            else
            {
                    this.Remove = true;
            }
        }

        public override void UpdateCollisionRectangles()
        {
            RectangleCollision.X = LocationX;
            RectangleCollision.Y = LocationY;
        }

        public static void LoadContent(ContentManager content)
        {
            _textureMonster = content.Load<Texture2D>("MonsterRight");
        }

    }
}
