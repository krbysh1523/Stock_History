
namespace History
{
    partial class ResultUI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ResultID = new System.Windows.Forms.Label();
            this.txt_DESC = new System.Windows.Forms.RichTextBox();
            this.lbl_Symbol = new System.Windows.Forms.Label();
            this.dgv_Buttons = new System.Windows.Forms.DataGridView();
            this.stockDataSet = new History.stockDataSet();
            this.resultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.resultTableAdapter = new History.stockDataSetTableAdapters.resultTableAdapter();
            this.resultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Buttons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_ResultID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Symbol, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_DESC, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgv_Buttons, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 224);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lbl_ResultID
            // 
            this.lbl_ResultID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ResultID.Location = new System.Drawing.Point(166, 0);
            this.lbl_ResultID.Name = "lbl_ResultID";
            this.lbl_ResultID.Size = new System.Drawing.Size(157, 30);
            this.lbl_ResultID.TabIndex = 4;
            this.lbl_ResultID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_DESC
            // 
            this.txt_DESC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_DESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DESC.Location = new System.Drawing.Point(166, 33);
            this.txt_DESC.Name = "txt_DESC";
            this.txt_DESC.Size = new System.Drawing.Size(157, 188);
            this.txt_DESC.TabIndex = 0;
            this.txt_DESC.Text = "";
            this.txt_DESC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_DESC_KeyUp);
            // 
            // lbl_Symbol
            // 
            this.lbl_Symbol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Symbol.Location = new System.Drawing.Point(3, 0);
            this.lbl_Symbol.Name = "lbl_Symbol";
            this.lbl_Symbol.Size = new System.Drawing.Size(157, 30);
            this.lbl_Symbol.TabIndex = 3;
            this.lbl_Symbol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgv_Buttons
            // 
            this.dgv_Buttons.AllowUserToAddRows = false;
            this.dgv_Buttons.AllowUserToDeleteRows = false;
            this.dgv_Buttons.AutoGenerateColumns = false;
            this.dgv_Buttons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Buttons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.resultDataGridViewTextBoxColumn});
            this.dgv_Buttons.DataSource = this.resultBindingSource;
            this.dgv_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Buttons.Location = new System.Drawing.Point(3, 33);
            this.dgv_Buttons.Name = "dgv_Buttons";
            this.dgv_Buttons.ReadOnly = true;
            this.dgv_Buttons.RowHeadersVisible = false;
            this.dgv_Buttons.Size = new System.Drawing.Size(157, 188);
            this.dgv_Buttons.TabIndex = 5;
            this.dgv_Buttons.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Buttons_CellContentClick);
            // 
            // stockDataSet
            // 
            this.stockDataSet.DataSetName = "stockDataSet";
            this.stockDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // resultBindingSource
            // 
            this.resultBindingSource.DataMember = "result";
            this.resultBindingSource.DataSource = this.stockDataSet;
            // 
            // resultTableAdapter
            // 
            this.resultTableAdapter.ClearBeforeFill = true;
            // 
            // resultDataGridViewTextBoxColumn
            // 
            this.resultDataGridViewTextBoxColumn.DataPropertyName = "result";
            this.resultDataGridViewTextBoxColumn.HeaderText = "Result";
            this.resultDataGridViewTextBoxColumn.Name = "resultDataGridViewTextBoxColumn";
            this.resultDataGridViewTextBoxColumn.ReadOnly = true;
            this.resultDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ResultUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 224);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResultUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Result";
            this.Load += new System.EventHandler(this.ResultUI_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ResultUI_KeyUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Buttons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label lbl_ResultID;
        public System.Windows.Forms.Label lbl_Symbol;
        public System.Windows.Forms.RichTextBox txt_DESC;
        private System.Windows.Forms.DataGridView dgv_Buttons;
        private stockDataSet stockDataSet;
        private System.Windows.Forms.BindingSource resultBindingSource;
        private stockDataSetTableAdapters.resultTableAdapter resultTableAdapter;
        private System.Windows.Forms.DataGridViewButtonColumn resultDataGridViewTextBoxColumn;
    }
}