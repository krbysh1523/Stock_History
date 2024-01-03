
namespace Helper
{
    partial class Index_Analyzer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chart_Main = new LiveCharts.WinForms.CartesianChart();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.dt_Symbol_From = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dt_Symbol_To = new System.Windows.Forms.DateTimePicker();
            this.btn_PD1 = new System.Windows.Forms.Button();
            this.btn_ND1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.nmb_Y_Min = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nmb_Y_Max = new System.Windows.Forms.NumericUpDown();
            this.btn_LowerAxis = new System.Windows.Forms.Button();
            this.btn_RaiseAxis = new System.Windows.Forms.Button();
            this.btn_NarrowAxis = new System.Windows.Forms.Button();
            this.btn_WideAxis = new System.Windows.Forms.Button();
            this.btn_Fixed = new System.Windows.Forms.Button();
            this.chk_FixedRange = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmb_2nd_Axix_Y_Min = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nmb_2nd_Axix_Y_Max = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.chk_IndexPrice = new System.Windows.Forms.CheckBox();
            this.chk_IndexSMA5 = new System.Windows.Forms.CheckBox();
            this.chk_S_Price = new System.Windows.Forms.CheckBox();
            this.chk_S_Volume = new System.Windows.Forms.CheckBox();
            this.chk_S_SMA5 = new System.Windows.Forms.CheckBox();
            this.chk_TMB = new System.Windows.Forms.CheckBox();
            this.chk_TV = new System.Windows.Forms.CheckBox();
            this.chk_MV = new System.Windows.Forms.CheckBox();
            this.chk_BV = new System.Windows.Forms.CheckBox();
            this.chk_TMB_Ratio1 = new System.Windows.Forms.CheckBox();
            this.chk_TMB_Ratio2 = new System.Windows.Forms.CheckBox();
            this.chk_TMB_Ratio3 = new System.Windows.Forms.CheckBox();
            this.chk_EPS = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.nmb_CaptureList = new System.Windows.Forms.NumericUpDown();
            this.btn_Capture = new System.Windows.Forms.Button();
            this.btn_Capture_One = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.nmb_Sleep = new System.Windows.Forms.NumericUpDown();
            this.btn_CopyList = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pic_Chart = new System.Windows.Forms.PictureBox();
            this.table_Option = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmb_SP = new System.Windows.Forms.ComboBox();
            this.btn_SP = new System.Windows.Forms.Button();
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.txt_Log = new System.Windows.Forms.RichTextBox();
            this.chk_PassingList = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Max)).BeginInit();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_CaptureList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Sleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Chart)).BeginInit();
            this.table_Option.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_Main
            // 
            this.chart_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Main.Location = new System.Drawing.Point(0, 100);
            this.chart_Main.Margin = new System.Windows.Forms.Padding(2);
            this.chart_Main.Name = "chart_Main";
            this.chart_Main.Size = new System.Drawing.Size(999, 625);
            this.chart_Main.TabIndex = 0;
            this.chart_Main.Text = "cartesianChart1";
            this.chart_Main.DataClick += new LiveCharts.Events.DataClickHandler(this.chart_Main_DataClick);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel3.Controls.Add(this.dt_Symbol_From);
            this.flowLayoutPanel3.Controls.Add(this.label4);
            this.flowLayoutPanel3.Controls.Add(this.dt_Symbol_To);
            this.flowLayoutPanel3.Controls.Add(this.btn_PD1);
            this.flowLayoutPanel3.Controls.Add(this.btn_ND1);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(194, 94);
            this.flowLayoutPanel3.TabIndex = 37;
            // 
            // dt_Symbol_From
            // 
            this.dt_Symbol_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_Symbol_From.Location = new System.Drawing.Point(2, 2);
            this.dt_Symbol_From.Margin = new System.Windows.Forms.Padding(2);
            this.dt_Symbol_From.Name = "dt_Symbol_From";
            this.dt_Symbol_From.Size = new System.Drawing.Size(77, 20);
            this.dt_Symbol_From.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(83, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 26);
            this.label4.TabIndex = 19;
            this.label4.Text = "~";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dt_Symbol_To
            // 
            this.dt_Symbol_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_Symbol_To.Location = new System.Drawing.Point(107, 2);
            this.dt_Symbol_To.Margin = new System.Windows.Forms.Padding(2);
            this.dt_Symbol_To.Name = "dt_Symbol_To";
            this.dt_Symbol_To.Size = new System.Drawing.Size(77, 20);
            this.dt_Symbol_To.TabIndex = 20;
            // 
            // btn_PD1
            // 
            this.btn_PD1.Location = new System.Drawing.Point(2, 28);
            this.btn_PD1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_PD1.Name = "btn_PD1";
            this.btn_PD1.Size = new System.Drawing.Size(19, 19);
            this.btn_PD1.TabIndex = 14;
            this.btn_PD1.Text = "-";
            this.btn_PD1.UseVisualStyleBackColor = true;
            this.btn_PD1.Click += new System.EventHandler(this.btn_PD1_Click);
            // 
            // btn_ND1
            // 
            this.btn_ND1.Location = new System.Drawing.Point(25, 28);
            this.btn_ND1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ND1.Name = "btn_ND1";
            this.btn_ND1.Size = new System.Drawing.Size(19, 19);
            this.btn_ND1.TabIndex = 15;
            this.btn_ND1.Text = "+";
            this.btn_ND1.UseVisualStyleBackColor = true;
            this.btn_ND1.Click += new System.EventHandler(this.btn_ND1_Click);
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel6.Controls.Add(this.label6);
            this.flowLayoutPanel6.Controls.Add(this.nmb_Y_Min);
            this.flowLayoutPanel6.Controls.Add(this.label7);
            this.flowLayoutPanel6.Controls.Add(this.nmb_Y_Max);
            this.flowLayoutPanel6.Controls.Add(this.btn_LowerAxis);
            this.flowLayoutPanel6.Controls.Add(this.btn_RaiseAxis);
            this.flowLayoutPanel6.Controls.Add(this.btn_NarrowAxis);
            this.flowLayoutPanel6.Controls.Add(this.btn_WideAxis);
            this.flowLayoutPanel6.Controls.Add(this.btn_Fixed);
            this.flowLayoutPanel6.Controls.Add(this.chk_FixedRange);
            this.flowLayoutPanel6.Controls.Add(this.label1);
            this.flowLayoutPanel6.Controls.Add(this.nmb_2nd_Axix_Y_Min);
            this.flowLayoutPanel6.Controls.Add(this.label3);
            this.flowLayoutPanel6.Controls.Add(this.nmb_2nd_Axix_Y_Max);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(203, 3);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(294, 94);
            this.flowLayoutPanel6.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(2, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 26);
            this.label6.TabIndex = 23;
            this.label6.Text = "Y (Min-Max):";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmb_Y_Min
            // 
            this.nmb_Y_Min.Location = new System.Drawing.Point(79, 3);
            this.nmb_Y_Min.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmb_Y_Min.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nmb_Y_Min.Name = "nmb_Y_Min";
            this.nmb_Y_Min.Size = new System.Drawing.Size(46, 20);
            this.nmb_Y_Min.TabIndex = 22;
            this.nmb_Y_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmb_Y_Min.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(130, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 26);
            this.label7.TabIndex = 25;
            this.label7.Text = "~";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nmb_Y_Max
            // 
            this.nmb_Y_Max.Location = new System.Drawing.Point(155, 3);
            this.nmb_Y_Max.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmb_Y_Max.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nmb_Y_Max.Name = "nmb_Y_Max";
            this.nmb_Y_Max.Size = new System.Drawing.Size(46, 20);
            this.nmb_Y_Max.TabIndex = 24;
            this.nmb_Y_Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmb_Y_Max.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btn_LowerAxis
            // 
            this.btn_LowerAxis.Location = new System.Drawing.Point(206, 2);
            this.btn_LowerAxis.Margin = new System.Windows.Forms.Padding(2);
            this.btn_LowerAxis.Name = "btn_LowerAxis";
            this.btn_LowerAxis.Size = new System.Drawing.Size(19, 25);
            this.btn_LowerAxis.TabIndex = 26;
            this.btn_LowerAxis.Text = "↓";
            this.btn_LowerAxis.UseVisualStyleBackColor = true;
            this.btn_LowerAxis.Click += new System.EventHandler(this.btn_LowerAxis_Click);
            // 
            // btn_RaiseAxis
            // 
            this.btn_RaiseAxis.Location = new System.Drawing.Point(229, 2);
            this.btn_RaiseAxis.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RaiseAxis.Name = "btn_RaiseAxis";
            this.btn_RaiseAxis.Size = new System.Drawing.Size(19, 25);
            this.btn_RaiseAxis.TabIndex = 27;
            this.btn_RaiseAxis.Text = "↑";
            this.btn_RaiseAxis.UseVisualStyleBackColor = true;
            this.btn_RaiseAxis.Click += new System.EventHandler(this.btn_RaiseAxis_Click);
            // 
            // btn_NarrowAxis
            // 
            this.btn_NarrowAxis.Location = new System.Drawing.Point(252, 2);
            this.btn_NarrowAxis.Margin = new System.Windows.Forms.Padding(2);
            this.btn_NarrowAxis.Name = "btn_NarrowAxis";
            this.btn_NarrowAxis.Size = new System.Drawing.Size(19, 25);
            this.btn_NarrowAxis.TabIndex = 28;
            this.btn_NarrowAxis.Text = "-";
            this.btn_NarrowAxis.UseVisualStyleBackColor = true;
            this.btn_NarrowAxis.Click += new System.EventHandler(this.btn_NarrowAxis_Click);
            // 
            // btn_WideAxis
            // 
            this.btn_WideAxis.Location = new System.Drawing.Point(2, 31);
            this.btn_WideAxis.Margin = new System.Windows.Forms.Padding(2);
            this.btn_WideAxis.Name = "btn_WideAxis";
            this.btn_WideAxis.Size = new System.Drawing.Size(19, 25);
            this.btn_WideAxis.TabIndex = 29;
            this.btn_WideAxis.Text = "+";
            this.btn_WideAxis.UseVisualStyleBackColor = true;
            this.btn_WideAxis.Click += new System.EventHandler(this.btn_WideAxis_Click);
            // 
            // btn_Fixed
            // 
            this.btn_Fixed.Location = new System.Drawing.Point(25, 31);
            this.btn_Fixed.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Fixed.Name = "btn_Fixed";
            this.btn_Fixed.Size = new System.Drawing.Size(19, 25);
            this.btn_Fixed.TabIndex = 41;
            this.btn_Fixed.Text = "O";
            this.btn_Fixed.UseVisualStyleBackColor = true;
            this.btn_Fixed.Click += new System.EventHandler(this.btn_Fixed_Click);
            // 
            // chk_FixedRange
            // 
            this.chk_FixedRange.AutoSize = true;
            this.chk_FixedRange.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_FixedRange.Checked = true;
            this.chk_FixedRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_FixedRange.Location = new System.Drawing.Point(49, 32);
            this.chk_FixedRange.Name = "chk_FixedRange";
            this.chk_FixedRange.Size = new System.Drawing.Size(97, 17);
            this.chk_FixedRange.TabIndex = 36;
            this.chk_FixedRange.Text = "Is Fixed Range";
            this.chk_FixedRange.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(151, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 26);
            this.label1.TabIndex = 37;
            this.label1.Text = "2nd Y Axis (Max):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmb_2nd_Axix_Y_Min
            // 
            this.nmb_2nd_Axix_Y_Min.Location = new System.Drawing.Point(3, 61);
            this.nmb_2nd_Axix_Y_Min.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmb_2nd_Axix_Y_Min.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nmb_2nd_Axix_Y_Min.Name = "nmb_2nd_Axix_Y_Min";
            this.nmb_2nd_Axix_Y_Min.Size = new System.Drawing.Size(46, 20);
            this.nmb_2nd_Axix_Y_Min.TabIndex = 39;
            this.nmb_2nd_Axix_Y_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmb_2nd_Axix_Y_Min.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(54, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 26);
            this.label3.TabIndex = 40;
            this.label3.Text = "~";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nmb_2nd_Axix_Y_Max
            // 
            this.nmb_2nd_Axix_Y_Max.Location = new System.Drawing.Point(79, 61);
            this.nmb_2nd_Axix_Y_Max.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmb_2nd_Axix_Y_Max.Name = "nmb_2nd_Axix_Y_Max";
            this.nmb_2nd_Axix_Y_Max.Size = new System.Drawing.Size(46, 20);
            this.nmb_2nd_Axix_Y_Max.TabIndex = 38;
            this.nmb_2nd_Axix_Y_Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmb_2nd_Axix_Y_Max.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoScroll = true;
            this.flowLayoutPanel5.Controls.Add(this.chk_IndexPrice);
            this.flowLayoutPanel5.Controls.Add(this.chk_IndexSMA5);
            this.flowLayoutPanel5.Controls.Add(this.chk_S_Price);
            this.flowLayoutPanel5.Controls.Add(this.chk_S_Volume);
            this.flowLayoutPanel5.Controls.Add(this.chk_S_SMA5);
            this.flowLayoutPanel5.Controls.Add(this.chk_TMB);
            this.flowLayoutPanel5.Controls.Add(this.chk_TV);
            this.flowLayoutPanel5.Controls.Add(this.chk_MV);
            this.flowLayoutPanel5.Controls.Add(this.chk_BV);
            this.flowLayoutPanel5.Controls.Add(this.chk_TMB_Ratio1);
            this.flowLayoutPanel5.Controls.Add(this.chk_TMB_Ratio2);
            this.flowLayoutPanel5.Controls.Add(this.chk_TMB_Ratio3);
            this.flowLayoutPanel5.Controls.Add(this.chk_EPS);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(503, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(144, 94);
            this.flowLayoutPanel5.TabIndex = 42;
            // 
            // chk_IndexPrice
            // 
            this.chk_IndexPrice.AutoSize = true;
            this.chk_IndexPrice.Location = new System.Drawing.Point(3, 3);
            this.chk_IndexPrice.Name = "chk_IndexPrice";
            this.chk_IndexPrice.Size = new System.Drawing.Size(82, 17);
            this.chk_IndexPrice.TabIndex = 11;
            this.chk_IndexPrice.Text = "Index  Price";
            this.chk_IndexPrice.UseVisualStyleBackColor = true;
            // 
            // chk_IndexSMA5
            // 
            this.chk_IndexSMA5.AutoSize = true;
            this.chk_IndexSMA5.Location = new System.Drawing.Point(3, 26);
            this.chk_IndexSMA5.Name = "chk_IndexSMA5";
            this.chk_IndexSMA5.Size = new System.Drawing.Size(84, 17);
            this.chk_IndexSMA5.TabIndex = 0;
            this.chk_IndexSMA5.Text = "Index SMA5";
            this.chk_IndexSMA5.UseVisualStyleBackColor = true;
            // 
            // chk_S_Price
            // 
            this.chk_S_Price.Location = new System.Drawing.Point(3, 49);
            this.chk_S_Price.Name = "chk_S_Price";
            this.chk_S_Price.Size = new System.Drawing.Size(50, 17);
            this.chk_S_Price.TabIndex = 2;
            this.chk_S_Price.Text = "Price";
            this.chk_S_Price.UseVisualStyleBackColor = true;
            // 
            // chk_S_Volume
            // 
            this.chk_S_Volume.AutoSize = true;
            this.chk_S_Volume.Checked = true;
            this.chk_S_Volume.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_S_Volume.Location = new System.Drawing.Point(59, 49);
            this.chk_S_Volume.Name = "chk_S_Volume";
            this.chk_S_Volume.Size = new System.Drawing.Size(61, 17);
            this.chk_S_Volume.TabIndex = 4;
            this.chk_S_Volume.Text = "Volume";
            this.chk_S_Volume.UseVisualStyleBackColor = true;
            // 
            // chk_S_SMA5
            // 
            this.chk_S_SMA5.AutoSize = true;
            this.chk_S_SMA5.Location = new System.Drawing.Point(3, 72);
            this.chk_S_SMA5.Name = "chk_S_SMA5";
            this.chk_S_SMA5.Size = new System.Drawing.Size(55, 17);
            this.chk_S_SMA5.TabIndex = 3;
            this.chk_S_SMA5.Text = "SMA5";
            this.chk_S_SMA5.UseVisualStyleBackColor = true;
            // 
            // chk_TMB
            // 
            this.chk_TMB.AutoSize = true;
            this.chk_TMB.Location = new System.Drawing.Point(3, 95);
            this.chk_TMB.Name = "chk_TMB";
            this.chk_TMB.Size = new System.Drawing.Size(81, 17);
            this.chk_TMB.TabIndex = 6;
            this.chk_TMB.Text = "TMB Signal";
            this.chk_TMB.UseVisualStyleBackColor = true;
            // 
            // chk_TV
            // 
            this.chk_TV.AutoSize = true;
            this.chk_TV.Location = new System.Drawing.Point(3, 118);
            this.chk_TV.Name = "chk_TV";
            this.chk_TV.Size = new System.Drawing.Size(83, 17);
            this.chk_TV.TabIndex = 13;
            this.chk_TV.Text = "Top Volume";
            this.chk_TV.UseVisualStyleBackColor = true;
            // 
            // chk_MV
            // 
            this.chk_MV.AutoSize = true;
            this.chk_MV.Location = new System.Drawing.Point(3, 141);
            this.chk_MV.Name = "chk_MV";
            this.chk_MV.Size = new System.Drawing.Size(95, 17);
            this.chk_MV.TabIndex = 12;
            this.chk_MV.Text = "Middle Volume";
            this.chk_MV.UseVisualStyleBackColor = true;
            // 
            // chk_BV
            // 
            this.chk_BV.AutoSize = true;
            this.chk_BV.Location = new System.Drawing.Point(3, 164);
            this.chk_BV.Name = "chk_BV";
            this.chk_BV.Size = new System.Drawing.Size(97, 17);
            this.chk_BV.TabIndex = 7;
            this.chk_BV.Text = "Bottom Volume";
            this.chk_BV.UseVisualStyleBackColor = true;
            // 
            // chk_TMB_Ratio1
            // 
            this.chk_TMB_Ratio1.AutoSize = true;
            this.chk_TMB_Ratio1.Location = new System.Drawing.Point(3, 187);
            this.chk_TMB_Ratio1.Name = "chk_TMB_Ratio1";
            this.chk_TMB_Ratio1.Size = new System.Drawing.Size(86, 17);
            this.chk_TMB_Ratio1.TabIndex = 10;
            this.chk_TMB_Ratio1.Text = "TMB Ratio 1";
            this.chk_TMB_Ratio1.UseVisualStyleBackColor = true;
            // 
            // chk_TMB_Ratio2
            // 
            this.chk_TMB_Ratio2.AutoSize = true;
            this.chk_TMB_Ratio2.Location = new System.Drawing.Point(3, 210);
            this.chk_TMB_Ratio2.Name = "chk_TMB_Ratio2";
            this.chk_TMB_Ratio2.Size = new System.Drawing.Size(86, 17);
            this.chk_TMB_Ratio2.TabIndex = 14;
            this.chk_TMB_Ratio2.Text = "TMB Ratio 2";
            this.chk_TMB_Ratio2.UseVisualStyleBackColor = true;
            // 
            // chk_TMB_Ratio3
            // 
            this.chk_TMB_Ratio3.AutoSize = true;
            this.chk_TMB_Ratio3.Location = new System.Drawing.Point(3, 233);
            this.chk_TMB_Ratio3.Name = "chk_TMB_Ratio3";
            this.chk_TMB_Ratio3.Size = new System.Drawing.Size(86, 17);
            this.chk_TMB_Ratio3.TabIndex = 15;
            this.chk_TMB_Ratio3.Text = "TMB Ratio 3";
            this.chk_TMB_Ratio3.UseVisualStyleBackColor = true;
            // 
            // chk_EPS
            // 
            this.chk_EPS.AutoSize = true;
            this.chk_EPS.Checked = true;
            this.chk_EPS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_EPS.Location = new System.Drawing.Point(3, 256);
            this.chk_EPS.Name = "chk_EPS";
            this.chk_EPS.Size = new System.Drawing.Size(73, 17);
            this.chk_EPS.TabIndex = 16;
            this.chk_EPS.Text = "EPS Date";
            this.chk_EPS.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoScroll = true;
            this.flowLayoutPanel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel7.Controls.Add(this.label8);
            this.flowLayoutPanel7.Controls.Add(this.nmb_CaptureList);
            this.flowLayoutPanel7.Controls.Add(this.btn_Capture);
            this.flowLayoutPanel7.Controls.Add(this.btn_Capture_One);
            this.flowLayoutPanel7.Controls.Add(this.label5);
            this.flowLayoutPanel7.Controls.Add(this.nmb_Sleep);
            this.flowLayoutPanel7.Controls.Add(this.btn_CopyList);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(653, 3);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(158, 94);
            this.flowLayoutPanel7.TabIndex = 41;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 26);
            this.label8.TabIndex = 33;
            this.label8.Text = "List:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmb_CaptureList
            // 
            this.nmb_CaptureList.Location = new System.Drawing.Point(36, 3);
            this.nmb_CaptureList.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmb_CaptureList.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmb_CaptureList.Name = "nmb_CaptureList";
            this.nmb_CaptureList.Size = new System.Drawing.Size(46, 20);
            this.nmb_CaptureList.TabIndex = 32;
            this.nmb_CaptureList.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmb_CaptureList.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btn_Capture
            // 
            this.btn_Capture.Location = new System.Drawing.Point(87, 2);
            this.btn_Capture.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Capture.Name = "btn_Capture";
            this.btn_Capture.Size = new System.Drawing.Size(62, 24);
            this.btn_Capture.TabIndex = 31;
            this.btn_Capture.Text = "Capture";
            this.btn_Capture.UseVisualStyleBackColor = true;
            this.btn_Capture.Click += new System.EventHandler(this.btn_Capture_Click);
            // 
            // btn_Capture_One
            // 
            this.btn_Capture_One.Location = new System.Drawing.Point(2, 30);
            this.btn_Capture_One.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Capture_One.Name = "btn_Capture_One";
            this.btn_Capture_One.Size = new System.Drawing.Size(29, 24);
            this.btn_Capture_One.TabIndex = 34;
            this.btn_Capture_One.Text = "⨀";
            this.btn_Capture_One.UseVisualStyleBackColor = true;
            this.btn_Capture_One.Click += new System.EventHandler(this.btn_Capture_One_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(35, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 26);
            this.label5.TabIndex = 36;
            this.label5.Text = "Sleep";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmb_Sleep
            // 
            this.nmb_Sleep.Location = new System.Drawing.Point(81, 31);
            this.nmb_Sleep.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmb_Sleep.Name = "nmb_Sleep";
            this.nmb_Sleep.Size = new System.Drawing.Size(46, 20);
            this.nmb_Sleep.TabIndex = 35;
            this.nmb_Sleep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nmb_Sleep.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // btn_CopyList
            // 
            this.btn_CopyList.Location = new System.Drawing.Point(2, 58);
            this.btn_CopyList.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CopyList.Name = "btn_CopyList";
            this.btn_CopyList.Size = new System.Drawing.Size(47, 24);
            this.btn_CopyList.TabIndex = 35;
            this.btn_CopyList.Text = "Copy";
            this.btn_CopyList.UseVisualStyleBackColor = true;
            this.btn_CopyList.Click += new System.EventHandler(this.btn_CopyList_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pic_Chart);
            this.splitContainer1.Panel1.Controls.Add(this.chart_Main);
            this.splitContainer1.Panel1.Controls.Add(this.table_Option);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_List);
            this.splitContainer1.Panel2.Controls.Add(this.txt_Log);
            this.splitContainer1.Size = new System.Drawing.Size(1384, 729);
            this.splitContainer1.SplitterDistance = 1003;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // pic_Chart
            // 
            this.pic_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_Chart.Location = new System.Drawing.Point(0, 100);
            this.pic_Chart.Name = "pic_Chart";
            this.pic_Chart.Size = new System.Drawing.Size(999, 625);
            this.pic_Chart.TabIndex = 2;
            this.pic_Chart.TabStop = false;
            // 
            // table_Option
            // 
            this.table_Option.ColumnCount = 5;
            this.table_Option.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.table_Option.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.table_Option.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.table_Option.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_Option.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.table_Option.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table_Option.Controls.Add(this.flowLayoutPanel7, 3, 0);
            this.table_Option.Controls.Add(this.flowLayoutPanel5, 2, 0);
            this.table_Option.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.table_Option.Controls.Add(this.flowLayoutPanel6, 1, 0);
            this.table_Option.Controls.Add(this.flowLayoutPanel1, 4, 0);
            this.table_Option.Dock = System.Windows.Forms.DockStyle.Top;
            this.table_Option.Location = new System.Drawing.Point(0, 0);
            this.table_Option.Name = "table_Option";
            this.table_Option.RowCount = 1;
            this.table_Option.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_Option.Size = new System.Drawing.Size(999, 100);
            this.table_Option.TabIndex = 43;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmb_SP);
            this.flowLayoutPanel1.Controls.Add(this.btn_SP);
            this.flowLayoutPanel1.Controls.Add(this.chk_PassingList);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(817, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(179, 94);
            this.flowLayoutPanel1.TabIndex = 43;
            // 
            // cmb_SP
            // 
            this.cmb_SP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SP.FormattingEnabled = true;
            this.cmb_SP.Items.AddRange(new object[] {
            "01.안전 그물망"});
            this.cmb_SP.Location = new System.Drawing.Point(3, 3);
            this.cmb_SP.Name = "cmb_SP";
            this.cmb_SP.Size = new System.Drawing.Size(160, 21);
            this.cmb_SP.TabIndex = 37;
            // 
            // btn_SP
            // 
            this.btn_SP.Location = new System.Drawing.Point(2, 29);
            this.btn_SP.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SP.Name = "btn_SP";
            this.btn_SP.Size = new System.Drawing.Size(66, 24);
            this.btn_SP.TabIndex = 36;
            this.btn_SP.Text = "Exec SP";
            this.btn_SP.UseVisualStyleBackColor = true;
            this.btn_SP.Click += new System.EventHandler(this.btn_SP_Click);
            // 
            // dgv_List
            // 
            this.dgv_List.AllowUserToAddRows = false;
            this.dgv_List.AllowUserToDeleteRows = false;
            this.dgv_List.AllowUserToResizeColumns = false;
            this.dgv_List.AllowUserToResizeRows = false;
            this.dgv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Chartreuse;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_List.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_List.Location = new System.Drawing.Point(0, 0);
            this.dgv_List.MultiSelect = false;
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.ReadOnly = true;
            this.dgv_List.RowHeadersVisible = false;
            this.dgv_List.Size = new System.Drawing.Size(373, 623);
            this.dgv_List.TabIndex = 0;
            this.dgv_List.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_List_CellMouseClick);
            this.dgv_List.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_List_ColumnHeaderMouseClick);
            this.dgv_List.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_List_DataBindingComplete);
            // 
            // txt_Log
            // 
            this.txt_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txt_Log.Location = new System.Drawing.Point(0, 623);
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.Size = new System.Drawing.Size(373, 102);
            this.txt_Log.TabIndex = 4;
            this.txt_Log.Text = "";
            // 
            // chk_PassingList
            // 
            this.chk_PassingList.AutoSize = true;
            this.chk_PassingList.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_PassingList.Checked = true;
            this.chk_PassingList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_PassingList.Location = new System.Drawing.Point(73, 30);
            this.chk_PassingList.Name = "chk_PassingList";
            this.chk_PassingList.Size = new System.Drawing.Size(82, 17);
            this.chk_PassingList.TabIndex = 38;
            this.chk_PassingList.Text = "Passing List";
            this.chk_PassingList.UseVisualStyleBackColor = true;
            // 
            // Index_Analyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 729);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Index_Analyzer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Index_Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Index_Analyzer_FormClosing);
            this.Load += new System.EventHandler(this.Index_Analyzer_Load);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Max)).EndInit();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmb_CaptureList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Sleep)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Chart)).EndInit();
            this.table_Option.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        public LiveCharts.WinForms.CartesianChart chart_Main;
        public System.Windows.Forms.DateTimePicker dt_Symbol_From;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DateTimePicker dt_Symbol_To;
        private System.Windows.Forms.DataGridView dgv_List;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nmb_Y_Min;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nmb_Y_Max;
        private System.Windows.Forms.Button btn_LowerAxis;
        private System.Windows.Forms.Button btn_RaiseAxis;
        private System.Windows.Forms.Button btn_NarrowAxis;
        private System.Windows.Forms.Button btn_WideAxis;
        public System.Windows.Forms.Button btn_Capture;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nmb_CaptureList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.CheckBox chk_FixedRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmb_2nd_Axix_Y_Max;
        private System.Windows.Forms.NumericUpDown nmb_2nd_Axix_Y_Min;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_PD1;
        private System.Windows.Forms.Button btn_ND1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.CheckBox chk_IndexSMA5;
        private System.Windows.Forms.CheckBox chk_S_Price;
        private System.Windows.Forms.CheckBox chk_S_Volume;
        private System.Windows.Forms.CheckBox chk_S_SMA5;
        private System.Windows.Forms.Button btn_Fixed;
        public System.Windows.Forms.Button btn_Capture_One;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown nmb_Sleep;
        private System.Windows.Forms.PictureBox pic_Chart;
        public System.Windows.Forms.Button btn_CopyList;
        private System.Windows.Forms.TableLayoutPanel table_Option;
        private System.Windows.Forms.CheckBox chk_TMB;
        private System.Windows.Forms.CheckBox chk_BV;
        private System.Windows.Forms.CheckBox chk_TMB_Ratio1;
        private System.Windows.Forms.CheckBox chk_IndexPrice;
        private System.Windows.Forms.CheckBox chk_MV;
        private System.Windows.Forms.CheckBox chk_TV;
        private System.Windows.Forms.CheckBox chk_TMB_Ratio2;
        private System.Windows.Forms.CheckBox chk_TMB_Ratio3;
        private System.Windows.Forms.CheckBox chk_EPS;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox txt_Log;
        public System.Windows.Forms.Button btn_SP;
        private System.Windows.Forms.ComboBox cmb_SP;
        private System.Windows.Forms.CheckBox chk_PassingList;
    }
}