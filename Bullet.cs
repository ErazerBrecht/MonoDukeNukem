using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono
{
    abstract class Bullet: MoveGameObject
    {
        protected Richting _richting;
        public bool Explode { get; set; }

        protected static void AddBullet(Bullet b)
        {
            BulletList.Add(b);
        }

        public static List<Bullet> BulletList = new List<Bullet>();
    }

  
}
