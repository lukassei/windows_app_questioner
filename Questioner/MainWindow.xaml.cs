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

namespace Questioner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSaver _dataSaverClass;
        public MainWindow()
        {
            InitializeComponent();
            _dataSaverClass = new DataSaver();
            _dataSaverClass.LoadQuestionFiles();
            wpfMain_NumberOfQuestionFiles.Text = _dataSaverClass.QuestionFiles.Count.ToString();
            DataContext = _dataSaverClass;

        }

        private void WpfMain_ListOfQuestionFiles_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _dataSaverClass.LoadQuestionFile(@"Questions\" + (string)WpfMain_ListOfQuestionFiles.SelectedItem + ".txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba");
            }
        }
    }
}
