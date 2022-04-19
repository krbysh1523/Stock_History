
namespace History
{
    partial class Query
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.rd_Chart = new System.Windows.Forms.RadioButton();
            this.rd_List = new System.Windows.Forms.RadioButton();
            this.rd_Full = new System.Windows.Forms.RadioButton();
            this.btn_Chart = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_Index = new System.Windows.Forms.Button();
            this.btn_Message = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.dgv_Lookup = new System.Windows.Forms.DataGridView();
            this.ltypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lorderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latt1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latt2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latt3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latt4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latt5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindLookup = new System.Windows.Forms.BindingSource(this.components);
            this.stockDataSet = new History.stockDataSet();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgv_TestPlan = new System.Windows.Forms.DataGridView();
            this.plannameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testseqDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passlistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filteroptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filter_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filter_att1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filter_att2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filter_att3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filter_att4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filter_att5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.test_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindTest = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Test_Refresh = new System.Windows.Forms.Button();
            this.btn_Test_Save = new System.Windows.Forms.Button();
            this.btn_Test_FillIn = new System.Windows.Forms.Button();
            this.btn_Test_Run = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.lookupTableAdapter = new History.stockDataSetTableAdapters.lookupTableAdapter();
            this.test_planTableAdapter = new History.stockDataSetTableAdapters.test_planTableAdapter();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Lookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TestPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindTest)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.btn_Chart);
            this.flowLayoutPanel1.Controls.Add(this.btn_Import);
            this.flowLayoutPanel1.Controls.Add(this.btn_Index);
            this.flowLayoutPanel1.Controls.Add(this.btn_Message);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1154, 39);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.rd_Chart);
            this.flowLayoutPanel4.Controls.Add(this.rd_List);
            this.flowLayoutPanel4.Controls.Add(this.rd_Full);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(182, 30);
            this.flowLayoutPanel4.TabIndex = 15;
            // 
            // rd_Chart
            // 
            this.rd_Chart.AutoSize = true;
            this.rd_Chart.Location = new System.Drawing.Point(3, 3);
            this.rd_Chart.Name = "rd_Chart";
            this.rd_Chart.Size = new System.Drawing.Size(50, 17);
            this.rd_Chart.TabIndex = 0;
            this.rd_Chart.TabStop = true;
            this.rd_Chart.Text = "Chart";
            this.rd_Chart.UseVisualStyleBackColor = true;
            this.rd_Chart.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // rd_List
            // 
            this.rd_List.AutoSize = true;
            this.rd_List.Location = new System.Drawing.Point(59, 3);
            this.rd_List.Name = "rd_List";
            this.rd_List.Size = new System.Drawing.Size(41, 17);
            this.rd_List.TabIndex = 1;
            this.rd_List.TabStop = true;
            this.rd_List.Text = "List";
            this.rd_List.UseVisualStyleBackColor = true;
            this.rd_List.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // rd_Full
            // 
            this.rd_Full.AutoSize = true;
            this.rd_Full.Location = new System.Drawing.Point(106, 3);
            this.rd_Full.Name = "rd_Full";
            this.rd_Full.Size = new System.Drawing.Size(41, 17);
            this.rd_Full.TabIndex = 2;
            this.rd_Full.TabStop = true;
            this.rd_Full.Text = "Full";
            this.rd_Full.UseVisualStyleBackColor = true;
            this.rd_Full.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // btn_Chart
            // 
            this.btn_Chart.Location = new System.Drawing.Point(191, 3);
            this.btn_Chart.Name = "btn_Chart";
            this.btn_Chart.Size = new System.Drawing.Size(61, 23);
            this.btn_Chart.TabIndex = 2;
            this.btn_Chart.Text = "Chart";
            this.btn_Chart.UseVisualStyleBackColor = true;
            this.btn_Chart.Click += new System.EventHandler(this.btn_Chart_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(258, 3);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(60, 23);
            this.btn_Import.TabIndex = 3;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_Index
            // 
            this.btn_Index.Location = new System.Drawing.Point(324, 3);
            this.btn_Index.Name = "btn_Index";
            this.btn_Index.Size = new System.Drawing.Size(87, 23);
            this.btn_Index.TabIndex = 12;
            this.btn_Index.Text = "Index Analysis";
            this.btn_Index.UseVisualStyleBackColor = true;
            this.btn_Index.Click += new System.EventHandler(this.btn_Index_Click);
            // 
            // btn_Message
            // 
            this.btn_Message.Location = new System.Drawing.Point(417, 3);
            this.btn_Message.Name = "btn_Message";
            this.btn_Message.Size = new System.Drawing.Size(66, 23);
            this.btn_Message.TabIndex = 13;
            this.btn_Message.Text = "Message";
            this.btn_Message.UseVisualStyleBackColor = true;
            this.btn_Message.Click += new System.EventHandler(this.btn_Message_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(3, 3);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(85, 23);
            this.btn_Refresh.TabIndex = 13;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(3, 32);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(85, 23);
            this.btn_Save.TabIndex = 14;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // dgv_Lookup
            // 
            this.dgv_Lookup.AutoGenerateColumns = false;
            this.dgv_Lookup.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Lookup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Lookup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ltypeDataGridViewTextBoxColumn,
            this.lidDataGridViewTextBoxColumn,
            this.lnameDataGridViewTextBoxColumn,
            this.lorderDataGridViewTextBoxColumn,
            this.latt1DataGridViewTextBoxColumn,
            this.latt2DataGridViewTextBoxColumn,
            this.latt3DataGridViewTextBoxColumn,
            this.latt4DataGridViewTextBoxColumn,
            this.latt5DataGridViewTextBoxColumn});
            this.dgv_Lookup.DataSource = this.bindLookup;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Lookup.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Lookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Lookup.Location = new System.Drawing.Point(0, 0);
            this.dgv_Lookup.Name = "dgv_Lookup";
            this.dgv_Lookup.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Lookup.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Lookup.Size = new System.Drawing.Size(1032, 309);
            this.dgv_Lookup.TabIndex = 1;
            this.dgv_Lookup.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Lookup_CellMouseClick);
            // 
            // ltypeDataGridViewTextBoxColumn
            // 
            this.ltypeDataGridViewTextBoxColumn.DataPropertyName = "l_type";
            this.ltypeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.ltypeDataGridViewTextBoxColumn.Name = "ltypeDataGridViewTextBoxColumn";
            this.ltypeDataGridViewTextBoxColumn.Width = 50;
            // 
            // lidDataGridViewTextBoxColumn
            // 
            this.lidDataGridViewTextBoxColumn.DataPropertyName = "l_id";
            this.lidDataGridViewTextBoxColumn.HeaderText = "ID";
            this.lidDataGridViewTextBoxColumn.Name = "lidDataGridViewTextBoxColumn";
            this.lidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lidDataGridViewTextBoxColumn.Width = 60;
            // 
            // lnameDataGridViewTextBoxColumn
            // 
            this.lnameDataGridViewTextBoxColumn.DataPropertyName = "l_name";
            this.lnameDataGridViewTextBoxColumn.HeaderText = "Description";
            this.lnameDataGridViewTextBoxColumn.Name = "lnameDataGridViewTextBoxColumn";
            this.lnameDataGridViewTextBoxColumn.Width = 400;
            // 
            // lorderDataGridViewTextBoxColumn
            // 
            this.lorderDataGridViewTextBoxColumn.DataPropertyName = "l_order";
            this.lorderDataGridViewTextBoxColumn.HeaderText = "l_order";
            this.lorderDataGridViewTextBoxColumn.Name = "lorderDataGridViewTextBoxColumn";
            this.lorderDataGridViewTextBoxColumn.Visible = false;
            // 
            // latt1DataGridViewTextBoxColumn
            // 
            this.latt1DataGridViewTextBoxColumn.DataPropertyName = "l_att1";
            this.latt1DataGridViewTextBoxColumn.HeaderText = "Att1";
            this.latt1DataGridViewTextBoxColumn.Name = "latt1DataGridViewTextBoxColumn";
            this.latt1DataGridViewTextBoxColumn.Width = 50;
            // 
            // latt2DataGridViewTextBoxColumn
            // 
            this.latt2DataGridViewTextBoxColumn.DataPropertyName = "l_att2";
            this.latt2DataGridViewTextBoxColumn.HeaderText = "Att2";
            this.latt2DataGridViewTextBoxColumn.Name = "latt2DataGridViewTextBoxColumn";
            this.latt2DataGridViewTextBoxColumn.Width = 50;
            // 
            // latt3DataGridViewTextBoxColumn
            // 
            this.latt3DataGridViewTextBoxColumn.DataPropertyName = "l_att3";
            this.latt3DataGridViewTextBoxColumn.HeaderText = "Att3";
            this.latt3DataGridViewTextBoxColumn.Name = "latt3DataGridViewTextBoxColumn";
            this.latt3DataGridViewTextBoxColumn.Width = 50;
            // 
            // latt4DataGridViewTextBoxColumn
            // 
            this.latt4DataGridViewTextBoxColumn.DataPropertyName = "l_att4";
            this.latt4DataGridViewTextBoxColumn.HeaderText = "Att4";
            this.latt4DataGridViewTextBoxColumn.Name = "latt4DataGridViewTextBoxColumn";
            this.latt4DataGridViewTextBoxColumn.Width = 50;
            // 
            // latt5DataGridViewTextBoxColumn
            // 
            this.latt5DataGridViewTextBoxColumn.DataPropertyName = "l_att5";
            this.latt5DataGridViewTextBoxColumn.HeaderText = "Att5";
            this.latt5DataGridViewTextBoxColumn.Name = "latt5DataGridViewTextBoxColumn";
            this.latt5DataGridViewTextBoxColumn.Width = 50;
            // 
            // bindLookup
            // 
            this.bindLookup.DataMember = "lookup";
            this.bindLookup.DataSource = this.stockDataSet;
            // 
            // stockDataSet
            // 
            this.stockDataSet.DataSetName = "stockDataSet";
            this.stockDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_Lookup);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_TestPlan);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1154, 608);
            this.splitContainer1.SplitterDistance = 313;
            this.splitContainer1.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btn_Refresh);
            this.flowLayoutPanel3.Controls.Add(this.btn_Save);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(1032, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(118, 309);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // dgv_TestPlan
            // 
            this.dgv_TestPlan.AutoGenerateColumns = false;
            this.dgv_TestPlan.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgv_TestPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TestPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plannameDataGridViewTextBoxColumn,
            this.testseqDataGridViewTextBoxColumn,
            this.passlistDataGridViewTextBoxColumn,
            this.filteroptionDataGridViewTextBoxColumn,
            this.filter_desc,
            this.filter_att1,
            this.filter_att2,
            this.filter_att3,
            this.filter_att4,
            this.filter_att5,
            this.remark,
            this.test_id});
            this.dgv_TestPlan.DataSource = this.bindTest;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Gulim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_TestPlan.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_TestPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_TestPlan.Location = new System.Drawing.Point(0, 0);
            this.dgv_TestPlan.Name = "dgv_TestPlan";
            this.dgv_TestPlan.RowHeadersVisible = false;
            this.dgv_TestPlan.Size = new System.Drawing.Size(1032, 287);
            this.dgv_TestPlan.TabIndex = 0;
            this.dgv_TestPlan.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_TestPlan_CellValueChanged);
            this.dgv_TestPlan.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_TestPlan_DataBindingComplete);
            // 
            // plannameDataGridViewTextBoxColumn
            // 
            this.plannameDataGridViewTextBoxColumn.DataPropertyName = "plan_name";
            this.plannameDataGridViewTextBoxColumn.HeaderText = "Plan Name";
            this.plannameDataGridViewTextBoxColumn.Name = "plannameDataGridViewTextBoxColumn";
            // 
            // testseqDataGridViewTextBoxColumn
            // 
            this.testseqDataGridViewTextBoxColumn.DataPropertyName = "test_seq";
            this.testseqDataGridViewTextBoxColumn.HeaderText = "Order";
            this.testseqDataGridViewTextBoxColumn.Name = "testseqDataGridViewTextBoxColumn";
            this.testseqDataGridViewTextBoxColumn.Width = 50;
            // 
            // passlistDataGridViewTextBoxColumn
            // 
            this.passlistDataGridViewTextBoxColumn.DataPropertyName = "pass_list";
            this.passlistDataGridViewTextBoxColumn.HeaderText = "Pass List";
            this.passlistDataGridViewTextBoxColumn.Name = "passlistDataGridViewTextBoxColumn";
            this.passlistDataGridViewTextBoxColumn.Width = 50;
            // 
            // filteroptionDataGridViewTextBoxColumn
            // 
            this.filteroptionDataGridViewTextBoxColumn.DataPropertyName = "filter_option";
            this.filteroptionDataGridViewTextBoxColumn.HeaderText = "Filter Option";
            this.filteroptionDataGridViewTextBoxColumn.Name = "filteroptionDataGridViewTextBoxColumn";
            this.filteroptionDataGridViewTextBoxColumn.Width = 50;
            // 
            // filter_desc
            // 
            this.filter_desc.DataPropertyName = "filter_desc";
            this.filter_desc.HeaderText = "Description";
            this.filter_desc.Name = "filter_desc";
            this.filter_desc.Width = 250;
            // 
            // filter_att1
            // 
            this.filter_att1.DataPropertyName = "filter_att1";
            this.filter_att1.HeaderText = "Att1";
            this.filter_att1.Name = "filter_att1";
            this.filter_att1.Width = 50;
            // 
            // filter_att2
            // 
            this.filter_att2.DataPropertyName = "filter_att2";
            this.filter_att2.HeaderText = "Att2";
            this.filter_att2.Name = "filter_att2";
            this.filter_att2.Width = 50;
            // 
            // filter_att3
            // 
            this.filter_att3.DataPropertyName = "filter_att3";
            this.filter_att3.HeaderText = "Att3";
            this.filter_att3.Name = "filter_att3";
            this.filter_att3.Width = 50;
            // 
            // filter_att4
            // 
            this.filter_att4.DataPropertyName = "filter_att4";
            this.filter_att4.HeaderText = "Att4";
            this.filter_att4.Name = "filter_att4";
            this.filter_att4.Width = 50;
            // 
            // filter_att5
            // 
            this.filter_att5.DataPropertyName = "filter_att5";
            this.filter_att5.HeaderText = "Att5";
            this.filter_att5.Name = "filter_att5";
            this.filter_att5.Width = 50;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "Remark";
            this.remark.Name = "remark";
            this.remark.Width = 250;
            // 
            // test_id
            // 
            this.test_id.DataPropertyName = "test_id";
            this.test_id.HeaderText = "test_id";
            this.test_id.Name = "test_id";
            this.test_id.ReadOnly = true;
            this.test_id.Visible = false;
            // 
            // bindTest
            // 
            this.bindTest.DataMember = "test_plan";
            this.bindTest.DataSource = this.stockDataSet;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btn_Test_Refresh);
            this.flowLayoutPanel2.Controls.Add(this.btn_Test_Save);
            this.flowLayoutPanel2.Controls.Add(this.btn_Test_FillIn);
            this.flowLayoutPanel2.Controls.Add(this.btn_Test_Run);
            this.flowLayoutPanel2.Controls.Add(this.btn_Delete);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1032, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(118, 287);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btn_Test_Refresh
            // 
            this.btn_Test_Refresh.Location = new System.Drawing.Point(3, 3);
            this.btn_Test_Refresh.Name = "btn_Test_Refresh";
            this.btn_Test_Refresh.Size = new System.Drawing.Size(85, 23);
            this.btn_Test_Refresh.TabIndex = 15;
            this.btn_Test_Refresh.Text = "Refresh";
            this.btn_Test_Refresh.UseVisualStyleBackColor = true;
            this.btn_Test_Refresh.Click += new System.EventHandler(this.btn_Test_Refresh_Click);
            // 
            // btn_Test_Save
            // 
            this.btn_Test_Save.Location = new System.Drawing.Point(3, 32);
            this.btn_Test_Save.Name = "btn_Test_Save";
            this.btn_Test_Save.Size = new System.Drawing.Size(85, 23);
            this.btn_Test_Save.TabIndex = 16;
            this.btn_Test_Save.Text = "Save List";
            this.btn_Test_Save.UseVisualStyleBackColor = true;
            this.btn_Test_Save.Click += new System.EventHandler(this.btn_Test_Save_Click);
            // 
            // btn_Test_FillIn
            // 
            this.btn_Test_FillIn.Location = new System.Drawing.Point(3, 61);
            this.btn_Test_FillIn.Name = "btn_Test_FillIn";
            this.btn_Test_FillIn.Size = new System.Drawing.Size(85, 23);
            this.btn_Test_FillIn.TabIndex = 17;
            this.btn_Test_FillIn.Text = "Update/Add from List";
            this.btn_Test_FillIn.UseVisualStyleBackColor = true;
            this.btn_Test_FillIn.Click += new System.EventHandler(this.btn_Test_FillIn_Click);
            // 
            // btn_Test_Run
            // 
            this.btn_Test_Run.Location = new System.Drawing.Point(3, 90);
            this.btn_Test_Run.Name = "btn_Test_Run";
            this.btn_Test_Run.Size = new System.Drawing.Size(85, 23);
            this.btn_Test_Run.TabIndex = 18;
            this.btn_Test_Run.Text = "Run Test";
            this.btn_Test_Run.UseVisualStyleBackColor = true;
            this.btn_Test_Run.Click += new System.EventHandler(this.btn_Test_Run_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(3, 119);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(85, 23);
            this.btn_Delete.TabIndex = 19;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // lookupTableAdapter
            // 
            this.lookupTableAdapter.ClearBeforeFill = true;
            // 
            // test_planTableAdapter
            // 
            this.test_planTableAdapter.ClearBeforeFill = true;
            // 
            // Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 647);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "Query";
            this.Text = "Query";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Query_FormClosing);
            this.Load += new System.EventHandler(this.Query_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Lookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_TestPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindTest)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_Chart;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.DataGridViewButtonColumn symbolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datefromDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datetoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarkDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btn_Index;
        private System.Windows.Forms.DataGridView dgv_Lookup;
        private System.Windows.Forms.BindingSource bindLookup;
        private stockDataSet stockDataSet;
        private stockDataSetTableAdapters.lookupTableAdapter lookupTableAdapter;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.DataGridView dgv_TestPlan;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btn_Test_Refresh;
        private System.Windows.Forms.Button btn_Test_Save;
        private System.Windows.Forms.BindingSource bindTest;
        private stockDataSetTableAdapters.test_planTableAdapter test_planTableAdapter;
        private System.Windows.Forms.Button btn_Test_FillIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filterrankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankoptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ranklimitDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btn_Test_Run;
        private System.Windows.Forms.Button btn_Message;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ltypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lorderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latt1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latt2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latt3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latt4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latt5DataGridViewTextBoxColumn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        public System.Windows.Forms.RadioButton rd_Chart;
        public System.Windows.Forms.RadioButton rd_List;
        public System.Windows.Forms.RadioButton rd_Full;
        private System.Windows.Forms.DataGridViewTextBoxColumn plannameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn testseqDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passlistDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filteroptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filter_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn filter_att1;
        private System.Windows.Forms.DataGridViewTextBoxColumn filter_att2;
        private System.Windows.Forms.DataGridViewTextBoxColumn filter_att3;
        private System.Windows.Forms.DataGridViewTextBoxColumn filter_att4;
        private System.Windows.Forms.DataGridViewTextBoxColumn filter_att5;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn test_id;
    }
}