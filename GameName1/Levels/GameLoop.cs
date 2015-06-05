using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    abstract class GameLoop
    {
        public abstract GameLoop Update(GameTime g);
        public abstract void Teken(SpriteBatch spriteBatch);

        protected const string _FILENAME = "Save.csv";
    }
}
