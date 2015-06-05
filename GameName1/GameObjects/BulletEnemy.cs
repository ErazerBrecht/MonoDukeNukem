using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class BulletEnemy : Bullet
    {
        protected static Texture2D _textureBullet;
        protected static Texture2D _textureFire;
        public BulletEnemy(int posX, int posY, Richting r)
        {
            TextureActive = _textureBullet;
            RectangleActive = new Rectangle(0, 0, TextureActive.Width / 2, TextureActive.Height);

            _richting = r;

            if (_richting == Richting.Links)
                Positie.X = posX - RectangleActive.Width;
            else
                Positie.X = posX;

            Positie.Y = posY;
            RectangleCollision = new Rectangle((int)Positie.X, LocationY, RectangleActive.Width, RectangleActive.Height);
            AddBullet(this);
        }

        public override void Update(GameTime g)
        {
            Ticks += g.ElapsedGameTime.Milliseconds;

            if (!this.Explode)
            {
                if (Ticks >= 66)
                {
                    RectangleActive.X += 32;
                    Ticks = 0;
                }

                if (RectangleActive.X >= 64)
                    RectangleActive.X = 0;

                if (_richting == Richting.Rechts)
                    Positie.X += 300 * (float)g.ElapsedGameTime.TotalSeconds;
                else
                    Positie.X -= 300 * (float)g.ElapsedGameTime.TotalSeconds;

                UpdateCollisionRectangles();
            }
            else
            {
                TextureActive = _textureFire;
                RectangleActive.Height = TextureActive.Height;

                if (Ticks >= 66)
                {
                    RectangleActive.X += 32;
                    Ticks = 0;
                }


                if (RectangleActive.X >= 192)
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
           _textureBullet = content.Load<Texture2D>("Bullet");
           _textureFire = content.Load<Texture2D>("Fire");
        }
       

    }
}
