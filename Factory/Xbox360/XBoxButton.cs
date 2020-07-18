using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSPadSpy2.Factory.Xbox360
{
    class XBoxButton
    {
        private int x, y;
        private Bitmap bmp;

        public Bitmap BMP
        {
            get { return bmp; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public XBoxButton(int x, int y, Bitmap bmp)
        {
            this.x = x;
            this.y = y;
            this.bmp = bmp;
        }
    }
}
