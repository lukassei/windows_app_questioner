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
using System.Windows.Shapes;

namespace Questioner
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public Question QuestionNext { get; set; }
        private DataSaver DataSaverClass { get; set; }
        public TestWindow(DataSaver _dataSaver)
        {
            InitializeComponent();
            DataSaverClass = _dataSaver;
            PrepareWindowForNewQuestion();
        }
        private void PrepareWindowForNewQuestion()
        {
            
            DataSaverClass.WpfTest_setNextQuestion(this);
            WpfTest_questionTask.Text = QuestionNext.QuestionTask;
            WpfTest_answer1.Content = QuestionNext.Answer1;
            WpfTest_answer2.Content = QuestionNext.Answer2;
            WpfTest_answer3.Content = QuestionNext.Answer3;
        }
        private void WpfTest_answer1_Click(object sender, RoutedEventArgs e)
        {
            QuestionNext.ChoosenAnswer = 1;
            DataSaverClass.NumberOfAnsweredQuestions++;
            PrepareWindowForNewQuestion();

        }

        private void WpfTest_answer2_Click(object sender, RoutedEventArgs e)
        {
            QuestionNext.ChoosenAnswer = 2;
            DataSaverClass.NumberOfAnsweredQuestions++;
            PrepareWindowForNewQuestion();
        }

        private void WpfTest_answer3_Click(object sender, RoutedEventArgs e)
        {
            QuestionNext.ChoosenAnswer = 3;
            DataSaverClass.NumberOfAnsweredQuestions++;
            PrepareWindowForNewQuestion();
        }
    }
}
