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
using System.IO;

namespace InstaBot
{
 internal class Instagram
    {
        private string _filePathCSV; // path to where the data are 
        private string _filePathTXT; // path to where prepared messages are
        private string _infoFile;    //name for the file where number of read bytes is stored
        private StreamReader _openedCSV; //used to read nicknames from csv file line by line
        private List<string>  messagesToUser = new List<string>();  //stores messages that will be sent to users

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

        public Instagram(string filePathCSV, string filePathTXT)
        {
            _filePathCSV = filePathCSV;
            _filePathTXT = filePathTXT;

            //create name for info file 
            string csvFileName = _filePathCSV.Split('\\').Last().Split('.')[0];
            _infoFile = $"{csvFileName}_info.txt"; 

            _OpenCSVFile();
            _LoadTXTFile();
        }

        /// <summary>
        /// Entry point to start the bot
        /// </summary>
        /// <param name="worker">Backgroundworker that started current process</param>
        /// <param name="events"></param>
        public void Start(BackgroundWorker worker, DoWorkEventArgs events) 
        {
            if(!_CheckInternetConnection())
            {
                events.Result = ResultsEnum.NO_INTERNET;
                return;
            }

            // set Chrome browser an active window
            LoadChrome(); 
            

            while(true)
            {
                if(worker.CancellationPending)
                {
                    events.Cancel = true;
                    break; 
                }
            }
            
        }

        /// <summary>
        /// Sets Chrome browset an active window.
        /// It is important that Chrome window remains maximized
        /// Instagram account must be logged in before program starts
        /// During the program working you shound`t change tab in Chrome
        /// </summary>
        private void LoadChrome()
        {
            Process.Start("chrome", "https://www.instagram.com/"); // if chrome wasn`t loaded it will create a new process
            Thread.Sleep(5000); // wait Instagram to load  

            //SendKeys.SendWait("^{w}");                 
        }
        /// <summary>
        /// opens a CSV file with users nicknames
        /// </summary>
        private void _OpenCSVFile()
        {
            //open csv file. This file stream is open as long as the app is running
            FileStream csvStream = new FileStream(_filePathCSV,FileMode.Open);
            csvStream.Position = _LoadInfoFile(); // if we had this file processed before load info from where to start reading file

            _openedCSV = new StreamReader(csvStream);
            
        }

        /// <summary>
        /// Load information about how many bytes were read  from csv file
        /// This information is used if  we stopped processing file and need to resume later
        /// If there is no such file or file contains incorrect value the returning value is 0
        /// </summary>
        /// <returns></returns>
        private int _LoadInfoFile()
        {
            if (File.Exists(_infoFile))
            {
                using (StreamReader file = new StreamReader(_infoFile))
                {
                    int readBytes;
                    int.TryParse(file.ReadLine(), out readBytes);
                   
                    return readBytes;
                }
            }
            else
            {
                return 0;
            } 
        }

        /// <summary>
        /// Save information about how many bytes were read  from csv file
        /// This information is used if  we stopped processing file and need to resume later
        /// </summary>
        /// <param name="readBytes"></param>
        private void _SaveInfoFile(int readBytes)
        {
            using (StreamWriter file = new StreamWriter(_infoFile))
            {
                file.Write(readBytes);
            }
        }

        /// <summary>
        /// loads messages for users  from txt file
        /// file shoud be in UTF8 format
        /// </summary>
        /// 
        private void _LoadTXTFile()
        {             
            using(StreamReader file = new StreamReader(_filePathTXT))
            {
                string message; 
                while ((message = file.ReadLine()) != null )
                {
                    //to avoid lines that consist only of /n and spaces 
                    if (message.Length > 1 && message[0] !=' ')
                    {
                        messagesToUser.Add(message);                                       
                    }
                }
            }
        }

    }
}
