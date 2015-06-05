using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    abstract class Enemy : MoveGameObject
    {
        protected Rectangle _rectangleMove;
        public int Direction { get; set; }
        public Beweging BewegingsRichting { get; set; }      

        public enum Beweging
        {
            Horizontaal,
            Verticaal
        }
        
        
    }
}
