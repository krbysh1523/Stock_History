
namespace History
{
    partial class GetData
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
            this.chk_Processing = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_ProcAll = new System.Windows.Forms.Button();
            this.btn_CheckAllProc = new System.Windows.Forms.Button();
            this.btn_View = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_Downloads = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Download = new System.Windows.Forms.Button();
            this.chk_CheckAll_Download = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chk_Symbols = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk_CheckAll_Symbol = new System.Windows.Forms.Button();
            this.btn_Check_Not_Updated = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            this.chk_Selected = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Symbols)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.chk_Processing);
            this.groupBox1.Controls.Add(this.flowLayoutPanel3);
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
            // chk_Processing
            // 
            this.chk_Processing.CheckOnClick = true;
            this.chk_Processing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chk_Processing.FormattingEnabled = true;
            this.chk_Processing.Items.AddRange(new object[] {
            "History",
            "SMA",
            "EMA",
            "RSI",
            "MACD",
            "Forecast",
            "Change",
            "Analysis",
            "EPR",
            "Volume",
            "Index"});
            this.chk_Processing.Location = new System.Drawing.Point(2, 115);
            this.chk_Processing.Margin = new System.Windows.Forms.Padding(2);
            this.chk_Processing.Name = "chk_Processing";
            this.chk_Processing.Size = new System.Drawing.Size(180, 313);
            this.chk_Processing.TabIndex = 14;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btn_ProcAll);
            this.flowLayoutPanel3.Controls.Add(this.btn_CheckAllProc);
            this.flowLayoutPanel3.Controls.Add(this.btn_View);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(2, 15);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(180, 100);
            this.flowLayoutPanel3.TabIndex = 16;
            // 
            // btn_ProcAll
            // 
            this.btn_ProcAll.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_ProcAll.Location = new System.Drawing.Point(3, 3);
            this.btn_ProcAll.Name = "btn_ProcAll";
            this.btn_ProcAll.Size = new System.Drawing.Size(88, 27);
            this.btn_ProcAll.TabIndex = 12;
            this.btn_ProcAll.Text = "Process";
            this.btn_ProcAll.UseVisualStyleBackColor = false;
            this.btn_ProcAll.Click += new System.EventHandler(this.btn_ProcAll_Click);
            // 
            // btn_CheckAllProc
            // 
            this.btn_CheckAllProc.Location = new System.Drawing.Point(2, 35);
            this.btn_CheckAllProc.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CheckAllProc.Name = "btn_CheckAllProc";
            this.btn_CheckAllProc.Size = new System.Drawing.Size(88, 27);
            this.btn_CheckAllProc.TabIndex = 15;
            this.btn_CheckAllProc.Text = "Check All";
            this.btn_CheckAllProc.UseVisualStyleBackColor = true;
            this.btn_CheckAllProc.Click += new System.EventHandler(this.btn_CheckAllProc_Click);
            // 
            // btn_View
            // 
            this.btn_View.Location = new System.Drawing.Point(2, 66);
            this.btn_View.Margin = new System.Windows.Forms.Padding(2);
            this.btn_View.Name = "btn_View";
            this.btn_View.Size = new System.Drawing.Size(89, 26);
            this.btn_View.TabIndex = 0;
            this.btn_View.Text = "Recreate View";
            this.btn_View.UseVisualStyleBackColor = true;
            this.btn_View.Click += new System.EventHandler(this.btn_View_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_Downloads);
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(447, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(183, 430);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Download";
            // 
            // chk_Downloads
            // 
            this.chk_Downloads.CheckOnClick = true;
            this.chk_Downloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chk_Downloads.FormattingEnabled = true;
            this.chk_Downloads.Items.AddRange(new object[] {
            "Download Income",
            "Download Earning",
            "Download Latest",
            "Download All History"});
            this.chk_Downloads.Location = new System.Drawing.Point(2, 85);
            this.chk_Downloads.Margin = new System.Windows.Forms.Padding(2);
            this.chk_Downloads.Name = "chk_Downloads";
            this.chk_Downloads.Size = new System.Drawing.Size(179, 343);
            this.chk_Downloads.TabIndex = 16;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btn_Download);
            this.flowLayoutPanel2.Controls.Add(this.chk_CheckAll_Download);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(2, 15);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(179, 70);
            this.flowLayoutPanel2.TabIndex = 19;
            // 
            // btn_Download
            // 
            this.btn_Download.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_Download.Location = new System.Drawing.Point(3, 3);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(88, 27);
            this.btn_Download.TabIndex = 17;
            this.btn_Download.Text = "Download";
            this.btn_Download.UseVisualStyleBackColor = false;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // chk_CheckAll_Download
            // 
            this.chk_CheckAll_Download.Location = new System.Drawing.Point(2, 35);
            this.chk_CheckAll_Download.Margin = new System.Windows.Forms.Padding(2);
            this.chk_CheckAll_Download.Name = "chk_CheckAll_Download";
            this.chk_CheckAll_Download.Size = new System.Drawing.Size(88, 27);
            this.chk_CheckAll_Download.TabIndex = 18;
            this.chk_CheckAll_Download.Text = "Check All";
            this.chk_CheckAll_Download.UseVisualStyleBackColor = true;
            this.chk_CheckAll_Download.Click += new System.EventHandler(this.chk_CheckAll_Download_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chk_Symbols);
            this.groupBox4.Controls.Add(this.flowLayoutPanel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(447, 430);
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
            this.chk_Symbols.Location = new System.Drawing.Point(2, 52);
            this.chk_Symbols.Margin = new System.Windows.Forms.Padding(2);
            this.chk_Symbols.Name = "chk_Symbols";
            this.chk_Symbols.ReadOnly = true;
            this.chk_Symbols.RowHeadersVisible = false;
            this.chk_Symbols.RowHeadersWidth = 51;
            this.chk_Symbols.RowTemplate.Height = 24;
            this.chk_Symbols.Size = new System.Drawing.Size(443, 376);
            this.chk_Symbols.TabIndex = 17;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chk_CheckAll_Symbol);
            this.flowLayoutPanel1.Controls.Add(this.btn_Check_Not_Updated);
            this.flowLayoutPanel1.Controls.Add(this.btn_Test);
            this.flowLayoutPanel1.Controls.Add(this.chk_Selected);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 15);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 37);
            this.flowLayoutPanel1.TabIndex = 21;
            // 
            // chk_CheckAll_Symbol
            // 
            this.chk_CheckAll_Symbol.Location = new System.Drawing.Point(3, 3);
            this.chk_CheckAll_Symbol.Name = "chk_CheckAll_Symbol";
            this.chk_CheckAll_Symbol.Size = new System.Drawing.Size(81, 27);
            this.chk_CheckAll_Symbol.TabIndex = 16;
            this.chk_CheckAll_Symbol.Text = "Check All";
            this.chk_CheckAll_Symbol.UseVisualStyleBackColor = true;
            this.chk_CheckAll_Symbol.Click += new System.EventHandler(this.chk_CheckAll_Symbol_Click);
            // 
            // btn_Check_Not_Updated
            // 
            this.btn_Check_Not_Updated.Location = new System.Drawing.Point(90, 3);
            this.btn_Check_Not_Updated.Name = "btn_Check_Not_Updated";
            this.btn_Check_Not_Updated.Size = new System.Drawing.Size(120, 27);
            this.btn_Check_Not_Updated.TabIndex = 18;
            this.btn_Check_Not_Updated.Text = "Check Not Updated";
            this.btn_Check_Not_Updated.UseVisualStyleBackColor = true;
            this.btn_Check_Not_Updated.Click += new System.EventHandler(this.btn_Check_Not_Updated_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(216, 3);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(62, 27);
            this.btn_Test.TabIndex = 19;
            this.btn_Test.Text = "Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // chk_Selected
            // 
            this.chk_Selected.Location = new System.Drawing.Point(284, 3);
            this.chk_Selected.Name = "chk_Selected";
            this.chk_Selected.Size = new System.Drawing.Size(81, 27);
            this.chk_Selected.TabIndex = 20;
            this.chk_Selected.Text = "Selected";
            this.chk_Selected.UseVisualStyleBackColor = true;
            this.chk_Selected.Click += new System.EventHandler(this.chk_Selected_Click);
            // 
            // GetData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 561);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Log);
            this.Name = "GetData";
            this.Text = "Get Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetData_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_Symbols)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox txt_Log;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_View;
        private System.Windows.Forms.Button btn_ProcAll;
        private System.Windows.Forms.CheckedListBox chk_Processing;
        private System.Windows.Forms.Button btn_CheckAllProc;
        private System.Windows.Forms.Button chk_CheckAll_Download;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.CheckedListBox chk_Downloads;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button chk_CheckAll_Symbol;
        private System.Windows.Forms.DataGridView chk_Symbols;
        private System.Windows.Forms.Button btn_Check_Not_Updated;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button chk_Selected;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

