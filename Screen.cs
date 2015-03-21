using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Mono
{
    static class Screen
    {
        static public Vector2 Positie;
        static public int _width { get; set; }
        static public int _height { get; set; }

        static private Matrix mMatrix;
        static public Matrix ViewMatrix
        {
            get { return mMatrix; }

            set
            {
                mMatrix = value;
            } 
        }

        static public void Update(int _characterPositionX, int _characterPositionY)
        {
            Positie = new Vector2(_characterPositionX - GraphicsDeviceManager.DefaultBackBufferWidth / 2, _characterPositionY - 600 / 2);            //Pas aanpassen indien we in het midden van het scherm zijn (/2)

            if (Positie.X < 0)
                Positie.X = 0;
            else if (Positie.X > _width - 800)
                 Positie.X = _width - 800;

            if (Positie.Y < 0)
                Positie.Y = 0;
            else if (Positie.Y > _height - 600)
                Positie.Y = _height - 600;

                UpdatePositie.Update(Positie.X, Positie.Y);
                ViewMatrix = Matrix.CreateTranslation(new Vector3(-Positie, 0));
        }
    }
}
