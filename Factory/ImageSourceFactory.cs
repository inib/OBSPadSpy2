using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLROBS;
using System.Windows.Media.Imaging;

namespace OBSPadSpy2.Factory
{
    class ImageSourceFactory : AbstractImageSourceFactory
    {
        //private XElement source;

        public ImageSourceFactory()
        {
            ClassName = "ImageSourcePadspy";
            DisplayName = "PadSpy2";
        }

        public override ImageSource Create(XElement data)
        {
            //LoadImage(data);
            return new PadSource(data);
        }

        private void LoadImage(XElement data)
        {
            data.AddString("image", "test\\1000_2.jpg");
        }

        public override bool ShowConfiguration(XElement data)
        {
            PadConfigWindow padWin = new PadConfigWindow(data);
            return padWin.ShowDialog().GetValueOrDefault(false);
        }
    }
}
