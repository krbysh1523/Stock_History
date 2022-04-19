
namespace History
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dt_From = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dt_To = new System.Windows.Forms.DateTimePicker();
            this.btn_P1 = new System.Windows.Forms.Button();
            this.btn_N1 = new System.Windows.Forms.Button();
            this.btn_Index = new System.Windows.Forms.Button();
            this.cmb_Type = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.nmb_CaptureList = new System.Windows.Forms.NumericUpDown();
            this.btn_Capture = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.dt_Symbol_From = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dt_Symbol_To = new System.Windows.Forms.DateTimePicker();
            this.chk_PassingList = new System.Windows.Forms.CheckBox();
            this.chk_Analysis = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.nmb_Y_Min = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nmb_Y_Max = new System.Windows.Forms.NumericUpDown();
            this.btn_LowerAxis = new System.Windows.Forms.Button();
            this.btn_RaiseAxis = new System.Windows.Forms.Button();
            this.btn_NarrowAxis = new System.Windows.Forms.Button();
            this.btn_WideAxis = new System.Windows.Forms.Button();
            this.chk_FixedRange = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmb_2nd_Axix_Y_Max = new System.Windows.Forms.NumericUpDown();
            this.chk_Simul = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.txt_OptionDesc = new System.Windows.Forms.TextBox();
            this.nmb_2nd_Axix_Y_Min = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_CaptureList)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).BeginInit();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Min)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_Main
            // 
            this.chart_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart_Main.Location = new System.Drawing.Point(0, 70);
            this.chart_Main.Margin = new System.Windows.Forms.Padding(2);
            this.chart_Main.Name = "chart_Main";
            this.chart_Main.Size = new System.Drawing.Size(999, 655);
            this.chart_Main.TabIndex = 0;
            this.chart_Main.Text = "cartesianChart1";
            this.chart_Main.DataClick += new LiveCharts.Events.DataClickHandler(this.chart_Main_DataClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel7);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel6);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(999, 70);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel2.Controls.Add(this.dt_From);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.dt_To);
            this.flowLayoutPanel2.Controls.Add(this.btn_P1);
            this.flowLayoutPanel2.Controls.Add(this.btn_N1);
            this.flowLayoutPanel2.Controls.Add(this.btn_Index);
            this.flowLayoutPanel2.Controls.Add(this.cmb_Type);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(192, 62);
            this.flowLayoutPanel2.TabIndex = 36;
            // 
            // dt_From
            // 
            this.dt_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_From.Location = new System.Drawing.Point(2, 2);
            this.dt_From.Margin = new System.Windows.Forms.Padding(2);
            this.dt_From.Name = "dt_From";
            this.dt_From.Size = new System.Drawing.Size(77, 20);
            this.dt_From.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(83, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "~";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dt_To
            // 
            this.dt_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_To.Location = new System.Drawing.Point(107, 2);
            this.dt_To.Margin = new System.Windows.Forms.Padding(2);
            this.dt_To.Name = "dt_To";
            this.dt_To.Size = new System.Drawing.Size(77, 20);
            this.dt_To.TabIndex = 3;
            // 
            // btn_P1
            // 
            this.btn_P1.Location = new System.Drawing.Point(2, 28);
            this.btn_P1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_P1.Name = "btn_P1";
            this.btn_P1.Size = new System.Drawing.Size(19, 19);
            this.btn_P1.TabIndex = 10;
            this.btn_P1.Text = "<";
            this.btn_P1.UseVisualStyleBackColor = true;
            this.btn_P1.Click += new System.EventHandler(this.btn_P1_Click);
            // 
            // btn_N1
            // 
            this.btn_N1.Location = new System.Drawing.Point(25, 28);
            this.btn_N1.Margin = new System.Windows.Forms.Padding(2);
            this.btn_N1.Name = "btn_N1";
            this.btn_N1.Size = new System.Drawing.Size(19, 19);
            this.btn_N1.TabIndex = 12;
            this.btn_N1.Text = ">";
            this.btn_N1.UseVisualStyleBackColor = true;
            this.btn_N1.Click += new System.EventHandler(this.btn_N1_Click);
            // 
            // btn_Index
            // 
            this.btn_Index.Location = new System.Drawing.Point(48, 28);
            this.btn_Index.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Index.Name = "btn_Index";
            this.btn_Index.Size = new System.Drawing.Size(55, 24);
            this.btn_Index.TabIndex = 9;
            this.btn_Index.Text = "Index";
            this.btn_Index.UseVisualStyleBackColor = true;
            this.btn_Index.Click += new System.EventHandler(this.btn_Index_Click);
            // 
            // cmb_Type
            // 
            this.cmb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.Items.AddRange(new object[] {
            "1.Percent",
            "2.SMA"});
            this.cmb_Type.Location = new System.Drawing.Point(108, 29);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(76, 21);
            this.cmb_Type.TabIndex = 13;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel7.Controls.Add(this.label8);
            this.flowLayoutPanel7.Controls.Add(this.nmb_CaptureList);
            this.flowLayoutPanel7.Controls.Add(this.btn_Capture);
            this.flowLayoutPanel7.Location = new System.Drawing.Point(201, 3);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(91, 62);
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
            this.btn_Capture.Location = new System.Drawing.Point(2, 28);
            this.btn_Capture.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Capture.Name = "btn_Capture";
            this.btn_Capture.Size = new System.Drawing.Size(62, 24);
            this.btn_Capture.TabIndex = 31;
            this.btn_Capture.Text = "Capture";
            this.btn_Capture.UseVisualStyleBackColor = true;
            this.btn_Capture.Click += new System.EventHandler(this.btn_Capture_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel3.Controls.Add(this.dt_Symbol_From);
            this.flowLayoutPanel3.Controls.Add(this.label4);
            this.flowLayoutPanel3.Controls.Add(this.dt_Symbol_To);
            this.flowLayoutPanel3.Controls.Add(this.chk_PassingList);
            this.flowLayoutPanel3.Controls.Add(this.chk_Analysis);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(298, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(237, 62);
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
            // chk_PassingList
            // 
            this.chk_PassingList.AutoSize = true;
            this.chk_PassingList.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_PassingList.Location = new System.Drawing.Point(3, 29);
            this.chk_PassingList.Name = "chk_PassingList";
            this.chk_PassingList.Size = new System.Drawing.Size(88, 17);
            this.chk_PassingList.TabIndex = 36;
            this.chk_PassingList.Text = "Passing List?";
            this.chk_PassingList.UseVisualStyleBackColor = true;
            // 
            // chk_Analysis
            // 
            this.chk_Analysis.AutoSize = true;
            this.chk_Analysis.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Analysis.Location = new System.Drawing.Point(97, 29);
            this.chk_Analysis.Name = "chk_Analysis";
            this.chk_Analysis.Size = new System.Drawing.Size(51, 17);
            this.chk_Analysis.TabIndex = 35;
            this.chk_Analysis.Text = "Chart";
            this.chk_Analysis.UseVisualStyleBackColor = true;
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
            this.flowLayoutPanel6.Controls.Add(this.chk_FixedRange);
            this.flowLayoutPanel6.Controls.Add(this.label1);
            this.flowLayoutPanel6.Controls.Add(this.nmb_2nd_Axix_Y_Min);
            this.flowLayoutPanel6.Controls.Add(this.label3);
            this.flowLayoutPanel6.Controls.Add(this.nmb_2nd_Axix_Y_Max);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(541, 3);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(343, 62);
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
            this.btn_WideAxis.Location = new System.Drawing.Point(275, 2);
            this.btn_WideAxis.Margin = new System.Windows.Forms.Padding(2);
            this.btn_WideAxis.Name = "btn_WideAxis";
            this.btn_WideAxis.Size = new System.Drawing.Size(19, 25);
            this.btn_WideAxis.TabIndex = 29;
            this.btn_WideAxis.Text = "+";
            this.btn_WideAxis.UseVisualStyleBackColor = true;
            this.btn_WideAxis.Click += new System.EventHandler(this.btn_WideAxis_Click);
            // 
            // chk_FixedRange
            // 
            this.chk_FixedRange.AutoSize = true;
            this.chk_FixedRange.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_FixedRange.Location = new System.Drawing.Point(3, 32);
            this.chk_FixedRange.Name = "chk_FixedRange";
            this.chk_FixedRange.Size = new System.Drawing.Size(97, 17);
            this.chk_FixedRange.TabIndex = 36;
            this.chk_FixedRange.Text = "Is Fixed Range";
            this.chk_FixedRange.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(105, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 26);
            this.label1.TabIndex = 37;
            this.label1.Text = "2nd Y Axis (Max):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmb_2nd_Axix_Y_Max
            // 
            this.nmb_2nd_Axix_Y_Max.Location = new System.Drawing.Point(282, 32);
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
            // chk_Simul
            // 
            this.chk_Simul.AutoSize = true;
            this.chk_Simul.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_Simul.Checked = true;
            this.chk_Simul.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Simul.Dock = System.Windows.Forms.DockStyle.Left;
            this.chk_Simul.Location = new System.Drawing.Point(3, 3);
            this.chk_Simul.Name = "chk_Simul";
            this.chk_Simul.Size = new System.Drawing.Size(137, 17);
            this.chk_Simul.TabIndex = 30;
            this.chk_Simul.Text = "Show Simulation Result";
            this.chk_Simul.UseVisualStyleBackColor = true;
            this.chk_Simul.CheckedChanged += new System.EventHandler(this.chk_Simul_CheckedChanged);
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
            this.splitContainer1.Panel1.Controls.Add(this.chart_Main);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_List);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel4);
            this.splitContainer1.Size = new System.Drawing.Size(1384, 729);
            this.splitContainer1.SplitterDistance = 1003;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
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
            this.dgv_List.Location = new System.Drawing.Point(0, 31);
            this.dgv_List.MultiSelect = false;
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.ReadOnly = true;
            this.dgv_List.RowHeadersVisible = false;
            this.dgv_List.Size = new System.Drawing.Size(373, 694);
            this.dgv_List.TabIndex = 0;
            this.dgv_List.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_List_CellMouseClick);
            this.dgv_List.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_List_ColumnHeaderMouseClick);
            this.dgv_List.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_List_DataBindingComplete);
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.chk_Simul);
            this.flowLayoutPanel4.Controls.Add(this.txt_OptionDesc);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(373, 31);
            this.flowLayoutPanel4.TabIndex = 43;
            // 
            // txt_OptionDesc
            // 
            this.txt_OptionDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OptionDesc.Location = new System.Drawing.Point(146, 3);
            this.txt_OptionDesc.Name = "txt_OptionDesc";
            this.txt_OptionDesc.Size = new System.Drawing.Size(205, 20);
            this.txt_OptionDesc.TabIndex = 42;
            // 
            // nmb_2nd_Axix_Y_Min
            // 
            this.nmb_2nd_Axix_Y_Min.Location = new System.Drawing.Point(206, 32);
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
            this.label3.Location = new System.Drawing.Point(257, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 26);
            this.label3.TabIndex = 40;
            this.label3.Text = "~";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Index_Analyzer_KeyDown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmb_CaptureList)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_Y_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Max)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_List)).EndInit();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_2nd_Axix_Y_Min)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Index;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_P1;
        private System.Windows.Forms.Button btn_N1;
        public System.Windows.Forms.DateTimePicker dt_From;
        public System.Windows.Forms.DateTimePicker dt_To;
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
        private System.Windows.Forms.CheckBox chk_Analysis;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        public System.Windows.Forms.CheckBox chk_PassingList;
        public System.Windows.Forms.CheckBox chk_Simul;
        private System.Windows.Forms.TextBox txt_OptionDesc;
        private System.Windows.Forms.ComboBox cmb_Type;
        private System.Windows.Forms.CheckBox chk_FixedRange;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmb_2nd_Axix_Y_Max;
        private System.Windows.Forms.NumericUpDown nmb_2nd_Axix_Y_Min;
        private System.Windows.Forms.Label label3;
    }
}