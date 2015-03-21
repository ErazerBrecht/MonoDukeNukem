using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mono
{
    public abstract class GameObject
    {
        protected Texture2D TextureActive;
        protected Vector2 Positie;

        protected Rectangle RectangleActive;
        public Rectangle RectangleCollision;

        public static int MaxWidth { set; get; }

        public int LocationX
        {
            get { return (int)Positie.X; }
            set
            {
                //Gameobject mag zich niet buiten het spel bevinden (max width is de breedte van het level en is dus de maximum x waarde
                //Indien groter wordt de waarde niet meer geset!
                if ((Positie.X < MaxWidth - RectangleCollision.Width - 40 || value < Positie.X) && (Positie.X >= 0 || value > Positie.X))
                    Positie.X = value;
            }
        }

        public int LocationY
        {
            get { return (int)Positie.Y - RectangleActive.Height; }
            set { Positie.Y = value; }
        }

        public bool Damage { get; set; }

        public virtual void Teken(SpriteBatch spriteBatch)
        {
            Positie.Y -= RectangleActive.Height;
            spriteBatch.Draw(TextureActive, Positie, RectangleActive, Color.White);      //Teken vanaf de onderkant van de afbeelding i.p.v. de bovenkant
            Positie.Y += RectangleActive.Height;
        }

        public override string ToString()
        {
            return this.GetType().Name +";"+ this.Positie.X + ";"+ this.Positie.Y;
        }
    }
}
