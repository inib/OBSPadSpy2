using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DI = SharpDX.DirectInput;

namespace OBSPadSpy2.Factory
{
    class XBoxDrawer
    {
        private byte[] imageArray;

        private Bitmap pad;
        private Bitmap bx;
        private Bitmap by;
        private Bitmap ba;
        private Bitmap bb;
        private Bitmap bl;
        private Bitmap br;
        private Bitmap bse;
        private Bitmap bst;
        private Bitmap sl;
        private Bitmap sr;
        private Bitmap dl;
        private Bitmap dr;
        private Bitmap du;
        private Bitmap dd;
        private Bitmap dru;
        private Bitmap drd;
        private Bitmap dlu;
        private Bitmap dld;
        private Bitmap tl0;
        private Bitmap tl1;
        private Bitmap tl2;
        private Bitmap tr0;
        private Bitmap tr1;
        private Bitmap tr2;
        private Bitmap workBmp;

        private Bitmap trace;
        private Brush traceBrush;
        private Graphics traceGraph;

        private CLROBS.Vector2 _size;
        private Graphics graph;
        private BitmapData bmpData;
        private List<Xbox360.XBoxButton> buttons;


        public XBoxDrawer()
        {
            buttons = new List<Xbox360.XBoxButton>();
            pad = global::OBSPadSpy2.Properties.Resources.xbox_hg;
            bx = global::OBSPadSpy2.Properties.Resources.b_x;
            by = global::OBSPadSpy2.Properties.Resources.b_y;
            ba = global::OBSPadSpy2.Properties.Resources.b_a;
            bb = global::OBSPadSpy2.Properties.Resources.b_b;
            bl = global::OBSPadSpy2.Properties.Resources.b_l;
            br = global::OBSPadSpy2.Properties.Resources.b_r;
            bse = global::OBSPadSpy2.Properties.Resources.b_sel;
            bst = global::OBSPadSpy2.Properties.Resources.b_sel;
            sl = global::OBSPadSpy2.Properties.Resources.trigger;
            sr = global::OBSPadSpy2.Properties.Resources.trigger;
            dl = global::OBSPadSpy2.Properties.Resources.dp_l;
            dr = global::OBSPadSpy2.Properties.Resources.dp_r;
            du = global::OBSPadSpy2.Properties.Resources.dp_u;
            dd = global::OBSPadSpy2.Properties.Resources.dp_d;
            dru = global::OBSPadSpy2.Properties.Resources.dp_ru;
            drd = global::OBSPadSpy2.Properties.Resources.dp_rd;
            dlu = global::OBSPadSpy2.Properties.Resources.dp_lu;
            dld = global::OBSPadSpy2.Properties.Resources.dp_ld;
            tl0 = global::OBSPadSpy2.Properties.Resources.lt_0;
            tl1 = global::OBSPadSpy2.Properties.Resources.lt_1;
            tl2 = global::OBSPadSpy2.Properties.Resources.lt_2;
            tr0 = global::OBSPadSpy2.Properties.Resources.rt_0;
            tr1 = global::OBSPadSpy2.Properties.Resources.rt_1;
            tr2 = global::OBSPadSpy2.Properties.Resources.rt_2;

            buttons.Add(new Xbox360.XBoxButton(382, 171, ba));
            buttons.Add(new Xbox360.XBoxButton(422, 132, bb));
            buttons.Add(new Xbox360.XBoxButton(341, 132, bx));
            buttons.Add(new Xbox360.XBoxButton(382, 91, by));
            buttons.Add(new Xbox360.XBoxButton(60, 56, bl));
            buttons.Add(new Xbox360.XBoxButton(356, 56, br));
            buttons.Add(new Xbox360.XBoxButton(185, 134, bse));
            buttons.Add(new Xbox360.XBoxButton(288, 134, bst));

            _size = new CLROBS.Vector2(pad.Width, pad.Height);
            imageArray = new byte[(int)_size.X * (int)_size.Y * 4];
            workBmp = new Bitmap(pad);
            trace = new Bitmap(52, 52, PixelFormat.Format32bppArgb);
            traceBrush = Brushes.Red;
        }

        public CLROBS.Vector2 Size
        {
            get { return _size; }
        }

        public byte[] Draw(DI.JoystickState state)
        {
            workBmp = new Bitmap(pad);
            graph = Graphics.FromImage(workBmp);
            graph.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            for (int i = 0; i < 8; i++)
            {
                if (state.Buttons[i])
                {
                    graph.DrawImage(buttons[i].BMP, new Point(buttons[i].X, buttons[i].Y));
                }
            }

            int slx = 65 + ((state.X - 32767)/1310);
            int sly = 121 + ((state.Y - 32767)/1310);

            int srx = 281 + ((state.RotationX - 32767) / 1310);
            int sry = 202 + ((state.RotationY - 32767) / 1310);

            Color pixel;
            for (int i = 0; i < (52); i++)
            {
                for (int j = 0; j < 52; j++)
                {
                    pixel = trace.GetPixel(i, j);
                    trace.SetPixel(i,j,Color.FromArgb((byte)Math.Floor(pixel.A/1.1),pixel));
                }
            }
            traceGraph = Graphics.FromImage(trace);
            traceGraph.DrawEllipse(new Pen(traceBrush), slx - 65, sly - 121, 10, 10);

            graph.DrawImage(sl, new Point(slx, sly));
            graph.DrawImage(trace, new Point(slx, sly));
            graph.DrawImage(sr, new Point(srx, sry));

            if (state.Z <= 32500)
            {
                if (state.Z <= 3000)
                {
                    graph.DrawImage(tl0, new Point(90, 8));
                    graph.DrawImage(tr2, new Point(368, 8));
                }
                else
                {
                    graph.DrawImage(tl0, new Point(90, 8));
                    graph.DrawImage(tr1, new Point(368, 8));
                }
                
            }
            else if (state.Z >= 33000)
            {
                if (state.Z >= 63000)
                {
                    graph.DrawImage(tr0, new Point(368, 8));
                    graph.DrawImage(tl2, new Point(90, 8));
                }
                else
                {
                    graph.DrawImage(tr0, new Point(368, 8));
                    graph.DrawImage(tl1, new Point(90, 8));
                }
            }
            else
            {
                graph.DrawImage(tr0, new Point(368, 8));
                graph.DrawImage(tl0, new Point(90, 8));
            }

            switch (state.PointOfViewControllers[0])
            {
                case 0:
                    graph.DrawImage(du, new Point(136, 178));
                    break;
                case 4500:
                    graph.DrawImage(dru, new Point(136, 178));
                    break;
                case 9000:
                    graph.DrawImage(dr, new Point(136, 178));
                    break;
                case 13500:
                    graph.DrawImage(drd, new Point(136, 178));
                    break;
                case 18000:
                    graph.DrawImage(dd, new Point(136, 178));
                    break;
                case 22500:
                    graph.DrawImage(dld, new Point(136, 178));
                    break;
                case 27000:
                    graph.DrawImage(dl, new Point(136, 178));
                    break;
                case 31500:
                    graph.DrawImage(dlu, new Point(136, 178));
                    break;
                default:
                    break;
            }

            bmpData = workBmp.LockBits(new Rectangle(0, 0, workBmp.Width, workBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(bmpData.Scan0, imageArray, 0, bmpData.Stride * workBmp.Height);
            workBmp.UnlockBits(bmpData);
            return imageArray;
        }
    }
}
