using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Robot:Enemy
    {
        private static Texture2D _textureWalk;
        public Robot(int x, int y)
        {
            TextureActive = _textureWalk;
            Positie = new Vector2(x, y);
            _rectangleMove = new Rectangle(0, 0, TextureActive.Width / 9, TextureActive.Height);
            RectangleActive = _rectangleMove;
            RectangleCollision = new Rectangle(LocationX, LocationY, TextureActive.Width / 9, TextureActive.Height);
            Direction = 1;
            BewegingsRichting = Beweging.Horizontaal;
        }

        public override void Update(GameTime g)
        {
            Ticks += g.ElapsedGameTime.Milliseconds;

            if (Ticks >= 66 * 3)
            {
                RectangleActive.X += 48;
                Ticks = 0;
            }


            if (this.Die == false)
            {
                Positie.X += 200 * Direction * (float)g.ElapsedGameTime.TotalSeconds;

                if (RectangleActive.X >= 144)
                    RectangleActive.X = 0;

                UpdateCollisionRectangles();
            }

            else
            {
                RectangleCollision = new Rectangle(0, 0, 0, 0);     //Zorgen dat de speler niet meer kan botsen met een stervende enemy!
                if (RectangleActive.X < 144)
                    RectangleActive.X = 144;

                if (RectangleActive.X >= TextureActive.Width)
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
            _textureWalk = content.Load<Texture2D>("Enemy");
        }

    }
}
