using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Flamethrower:GameObject
    {
        private static Texture2D _textureFlame;
        private BulletFlame _bulletFlame;

        public Flamethrower(int x, int y)
        {
            TextureActive = _textureFlame;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 32, 32);
            RectangleCollision = new Rectangle(LocationX, LocationY, TextureActive.Width, TextureActive.Height);
            _bulletFlame = new BulletFlame(x + TextureActive.Width,y);

        }

        public static void LoadContent(ContentManager content)
        {
            _textureFlame = content.Load<Texture2D>("Flamethrower");
        }
    }


}
