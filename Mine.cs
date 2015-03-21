using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Mine : Enemy
    {
        private static Texture2D _textureMine;
        private float _gravity = 300;
        private int _oldDirection;
        public Mine(int x, int y)
        {
            TextureActive = _textureMine;
            Positie = new Vector2(x, y);
            _rectangleMove = new Rectangle(0, 0, TextureActive.Width, TextureActive.Height);
            RectangleActive = _rectangleMove;
            RectangleCollision = new Rectangle(LocationX, LocationY, TextureActive.Width, TextureActive.Height);
            Direction = 1;
            _oldDirection = Direction;
            BewegingsRichting = Beweging.Verticaal;
        }

        public override void Update(GameTime g)
        {
            if (Direction != _oldDirection)
                _gravity = -300;

                Positie.Y += _gravity  * (float) g.ElapsedGameTime.TotalSeconds;
                _gravity += 300 * (float)g.ElapsedGameTime.TotalSeconds;


           
            UpdateCollisionRectangles();
            _oldDirection = Direction;
        }

        public override void UpdateCollisionRectangles()
        {
            RectangleCollision.X = LocationX;
            RectangleCollision.Y = LocationY;
        }

        public static void LoadContent(ContentManager content)
        {
            _textureMine = content.Load<Texture2D>("Mine");
        }

    }
}
