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
        Test _testClass;
        public MainWindow()
        {
            InitializeComponent();
            _dataSaverClass = new DataSaver(this);
            _dataSaverClass.LoadQuestionFiles();
            _testClass = new Test(_dataSaverClass);
            wpfMain_NumberOfQuestionFiles.Text = _dataSaverClass.QuestionFiles.Count.ToString();
            DataContext = _dataSaverClass;

        }

        private void WpfMain_ListOfQuestionFiles_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _dataSaverClass.LoadQuestionFile(@"Questions\" + (string)WpfMain_ListOfQuestionFiles.SelectedItem + ".txt");
                _testClass.StartTest();
                //start test depending on type selected
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba");
            }
        }

        private void WpfMain_TestType_ShowAnswers_Click(object sender, RoutedEventArgs e)
        {
            _dataSaverClass.TestTypeSelected = 1;
            _dataSaverClass.UpdateColorOfTestTypeSelected();
        }

        private void WpfMain_TestType_NotLoggedTest_Click(object sender, RoutedEventArgs e)
        {
            _dataSaverClass.TestTypeSelected = 2;
            _dataSaverClass.UpdateColorOfTestTypeSelected();
        }

        private void WpfMain_TestType_LoggedTest_Click(object sender, RoutedEventArgs e)
        {
            _dataSaverClass.TestTypeSelected = 3;
            _dataSaverClass.UpdateColorOfTestTypeSelected();
        }
    }
}
