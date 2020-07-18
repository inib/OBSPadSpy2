using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OBSPadSpy2
{
    /// <summary>
    /// Interaction logic for PadConfigWindow.xaml
    /// </summary>
    public partial class PadConfigWindow : Window
    {
        private SharpDX.DirectInput.DirectInput dInput;

        public PadConfigWindow(CLROBS.XElement config)
        {
            InitializeComponent();
            dInput = new SharpDX.DirectInput.DirectInput();
            var list = dInput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, SharpDX.DirectInput.DeviceEnumerationFlags.AttachedOnly);
            foreach (var item in list)
            {
                PadBox.Items.Add(new { item.ProductName });
            }
            DebugText.Text = "not used curently - debug  see OBS logs";
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
