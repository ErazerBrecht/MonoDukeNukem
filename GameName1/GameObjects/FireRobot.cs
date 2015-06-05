using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class FireRobot:Enemy
    {
        private static Texture2D _textureFireRobot;
        private int _health;


        public FireRobot(int x, int y)
        {
            TextureActive = _textureFireRobot;
            Positie = new Vector2(x, y);
            _rectangleMove = new Rectangle(0, 0, 66, TextureActive.Height);
            RectangleActive = _rectangleMove;
            RectangleCollision = new Rectangle(LocationX, LocationY, 66, TextureActive.Height);
            Direction = -1;
            BewegingsRichting = Beweging.Horizontaal;
            _health = 4;
        }

        public FireRobot(int x, int y, int health) : this(x, y)
        {
            _health = health;
        }

        public override void Update(GameTime g)
        {
            
            Ticks += g.ElapsedGameTime.Milliseconds;

            if (Ticks >= 66 * 2)
            {
                RectangleActive.X += RectangleActive.Width;
                Ticks = 0;
            }
            
            if (this.Die == false)
            {
                Positie.X += 300 * Direction * (float)g.ElapsedGameTime.TotalSeconds;

                if (RectangleActive.X == 264)
                {
                    RectangleCollision.Height = RectangleActive.Height;
                    RectangleCollision.Y = LocationY;
                    RectangleActive.Width = 92;
                }
                    
                if (RectangleActive.X >= 632)
                {
                    RectangleCollision.Height = 69;
                    RectangleCollision.Y = RectangleCollision.Y + RectangleActive.Height - 69;
                    RectangleActive.X = 0;
                    RectangleActive.Width = 66;
                }

                UpdateCollisionRectangles();
              
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

        public override void UpdateCollisionRectangles()
        {
            RectangleCollision.X = LocationX;
            //RectangleCollision.Y = LocationY;
            RectangleCollision.Width = RectangleActive.Width;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (_health < 4)
            {
                PrimitiveDrawing.DrawLiveBar(spriteBatch, new Vector2(LocationX + 10, LocationY - 15), (int) (100 / (4.0 / _health)), Color.Red);
            }
        }

        public static void LoadContent(ContentManager content)
        {
            _textureFireRobot = content.Load<Texture2D>("FireRobot");
        }
        public override string ToString()
        {
            return base.ToString() + ";" + _health;
        }

    }
}
