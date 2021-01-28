using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace InstaBot
{
    public partial class MainWindow : Form
    {
        private string _filePathCSV; // path to where the data are
        private string _filePathTXT; // path to where prepared messages are

        private Instagram _inst;

        private Thread _instThread;    // thread where csv file is processed
        ManualResetEvent _termination; // terminate the thread where csv file is processing


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

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
            if(!IsValid())
            {
                MessageBox.Show("Не все данные были введены","Info" ); 
            }
            else
            {
                //if we press stat button for the first time
                if (_termination == null)
                {
                    _termination = new ManualResetEvent(false);
                    _inst = new Instagram(_filePathCSV, _filePathTXT, _termination);    
                } 
                else
                {
                    _termination.Reset();
                }


                stopButt.Enabled = true; 
                startButt.Enabled = false;

                _instThread = new Thread(_inst.Start);
                _instThread.Start();

                progressBar.Show();
                
            }
        }
        private void stopButt_Click(object sender, EventArgs e)
        {
            stopButt.Enabled = false;
            startButt.Enabled = true;

            _termination.Set(); //stop  sending messages

            progressBar.Hide();
        }

        /// <summary>
        /// Check if the user chose two files
        /// </summary>
        private bool IsValid()
        {
            return _filePathCSV != null && _filePathTXT != null; 
        }
            
        private bool CheckInternetConnection()
        {
            using(WebClient client = new WebClient())
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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _termination.Set(); //stop  sending messages
            _instThread.Join(); //wait until instThread ends 
        }
    }
}
