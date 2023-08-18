using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.UtilityResource;
using Xbim.Presentation.Extensions;


namespace XbimXplorer.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class TestWindow
    {
        private readonly Assembly _assembly;

        public TestWindow()
        {
            InitializeComponent();
            DataContext = this;
            // Logo.Source = new BitmapImage(new Uri(@"pack://application:,,/xBIM.ico", UriKind.RelativeOrAbsolute));
            _assembly = Assembly.GetEntryAssembly();
            
            // Logo.Source = new BitmapImage(new Uri(@"pack://application:,,/xBIM.ico", UriKind.RelativeOrAbsolute));
            
        }

        internal XplorerMainWindow MainWindow { get; set; }

        private void DocumentAssembly(Assembly assembly, StringBuilder sb)
        {
            var refs = assembly.MyGetReferencedAssembliesRecursive();
            foreach (var key in refs.Keys.Where(x => x.ToLowerInvariant().Contains("xbim")).OrderBy(x => x))
            {
                var a = refs[key];
                DocumentSingleAssembly(a, sb);
            }
        }

        private void DocumentSingleAssembly(Assembly a, StringBuilder sb)
        {
            if (!a.GetName().Name.ToLowerInvariant().Contains(@"xbim"))
                return;
            var xa = new XbimAssemblyInfo(a);
            if (string.IsNullOrEmpty(a.Location))
                xa.OverrideLocation = MainWindow.GetAssemblyLocation(a);
            var assemblyDescription = $"{a.GetName().Name}\t{xa.AssemblyVersion}\t{xa.FileVersion}\r\n";
            sb.Append(assemblyDescription);
        }

        public IfcStore Model { get; set; }
        public List<Assembly> Assemblies { get; set; }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            DragMove();
        }

        
    }
}
