
namespace History
{
    partial class Analysis
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
            this.chart_Main = new LiveCharts.WinForms.CartesianChart();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_From = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dt_To = new System.Windows.Forms.DateTimePicker();
            this.btn_P1 = new System.Windows.Forms.Button();
            this.btn_N1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Symbol = new System.Windows.Forms.TextBox();
            this.btn_Symbol = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_Index = new System.Windows.Forms.ComboBox();
            this.btn_Index = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.chart_MainVol = new LiveCharts.WinForms.CartesianChart();
            this.chart_Chg = new LiveCharts.WinForms.CartesianChart();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chart_Index = new LiveCharts.WinForms.CartesianChart();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.chart_IndexVol = new LiveCharts.WinForms.CartesianChart();
            this.btn_Screenshot = new System.Windows.Forms.Button();
            this.btn_HideIndex = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_Main
            // 
            this.chart_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Main.Location = new System.Drawing.Point(0, 0);
            this.chart_Main.Name = "chart_Main";
            this.chart_Main.Size = new System.Drawing.Size(393, 341);
            this.chart_Main.TabIndex = 0;
            this.chart_Main.Text = "cartesianChart1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.dt_From);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.dt_To);
            this.flowLayoutPanel1.Controls.Add(this.btn_P1);
            this.flowLayoutPanel1.Controls.Add(this.btn_N1);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.txt_Symbol);
            this.flowLayoutPanel1.Controls.Add(this.btn_Symbol);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.cmb_Index);
            this.flowLayoutPanel1.Controls.Add(this.btn_Index);
            this.flowLayoutPanel1.Controls.Add(this.btn_Screenshot);
            this.flowLayoutPanel1.Controls.Add(this.btn_HideIndex);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1194, 40);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dt_From
            // 
            this.dt_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_From.Location = new System.Drawing.Point(65, 3);
            this.dt_From.Name = "dt_From";
            this.dt_From.Size = new System.Drawing.Size(101, 22);
            this.dt_From.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(172, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dt_To
            // 
            this.dt_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_To.Location = new System.Drawing.Point(205, 3);
            this.dt_To.Name = "dt_To";
            this.dt_To.Size = new System.Drawing.Size(101, 22);
            this.dt_To.TabIndex = 3;
            // 
            // btn_P1
            // 
            this.btn_P1.Location = new System.Drawing.Point(312, 3);
            this.btn_P1.Name = "btn_P1";
            this.btn_P1.Size = new System.Drawing.Size(25, 23);
            this.btn_P1.TabIndex = 10;
            this.btn_P1.Text = "<";
            this.btn_P1.UseVisualStyleBackColor = true;
            this.btn_P1.Click += new System.EventHandler(this.btn_P1_Click);
            // 
            // btn_N1
            // 
            this.btn_N1.Location = new System.Drawing.Point(343, 3);
            this.btn_N1.Name = "btn_N1";
            this.btn_N1.Size = new System.Drawing.Size(25, 23);
            this.btn_N1.TabIndex = 12;
            this.btn_N1.Text = ">";
            this.btn_N1.UseVisualStyleBackColor = true;
            this.btn_N1.Click += new System.EventHandler(this.btn_N1_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(374, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Symbol:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Symbol
            // 
            this.txt_Symbol.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_Symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Symbol.Location = new System.Drawing.Point(458, 3);
            this.txt_Symbol.Name = "txt_Symbol";
            this.txt_Symbol.Size = new System.Drawing.Size(124, 30);
            this.txt_Symbol.TabIndex = 7;
            this.txt_Symbol.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_Symbol_MouseClick);
            this.txt_Symbol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_Symbol_KeyUp);
            // 
            // btn_Symbol
            // 
            this.btn_Symbol.Location = new System.Drawing.Point(588, 3);
            this.btn_Symbol.Name = "btn_Symbol";
            this.btn_Symbol.Size = new System.Drawing.Size(67, 29);
            this.btn_Symbol.TabIndex = 5;
            this.btn_Symbol.Text = "Stock";
            this.btn_Symbol.UseVisualStyleBackColor = true;
            this.btn_Symbol.Click += new System.EventHandler(this.btn_Symbol_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(661, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Index:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_Index
            // 
            this.cmb_Index.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Index.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Index.FormattingEnabled = true;
            this.cmb_Index.Items.AddRange(new object[] {
            "SPY",
            "DIA",
            "QQQ",
            "VGT",
            "ARKK"});
            this.cmb_Index.Location = new System.Drawing.Point(743, 3);
            this.cmb_Index.Name = "cmb_Index";
            this.cmb_Index.Size = new System.Drawing.Size(121, 33);
            this.cmb_Index.TabIndex = 6;
            // 
            // btn_Index
            // 
            this.btn_Index.Location = new System.Drawing.Point(870, 3);
            this.btn_Index.Name = "btn_Index";
            this.btn_Index.Size = new System.Drawing.Size(73, 29);
            this.btn_Index.TabIndex = 9;
            this.btn_Index.Text = "Index";
            this.btn_Index.UseVisualStyleBackColor = true;
            this.btn_Index.Click += new System.EventHandler(this.btn_Index_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1194, 705);
            this.splitContainer1.SplitterDistance = 397;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chart_Main);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(397, 705);
            this.splitContainer2.SplitterDistance = 345;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.chart_MainVol);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.chart_Chg);
            this.splitContainer4.Size = new System.Drawing.Size(397, 356);
            this.splitContainer4.SplitterDistance = 121;
            this.splitContainer4.TabIndex = 2;
            // 
            // chart_MainVol
            // 
            this.chart_MainVol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_MainVol.Location = new System.Drawing.Point(0, 0);
            this.chart_MainVol.Name = "chart_MainVol";
            this.chart_MainVol.Size = new System.Drawing.Size(393, 117);
            this.chart_MainVol.TabIndex = 1;
            this.chart_MainVol.Text = "cartesianChart1";
            // 
            // chart_Chg
            // 
            this.chart_Chg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Chg.Location = new System.Drawing.Point(0, 0);
            this.chart_Chg.Name = "chart_Chg";
            this.chart_Chg.Size = new System.Drawing.Size(393, 227);
            this.chart_Chg.TabIndex = 2;
            this.chart_Chg.Text = "cartesianChart1";
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chart_Index);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(791, 705);
            this.splitContainer3.SplitterDistance = 337;
            this.splitContainer3.TabIndex = 2;
            // 
            // chart_Index
            // 
            this.chart_Index.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Index.Location = new System.Drawing.Point(0, 0);
            this.chart_Index.Name = "chart_Index";
            this.chart_Index.Size = new System.Drawing.Size(787, 333);
            this.chart_Index.TabIndex = 1;
            this.chart_Index.Text = "cartesianChart1";
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.chart_IndexVol);
            this.splitContainer5.Size = new System.Drawing.Size(791, 364);
            this.splitContainer5.SplitterDistance = 242;
            this.splitContainer5.TabIndex = 3;
            // 
            // chart_IndexVol
            // 
            this.chart_IndexVol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_IndexVol.Location = new System.Drawing.Point(0, 0);
            this.chart_IndexVol.Name = "chart_IndexVol";
            this.chart_IndexVol.Size = new System.Drawing.Size(787, 238);
            this.chart_IndexVol.TabIndex = 2;
            this.chart_IndexVol.Text = "cartesianChart1";
            // 
            // btn_Screenshot
            // 
            this.btn_Screenshot.Location = new System.Drawing.Point(949, 3);
            this.btn_Screenshot.Name = "btn_Screenshot";
            this.btn_Screenshot.Size = new System.Drawing.Size(110, 29);
            this.btn_Screenshot.TabIndex = 13;
            this.btn_Screenshot.Text = "Screenshot";
            this.btn_Screenshot.UseVisualStyleBackColor = true;
            this.btn_Screenshot.Click += new System.EventHandler(this.btn_Screenshot_Click);
            // 
            // btn_HideIndex
            // 
            this.btn_HideIndex.Location = new System.Drawing.Point(1065, 3);
            this.btn_HideIndex.Name = "btn_HideIndex";
            this.btn_HideIndex.Size = new System.Drawing.Size(93, 29);
            this.btn_HideIndex.TabIndex = 14;
            this.btn_HideIndex.Text = "Hide Index";
            this.btn_HideIndex.UseVisualStyleBackColor = true;
            this.btn_HideIndex.Click += new System.EventHandler(this.btn_HideIndex_Click);
            // 
            // Analysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 745);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Analysis";
            this.Text = "Analysis";
            this.Load += new System.EventHandler(this.Analysis_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart chart_Main;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dt_From;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dt_To;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Symbol;
        private System.Windows.Forms.Button btn_Symbol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_Index;
        private System.Windows.Forms.Button btn_Index;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private LiveCharts.WinForms.CartesianChart chart_Index;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private LiveCharts.WinForms.CartesianChart chart_MainVol;
        private LiveCharts.WinForms.CartesianChart chart_IndexVol;
        private System.Windows.Forms.Button btn_P1;
        private System.Windows.Forms.Button btn_N1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private LiveCharts.WinForms.CartesianChart chart_Chg;
        private System.Windows.Forms.Button btn_Screenshot;
        private System.Windows.Forms.Button btn_HideIndex;
    }
}