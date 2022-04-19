using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace History
{
    public partial class MessageUI : Form
    {
        #region + Form
        Query query = null;

        public MessageUI()
        {
            InitializeComponent();
        }

        public MessageUI(Query _query, Point _location)
        {
            query = _query;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = _location;
        }
        private void MessageUI_Load(object sender, EventArgs e)
        {
            Load_History();
        }

        private void MessageUI_SizeChanged(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = 360;
        }
        #endregion

        #region + Message
        public void show_Message(string _log)
        {
            if (txt_Log.InvokeRequired)
            {
                txt_Log.BeginInvoke(new MethodInvoker(delegate () { show_Message(_log); }));
            }
            else
            {
                if (txt_Log.Lines.Length > 100)
                {
                    txt_Log.Text = string.Empty;
                }
                txt_Log.Text = DateTime.Now.ToString("hh:mm:ss") + "-" + _log + "\r\n" + txt_Log.Text;
            }
        }
        #endregion

        #region + History
        public void Load_History()
        {
            if (dgv_History.InvokeRequired)
            {
                dgv_History.BeginInvoke(new MethodInvoker(delegate () { Load_History(); }));
            }
            else
            {
                this.list_historyTableAdapter.Fill(this.stockDataSet.list_history);
            }
        }
        private void dgv_History_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.ColumnIndex == 0)
            {
                query.RetrieveHistoryList(dgv_History[0, e.RowIndex].Value.ToString(),
                    dgv_History["sTARTDTTMDataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                    dgv_History["eNDDTTMDataGridViewTextBoxColumn", e.RowIndex].Value.ToString());
            }
            if (e.RowIndex >= 0 && e.ColumnIndex == dgv_History.Columns["option_desc"].Index)
            {
                show_Message(dgv_History[e.ColumnIndex, e.RowIndex].ToolTipText);
            }
        }
        #endregion

        #region + GridView
        private void dgv_History_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int r = 0; r < dgv_History.Rows.Count - 1; r++)
            {
                if(dgv_History.Rows[r].Cells[4].Value != DBNull.Value &&
                    dgv_History.Rows[r+1].Cells[4].Value != DBNull.Value)
                for (int c = 4; c < 10; c++)
                {
                    double pre = Convert.ToDouble(dgv_History.Rows[r+1].Cells[c].Value.ToString());
                    double cur = Convert.ToDouble(dgv_History.Rows[r].Cells[c].Value.ToString());
                    if(cur >= 70)
                        dgv_History.Rows[r].Cells[c].Style.BackColor = Color.SkyBlue;
                    else if (cur > pre)
                        dgv_History.Rows[r].Cells[c].Style.BackColor = Color.GreenYellow;
                    else
                        dgv_History.Rows[r].Cells[c].Style.BackColor = Color.OrangeRed;
                }
            }
        }
        #endregion

        private void dgv_History_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == dgv_History.Columns["option_desc"].Index && e.Value != null)
            {
                dgv_History[e.ColumnIndex, e.RowIndex].ToolTipText = e.Value.ToString();
            }
        }
    }
}
