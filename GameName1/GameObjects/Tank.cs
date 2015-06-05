using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Tank:Enemy
    {
        private static Texture2D _textureRight;
        private static Texture2D _textureLeft;
        private int _oldDirection;
        private int _health;
        private BulletEnemy _laser;

        public Tank(int x, int y)
        {
            TextureActive = _textureRight;
            Positie = new Vector2(x, y);
            _rectangleMove = new Rectangle(0, 0, TextureActive.Width / 2, TextureActive.Height);
            RectangleActive = _rectangleMove;
            RectangleCollision = new Rectangle(LocationX, LocationY, TextureActive.Width / 2, TextureActive.Height);
            Direction = -1;
            BewegingsRichting = Beweging.Horizontaal;
            _health = 3;
        }

        public Tank(int x, int y, int health) : this(x, y)
        {
            _health = health;
        }

        public override void Update(GameTime g)
        {
            if (Direction != _oldDirection)
            {
                if (Direction == 1)
                {
                    TextureActive = _textureRight;
                    _laser = new BulletEnemy(LocationX + RectangleActive.Width, LocationY + RectangleActive.Height / 2, Richting.Rechts);
                }
                else
                {
                    TextureActive = _textureLeft;
                    _laser = new BulletEnemy(LocationX, LocationY + RectangleActive.Height / 2, Richting.Links);
                }
            }

            Ticks += g.ElapsedGameTime.Milliseconds;

            if (Ticks >= 66 * 3)
            {
                RectangleActive.X += 92;
                Ticks = 0;
            }
            
            if (this.Die == false)
            {
                Positie.X += 200 * Direction * (float)g.ElapsedGameTime.TotalSeconds;

                if (RectangleActive.X >= 184)
                    RectangleActive.X = 0;

                UpdateCollisionRectangles();
                _oldDirection = Direction;
            }

            else
            {
                _health--;
                this.Die = false;   //Levens nog niet op!
            }

            if (_health <= 0)
            {
                this.Remove = true;

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (_health < 3)
            {
                PrimitiveDrawing.DrawLiveBar(spriteBatch, new Vector2(LocationX + 10, LocationY - 15) , (int)(100 / (3.0 / _health)), Color.Red);
            }
        }

        public override void UpdateCollisionRectangles()
        {
            RectangleCollision.X = LocationX;
            RectangleCollision.Y = LocationY;
        }

        public static void LoadContent(ContentManager content)
        {
            _textureRight = content.Load<Texture2D>("TankRight");
            _textureLeft = content.Load<Texture2D>("TankLeft");
        }

        public override string ToString()
        {
            return base.ToString() + ";" + _health;
        }
    }
}
