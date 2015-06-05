using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono
{

    public delegate void UpdatePositionHandler(float NewX, float NewY);
    static public class UpdatePositie
    {
        public static event UpdatePositionHandler UpdateEvent;

        static public void Update(float NewX, float NewY)
        {
            if (UpdateEvent != null)
            {
                UpdateEvent(NewX, NewY);
            }
        }
    }
}
