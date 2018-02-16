using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Questioner
{
    public class DataSaver
    {
        //this class is supposed to be databinded from mainwindow
        //here will be stored all the question file names (in  "QuestionFiles")
        //here will be loaded all the questions from the apropriet question file after user have selected it.
        public ObservableCollection<string> QuestionFiles { get; set; }
        public List<Question> QuestionsInSet { get; set; }
        public DataSaver()
        {
            QuestionFiles = new ObservableCollection<string>();
            QuestionsInSet = new List<Question>();
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
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string _questionLine;
                while ((_questionLine = sr.ReadLine()) != null)
                {
                    string[] _questionElements = _questionLine.Split(';');
                    Question _question = new Question(_questionElements[0], _questionElements[1], _questionElements[2], _questionElements[3], int.Parse(_questionElements[4]));
                    QuestionsInSet.Add(_question);
                }
            }
        }
    }
}
