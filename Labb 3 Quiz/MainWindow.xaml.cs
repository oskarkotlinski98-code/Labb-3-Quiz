using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb_3_Quiz
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
            ShowView(new MainMenuView(this));
        }

        public void ShowView(UserControl view)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(view);
        }
    }
}