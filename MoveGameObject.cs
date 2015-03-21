using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mono
{
    abstract class MoveGameObject : RemoveGameObject
    {
        protected int Ticks;            //Frame indepedant
        public bool Die { get; set; }

        public abstract void Update(GameTime g);
        abstract public void UpdateCollisionRectangles();

        public enum Richting
        {
            Rechts,
            Links,
        }
    }
}
