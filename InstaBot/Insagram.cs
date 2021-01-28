using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading; 

namespace InstaBot
{
 internal class Instagram
    {
        private string _filePathCSV; // path to where the data are 
        private string _filePathTXT; // path to where prepared messages are
        private  ManualResetEvent _termination; // track if the thread has to stop

        /// <summary>
        /// 
        /// </summary> 
        /// <param name="terminationEvent">event used to stop current thread if need </param>
        public Instagram(string filePathCSV, string filePathTXT, ManualResetEvent terminationEvent)
        {
            _filePathCSV = filePathCSV;
            _filePathTXT = filePathTXT;
            _termination = terminationEvent;
        }

        public void Start() 
        {
            int a = 2; 

            while(!_termination.WaitOne(0))
            {
                int b = a / 1;
            }
        }
    }
}
