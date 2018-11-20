using FileExplorer.ViewModels;
using System.Windows;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DirectoryStructureViewModel();
        }
    }
}
