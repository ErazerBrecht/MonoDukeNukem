using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mono;

namespace Mono
{
    class Background : GameObject
    {
        private static Texture2D _picBackground;
        public Background(int x, int y)
        {
            TextureActive = _picBackground;
            Positie = new Vector2(x, y);
            RectangleActive = new Rectangle(0, 0, 800, 640);
            UpdatePositie.UpdateEvent += UpdatePositie_UpdateEvent;
        }

        private void UpdatePositie_UpdateEvent(float NewX, float NewY)
        {
            LocationX = (int)NewX;
            LocationY = (int)NewY;
        }

        public static void LoadContent(ContentManager content)
        {
            _picBackground = content.Load<Texture2D>("Background");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Overidden omdat background vanaf 0,0 getekend moet worden!
            spriteBatch.Draw(TextureActive, Positie, RectangleActive, Color.White);      
        }

    }
}
