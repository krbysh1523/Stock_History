
namespace Helper
{
    partial class Import
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_Log = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Open = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.chk_CheckAll_Symbol = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            this.chk_Selected = new System.Windows.Forms.Button();
            this.btn_Open_Daily = new System.Windows.Forms.Button();
            this.btn_ImportDaily = new System.Windows.Forms.Button();
            this.btn_LoadAnalysis = new System.Windows.Forms.Button();
            this.btn_ProcAnalysis = new System.Windows.Forms.Button();
            this.dt_Weekly = new System.Windows.Forms.DateTimePicker();
            this.btn_Weekly = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chk_Symbols = new System.Windows.Forms.DataGridView();
            this.btn_Week_Long = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Symbols)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Log
            // 
            this.txt_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_Log.Location = new System.Drawing.Point(0, 430);
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.Size = new System.Drawing.Size(814, 131);
            this.txt_Log.TabIndex = 3;
            this.txt_Log.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(630, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(184, 430);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processing Data";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_Open);
            this.flowLayoutPanel1.Controls.Add(this.btn_Download);
            this.flowLayoutPanel1.Controls.Add(this.chk_CheckAll_Symbol);
            this.flowLayoutPanel1.Controls.Add(this.btn_Test);
            this.flowLayoutPanel1.Controls.Add(this.chk_Selected);
            this.flowLayoutPanel1.Controls.Add(this.btn_Open_Daily);
            this.flowLayoutPanel1.Controls.Add(this.btn_ImportDaily);
            this.flowLayoutPanel1.Controls.Add(this.btn_LoadAnalysis);
            this.flowLayoutPanel1.Controls.Add(this.btn_ProcAnalysis);
            this.flowLayoutPanel1.Controls.Add(this.dt_Weekly);
            this.flowLayoutPanel1.Controls.Add(this.btn_Weekly);
            this.flowLayoutPanel1.Controls.Add(this.btn_Week_Long);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 15);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 413);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // btn_Open
            // 
            this.btn_Open.BackColor = System.Drawing.Color.LightSalmon;
            this.btn_Open.Location = new System.Drawing.Point(3, 3);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(116, 27);
            this.btn_Open.TabIndex = 21;
            this.btn_Open.Text = "Open Full History";
            this.btn_Open.UseVisualStyleBackColor = false;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_Download.Location = new System.Drawing.Point(3, 36);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(116, 27);
            this.btn_Download.TabIndex = 17;
            this.btn_Download.Text = "Start Import Full";
            this.btn_Download.UseVisualStyleBackColor = false;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // chk_CheckAll_Symbol
            // 
            this.chk_CheckAll_Symbol.Location = new System.Drawing.Point(3, 69);
            this.chk_CheckAll_Symbol.Name = "chk_CheckAll_Symbol";
            this.chk_CheckAll_Symbol.Size = new System.Drawing.Size(81, 27);
            this.chk_CheckAll_Symbol.TabIndex = 16;
            this.chk_CheckAll_Symbol.Text = "Check All";
            this.chk_CheckAll_Symbol.UseVisualStyleBackColor = true;
            this.chk_CheckAll_Symbol.Click += new System.EventHandler(this.chk_CheckAll_Symbol_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(90, 69);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(62, 27);
            this.btn_Test.TabIndex = 19;
            this.btn_Test.Text = "Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // chk_Selected
            // 
            this.chk_Selected.Location = new System.Drawing.Point(3, 102);
            this.chk_Selected.Name = "chk_Selected";
            this.chk_Selected.Size = new System.Drawing.Size(81, 27);
            this.chk_Selected.TabIndex = 20;
            this.chk_Selected.Text = "Selected";
            this.chk_Selected.UseVisualStyleBackColor = true;
            this.chk_Selected.Click += new System.EventHandler(this.chk_Selected_Click);
            // 
            // btn_Open_Daily
            // 
            this.btn_Open_Daily.BackColor = System.Drawing.Color.LightSalmon;
            this.btn_Open_Daily.Location = new System.Drawing.Point(3, 135);
            this.btn_Open_Daily.Name = "btn_Open_Daily";
            this.btn_Open_Daily.Size = new System.Drawing.Size(116, 27);
            this.btn_Open_Daily.TabIndex = 22;
            this.btn_Open_Daily.Text = "Open Daily History";
            this.btn_Open_Daily.UseVisualStyleBackColor = false;
            this.btn_Open_Daily.Click += new System.EventHandler(this.btn_Open_Daily_Click);
            // 
            // btn_ImportDaily
            // 
            this.btn_ImportDaily.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_ImportDaily.Location = new System.Drawing.Point(3, 168);
            this.btn_ImportDaily.Name = "btn_ImportDaily";
            this.btn_ImportDaily.Size = new System.Drawing.Size(116, 27);
            this.btn_ImportDaily.TabIndex = 23;
            this.btn_ImportDaily.Text = "Start Import Daily";
            this.btn_ImportDaily.UseVisualStyleBackColor = false;
            this.btn_ImportDaily.Click += new System.EventHandler(this.btn_ImportDaily_Click);
            // 
            // btn_LoadAnalysis
            // 
            this.btn_LoadAnalysis.BackColor = System.Drawing.Color.Pink;
            this.btn_LoadAnalysis.Location = new System.Drawing.Point(3, 201);
            this.btn_LoadAnalysis.Name = "btn_LoadAnalysis";
            this.btn_LoadAnalysis.Size = new System.Drawing.Size(116, 27);
            this.btn_LoadAnalysis.TabIndex = 25;
            this.btn_LoadAnalysis.Text = "Load Unupdated";
            this.btn_LoadAnalysis.UseVisualStyleBackColor = false;
            this.btn_LoadAnalysis.Click += new System.EventHandler(this.btn_LoadAnalysis_Click);
            // 
            // btn_ProcAnalysis
            // 
            this.btn_ProcAnalysis.BackColor = System.Drawing.Color.Cyan;
            this.btn_ProcAnalysis.Location = new System.Drawing.Point(3, 234);
            this.btn_ProcAnalysis.Name = "btn_ProcAnalysis";
            this.btn_ProcAnalysis.Size = new System.Drawing.Size(116, 27);
            this.btn_ProcAnalysis.TabIndex = 24;
            this.btn_ProcAnalysis.Text = "Start SP Analysis";
            this.btn_ProcAnalysis.UseVisualStyleBackColor = false;
            this.btn_ProcAnalysis.Click += new System.EventHandler(this.btn_ProcAnalysis_Click);
            // 
            // dt_Weekly
            // 
            this.dt_Weekly.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_Weekly.Location = new System.Drawing.Point(3, 267);
            this.dt_Weekly.Name = "dt_Weekly";
            this.dt_Weekly.Size = new System.Drawing.Size(116, 20);
            this.dt_Weekly.TabIndex = 27;
            // 
            // btn_Weekly
            // 
            this.btn_Weekly.BackColor = System.Drawing.Color.Orange;
            this.btn_Weekly.Location = new System.Drawing.Point(3, 293);
            this.btn_Weekly.Name = "btn_Weekly";
            this.btn_Weekly.Size = new System.Drawing.Size(116, 27);
            this.btn_Weekly.TabIndex = 26;
            this.btn_Weekly.Text = "Start Week Analysis";
            this.btn_Weekly.UseVisualStyleBackColor = false;
            this.btn_Weekly.Click += new System.EventHandler(this.btn_Weekly_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chk_Symbols);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(630, 430);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Symbols";
            // 
            // chk_Symbols
            // 
            this.chk_Symbols.AllowUserToAddRows = false;
            this.chk_Symbols.AllowUserToDeleteRows = false;
            this.chk_Symbols.AllowUserToResizeRows = false;
            this.chk_Symbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chk_Symbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chk_Symbols.Location = new System.Drawing.Point(2, 15);
            this.chk_Symbols.Margin = new System.Windows.Forms.Padding(2);
            this.chk_Symbols.Name = "chk_Symbols";
            this.chk_Symbols.ReadOnly = true;
            this.chk_Symbols.RowHeadersVisible = false;
            this.chk_Symbols.RowHeadersWidth = 51;
            this.chk_Symbols.RowTemplate.Height = 24;
            this.chk_Symbols.Size = new System.Drawing.Size(626, 413);
            this.chk_Symbols.TabIndex = 17;
            // 
            // btn_Week_Long
            // 
            this.btn_Week_Long.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_Week_Long.Location = new System.Drawing.Point(3, 326);
            this.btn_Week_Long.Name = "btn_Week_Long";
            this.btn_Week_Long.Size = new System.Drawing.Size(149, 27);
            this.btn_Week_Long.TabIndex = 28;
            this.btn_Week_Long.Text = "Start Week Long Analysis";
            this.btn_Week_Long.UseVisualStyleBackColor = false;
            this.btn_Week_Long.Click += new System.EventHandler(this.btn_Week_Long_Click);
            // 
            // Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 561);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Log);
            this.Name = "Import";
            this.Text = "Import";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Import_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_Symbols)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox txt_Log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button chk_CheckAll_Symbol;
        private System.Windows.Forms.DataGridView chk_Symbols;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button chk_Selected;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_Open_Daily;
        private System.Windows.Forms.Button btn_ImportDaily;
        private System.Windows.Forms.Button btn_ProcAnalysis;
        private System.Windows.Forms.Button btn_LoadAnalysis;
        private System.Windows.Forms.Button btn_Weekly;
        private System.Windows.Forms.DateTimePicker dt_Weekly;
        private System.Windows.Forms.Button btn_Week_Long;
    }
}

