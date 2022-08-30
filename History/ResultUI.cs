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
    public partial class ResultUI : Form
    {
        #region + Form
        public string pub_result;
        public ResultUI()
        {
            InitializeComponent();
        }

        private void ResultUI_Load(object sender, EventArgs e)
        {
            this.resultTableAdapter.Fill(this.stockDataSet.result);

        }
        #endregion

        #region + Button
        private void dgv_Buttons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ResultID = Convert.ToInt32(lbl_ResultID.Text);
            using (stockEntity stock = new stockEntity())
            {
                var result = stock.prediction_history.FirstOrDefault(r => r.result_id == ResultID);
                pub_result = dgv_Buttons[0, e.RowIndex].Value.ToString();
                result.result = pub_result;
                result.result_desc = txt_DESC.Text;

                stock.SaveChanges();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        #region + User Event
        private void txt_DESC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        private void ResultUI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        #endregion

    }
}
