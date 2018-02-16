using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questioner
{
    public class Test
    {
        private DataSaver DataSaverClass { get; set; }
        public Test(DataSaver _dataSaver)
        {
            DataSaverClass = _dataSaver;
        }
        public void StartTest()
        {
            switch(DataSaverClass.TestTypeSelected)
            {
                case 1:
                    Start_ShowTheAnswers();
                    break;
                case 2:
                    Start_TestNotLogged();
                    break;
                case 3:
                    Start_TestLogged();
                    break;
            }
        }
        private void Start_ShowTheAnswers()
        {

        }
        private void Start_TestNotLogged()
        {
            TestWindow _testWindow = new TestWindow(DataSaverClass);
            _testWindow.Show();           
        }
        private void Start_TestLogged()
        {

        }
    }
}
