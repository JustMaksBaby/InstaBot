using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading; 
using System.Net;
using System.Net.Http;
using System.Windows.Forms; 
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace InstaBot
{
    internal class MouseAction
    {
        /// <summary>
        /// Immitates mouse`s buttons pressing
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo); 
   
        [DllImport("User32.Dll")]
        private static extern long SetCursorPos(uint x, uint y);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        public static  void MousePressLeft(uint x, uint y, Random random)
        {
            SetCursorPos(x, y);

            Thread.Sleep(random.Next(2000, 2500));  // immitates pause like a human

            mouse_event(MOUSEEVENTF_LEFTDOWN|MOUSEEVENTF_LEFTUP, x, y, 0,0); 
        }
    } 
    internal class Instagram
    {
        private string _filePathCSV; // path to where the users nick names  are 
        private string _filePathTXT; // path to where prepared messages are
        private string _infoFile;    // name for the file where number of read bytes is stored

        private List<string>  _messagesToUser = new List<string>();  // stores messages that will be sent to users

        private Random _random; 

        /// <summary>
        /// Checks the internet connection
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

            _LoadTXTFile(); 

            //create name for info file 
            string csvFileName = _filePathCSV.Split('\\').Last().Split('.')[0];
            _infoFile = $"{csvFileName}_info.txt"; 
        }

        // MAIN WORK
        /// <summary>
        /// Entry point to start the bot
        /// </summary>
        /// <param name="worker">Backgroundworker that started current process</param>
        /// <param name="events"></param>
        public  void Start(BackgroundWorker worker, DoWorkEventArgs events) 
        {
            _random = new Random();

            // to prevent blocking the ability to send messages 
            // set the maximum number of users before pause  to 15
            int usersAmount = _random.Next(10,15); 

            _LoadChrome();

            StreamReader openedCSV; //used to read nicknames from csv file line by line
            using (FileStream csvStream = new FileStream(_filePathCSV, FileMode.Open))
            {
                int readBytes = _LoadInfoFile(); 
                csvStream.Position = readBytes;

                openedCSV = new StreamReader(csvStream);
                string line; // unprocessed line from csv file
                string trimedLine; // contains string without spaces
                while (--usersAmount != 0)
                {
                    if ((line = openedCSV.ReadLine()) == null)
                    {
                        events.Result = ResultsEnum.COMPLETED;
                        return;
                    }

                    if (!_CheckInternetConnection())
                    { 
                        events.Result = ResultsEnum.NO_INTERNET;
                        _CloseChromeTab(); 
                        return;
                    }

                    trimedLine = line.Trim();  
                    readBytes += line.Length + 2; // add original size of the line. +2 for '/n' and '/r' 

                    //not instagram link was passed
                    if (!_IsInstagramLink(trimedLine))
                    {
                        _SaveInfoFile(readBytes);
                        continue; 
                    }

                    string userName = _GetNickname(trimedLine); // get only user name from line
                    if (_UserExists(userName)) // check if the user with current name exists in Instragram
                    {
                        string message = _messagesToUser[_random.Next(0, _messagesToUser.Count)]; //get random message for each user
                        _SendMessageAction(userName, message);
                    }
                   
                    _SaveInfoFile(readBytes); 

                    //catch  the case when user whants to interrup the process
                    if (worker.CancellationPending)
                    {
                        events.Cancel = true;
                        return;
                    }
                }

                _CloseChromeTab();

                events.Result = ResultsEnum.PAUSE_STARTED;
            }
        }

        //WORK WITH CHROME
        /// <summary>
        /// Sets Chrome browser an active window.
        /// It is important that Chrome window remains maximized
        /// Instagram account must be logged in before program starts
        /// During the program working you shound`t change tab in Chrome
        /// </summary>
        private void _LoadChrome()
        {
            Process.Start("chrome", "https://www.instagram.com/"); // if chrome wasn`t loaded it will create a new process
            Thread.Sleep(5000); // wait Instagram to load  
        }
        /// <summary>
        /// opens a CSV file with users nicknames
        /// </summary>
        private void _CloseChromeTab()
        {   
             SendKeys.SendWait("^{w}");                 
        }

        //FILE PROCESSING
        /// <summary>
        /// Load information about how many bytes were read  from csv file
        /// This information is used if  we stopped processing file and need to resume later
        /// If there is no such file or file contains incorrect value the returning value is 0
        /// </summary>
        /// <returns></returns>
        private int _LoadInfoFile()
        {
            // if csv file  has been processed before 
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
                        _messagesToUser.Add(message);                                       
                    }
                }
            }
        }

        //
        /// <summary>
        /// Cleans data and returns pure  user name
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string _GetNickname(string line)
        {
            string[] sepStr = line.Split('/');
            //case '\USER_NAME'
            if (sepStr.Length == 2)
            {
                return sepStr[1]; 
            }
            //case 'USER_NAME'
            else if(sepStr.Length == 1)
            {
                return sepStr[0]; 
            }
            //case 'https://www.instagram.com/USER_NAME/?hl=ru'|'https://www.instagram.com/USER_NAME'|https://www.instagram.com/USER_NAME?123123'
            else
            {
                //'https://www.instagram.com/USER_NAME/?hl=ru'
                if (sepStr.Length == 5)
                {
                    return sepStr[3]; 
                }
                //'https://www.instagram.com/USER_NAME' + https://www.instagram.com/USER_NAME?123123'
                else
                {
                    return sepStr[3].Split('?')[0]; 
                }

            }

        }  
        
        /// <summary>
        /// If from the csv file was read link  'https://...'
        /// This function checks if the link is from instagram
        /// If just username was passed to link the return values is true
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private bool _IsInstagramLink(string link)
        {
            // if from file was read '\n' or line contained only spaces
            if(link.Length == 0)
            {
                return false;
            }
            else if(link.Length >4)
            {
               // instagram link will be splited to [https://wwww, instagram, com/....]
               if(link.Substring(0,4) == "http" && link.Split('.')[1] != "instagram")
                {
                    return false; 
                }
                return true; 
            }
            return true;
        }

        /// <summary>
        /// Checks is the user with a certain nick name  exists in Instagram
        /// </summary>
        private  bool _UserExists(string user)
        {
            using (HttpClient client = new HttpClient())
            { 
                Task<HttpResponseMessage> response =  client.GetAsync($"https://www.instagram.com/{user}/");

                Task.WaitAll(response);
                if (response.Result.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }
            }  
                return true;
        }

        /// <summary>
        /// Send a message to a user
        /// </summary>
        private void _SendMessageAction(string userName, string message)
        {
            
            //press button to go on messages page
            MouseAction.MousePressLeft(1086,134, _random); 
            Thread.Sleep(_random.Next(1000, 2000));

            //press button to seach users
            MouseAction.MousePressLeft(615, 202, _random);
            Thread.Sleep(_random.Next(1000, 3000));

            //copy and paste user name in the opened field
            Thread thread = new Thread(() => Clipboard.SetText(userName));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
            SendKeys.SendWait("^{v}");
            Thread.Sleep(_random.Next(2000, 3000));  

            //press on the the first user
            MouseAction.MousePressLeft(752,443, _random);
            Thread.Sleep(_random.Next(2000, 3000));

            // press on the button "Next"
            MouseAction.MousePressLeft(923, 314, _random);
            Thread.Sleep(_random.Next(2000, 3000));

            //copy and paste message for the user
            thread = new Thread(() => Clipboard.SetText(message));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();
            Thread.Sleep(_random.Next(5000, 6000));
            SendKeys.SendWait("^{v}");

            //press Enter to send the message
            Thread.Sleep(_random.Next(2000, 3000)); 
            SendKeys.SendWait("{ENTER}");


            Thread.Sleep(_random.Next(2000, 3000)); 
        }

    }
}
