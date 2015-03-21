using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Leven : GameObject
    {
        private static Texture2D _textureLeven;
        private Vector2 _startPositie;
        public Leven(int x, int y)
        {
            TextureActive = _textureLeven;
            Positie = new Vector2(x, y);
            _startPositie = Positie;      
            RectangleActive = new Rectangle(0, 0, 25, 25);
            UpdatePositie.UpdateEvent += UpdatePositie_UpdateEvent;
        }

        public static void LoadContent(ContentManager content)
        {
            _textureLeven = content.Load<Texture2D>("Heart"); 
        }

        void UpdatePositie_UpdateEvent(float NewX, float NewY)
        {
            LocationX = (int)NewX + (int)_startPositie.X;
            LocationY = (int)NewY + (int)_startPositie.Y;
        }

    }
}
