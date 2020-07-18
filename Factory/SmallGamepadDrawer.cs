using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI = SharpDX.DirectInput;

namespace OBSPadSpy2.Factory
{
    class SmallGamepadDrawer
    {
        private byte[] imageArray;

        public SmallGamepadDrawer()
        {

        }

        public byte[] Draw(DI.JoystickState state)
        {

            return imageArray;
        }
    }
}
