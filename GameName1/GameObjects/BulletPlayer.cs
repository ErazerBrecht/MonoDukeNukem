using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class BulletPlayer : Bullet
    {

        private Vector2 _startPos;
        private Vector2 _stopPos;
        protected static Texture2D _textureBullet;
        protected static Texture2D _textureFire;

        public BulletPlayer(int posX, int posY, Richting r)
        {
            TextureActive = _textureBullet;
            RectangleActive = new Rectangle(0, 0, TextureActive.Width / 2, TextureActive.Height);

            _richting = r;

            if (_richting == Richting.Links)
                Positie.X = posX - RectangleActive.Width;
            else
                Positie.X = posX;

            Positie.Y = posY;
            _startPos = Positie;
            RectangleCollision = new Rectangle((int)Positie.X, LocationY, RectangleActive.Width, RectangleActive.Height);
            AddBullet(this);

			UpdatePositie.UpdateEvent+=UpdatePositie_UpdateEvent;
        }
		
		void UpdatePositie_UpdateEvent(float NewX, float NewY)
        {
            _stopPos.X = NewX;
            //LocationY = (int)NewY + (int)_startPositie.Y;
        }

        public override void Update(GameTime g)
        {
            Ticks += g.ElapsedGameTime.Milliseconds;
            
            if (Positie.X + this.RectangleActive.Width > _stopPos.X + 800 || Positie.X < _stopPos.X)
                this.Remove = true;                 //Remove bullet if he leaves screen!
            
            else
            {
                if (this.Explode == false)
                {
                    if (Ticks >= 66)
                    {
                        RectangleActive.X += 32;
                        Ticks = 0;
                    }

                    if (RectangleActive.X >= 64)
                        RectangleActive.X = 0;

                    if (_richting == Richting.Rechts)
                    {
                        Positie.X += 400 * (float)g.ElapsedGameTime.TotalSeconds;
                        if (Positie.X > _startPos.X + 300)
                        {
                            Positie.Y += 25 * (float)g.ElapsedGameTime.TotalSeconds; ;
                        }
                    }
                    else
                    {
                        Positie.X -= 400 * (float)g.ElapsedGameTime.TotalSeconds;
                        if (Positie.X < _startPos.X - 300)
                        {
                            Positie.Y += 25 * (float)g.ElapsedGameTime.TotalSeconds; ;
                        }
                    }

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

                UpdateCollisionRectangles();
            }
        }

        public override void UpdateCollisionRectangles()
        {
            RectangleCollision.X = LocationX;
            RectangleCollision.Y = LocationY;
        }

        public static void LoadContent(ContentManager content)
        {
           _textureBullet = content.Load<Texture2D>("BulletEnemy");
           _textureFire = content.Load<Texture2D>("Fire");
        }
       

    }
}
