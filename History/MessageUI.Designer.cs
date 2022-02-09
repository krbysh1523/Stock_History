
namespace History
{
    partial class MessageUI
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
            this.txt_Log = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_History = new System.Windows.Forms.DataGridView();
            this.listhistoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stockDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stockDataSet = new History.stockDataSet();
            this.list_historyTableAdapter = new History.stockDataSetTableAdapters.list_historyTableAdapter();
            this.hISTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fILTEROPTIONDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.option_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratio1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pASSLISTDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hISTDTTMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.att1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.att2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.att3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.att4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.att5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTARTDTTMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eNDDTTMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listhistoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Log
            // 
            this.txt_Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Log.Location = new System.Drawing.Point(0, 0);
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.Size = new System.Drawing.Size(356, 357);
            this.txt_Log.TabIndex = 5;
            this.txt_Log.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_Log);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_History);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 361);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 6;
            // 
            // dgv_History
            // 
            this.dgv_History.AllowUserToAddRows = false;
            this.dgv_History.AllowUserToDeleteRows = false;
            this.dgv_History.AllowUserToResizeRows = false;
            this.dgv_History.AutoGenerateColumns = false;
            this.dgv_History.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_History.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hISTIDDataGridViewTextBoxColumn,
            this.fILTEROPTIONDataGridViewTextBoxColumn,
            this.option_desc,
            this.ratio1,
            this.rate_5,
            this.rate_10,
            this.rate_15,
            this.rate_20,
            this.rate_40,
            this.rate_60,
            this.pASSLISTDataGridViewTextBoxColumn,
            this.hISTDTTMDataGridViewTextBoxColumn,
            this.att1DataGridViewTextBoxColumn,
            this.att2DataGridViewTextBoxColumn,
            this.att3DataGridViewTextBoxColumn,
            this.att4DataGridViewTextBoxColumn,
            this.att5DataGridViewTextBoxColumn,
            this.sTARTDTTMDataGridViewTextBoxColumn,
            this.eNDDTTMDataGridViewTextBoxColumn});
            this.dgv_History.DataSource = this.listhistoryBindingSource;
            this.dgv_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_History.Location = new System.Drawing.Point(0, 0);
            this.dgv_History.Name = "dgv_History";
            this.dgv_History.ReadOnly = true;
            this.dgv_History.RowHeadersVisible = false;
            this.dgv_History.Size = new System.Drawing.Size(636, 357);
            this.dgv_History.TabIndex = 0;
            this.dgv_History.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_History_CellContentClick);
            this.dgv_History.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_History_DataBindingComplete);
            // 
            // listhistoryBindingSource
            // 
            this.listhistoryBindingSource.DataMember = "list_history";
            this.listhistoryBindingSource.DataSource = this.stockDataSetBindingSource;
            // 
            // stockDataSetBindingSource
            // 
            this.stockDataSetBindingSource.DataSource = this.stockDataSet;
            this.stockDataSetBindingSource.Position = 0;
            // 
            // stockDataSet
            // 
            this.stockDataSet.DataSetName = "stockDataSet";
            this.stockDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // list_historyTableAdapter
            // 
            this.list_historyTableAdapter.ClearBeforeFill = true;
            // 
            // hISTIDDataGridViewTextBoxColumn
            // 
            this.hISTIDDataGridViewTextBoxColumn.DataPropertyName = "HIST_ID";
            this.hISTIDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.hISTIDDataGridViewTextBoxColumn.Name = "hISTIDDataGridViewTextBoxColumn";
            this.hISTIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.hISTIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.hISTIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.hISTIDDataGridViewTextBoxColumn.Width = 50;
            // 
            // fILTEROPTIONDataGridViewTextBoxColumn
            // 
            this.fILTEROPTIONDataGridViewTextBoxColumn.DataPropertyName = "FILTER_OPTION";
            this.fILTEROPTIONDataGridViewTextBoxColumn.HeaderText = "Option";
            this.fILTEROPTIONDataGridViewTextBoxColumn.Name = "fILTEROPTIONDataGridViewTextBoxColumn";
            this.fILTEROPTIONDataGridViewTextBoxColumn.ReadOnly = true;
            this.fILTEROPTIONDataGridViewTextBoxColumn.Width = 50;
            // 
            // option_desc
            // 
            this.option_desc.DataPropertyName = "option_desc";
            this.option_desc.HeaderText = "Description";
            this.option_desc.Name = "option_desc";
            this.option_desc.ReadOnly = true;
            this.option_desc.Width = 150;
            // 
            // ratio1
            // 
            this.ratio1.DataPropertyName = "ratio1";
            this.ratio1.HeaderText = "Count";
            this.ratio1.Name = "ratio1";
            this.ratio1.ReadOnly = true;
            this.ratio1.Width = 35;
            // 
            // rate_5
            // 
            this.rate_5.DataPropertyName = "rate_5";
            this.rate_5.HeaderText = "+5";
            this.rate_5.Name = "rate_5";
            this.rate_5.ReadOnly = true;
            this.rate_5.Width = 35;
            // 
            // rate_10
            // 
            this.rate_10.DataPropertyName = "rate_10";
            this.rate_10.HeaderText = "+10";
            this.rate_10.Name = "rate_10";
            this.rate_10.ReadOnly = true;
            this.rate_10.Width = 35;
            // 
            // rate_15
            // 
            this.rate_15.DataPropertyName = "rate_15";
            this.rate_15.HeaderText = "+15";
            this.rate_15.Name = "rate_15";
            this.rate_15.ReadOnly = true;
            this.rate_15.Width = 35;
            // 
            // rate_20
            // 
            this.rate_20.DataPropertyName = "rate_20";
            this.rate_20.HeaderText = "+20";
            this.rate_20.Name = "rate_20";
            this.rate_20.ReadOnly = true;
            this.rate_20.Width = 35;
            // 
            // rate_40
            // 
            this.rate_40.DataPropertyName = "rate_40";
            this.rate_40.HeaderText = "+40";
            this.rate_40.Name = "rate_40";
            this.rate_40.ReadOnly = true;
            this.rate_40.Width = 35;
            // 
            // rate_60
            // 
            this.rate_60.DataPropertyName = "rate_60";
            this.rate_60.HeaderText = "+60";
            this.rate_60.Name = "rate_60";
            this.rate_60.ReadOnly = true;
            this.rate_60.Width = 35;
            // 
            // pASSLISTDataGridViewTextBoxColumn
            // 
            this.pASSLISTDataGridViewTextBoxColumn.DataPropertyName = "PASS_LIST";
            this.pASSLISTDataGridViewTextBoxColumn.FillWeight = 40F;
            this.pASSLISTDataGridViewTextBoxColumn.HeaderText = "Passed";
            this.pASSLISTDataGridViewTextBoxColumn.Name = "pASSLISTDataGridViewTextBoxColumn";
            this.pASSLISTDataGridViewTextBoxColumn.ReadOnly = true;
            this.pASSLISTDataGridViewTextBoxColumn.Width = 50;
            // 
            // hISTDTTMDataGridViewTextBoxColumn
            // 
            this.hISTDTTMDataGridViewTextBoxColumn.DataPropertyName = "HIST_DTTM";
            this.hISTDTTMDataGridViewTextBoxColumn.HeaderText = "Date";
            this.hISTDTTMDataGridViewTextBoxColumn.Name = "hISTDTTMDataGridViewTextBoxColumn";
            this.hISTDTTMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // att1DataGridViewTextBoxColumn
            // 
            this.att1DataGridViewTextBoxColumn.DataPropertyName = "att1";
            this.att1DataGridViewTextBoxColumn.FillWeight = 50F;
            this.att1DataGridViewTextBoxColumn.HeaderText = "att1";
            this.att1DataGridViewTextBoxColumn.Name = "att1DataGridViewTextBoxColumn";
            this.att1DataGridViewTextBoxColumn.ReadOnly = true;
            this.att1DataGridViewTextBoxColumn.Width = 50;
            // 
            // att2DataGridViewTextBoxColumn
            // 
            this.att2DataGridViewTextBoxColumn.DataPropertyName = "att2";
            this.att2DataGridViewTextBoxColumn.FillWeight = 50F;
            this.att2DataGridViewTextBoxColumn.HeaderText = "att2";
            this.att2DataGridViewTextBoxColumn.Name = "att2DataGridViewTextBoxColumn";
            this.att2DataGridViewTextBoxColumn.ReadOnly = true;
            this.att2DataGridViewTextBoxColumn.Width = 50;
            // 
            // att3DataGridViewTextBoxColumn
            // 
            this.att3DataGridViewTextBoxColumn.DataPropertyName = "att3";
            this.att3DataGridViewTextBoxColumn.FillWeight = 50F;
            this.att3DataGridViewTextBoxColumn.HeaderText = "att3";
            this.att3DataGridViewTextBoxColumn.Name = "att3DataGridViewTextBoxColumn";
            this.att3DataGridViewTextBoxColumn.ReadOnly = true;
            this.att3DataGridViewTextBoxColumn.Width = 50;
            // 
            // att4DataGridViewTextBoxColumn
            // 
            this.att4DataGridViewTextBoxColumn.DataPropertyName = "att4";
            this.att4DataGridViewTextBoxColumn.FillWeight = 50F;
            this.att4DataGridViewTextBoxColumn.HeaderText = "att4";
            this.att4DataGridViewTextBoxColumn.Name = "att4DataGridViewTextBoxColumn";
            this.att4DataGridViewTextBoxColumn.ReadOnly = true;
            this.att4DataGridViewTextBoxColumn.Width = 50;
            // 
            // att5DataGridViewTextBoxColumn
            // 
            this.att5DataGridViewTextBoxColumn.DataPropertyName = "att5";
            this.att5DataGridViewTextBoxColumn.FillWeight = 50F;
            this.att5DataGridViewTextBoxColumn.HeaderText = "att5";
            this.att5DataGridViewTextBoxColumn.Name = "att5DataGridViewTextBoxColumn";
            this.att5DataGridViewTextBoxColumn.ReadOnly = true;
            this.att5DataGridViewTextBoxColumn.Width = 50;
            // 
            // sTARTDTTMDataGridViewTextBoxColumn
            // 
            this.sTARTDTTMDataGridViewTextBoxColumn.DataPropertyName = "START_DTTM";
            this.sTARTDTTMDataGridViewTextBoxColumn.HeaderText = "Start";
            this.sTARTDTTMDataGridViewTextBoxColumn.Name = "sTARTDTTMDataGridViewTextBoxColumn";
            this.sTARTDTTMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eNDDTTMDataGridViewTextBoxColumn
            // 
            this.eNDDTTMDataGridViewTextBoxColumn.DataPropertyName = "END_DTTM";
            this.eNDDTTMDataGridViewTextBoxColumn.HeaderText = "End";
            this.eNDDTTMDataGridViewTextBoxColumn.Name = "eNDDTTMDataGridViewTextBoxColumn";
            this.eNDDTTMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MessageUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 361);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MessageUI";
            this.Text = "MessageUI";
            this.Load += new System.EventHandler(this.MessageUI_Load);
            this.SizeChanged += new System.EventHandler(this.MessageUI_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listhistoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txt_Log;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_History;
        private stockDataSet stockDataSet;
        private System.Windows.Forms.BindingSource listhistoryBindingSource;
        private stockDataSetTableAdapters.list_historyTableAdapter list_historyTableAdapter;
        private System.Windows.Forms.BindingSource stockDataSetBindingSource;
        private System.Windows.Forms.DataGridViewButtonColumn hISTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fILTEROPTIONDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn option_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratio1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_5;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_10;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_15;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_20;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_40;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_60;
        private System.Windows.Forms.DataGridViewTextBoxColumn pASSLISTDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hISTDTTMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn att1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn att2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn att3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn att4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn att5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sTARTDTTMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eNDDTTMDataGridViewTextBoxColumn;
    }
}