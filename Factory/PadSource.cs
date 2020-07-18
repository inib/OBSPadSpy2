using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLROBS;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace OBSPadSpy2.Factory
{
    class PadSource : AbstractImageSource
    {
        private XElement _config;
        private Texture _texture;
        private Object ImageLock;
        private Pad.Gamepad Pad;
        
        // Graphics

        // Buffers
        private Bitmap preBitmap;

        // Bitmaps
        private Bitmap baseGamepad;

        // Helper
        Rectangle bmpRect;
        private XBoxDrawer XBDrawer;

        public PadSource(XElement config)
        {
            Api.Log("PadSpy2: Initalizing");
            ImageLock = new Object();

            Pad = new OBSPadSpy2.Pad.Gamepad();
            Api.Log("PadSpy2: Pad connected: " + Pad.ToString());
            XBDrawer = new XBoxDrawer();
            Api.Log("PadSpy2: 360 Drawer ready");
            InitBmp();
            Init();


            _config = config;
            UpdateSettings();
        }

        private void InitBmp()
        {
            baseGamepad = global::OBSPadSpy2.Properties.Resources.xbox_hg;
            Size.X = baseGamepad.Width;
            Size.Y = baseGamepad.Height;
            Api.Log("PadSpy2: InitBMP");

            //_config.Parent.SetInt("cx", (int)Size.X);
            //_config.Parent.SetInt("cy", (int)Size.Y);
            //Api.Log("PadSpy2: SetSizeInt");
        }

        private void Init()
        {
            preBitmap = new Bitmap(baseGamepad);
            bmpRect = new Rectangle(0, 0, baseGamepad.Width, baseGamepad.Height);
            Api.Log("PadSpy2: InitBase");
        }
        
        public override void Render(float x, float y, float width, float height)
        {
            if (_texture == null)
            {
                InitBmp();
                Init();
                _texture = GS.CreateTexture((UInt32)Size.X, (UInt32)Size.Y, GSColorFormat.GS_BGRA, null, false, false);
            }
            else 
            {
                lock (ImageLock)
                {
                    Pad.RefreshState();
                    _texture.SetImage(XBDrawer.Draw(Pad.GetJoyState()), GSImageFormat.GS_IMAGEFORMAT_BGRA, (UInt32)(Size.X * 4));
                    GS.DrawSprite(_texture, 0xFFFFFFFF, x, y, x + width, y + height);
                }
            }
        }

        private void Draw()
        {
            //if (padState.Any(p => p))
            //{
            //    graph = Graphics.FromImage(preBitmap);
            //    graph.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            //    if (padState[0])
            //    {
            //        graph.DrawImage(buttonA, new Point(207, 109));
            //    }
            //    if (padState[1])
            //    {
            //        graph.DrawImage(buttonB, new Point(243, 73));
            //    }
            //    if (padState[2])
            //    {
            //        graph.DrawImage(buttonX, new Point(171, 73));
            //    }
            //    if (padState[3])
            //    {
            //        graph.DrawImage(buttonY, new Point(207, 37));
            //    }
            //    if (padState[4])
            //    {
            //        graph.DrawImage(digR, new Point(89, 77));
            //    }
            //    if (padState[5])
            //    {
            //        graph.DrawImage(digD, new Point(64, 102));
            //    }
            //    if (padState[6])
            //    {
            //        graph.DrawImage(digL, new Point(25, 77));
            //    }
            //    if (padState[7])
            //    {
            //        graph.DrawImage(digU, new Point(64, 38));
            //    }
            //}
        }
        
        public override void Preprocess()
        {
            //preBitmap = new Bitmap(baseGamepad);
        }

        public override void BeginScene()
        {
            Pad.Start();
            Api.Log("PadSpy2: Start aquire");
        }

        public override void EndScene()
        {
            Pad.Stop();
            Api.Log("PadSpy2: Stop aquire");
        }

        public override void UpdateSettings()
        {
           // XElement dataElement = _config.GetElement("data");
        }
    }
}
