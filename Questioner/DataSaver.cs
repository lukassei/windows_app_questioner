using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;

namespace Questioner
{
    public class DataSaver
    {
        //this class is supposed to be databinded from mainwindow
        //here will be stored all the question file names (in  "QuestionFiles")
        //here will be loaded all the questions from the apropriet question file after user have selected it.
        public ObservableCollection<string> QuestionFiles { get; set; }
        public List<Question> QuestionsInSet { get; set; }
        public int TestTypeSelected { get; set; }
        private MainWindow WpfMain { get; set; }
        public int NumberOfAnsweredQuestions { get; set; }
        private List<Question> QuestionsRandomlyChoosenForTest { get; set; }
        private int NumberOfQuestionsForThisTest { get; set; }
        public DataSaver(MainWindow _wpfMain)
        {
            QuestionFiles = new ObservableCollection<string>();
            QuestionsInSet = new List<Question>();
            QuestionsRandomlyChoosenForTest = new List<Question>();
            TestTypeSelected = 1;
            WpfMain = _wpfMain;
            UpdateColorOfTestTypeSelected();
            NumberOfAnsweredQuestions = 0;
            NumberOfQuestionsForThisTest = 0;
        }

        public void LoadQuestionFiles()
        {
            //this method will look inside of folder "/Questions" and save all text file names inside QuestionFiles
            DirectoryInfo _questionFiles__directory = new DirectoryInfo(@"Questions");
            FileInfo[] _questionFiles = _questionFiles__directory.GetFiles("*.txt");
            foreach (FileInfo _questionFile in _questionFiles)
            {
                string fileName = _questionFile.Name;
                int fileExtPos = fileName.LastIndexOf(".");
                if (fileExtPos >= 0)
                    fileName = fileName.Substring(0, fileExtPos);

                QuestionFiles.Add(fileName);
            }
        }
        public void LoadQuestionFile(string _filePath)
        {
            QuestionsInSet.Clear();
            NumberOfAnsweredQuestions = 0;
            QuestionsRandomlyChoosenForTest.Clear();
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string _questionLine;
                while ((_questionLine = sr.ReadLine()) != null)
                {
                    if (!_questionLine.Contains(';'))
                        NumberOfQuestionsForThisTest = int.Parse(_questionLine);
                    else
                    {
                        string[] _questionElements = _questionLine.Split(';');
                        Question _question = new Question(_questionElements[0], _questionElements[1], _questionElements[2], _questionElements[3], int.Parse(_questionElements[4]));
                        QuestionsInSet.Add(_question);
                    }
                }
            }
            if (NumberOfQuestionsForThisTest == 0)
                throw new Exception("Soubor otázek neobsahuje počet otázek vygenerovaných pro každý test. Nemůžu proto pokračovat.");
            ChooseQuestionsForTest();
        }
        public void ChooseQuestionsForTest()
        {
            while(QuestionsRandomlyChoosenForTest.Count < NumberOfQuestionsForThisTest)
            {
                Random r = new Random();
                int _question = r.Next(QuestionsInSet.Count);
                if (!QuestionsRandomlyChoosenForTest.Contains(QuestionsInSet[_question]))
                    QuestionsRandomlyChoosenForTest.Add(QuestionsInSet[_question]);
            }
        }
        public void UpdateColorOfTestTypeSelected()
        {
            switch(TestTypeSelected)
            {
                case 1:
                    WpfMain.WpfMain_TestType_ShowAnswers.Background = Brushes.GreenYellow;
                    WpfMain.WpfMain_TestType_LoggedTest.Background = Brushes.Gray;
                    WpfMain.WpfMain_TestType_NotLoggedTest.Background = Brushes.Gray;
                    break;
                case 2:
                    WpfMain.WpfMain_TestType_ShowAnswers.Background = Brushes.Gray;
                    WpfMain.WpfMain_TestType_LoggedTest.Background = Brushes.Gray;
                    WpfMain.WpfMain_TestType_NotLoggedTest.Background = Brushes.GreenYellow;
                    break;
                case 3:
                    WpfMain.WpfMain_TestType_ShowAnswers.Background = Brushes.Gray;
                    WpfMain.WpfMain_TestType_LoggedTest.Background = Brushes.GreenYellow;
                    WpfMain.WpfMain_TestType_NotLoggedTest.Background = Brushes.Gray;
                    break;
            }
        }
        public void WpfTest_setNextQuestion(TestWindow _testWindow)
        {
            if(NumberOfAnsweredQuestions < QuestionsRandomlyChoosenForTest.Count)
                _testWindow.QuestionNext = QuestionsRandomlyChoosenForTest[NumberOfAnsweredQuestions];
            else
            {
                _testWindow.Close();
                //will start to evaluate the answers
            }
        }
    }
}
