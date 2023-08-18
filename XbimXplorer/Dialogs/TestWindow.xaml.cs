using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.UtilityResource;
using Xbim.Presentation;
using Xbim.Presentation.Extensions;


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
        public IPersistEntity Selection { get; set; }
        public List<Assembly> Assemblies { get; set; }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            DragMove();
        }

		private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{

        }

		private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
		{
            textbox1.Text = Selection.ToString();
            System.Diagnostics.Debug.WriteLine(Selection.ToString());
            System.Diagnostics.Debug.WriteLine("Test");
            
        }
    }
}
