using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics; 



namespace InstaBot
{
    public partial class MainWindow : Form
    {
        private string _filePathCSV; // path to where the data are
        private string _filePathTXT; // path to where prepared messages are

        private Instagram _inst;



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
            //if the user didn`t choose csv or txt file
            if(!IsValid())
            {
                MessageBox.Show("Не все данные были введены","Info" ); 
            }
            else
            {
                _inst = new Instagram(_filePathCSV, _filePathTXT); 
               
                stopButt.Enabled = true;
                startButt.Enabled = false;

                progressBar.Show();
                    
                backgroundWorker.RunWorkerAsync(); 
            }
        }
        private void stopButt_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();     
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker.CancelAsync();              
        }

        /// <summary>
        /// Check if the user chose two files
        /// </summary>
        private bool IsValid()
        {
            return _filePathCSV != null && _filePathTXT != null; 
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        { 
            BackgroundWorker worker = sender as BackgroundWorker;

            _inst.Start(worker, e); 
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
       
          
            ResultsEnum result = (ResultsEnum)e.Result; 
            switch (result)
            {
                case ResultsEnum.NO_INTERNET:
                    {
                        progressBar.Hide(); 
                        MessageBox.Show("Нет интернета", "Info"); 
                    }break;
            }
         

            stopButt.Enabled = false;
            startButt.Enabled = true;

            progressBar.Hide();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //TODO get the  time for how long is the pause
            //TODO get the  notification that proccess started;
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //TODO implement counting the time to start 
        }
    }
}
