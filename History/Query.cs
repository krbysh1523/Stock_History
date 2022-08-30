using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace History
{
    public partial class Query : Form
    {
        #region + Forms
        Analysis chart = null;
        GetData import = null;
        Index_Analyzer index = null;
        MessageUI msg = null;

        public Query()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1390, 0);
        }

        private void Query_Load(object sender, EventArgs e)
        {
            this.test_planTableAdapter.Fill(this.stockDataSet.test_plan);
            bindLookup.Filter = "l_type like '%Option%'";
            this.lookupTableAdapter.Fill(this.stockDataSet.lookup);
            AddGVButtons();

            btn_Index_Click(this, null);
            btn_Message_Click(this, null);

            btn_LayoutRefresh_Click(this, null);
        }

        private void AddGVButtons()
        {
            CreateUnboundButtonColumn(dgv_Lookup, "Update", "Update", 0, 50);
            CreateUnboundButtonColumn(dgv_Lookup, "Sim", "Sim", 0, 35);
            CreateUnboundButtonColumn(dgv_Lookup, "Init", "Init", 0, 35);

        }

        private void Query_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void CloseChild(Form _form)
        {
            if(_form.Name == "chart")
                chart = null;
            if (_form.Name == "index")
                index = null;
            if (_form.Name == "import")
                import = null;
            if (_form.Name == "msg")
                msg = null;
        }

        private void CreateUnboundButtonColumn(DataGridView _dgv, string _header, string _text, int _insert, int _width)
        {
            // Initialize the button column.
            DataGridViewButtonColumn buttonColumn =
                new DataGridViewButtonColumn();
            buttonColumn.Name = "col_" + _header;
            buttonColumn.HeaderText = _header;
            buttonColumn.Text = _text;
            buttonColumn.Width = _width;

            // Use the Text property for the button text for all cells rather
            // than using each cell's value as the text for its own button.
            buttonColumn.UseColumnTextForButtonValue = true;

            // Add the button column to the control.
            _dgv.Columns.Insert(_insert, buttonColumn);
        }
        #endregion

        #region + Button
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.lookupTableAdapter.Fill(this.stockDataSet.lookup);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            this.lookupTableAdapter.Update(this.stockDataSet.lookup);
        }

        private void btn_Chart_Click(object sender, EventArgs e)
        {
            if (chart == null)
            {
                chart = new Analysis(this, new Point(800, 300));
                chart.Name = "chart";
                chart.Show();
            }
            else
            {
                chart.BringToFront();
            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            if (import == null)
            {
                import = new GetData(this);
                import.Name = "import";
                import.Show();
            }
            else
            {
                chart.BringToFront();
            }
        }
        private void btn_Index_Click(object sender, EventArgs e)
        {
            if (index == null)
            {
                index = new Index_Analyzer(this, new Point(0, 0));
                index.Name = "index";
                index.Show();
            }
            else
            {
                index.BringToFront();
            }
        }
        private void btn_Message_Click(object sender, EventArgs e)
        {
            if (msg == null)
            {
                msg = new MessageUI(this, new Point(1390, 650));
                msg.Name = "msg";
                msg.Show();
            }
            else
            {
                msg.BringToFront();
            }
        }
        #endregion

        #region + User Event
        public void SendAnalysis(string _symbol, DateTime _dt_from, DateTime _dt_to, DateTime _dt_from_highlight, DateTime _dt_to_highlight)
        {
            if (chart == null)
                btn_Chart_Click(this, null);

            chart.txt_Symbol.Text = _symbol;
            chart.dt_From.Value = _dt_from;
            chart.dt_To.Value = _dt_to;
            chart.dt_From_Highlight.Value = _dt_from_highlight;
            chart.dt_To_Highlight.Value = _dt_to_highlight;
            chart.btn_Symbol_Click(this, null);
        }

        public void Save_Analysis(string _dir, string _file_name)
        {
            chart.Save_Screenshot(_dir, _file_name);
        }

        private void dgv_List_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    if (chart != null & senderGrid[0, e.RowIndex].Value.ToString() != null)
                    {
                        chart.txt_Symbol.Text = senderGrid[0, e.RowIndex].Value.ToString();
                        chart.btn_Symbol_Click(chart, null);
                    }
                }
                else if(e.ColumnIndex == 1)
                {
                    if (chart != null)
                    {
                        chart.txt_Symbol.Text = senderGrid[0, e.RowIndex].Value.ToString();
                        chart.btn_Symbol_Click(chart, null);
                        chart.btn_Next_Click(chart, null);
                    }
                }
            }
        }

        private void dgv_Saved_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
            }
        }

        private void dgv_TestPlan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                if(dgv_TestPlan[e.ColumnIndex, e.RowIndex].Value.ToString().IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
                {
                    MessageBox.Show(this, "Contain invalid Character", "Error");
                    dgv_TestPlan[e.ColumnIndex, e.RowIndex].Value = "Test";
                }
            }
        }
        #endregion

        #region + Test Plan

        private void dgv_Lookup_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.ColumnIndex == 0) // Init
                {
                    index.populate_List(dgv_Lookup["lidDataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        "N",
                        dgv_Lookup["latt1DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt2DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt3DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt4DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt5DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        "Individual"
                        );
                }
                else if (e.ColumnIndex == 1) // Simulate
                {
                    index.populate_List(dgv_Lookup["lidDataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        "Y",
                        dgv_Lookup["latt1DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt2DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt3DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt4DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        dgv_Lookup["latt5DataGridViewTextBoxColumn", e.RowIndex].Value.ToString(),
                        "Individual"
                        );
                }
                else if (e.ColumnIndex == 2) // Update Test Plan
                {
                    if (dgv_Lookup[0, e.RowIndex].Value.ToString() != "")
                    {
                        var row = dgv_TestPlan.SelectedCells[0].RowIndex;
                        dgv_TestPlan[3, row].Value = dgv_Lookup["lidDataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                        dgv_TestPlan[4, row].Value = dgv_Lookup["lnameDataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                        dgv_TestPlan[5, row].Value = dgv_Lookup["latt1DataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                        dgv_TestPlan[6, row].Value = dgv_Lookup["latt2DataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                        dgv_TestPlan[7, row].Value = dgv_Lookup["latt3DataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                        dgv_TestPlan[8, row].Value = dgv_Lookup["latt4DataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                        dgv_TestPlan[9, row].Value = dgv_Lookup["latt5DataGridViewTextBoxColumn", e.RowIndex].Value.ToString();
                    }
                } 
            }
        }
        
        private void btn_Test_Refresh_Click(object sender, EventArgs e)
        {
            this.test_planTableAdapter.Fill(this.stockDataSet.test_plan);
        }
        private void dgv_TestPlan_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv_TestPlan.RowCount <= 1)
                return;

            string prev_test = "";
            Color prev_color = Color.Gainsboro;
            for(int r = 1; r < dgv_TestPlan.RowCount-1; r++)
            {
                if (prev_test == "" ||
                    prev_test != dgv_TestPlan[0, r].Value.ToString())
                {
                    if (prev_color == Color.Gainsboro)
                        prev_color = Color.White;
                    else
                        prev_color = Color.Gainsboro;
                    dgv_TestPlan.Rows[r].DefaultCellStyle.BackColor = prev_color;
                    prev_test = dgv_TestPlan[0, r].Value.ToString();
                }
                else
                {
                    dgv_TestPlan.Rows[r].DefaultCellStyle.BackColor = prev_color;
                }
            }
        }

        private void btn_Test_Save_Click(object sender, EventArgs e)
        {
            this.test_planTableAdapter.Update(this.stockDataSet.test_plan);
            btn_Test_Refresh_Click(this, null);
        }

        private void btn_Test_FillIn_Click(object sender, EventArgs e)
        {
            if (dgv_TestPlan.SelectedCells.Count > 0 && dgv_TestPlan.SelectedCells[0].RowIndex >= 0)
            { 
                if (dgv_TestPlan["test_id", dgv_TestPlan.SelectedCells[0].RowIndex].Value == null)
                {
                    var row = stockDataSet.test_plan.Newtest_planRow();
                    row.plan_name = "Test";
                    row.test_seq = 0;
                    row.pass_list = "Y";
                    row.filter_option = Convert.ToInt32(dgv_Lookup["lidDataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString());
                    row.filter_desc = dgv_Lookup["lnameDataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    row.filter_att1 = dgv_Lookup["latt1DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();                            
                    row.filter_att2 = dgv_Lookup["latt2DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();                            
                    row.filter_att3 = dgv_Lookup["latt3DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();                            
                    row.filter_att4 = dgv_Lookup["latt4DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();                            
                    row.filter_att5 = dgv_Lookup["latt5DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    stockDataSet.test_plan.Addtest_planRow(row);
                }
                else
                {
                    var test_id = dgv_TestPlan["test_id", dgv_TestPlan.SelectedCells[0].RowIndex].Value.ToString();
                    var row = stockDataSet.test_plan.FirstOrDefault(r => r.test_id.ToString() == test_id);
                    row.filter_option = Convert.ToInt32(dgv_Lookup["lidDataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString());
                    row.filter_desc = dgv_Lookup["lnameDataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    row.filter_att1 = dgv_Lookup["latt1DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    row.filter_att2 = dgv_Lookup["latt2DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    row.filter_att3 = dgv_Lookup["latt3DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    row.filter_att4 = dgv_Lookup["latt4DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                    row.filter_att5 = dgv_Lookup["latt5DataGridViewTextBoxColumn", dgv_Lookup.SelectedCells[0].RowIndex].Value.ToString();
                }
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_TestPlan.SelectedCells.Count > 0 && dgv_TestPlan.SelectedCells[0].RowIndex >= 0)
            {
                string Msg = "Delete to " + dgv_TestPlan[1, dgv_TestPlan.SelectedCells[0].RowIndex].Value.ToString() + " of " +
                    dgv_TestPlan[0, dgv_TestPlan.SelectedCells[0].RowIndex].Value.ToString() + "?";
                if (MessageBox.Show(Msg, "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int test_id = Convert.ToInt32(dgv_TestPlan["test_id", dgv_TestPlan.SelectedCells[0].RowIndex].Value.ToString());
                    this.test_planTableAdapter.Delete(test_id);
                    btn_Test_Refresh_Click(this, null);
                }
            }
        }

        Thread thd_Test;
        private void btn_Test_Run_Click(object sender, EventArgs e)
        {
            if (dgv_TestPlan.SelectedCells.Count > 0 && dgv_TestPlan.SelectedCells[0].RowIndex >= 0
                && dgv_TestPlan.SelectedCells[0].Value != null)
            {
                thd_Test = new Thread(new ParameterizedThreadStart(Test_Process));
                thd_Test.Start(new object[] { dgv_TestPlan[0, dgv_TestPlan.SelectedCells[0].RowIndex].Value.ToString(), chk_Capture.Checked });
            }
            else
            {
                Show_Message("Please Select one of Test Plan");
            }
        }

        private void Test_Process(object _param)
        {
            object[] param = (object[])_param;

            Run_Test(_param);
            if (Convert.ToBoolean(param[1]))
            {
                try
                {
                    // Capture Picture for each Symbol for past
                    index.Start_Capture(1, -4, 0);

                    Thread.Sleep(1000);

                    while (index.capture_continue)
                    {
                        Thread.Sleep(500);
                    }

                    // Capture Picture for each Symbol for future
                    index.Start_Capture(2, -1, 3);

                    Thread.Sleep(1000);

                    while (index.capture_continue)
                    {
                        Thread.Sleep(500);
                    }
                }
                catch(Exception ex)
                {
                    Show_Message("Error:" + ex.Message);
                }
            }
        }

        private void Run_Test(object _params)
        {
            object[] param = (object[])_params;
            if (index == null)
                this.btn_Index_Click(this, null);

            index.BringToFront();

            var tests = stockDataSet.test_plan.Where(r => r.plan_name == param[0].ToString()).OrderBy(r => r.test_seq).ToList();

            var count = tests.Count;
            int step = 1;

            foreach(var test in tests)
            {
                string status = test.test_seq + " of " + test.plan_name;
                
                if(step == count)
                    status = "(Final) " + test.test_seq + " of " + test.plan_name;
                Show_Message(status);

                index.populate_List(test.filter_option.ToString(), test.pass_list, test.filter_att1, test.filter_att2, test.filter_att3, test.filter_att4, test.filter_att5, status);

                Thread.Sleep(1000);

                while (index.is_processing)
                {
                    Thread.Sleep(1000);
                    Show_Message("Waiting...");
                }

                //index.CaptureList(status);
                
                Thread.Sleep(500);
                step++;
            }
        }

        private void btn_Test_All_Click(object sender, EventArgs e)
        {
            thd_Test = new Thread(new ParameterizedThreadStart(Test_All));
            thd_Test.Start(new object[] { chk_Capture.Checked });
        }
        private void Test_All(object _params)
        {
            object[] param = (object[])_params;
            var plans = stockDataSet.test_plan.GroupBy(r => r.plan_name).ToList();
            try
            {
                foreach (var plan in plans)
                {
                    // Run Test Plan
                    Run_Test(plan.Key);

                    if (Convert.ToBoolean(param[0]))
                    {
                        // Capture Picture for each Symbol for past
                        index.Start_Capture(1, -4, 0);

                        Thread.Sleep(1000);

                        while (index.capture_continue)
                        {
                            Thread.Sleep(500);
                        }

                        // Capture Picture for each Symbol for future
                        index.Start_Capture(2, -1, 3);

                        Thread.Sleep(1000);

                        while (index.capture_continue)
                        {
                            Thread.Sleep(500);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Show_Message("Error:" + ex.Message);
            }
        }

        private void chk_Capture_CheckedChanged(object sender, EventArgs e)
        {
            if(!chk_Capture.Checked && index.capture_continue)
                index.capture_continue = false;
        }

        #endregion

        #region + Message
        public void Show_Message(string _log)
        {
            if (msg == null)
                btn_Message_Click(this, null);

            msg.show_Message(_log);
        }
        public void Refresh_History()
        {
            if (msg == null)
                btn_Message_Click(this, null);

            msg.Load_History();
        }
        public void RetrieveHistoryList(string _hist_id, string _start_dttm, string _end_dttm)
        {
            if (index == null)
                this.btn_Index_Click(this, null);

            index.GetHistory_List(_hist_id, _start_dttm, _end_dttm);
        }

        public void RetrieveStatHistory(int _hist_count, string _final_only)
        {
            if (index == null)
                this.btn_Index_Click(this, null);

            index.GetStatHistory(_hist_count, _final_only);
        }

        #endregion

        #region + Layout
        private void btn_UpdateLayout_Click(object sender, EventArgs e)
        {
            using(stockEntity stock = new stockEntity())
            {
                var cmb_text = cmb_Layout.Text;
                lookup layout = stock.lookups.FirstOrDefault(r => r.l_type == "Layout" && r.l_name == cmb_text);
                string[] layouts = Save_Layout();
                layout.l_att1 = layouts[0];
                layout.l_att2 = layouts[1];
                layout.l_att3 = layouts[2];

                stock.SaveChanges();
            }
        }
        private string[] Save_Layout()
        {
            List<string> ret = new List<string>();

            ret.Add(index.Location.X.ToString() + "," + index.Location.Y.ToString() + ","
                + index.Size.Width.ToString() + "," + index.Size.Height.ToString());
            ret.Add(this.Location.X.ToString() + "," + this.Location.Y.ToString() + ","
                + this.Size.Width.ToString() + "," + this.Size.Height.ToString());
            ret.Add(msg.Location.X.ToString() + "," + msg.Location.Y.ToString() + ","
                + msg.Size.Width.ToString() + "," + msg.Size.Height.ToString());

            return ret.ToArray();
        }

        private void btn_LayoutRefresh_Click(object sender, EventArgs e)
        {
            cmb_Layout.SelectedIndexChanged -= cmb_Layout_SelectedIndexChanged;
            using(stockEntity stock = new stockEntity())
            {
                var layouts = stock.lookups.Where(r => r.l_type == "Layout").OrderBy(r => r.l_id).ToList();

                cmb_Layout.DataSource = layouts;
                cmb_Layout.DisplayMember = "l_name";
                cmb_Layout.ValueMember = "l_id";

            }
            cmb_Layout.SelectedIndexChanged += cmb_Layout_SelectedIndexChanged;
        }

        private void cmb_Layout_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (stockEntity stock = new stockEntity())
            {
                var cmb_text = cmb_Layout.Text;
                lookup layout = stock.lookups.FirstOrDefault(r => r.l_type == "Layout" && r.l_name == cmb_text);
                Set_Layout(layout);
            }
        }

        private void Set_Layout(lookup _layout)
        {
            int[] points = ret_points(_layout.l_att1);
            index.Location = new Point(points[0], points[1]);
            index.Size = new Size(points[2], points[3]);

            points = ret_points(_layout.l_att2); 
            this.Location = new Point(points[0], points[1]);
            this.Size = new Size(points[2], points[3]);

            points = ret_points(_layout.l_att3);
            msg.Location = new Point(points[0], points[1]);
            msg.Size = new Size(points[2], points[3]);

        }

        private int[] ret_points(string _att)
        {
            string[] s = _att.Split(',');
            List<int> points = new List<int>();
            foreach (var p in s)
                points.Add(Convert.ToInt32(p));
            return points.ToArray();
        }
        #endregion

        #region + Picture Tab
        public void LoadPicture(string _filename)
        {
            this.tabControl1.SelectedTab = tab_Picture;

            if(File.Exists(_filename))
            {
                pic_Symbol.Image = Image.FromFile(_filename);
            }
            else
            {
                pic_Symbol.Image = pic_Symbol.ErrorImage;
            }
        }
        #endregion

    }
}
