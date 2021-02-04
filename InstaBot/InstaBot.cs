using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO; 



namespace InstaBot
{
    public partial class MainWindow : Form
    {
        private string _filePathCSV; // path to where the data are
        private string _filePathTXT; // path to where prepared messages are

        private int _pauseFor = 0; // sets in seconds how long should the program wait before sending messages
        private const int _TIMER_INTERVAL = 1000;  //sets the interval for timer 

        private Random _random = new Random();

        private SendMessageClass _sendMessageCoor;    /* class with coordinates for buttons 
                                                       that are necessary for sending a message  in Instagram */

        //
        public MainWindow()
        {
            InitializeComponent();
            _LoadSendMessageCoor(); 
        }

        //
        /// <summary>
        /// Loads json file that contains information about button coordinates
        /// </summary>
        private async void _LoadSendMessageCoor()
        {
            if (File.Exists("insta_send_message.json"))
            {
                using (System.IO.FileStream file = new System.IO.FileStream("insta_send_message.json", FileMode.Open))
                {
                    _sendMessageCoor = await JsonSerializer.DeserializeAsync<SendMessageClass>(file);
                }
            }
            else
            {
                MessageBox.Show("Нет файла insta_send_message.json", "Info"); 
            }
        }
        private void loadCsvButt_Click(object sender, EventArgs e)
        {
            fileDialog.Filter = "csv files (*.csv)|*.csv"; 

            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                _filePathCSV = fileDialog.FileName; 

                //get only file name not the whole path
                csvTextBox.Text = fileDialog.FileName.Split('\\').Last(); 
            }
            else
            {
                return;
            }
        }
        private void loadTxtButt_Click(object sender, EventArgs e)
        {
            fileDialog.Filter = "txt files(*.txt) | *.txt";

            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                _filePathTXT = fileDialog.FileName; 

                //get only file name not the whole path
                txtTextBox.Text = fileDialog.FileName.Split('\\').Last();
            }
            else
            {
                return;
            }
        }
        private void startButt_Click(object sender, EventArgs e)
        {
            //if the user didn`t choose csv or txt file
            //or json file with coordinates is absent
            if(!_IsValid() ||  _sendMessageCoor == null)
            {
                MessageBox.Show("Не все данные были введены","Info" ); 
            }
            else
            {
                stopButt.Enabled = true;
                startButt.Enabled = false;

                progressBar.Show();
                    
                backgroundWorker.RunWorkerAsync(); 
            }
        }
        private void stopButt_Click(object sender, EventArgs e)
        {
            //check if backgrounder currently is working
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            } 
            // Otherwise it means that program in pause or in 'no Internet state'
            else
            {
                noInternetLabel.Visible = false; 

                timeInfoLabel.Visible = false; 

                timer.Enabled = false ; 

                stopButt.Enabled = false;
                startButt.Enabled = true;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            /*UPDATES EVERY _TIME_INTERVAL miliseconds*/

            if(_pauseFor <= 0) // create a new process if it is appropriate time
            {
                timeInfoLabel.Visible = false;  
                timer.Enabled = false;

                noInternetLabel.Visible = false;

                progressBar.Show();

                backgroundWorker.RunWorkerAsync(); 
            }
            else
            {                 
                _pauseFor -= 1; // count down remaining time
                TimeSpan time = TimeSpan.FromSeconds(_pauseFor);

                timeInfoLabel.Text = $"{time.Minutes}:{time.Seconds}"; 
            }
        }
       
        //
        /// <summary>
        /// Check if the user chose two files
        /// </summary>
        private bool _IsValid()
        {
            return _filePathCSV != null && _filePathTXT != null; 
        }
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        { 
            BackgroundWorker worker = sender as BackgroundWorker;

            Instagram inst = new Instagram(_sendMessageCoor, _filePathCSV, _filePathTXT);
            inst.Start(worker, e);
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (e.Result is ResultsEnum)
                {
                    ResultsEnum result = (ResultsEnum)e.Result;
                    switch (result)
                    {
                        case ResultsEnum.NO_INTERNET:
                            {
                                /* In case if the internet connection was lost the program will  
                                  be trying each 2 minutes to start new thread */ 

                                progressBar.Hide();

                                _pauseFor = 2 * 60; //in seconds 

                                timeInfoLabel.Visible = true;

                                timer.Interval = _TIMER_INTERVAL;
                                timer.Enabled = true;

                                noInternetLabel.Visible = true; 
                            }
                            break;
                        case ResultsEnum.COMPLETED: //if there is no more uprocessed data in csv file
                            {
                                progressBar.Hide();

                                stopButt.Enabled = false;
                                startButt.Enabled = true;

                                MessageBox.Show("Вся работа сделана", "Info");
                            }
                            break;
                        case ResultsEnum.PAUSE_STARTED: //this case means that background worker stoped processing a csv file 
                            {
                                progressBar.Hide();

                                _pauseFor = _random.Next(15, 19) * 60; //in seconds 

                                timeInfoLabel.Visible = true;

                                timer.Interval = _TIMER_INTERVAL;
                                timer.Enabled = true;
                            }
                            break;
                    }
                }
            }
            else // if the processed was interrupted by a  user
            {
                stopButt.Enabled = false;
                startButt.Enabled = true;

                progressBar.Hide();
            } 
        }

    }
}
