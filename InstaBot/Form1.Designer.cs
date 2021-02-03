namespace InstaBot
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.loadCsvButt = new System.Windows.Forms.Button();
            this.csvTextBox = new System.Windows.Forms.TextBox();
            this.txtTextBox = new System.Windows.Forms.TextBox();
            this.loadTxtButt = new System.Windows.Forms.Button();
            this.startButt = new System.Windows.Forms.Button();
            this.stopButt = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timeInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileDialog
            // 
            this.fileDialog.DefaultExt = "csv";
            this.fileDialog.Filter = "csv files (*.csv)|*.csv| txt files (*.txt)|*.txt";
            this.fileDialog.InitialDirectory = "C:\\\\";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadFileButton.Location = new System.Drawing.Point(361, 22);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(182, 34);
            this.loadFileButton.TabIndex = 1;
            this.loadFileButton.Text = "Найти файл";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadTxtButt_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fileNameTextBox.Location = new System.Drawing.Point(14, 22);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(334, 30);
            this.fileNameTextBox.TabIndex = 2;
            this.fileNameTextBox.Text = "Не выбрано";
            // 
            // loadCsvButt
            // 
            this.loadCsvButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadCsvButt.Location = new System.Drawing.Point(361, 22);
            this.loadCsvButt.Name = "loadCsvButt";
            this.loadCsvButt.Size = new System.Drawing.Size(182, 34);
            this.loadCsvButt.TabIndex = 1;
            this.loadCsvButt.Text = "Загрузить csv файл";
            this.loadCsvButt.UseVisualStyleBackColor = true;
            this.loadCsvButt.Click += new System.EventHandler(this.loadCsvButt_Click);
            // 
            // csvTextBox
            // 
            this.csvTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.csvTextBox.Location = new System.Drawing.Point(14, 22);
            this.csvTextBox.Name = "csvTextBox";
            this.csvTextBox.ReadOnly = true;
            this.csvTextBox.Size = new System.Drawing.Size(334, 30);
            this.csvTextBox.TabIndex = 2;
            this.csvTextBox.Text = "Не выбрано";
            // 
            // txtTextBox
            // 
            this.txtTextBox.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtTextBox.Location = new System.Drawing.Point(12, 72);
            this.txtTextBox.Name = "txtTextBox";
            this.txtTextBox.ReadOnly = true;
            this.txtTextBox.Size = new System.Drawing.Size(334, 30);
            this.txtTextBox.TabIndex = 4;
            this.txtTextBox.Text = "Не выбрано";
            // 
            // loadTxtButt
            // 
            this.loadTxtButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadTxtButt.Location = new System.Drawing.Point(361, 68);
            this.loadTxtButt.Name = "loadTxtButt";
            this.loadTxtButt.Size = new System.Drawing.Size(182, 34);
            this.loadTxtButt.TabIndex = 3;
            this.loadTxtButt.Text = "Загрузить txt файл";
            this.loadTxtButt.UseVisualStyleBackColor = true;
            this.loadTxtButt.Click += new System.EventHandler(this.loadTxtButt_Click);
            // 
            // startButt
            // 
            this.startButt.Location = new System.Drawing.Point(58, 169);
            this.startButt.Name = "startButt";
            this.startButt.Size = new System.Drawing.Size(159, 53);
            this.startButt.TabIndex = 5;
            this.startButt.Text = "Начать";
            this.startButt.UseVisualStyleBackColor = true;
            this.startButt.Click += new System.EventHandler(this.startButt_Click);
            // 
            // stopButt
            // 
            this.stopButt.Enabled = false;
            this.stopButt.Location = new System.Drawing.Point(316, 169);
            this.stopButt.Name = "stopButt";
            this.stopButt.Size = new System.Drawing.Size(159, 53);
            this.stopButt.TabIndex = 6;
            this.stopButt.Text = "Остановить";
            this.stopButt.UseVisualStyleBackColor = true;
            this.stopButt.Click += new System.EventHandler(this.stopButt_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(152, 254);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(241, 20);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timeInfoLabel
            // 
            this.timeInfoLabel.AutoSize = true;
            this.timeInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.timeInfoLabel.Location = new System.Drawing.Point(235, 250);
            this.timeInfoLabel.Name = "timeInfoLabel";
            this.timeInfoLabel.Size = new System.Drawing.Size(60, 24);
            this.timeInfoLabel.TabIndex = 11;
            this.timeInfoLabel.Text = "label1";
            this.timeInfoLabel.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 296);
            this.Controls.Add(this.timeInfoLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.stopButt);
            this.Controls.Add(this.startButt);
            this.Controls.Add(this.txtTextBox);
            this.Controls.Add(this.loadTxtButt);
            this.Controls.Add(this.csvTextBox);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.loadCsvButt);
            this.Controls.Add(this.loadFileButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "MainWindow";
            this.Text = "InstaBot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Button loadCsvButt;
        private System.Windows.Forms.TextBox csvTextBox;
        private System.Windows.Forms.TextBox txtTextBox;
        private System.Windows.Forms.Button loadTxtButt;
        private System.Windows.Forms.Button startButt;
        private System.Windows.Forms.Button stopButt;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label timeInfoLabel;
    }
}

