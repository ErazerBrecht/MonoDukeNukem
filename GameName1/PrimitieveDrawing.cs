using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Mono
{
    /// <summary>
    /// Based on Oliver's work (Bedank mij met koffie pls) - Static class for drawing simple shapes, based on XNA documentation.
    /// Made by Brecht Carlier (More efficient code!)
    /// </summary>
    internal static class PrimitiveDrawing
    {

        static private Rectangle temp;
        static private Texture2D whitePixel;

        /// <summary>
        /// Draw a rectangle of certain linewidth and color.
        /// </summary>
        /// <param name="whitePixel">A texture of a single white pixel.</param>
        /// <param name="batch">The spritebatch to draw to.</param>
        /// <param name="area">The rectangle to draw.</param>
        /// <param name="width">The linewidth of the rectangle.</param>
        /// <param name="color">The color to apply to the line.</param>
        /// 

        public static void DrawRectangle(SpriteBatch batch, Rectangle area, Color color)
        {
            temp = area;
            
            //Bovenste lijn
            temp.Height = 1;
            batch.Draw(whitePixel, temp, color);

            //Onderste lijn
            temp.Y = area.Y + area.Height - 1;
            batch.Draw(whitePixel, temp, color);

            //Linkse lijn
            temp.Y = area.Y;
            temp.Height = area.Height;
            temp.Width = 1;
            batch.Draw(whitePixel, temp, color);

            //Rechtse lijn
            temp.X = area.X + area.Width - 1;
            batch.Draw(whitePixel, temp, color);

            //Oliver's Code
            //batch.Draw(whitePixel, new Rectangle(area.X, area.Y, area.Width, 1), color);
            //batch.Draw(whitePixel, new Rectangle(area.X, area.Y, 1, area.Height), color);
            //batch.Draw(whitePixel, new Rectangle(area.X + area.Width - 1, area.Y, 1, area.Height), color);
            //batch.Draw(whitePixel, new Rectangle(area.X, area.Y + area.Height - 1, area.Width, 1), color);
        }

        public static void DrawLiveBar(SpriteBatch batch, Vector2 pos, int procent, Color color)
        {
            Rectangle temp = new Rectangle((int)pos.X,(int)pos.Y,60,7);
            DrawRectangle(batch, temp, color);
            temp.Width = (temp.Width * procent) / 100;
            batch.Draw(whitePixel, temp, color);
        }

        public static void LoadContent(ContentManager content)
        {
            whitePixel = content.Load<Texture2D>("pxl");
        }

    }
}