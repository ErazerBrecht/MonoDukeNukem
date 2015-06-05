using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    class Score : GameObject
    {
        private static SpriteFont _fontScore;
        private Vector2 _startPositie;
        public string ScoreString { get; set; }
        public Score(int x, int y)
        {
            Positie = new Vector2(x, y);
            _startPositie = Positie;
            UpdatePositie.UpdateEvent += UpdatePositie_UpdateEvent;
        }

        void UpdatePositie_UpdateEvent(float NewX, float NewY)
        {
            LocationX = (int)NewX + (int)_startPositie.X;
            LocationY = (int)NewY + (int)_startPositie.Y;
        }

        public static void LoadContent(ContentManager content)
        {
            _fontScore = content.Load<SpriteFont>("Font"); 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_fontScore, "Score: " + ScoreString, Positie, Color.White);
        }

    }
}
