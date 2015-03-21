using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mono
{

    public delegate void UpdateKeyHandler();
    static public class UpdateKey
    {
        public static event UpdateKeyHandler UpdateEvent;

        static public void Update()
        {
            if (UpdateEvent != null)
            {
                UpdateEvent();
            }
        }
    


    }

}
