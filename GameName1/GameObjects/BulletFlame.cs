using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class BulletFlame : Bullet
    {
        private int _teller = 0;
        protected static Texture2D _textureBullet;

        public BulletFlame(int posX, int posY)
        {
            TextureActive = _textureBullet;
            RectangleActive = new Rectangle(0, 0, 22, TextureActive.Height);
            Positie.X = posX;
            Positie.Y = posY;
            RectangleCollision = new Rectangle((int)Positie.X, LocationY, RectangleActive.Width, RectangleActive.Height);
            AddBullet(this);
        }

        public override void Update(GameTime g)
        {
            Ticks += g.ElapsedGameTime.Milliseconds;

            if (Ticks > 33)
            {
                _teller++;
                Ticks = 0;
            }

            if(_teller > 48)
                _teller = 0;

            if (_teller > 40)
                RectangleActive.X = 224;

            else if (_teller > 16)
            {
                RectangleActive.Width = 96;
                if ((_teller / 2) % 4 == 1)
                    RectangleActive.X = 0;

                else if (_teller % 2 == 1)
                    RectangleActive.X = 96;
            }

            else
            {
                RectangleActive.Width = 22;
                if ((_teller/2)%4 == 1)             
                    RectangleActive.X = 224;

                else if (_teller%2 == 1)
                    RectangleActive.X = 192;
            }

            UpdateCollisionRectangles();

        }

        public override void UpdateCollisionRectangles()
        {
            RectangleCollision.X = LocationX;
            RectangleCollision.Y = LocationY;

            RectangleCollision.Width = RectangleActive.Width;

            if (RectangleActive.X == 224)
                RectangleCollision.Width = 0;
        }

        public static void LoadContent(ContentManager content)
        {
           _textureBullet = content.Load<Texture2D>("BulletFire");
        }
    }
}
