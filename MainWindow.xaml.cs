using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                tbxFolder.Text = folderBrowser.SelectedPath;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] FFiles = Directory.GetFiles(tbxFolder.Text, "*.dll");
            foreach (string FFileName in FFiles)
            {
                try
                {
                    Assembly FAssembly = Assembly.LoadFile(FFileName);
                    foreach (Type FType in FAssembly.GetTypes())
                    {
                        TreeViewItem FTreeViewItem = new TreeViewItem { Header = FType.FullName };
                        foreach (MethodInfo FMethodInfo in FType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic))
                        {
                            FTreeViewItem.Items.Add(FMethodInfo.Name);
                        }
                        tvwMethodList.Items.Add(FTreeViewItem);
                    }
                }
                catch (Exception fe)
                {
                }
            }
        }
    }
}
