using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.UtilityResource;
using Xbim.Presentation;
using Xbim.Presentation.Extensions;
using XbimXplorer;


namespace XbimXplorer.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class TestWindow
    {

        public TestWindow()
        {
            InitializeComponent();
            DataContext = this;
            
        }

        internal XplorerMainWindow MainWindow { get; set; }

        public IfcStore Model { get; set; }
        public List<Assembly> Assemblies { get; set; }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            DragMove();
        }

		private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{

        }

        private Dictionary<string, string> getSelectedEntityString()
        {

            IIfcObject myObject = MainWindow.DrawingControl.SelectedEntity as IIfcObject;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (MainWindow.DrawingControl.SelectedEntity != null && myObject != null)
            {
                    dict.Add("GlobalID", myObject.GlobalId);
                    dict.Add("Type", myObject.ObjectType);
                    dict.Add("Label", myObject.EntityLabel.ToString());
                    dict.Add("Material", myObject.Material.ToString());
                    dict.Add("Name", myObject.Name.ToString().Substring(0, myObject.Name.ToString().IndexOf(":")));
            }
            return dict;
        }

        public BitmapImage TakeDrawingControlScreenshot()
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)MainWindow.DrawingControl.ActualWidth, (int)MainWindow.DrawingControl.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(MainWindow.DrawingControl);

            var bitmapImage = new BitmapImage();
            var bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var stream = new MemoryStream())
            {
                bitmapEncoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }
        
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
		{
            Dictionary<string, string> text = getSelectedEntityString();
            if(text.Count != 0 )
            {
                textbox1.Text = text["Name"];

                IFCImage1.Source = TakeDrawingControlScreenshot();
            }
            else
            {
                textbox1.Text = "No Entity Selected";
            }
            


        }

    }
}
