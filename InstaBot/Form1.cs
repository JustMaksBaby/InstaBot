using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstaBot
{
    public partial class InstaBot : Form
    {
        private string filePathCSV;
        private string filePathTXT;  

        public InstaBot()
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
                filePathCSV = fileDialog.FileName; 

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
                filePathTXT = fileDialog.FileName; 

                //get only file name not the whole path
                txtTextBox.Text = fileDialog.FileName.Split('\\').Last();
            }
            else
            {
                return;
            }
        }
    }
}
