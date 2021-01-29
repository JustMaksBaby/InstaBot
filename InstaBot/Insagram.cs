using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading; 
using System.Net;
using System.Windows.Forms; 
using System.Diagnostics;

namespace InstaBot
{
 internal class Instagram
    {
        private string _filePathCSV; // path to where the data are 
        private string _filePathTXT; // path to where prepared messages are
        private  ManualResetEvent _termination; // track if the thread has to stop

        /// <summary>
        /// Check the internet connection
        /// </summary>
        /// <returns></returns>
        private static bool _CheckInternetConnection()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    using (client.OpenRead("http://google.com/generate_204"))
                    {
                        return true;
                    }
                }
                catch (WebException)
                {
                    return false;
                }
            }
        }

        /// <summary>
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
            LoadChrome(); 
        }
        private void LoadChrome()
        {
            Process.Start("chrome", "https://www.instagram.com/"); // if chrome wasn`t loaded it will create a new process
            Thread.Sleep(5000); // wait Instagram to load  

            //SendKeys.SendWait("^{w}");                 
        }
    }
}
