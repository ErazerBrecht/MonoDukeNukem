using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Text : GameObject
    {
        public string Print { get; set; }
        private static SpriteFont _fontText;
        public Text(int x, int y)
        {
            Positie = new Vector2(x, y);
        }


        public static void LoadContent(ContentManager content)
        {
            _fontText = content.Load<SpriteFont>("Font");
        }

        public override void Teken(SpriteBatch spriteBatch)
        {
            //Zwart kaderje rond de tekst!
            spriteBatch.DrawString(_fontText, Print, new Vector2(Positie.X + 2, Positie.Y + 2), Color.Black);
            spriteBatch.DrawString(_fontText, Print, new Vector2(Positie.X + 2, Positie.Y - 2), Color.Black);
            spriteBatch.DrawString(_fontText, Print, new Vector2(Positie.X - 2, Positie.Y + 2), Color.Black);
            spriteBatch.DrawString(_fontText, Print, new Vector2(Positie.X - 2, Positie.Y - 2), Color.Black);
            //Tekst zelf
            spriteBatch.DrawString(_fontText, Print, Positie, Color.Yellow);
        }

    }
}
