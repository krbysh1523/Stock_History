using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Drawing;
using System.Drawing.Imaging;

namespace History
{
    public partial class Index_Analyzer : Form
    {
        #region + Forms
        Query query = null;
        List<daily_strong> hists = new List<daily_strong>();
        List<daily_close> hist2 = new List<daily_close>();
        bool is_1st_click_chart = true;
        int click_key_1st = 0;
        Thread thd_capture;
        Thread thd_list;
        bool capture_continue = false;
        public object process = new object();
        public bool is_processing = false;
        string filter_option;
        string filter_att1;
        string filter_att2;
        string filter_att3;
        string filter_att4;
        string filter_att5;
        string Picture_Dir = @"C:\Users\Sukho\OneDrive\투자\Program\Capture";

        public Index_Analyzer(Query _query, Point _location)
        {
            query = _query;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = _location;
        }

        public Index_Analyzer()
        {
            InitializeComponent();
        }

        private void Index_Analyzer_Load(object sender, EventArgs e)
        {
            splitContainer1_SizeChanged(this, null);
            dt_From.Value = DateTime.Today.AddDays(-30);
            dt_To.Value = DateTime.Today.AddDays(-1);
            cmb_Type.SelectedIndex = 0;
            LoadLast();
        }

        private void Index_Analyzer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLast();
            query.CloseChild(this);
        }

        string ConfigFile = "Config_Index_Analyzer.txt";
        private void LoadLast()
        {
            try
            {
                string config = System.Environment.CurrentDirectory + "\\" + ConfigFile;

                string[] lines = System.IO.File.ReadAllLines(config);
                foreach (var line in lines)
                {
                    string[] l = line.Split(',');
                    if (l[0] == "FROM_DTTM")
                        dt_From.Value = Convert.ToDateTime(l[1]);
                    else if (l[0] == "TO_DTTM")
                        dt_To.Value = Convert.ToDateTime(l[1]);
                    else if (l[0] == "FROM_STOCK_DTTM")
                        dt_Symbol_From.Value = Convert.ToDateTime(l[1]);
                    else if (l[0] == "TO_STOCK_DTTM")
                        dt_Symbol_To.Value = Convert.ToDateTime(l[1]);
                }
            }
            catch (Exception)
            { }
        }

        private void SaveLast()
        {
            string config = System.Environment.CurrentDirectory + "\\" + ConfigFile;

            File.WriteAllText(config, String.Empty);

            using (StreamWriter writer = new StreamWriter(config))
            {
                writer.WriteLine("FROM_DTTM," + dt_From.Value.ToString("MM/dd/yyyy"));
                writer.WriteLine("TO_DTTM," + dt_To.Value.ToString("MM/dd/yyyy"));
                writer.WriteLine("FROM_STOCK_DTTM," + dt_Symbol_From.Value.ToString("MM/dd/yyyy"));
                writer.WriteLine("TO_STOCK_DTTM," + dt_Symbol_To.Value.ToString("MM/dd/yyyy"));
            }
        }
        #endregion

        #region + Button Graph
        private void btn_Index_Click(object sender, EventArgs e)
        {
            Monitor.Enter(process);
            is_processing = true;

            Utility.ResetChart(chart_Main);
            DateTime dt_from = dt_From.Value;
            DateTime dt_to = dt_To.Value;
            DateTime dt_compare_start = dt_Symbol_From.Value;
            DateTime dt_compare_end = dt_Symbol_To.Value;

            using (stockEntities stock = new stockEntities())
            {
                if (cmb_Type.SelectedIndex == 0) // Percent
                {
                    hists = stock.daily_strong.Where(r => r.symbol == "AAPL" &&
                    r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                    List<double> chg = new List<double>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        if (hists[i].idx_sma5.HasValue)
                            chg.Add(hists[i].idx_sma5.Value);
                    }
                    chg = Utility.ConverToPerc(chg, 0, 1);

                    chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Tomato, "Index SMA5", "index_sma5"));

                    List<DateTime> dts = new List<DateTime>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        dts.Add(hists[i].date_hist.Date);
                    }

                    Utility.GetXLabels(chart_Main.AxisX, dts, dt_compare_end);

                    // Fixed or Relative Range
                    if (!chk_FixedRange.Checked)
                    {
                        double yMax = 0;
                        double yMin = 0;
                        Utility.RetYAxis(chart_Main, out yMax, out yMin);

                        nmb_Y_Max.Value = (decimal)yMax;
                        nmb_Y_Min.Value = (decimal)yMin;
                        Utility.SetAxis(chart_Main, hists.Count, (double)nmb_Y_Max.Value, (double)nmb_Y_Min.Value);
                    }

                    //Draw Section 
                    var start_idx = hists.FindIndex(r => r.date_hist == dt_compare_start);
                    var end_idx = hists.FindIndex(r => r.date_hist == dt_compare_end);
                    chart_Main.AxisX[0].Sections.Add(AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));

                }
                else if(cmb_Type.SelectedIndex == 1) //SMA
                {
                    hist2 = stock.daily_close.Where(r => r.symbol == "IDX" &&
                    r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                    List<DateTime> dts = new List<DateTime>();
                    for (int i = 0; i < hist2.Count; i++)
                    {
                        dts.Add(hist2[i].date_hist.Date);
                    }
                    Utility.GetXLabels(chart_Main.AxisX, dts, dt_compare_end);

                    chart_Main.Series.Add(Utility.AddLine(hist2, 240));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 120));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 60));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 30));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 20));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 10));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 5));
                    chart_Main.Series.Add(Utility.AddLine(hist2, 1));

                    // Fixed or Relative Range
                    if (!chk_FixedRange.Checked)
                    {
                        double yMax = 0;
                        double yMin = 0;
                        Utility.RetYAxis(chart_Main, out yMax, out yMin);

                        nmb_Y_Max.Value = (decimal)yMax;
                        nmb_Y_Min.Value = (decimal)yMin;
                        Utility.SetAxis(chart_Main, hist2.Count, (double)nmb_Y_Max.Value, (double)nmb_Y_Min.Value);
                    }

                    //Draw Section 
                    var start_idx = hist2.FindIndex(r => r.date_hist == dt_compare_start);
                    var end_idx = hist2.FindIndex(r => r.date_hist == dt_compare_end);
                    chart_Main.AxisX[0].Sections.Add(AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));

                }
            }
            chart_Main.DisableAnimations = true;

            is_processing = false;
            Monitor.Exit(process);

        }

        public void populate_List(string _option, string _passing_list, string _att1, string _att2, string _att3, string _att4, string _att5, string _desc)
        {
            if (chart_Main.InvokeRequired)
            {
                chart_Main.BeginInvoke(new MethodInvoker(delegate () { populate_List(_option, _passing_list, _att1, _att2, _att3, _att4, _att5, _desc); }));
            }
            else
            {

                if (_passing_list == "Y")
                    chk_PassingList.Checked = true;
                else
                    chk_PassingList.Checked = false;

                filter_option = _option;
                filter_att1 = _att1;
                filter_att2 = _att2;
                filter_att3 = _att3;
                filter_att4 = _att4;
                filter_att5 = _att5;
                txt_OptionDesc.Text = _desc;

                btn_Symbol_Click(this, null);
            }
        }
        #endregion

        #region + Button List
        public void btn_Symbol_Click(object sender, EventArgs e)
        {
            List<object> param = new List<object>();
            param.Add(dt_Symbol_From.Value);
            param.Add(dt_Symbol_To.Value);
            param.Add(Utility.RetChecked(chk_PassingList));
            param.Add(GetSymbolList());
            param.Add(filter_option);
            param.Add(filter_att1);
            param.Add(filter_att2);
            param.Add(filter_att3);
            param.Add(filter_att4);
            param.Add(filter_att5);
            param.Add(txt_OptionDesc.Text);

            thd_list = new Thread(new ParameterizedThreadStart(GetList_Proc));
            thd_list.Start(param.ToArray());

        }

        private void GetList_Proc(object _params)
        {
            try
            {
                GetList((object[])_params);
            }
            catch(Exception ex)
            {
                query.Show_Message("Get List Error:" + ex.Message);
                is_processing = false;
            }
        }
        private void GetList(object[] _params)
        {
            if (this.dgv_List.InvokeRequired)
            {
                dgv_List.BeginInvoke(new MethodInvoker(delegate () { GetList(_params); }));
            }
            else
            {
                Monitor.Enter(process);

                is_processing = true;

                query.Show_Message("Option " + _params[4].ToString() + " : Starting");

                dgv_List.DataSource = GetData_List(_params);

                Set_DGV_List();

                SetHeader(_params);

                chart_Main.DisableAnimations = true;

                query.Show_Message("Option " + _params[4].ToString() + " : Completed");
                query.Refresh_History();

                chk_Simul_CheckedChanged(this, null);

                is_processing = false;
                Monitor.Exit(process);
            }
        }

        public void Set_DGV_List()
        {
            dgv_List.Columns[0].Width = 50;
            dgv_List.Columns[1].Width = 40;
            dgv_List.Columns[2].Width = 40;
            dgv_List.Columns[3].Width = 40;
            dgv_List.Columns[4].Width = 40;
            dgv_List.Columns[5].Width = 40;
            dgv_List.Columns[6].Width = 40;
            dgv_List.Columns[7].Width = 40;
            dgv_List.Columns[8].Width = 40;
            dgv_List.Columns[9].Width = 40;

            dgv_List.Columns[0].HeaderText = "Symbol";
            dgv_List.Columns[1].HeaderText = "Rank";
            dgv_List.Columns[2].HeaderText = "R 1";
            dgv_List.Columns[3].HeaderText = "R 2";
            dgv_List.Columns[4].HeaderText = "+5";
            dgv_List.Columns[5].HeaderText = "+10";
            dgv_List.Columns[6].HeaderText = "+15";
            dgv_List.Columns[7].HeaderText = "+20";
            dgv_List.Columns[8].HeaderText = "+40";
            dgv_List.Columns[9].HeaderText = "+60";

            dgv_List.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[6].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[8].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[9].SortMode = DataGridViewColumnSortMode.Automatic;

            dgv_List.Columns[1].Resizable = DataGridViewTriState.True;
            dgv_List.Columns[2].Resizable = DataGridViewTriState.True;
            dgv_List.Columns[3].Resizable = DataGridViewTriState.True;

        }

        public SortableBindingList<filter_main_Result> GetData_List(object[] _params)
        {
            SortableBindingList<filter_main_Result> ranks = new SortableBindingList<filter_main_Result>();
            
            DateTime dt_from = Convert.ToDateTime(_params[0]);
            DateTime dt_to = Convert.ToDateTime(_params[1]);
            string passing_list = Convert.ToString(_params[2]);
            string passing_symbol = Convert.ToString(_params[3]);
            int filter_option = Convert.ToInt32(_params[4]);
            string att1 = Convert.ToString(_params[5]);
            string att2 = Convert.ToString(_params[6]);
            string att3 = Convert.ToString(_params[7]);
            string att4 = Convert.ToString(_params[8]);
            string att5 = Convert.ToString(_params[9]);
            string desc = Convert.ToString(_params[10]);

            query.Show_Message("Option " + filter_option.ToString() + " : Starting");
            using (stockEntities stock = new stockEntities())
            {
                List<filter_main_Result> results = stock.filter_main(dt_from, dt_to, filter_option,
                    passing_list, passing_symbol, att1, att2, att3, att4, att5,
                    desc).ToList();

                foreach (var r in results)
                    ranks.Add(r);
            }
            return ranks;
        }

        public void SetHeader(object[] _params)
        {
            DateTime dt_to = Convert.ToDateTime(_params[1]);

            using (stockEntities stock = new stockEntities())
            {
                List< filter_simul_header_Result> header = stock.filter_simul_header(dt_to).ToList();

                dgv_List.Columns[4].HeaderText = header[0].d5.Value.ToString("M/dd");
                dgv_List.Columns[5].HeaderText = header[0].d10.Value.ToString("M/dd");
                dgv_List.Columns[6].HeaderText = header[0].d15.Value.ToString("M/dd");
                dgv_List.Columns[7].HeaderText = header[0].d20.Value.ToString("M/dd");
                dgv_List.Columns[8].HeaderText = header[0].d40.Value.ToString("M/dd");
                dgv_List.Columns[9].HeaderText = header[0].d60.Value.ToString("M/dd");
            }
        }

            public void GetHistory_List(string _hist_id, string _start_dttm, string _end_dttm)
        {
            if (this.dgv_List.InvokeRequired)
            {
                dgv_List.BeginInvoke(new MethodInvoker(delegate () { GetHistory_List(_hist_id, _start_dttm, _end_dttm); }));
            }
            else
            {
                Monitor.Enter(process);

                is_processing = true;

                dgv_List.DataSource = GetDataHistory_List(_hist_id);

                Set_DGV_List();

                SetHeader(new object[] { "", _end_dttm });

                dt_Symbol_From.Value = Convert.ToDateTime(_start_dttm);
                dt_Symbol_To.Value = Convert.ToDateTime(_end_dttm);

                chart_Main.DisableAnimations = true;

                chk_Simul_CheckedChanged(this, null);

                query.Show_Message("History " + _hist_id.ToString() + " : Completed");

                is_processing = false;
                Monitor.Exit(process);
            }
        }
        public SortableBindingList<sp_get_prediction_history_Result> GetDataHistory_List(string _hist_id)
        {
            SortableBindingList<sp_get_prediction_history_Result> ranks = new SortableBindingList<sp_get_prediction_history_Result>();

            int hist_id = Convert.ToInt32(_hist_id);

            query.Show_Message("History " + _hist_id.ToString() + " : Retrieving");
            using (stockEntities stock = new stockEntities())
            {
                List<sp_get_prediction_history_Result> results = stock.sp_get_prediction_history(hist_id).ToList();

                foreach (var r in results)
                    ranks.Add(r);
            }
            return ranks;
        }
        private LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title, string _name, string _point_shape, double _point_size)
        {
            LineSeries series = new LineSeries();
            series.Name = _name;
            series.Stroke = new System.Windows.Media.SolidColorBrush(_color);
            series.Title = _title;
            if (_point_shape.ToLower() == "circle")
                series.PointGeometry = DefaultGeometries.Circle;
            else if (_point_shape.ToLower() == "diamond")
                series.PointGeometry = DefaultGeometries.Diamond;
            else if (_point_shape.ToLower() == "cross")
                series.PointGeometry = DefaultGeometries.Cross;
            else if (_point_shape.ToLower() == "square")
                series.PointGeometry = DefaultGeometries.Square;
            else
                series.PointGeometry = DefaultGeometries.None;

            series.PointGeometrySize = _point_size;
            ChartValues<double> points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                points.Add(h);
            }
            series.Values = points;

            return series;
        }
        private LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title, string _name)
        {
            return AddLine(_hist, _color, _title, _name, "circle", 10);
        }
        private LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title, string _name, int _AxisY)
        {
            LineSeries line = AddLine(_hist, _color, _title, _name, "circle", 5);
            line.ScalesYAt = _AxisY;
            return line;
        }

        private AxisSection AddSection(double _start, double _width, System.Windows.Media.Color _color, double _opacity)
        {
            AxisSection axis = new AxisSection();

            axis.Value = _start;
            axis.SectionWidth = _width;
            axis.Fill = new System.Windows.Media.SolidColorBrush
            {
                Color = _color,
                Opacity = _opacity
            };

            return axis;
        }

        #endregion

        #region + User Button
        private void btn_P1_Click(object sender, EventArgs e)
        {
            dt_From.Value = dt_From.Value.AddMonths(-1);
            dt_To.Value = dt_To.Value.AddMonths(-1);
        }

        private void btn_N1_Click(object sender, EventArgs e)
        {
            dt_From.Value = dt_From.Value.AddMonths(1);
            dt_To.Value = dt_To.Value.AddMonths(1);
        }

        private void btn_LowerAxis_Click(object sender, EventArgs e)
        {
            nmb_Y_Min.Value = nmb_Y_Min.Value + 5;
            nmb_Y_Max.Value = nmb_Y_Max.Value + 5;

            chart_Main.AxisY[0].MinValue = chart_Main.AxisY[0].MinValue + 5;
            chart_Main.AxisY[0].MaxValue = chart_Main.AxisY[0].MaxValue + 5;
        }

        private void btn_RaiseAxis_Click(object sender, EventArgs e)
        {
            nmb_Y_Min.Value = nmb_Y_Min.Value - 5;
            nmb_Y_Max.Value = nmb_Y_Max.Value - 5;

            chart_Main.AxisY[0].MinValue = chart_Main.AxisY[0].MinValue - 5;
            chart_Main.AxisY[0].MaxValue = chart_Main.AxisY[0].MaxValue - 5;
        }

        private void btn_NarrowAxis_Click(object sender, EventArgs e)
        {
            nmb_Y_Min.Value = nmb_Y_Min.Value - 5;
            nmb_Y_Max.Value = nmb_Y_Max.Value + 5;

            chart_Main.AxisY[0].MinValue = chart_Main.AxisY[0].MinValue - 5;
            chart_Main.AxisY[0].MaxValue = chart_Main.AxisY[0].MaxValue + 5;
        }

        private void btn_WideAxis_Click(object sender, EventArgs e)
        {

            nmb_Y_Min.Value = nmb_Y_Min.Value + 5;
            nmb_Y_Max.Value = nmb_Y_Max.Value - 5;

            chart_Main.AxisY[0].MinValue = chart_Main.AxisY[0].MinValue + 5;
            chart_Main.AxisY[0].MaxValue = chart_Main.AxisY[0].MaxValue - 5;

        }
        #endregion

        #region + User Event
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if(this.Width > 500)
                splitContainer1.SplitterDistance = this.Width - 500;
        }
        private void chart_Main_DataClick(object sender, ChartPoint chartPoint)
        {
            if (is_1st_click_chart)
            {
                dt_Symbol_From.Value = hists[chartPoint.Key].date_hist;
                is_1st_click_chart = false;
                click_key_1st = chartPoint.Key;
            }
            else
            {
                if (dt_Symbol_From.Value < hists[chartPoint.Key].date_hist)
                {
                    dt_Symbol_To.Value = hists[chartPoint.Key].date_hist;
                    is_1st_click_chart = true;
                    double pre = Math.Round(hists[click_key_1st].price.Value, 2);
                    double nex = Math.Round(hists[chartPoint.Key].price.Value, 2);
                    query.Show_Message(string.Format("Price: {0} → {1} Change: {2}%",
                        pre.ToString(),
                        nex.ToString(), 
                        Math.Round((nex - pre) / nex *100, 1).ToString()));
                }
            }
        }
        private void dgv_List_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 4 || e.ColumnIndex > 10)
                return;

            List<filter_main_Result> ranks = (List<filter_main_Result>)dgv_List.DataSource;
            List<filter_main_Result> _ranks = new List<filter_main_Result>();
            if (e.ColumnIndex == 2)
                _ranks = ranks.OrderByDescending(r => r.ratio1).ToList();
            else if (e.ColumnIndex == 3)
                _ranks = ranks.OrderByDescending(r => r.ratio2).ToList();
            else if (e.ColumnIndex == 4)
                _ranks = ranks.OrderByDescending(r => r.rate_5).ToList();
            else if (e.ColumnIndex == 5)
                _ranks = ranks.OrderByDescending(r => r.rate_10).ToList();
            else if (e.ColumnIndex == 6)
                _ranks = ranks.OrderByDescending(r => r.rate_15).ToList();
            else if (e.ColumnIndex == 7)
                _ranks = ranks.OrderByDescending(r => r.rate_20).ToList();
            else if (e.ColumnIndex == 8)
                _ranks = ranks.OrderByDescending(r => r.rate_40).ToList();
            else if (e.ColumnIndex == 9)
                _ranks = ranks.OrderByDescending(r => r.rate_60).ToList();

            dgv_List.DataSource = _ranks;
        }


        private void dgv_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow dr in dgv_List.Rows)
            {
                if (dr.Index > 1)
                {
                    for (int c = 4; c < dgv_List.ColumnCount; c++)
                    {
                        if (Convert.ToDouble(dgv_List[c, dr.Index].Value) < -10)
                            dr.Cells[c].Style.BackColor = Color.Red;
                        else if (Convert.ToDouble(dgv_List[c, dr.Index].Value) >= -10 && Convert.ToDouble(dgv_List[c, dr.Index].Value) < -5)
                            dr.Cells[c].Style.BackColor = Color.Orange;
                        else if (Convert.ToDouble(dgv_List[c, dr.Index].Value) >= -5 && Convert.ToDouble(dgv_List[c, dr.Index].Value) < 0)
                            dr.Cells[c].Style.BackColor = Color.Yellow;
                        else if (Convert.ToDouble(dgv_List[c, dr.Index].Value) >= 5 && Convert.ToDouble(dgv_List[c, dr.Index].Value) < 10)
                            dr.Cells[c].Style.BackColor = Color.YellowGreen;
                        else if (Convert.ToDouble(dgv_List[c, dr.Index].Value) >= 10 && Convert.ToDouble(dgv_List[c, dr.Index].Value) < 20)
                            dr.Cells[c].Style.BackColor = Color.Green;
                        else if (Convert.ToDouble(dgv_List[c, dr.Index].Value) >= 20 && Convert.ToDouble(dgv_List[c, dr.Index].Value) < 30)
                            dr.Cells[c].Style.BackColor = Color.SkyBlue;
                        else if (Convert.ToDouble(dgv_List[c, dr.Index].Value) >= 30)
                            dr.Cells[c].Style.BackColor = Color.Blue;
                        else
                            dr.Cells[c].Style.BackColor = Color.White;
                    }
                }
                else
                {
                    dr.DefaultCellStyle.BackColor = Color.Pink;
                }
            }
        }

        private string GetSymbolList()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                for (int r = 0; r < dgv_List.Rows.Count; r++)
                {
                    sb.Append(dgv_List[0, r].Value.ToString());
                    if (r < dgv_List.Rows.Count - 1)
                        sb.Append(",");
                }
            }
            catch (Exception)
            {
            }
            return sb.ToString();
        }

        private void Index_Analyzer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {
                this.btn_Symbol_Click(this, null);
            }
        }

        private void chk_Simul_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Simul.Checked)
            {
                for (int c = 4; c < dgv_List.ColumnCount; c++)
                    dgv_List.Columns[c].Visible = true;
            }
            else
            {
                for (int c = 4; c < dgv_List.ColumnCount; c++)
                    dgv_List.Columns[c].Visible = false;
            }
        }

        #endregion

        #region + Draw Symbol Graph
        private void dgv_List_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > 1)
            {
                Monitor.Enter(process);
                DrawSymbolChart(e.RowIndex);
                Monitor.Exit(process);
            }
        }
        private void DrawSymbolChart(int _rowIndex)
        {
            if (this.chart_Main.InvokeRequired)
            {
                chart_Main.BeginInvoke(new MethodInvoker(delegate () { DrawSymbolChart(_rowIndex); }));
            }
            else
            {
                try
                {
                    Utility.ResetChart(chart_Main);
                    DateTime dt_from = dt_From.Value;
                    DateTime dt_to = dt_To.Value;
                    DateTime dt_compare_start = dt_Symbol_From.Value;
                    DateTime dt_compare_end = dt_Symbol_To.Value;
                    string symbol = dgv_List[0, _rowIndex].Value.ToString();

                    //If Date is outside of Range
                    if(dt_from > dt_compare_start || dt_to < dt_compare_end)
                    {
                        MessageBox.Show("Date is not within Range to display Chart!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    query.Show_Message(symbol + ": " + dt_compare_start.ToString("MM/dd") + "~" + dt_compare_end.ToString("MM/dd"));

                    // Send to Analysis Page
                    if (chk_Analysis.Checked)
                        query.SendAnalysis(symbol, dt_from, dt_to, dt_compare_start, dt_compare_end);

                    if(query.rd_Chart.Checked || query.rd_Full.Checked)
                    {
                        using (stockEntities stock = new stockEntities())
                        {
                            hists = stock.daily_strong.Where(r => r.symbol == symbol &&
                            r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                            var start_idx = hists.FindIndex(r => r.date_hist == dt_compare_start);
                            var end_idx = hists.FindIndex(r => r.date_hist == dt_compare_end);

                            List<double> chg = new List<double>();
                            daily_strong c = hists.FirstOrDefault(r => r.date_hist == dt_compare_start);
                            double rate = (c.idx_sma5.Value / c.sma5.Value);
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].sma5.HasValue)
                                    chg.Add(hists[i].sma5.Value * rate);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Pink, "SMA5", "sma5"));

                            //SMA5
                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].idx_sma5.HasValue)
                                    chg.Add(hists[i].idx_sma5.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Tomato, "Index SMA5", "index_sma5"));

                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].price.HasValue)
                                    chg.Add(hists[i].price.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Green, "Price", "price"));

                            //Axis Label
                            List<DateTime> dts = new List<DateTime>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                dts.Add(hists[i].date_hist.Date);
                            }
                            Utility.GetXLabels(chart_Main.AxisX, dts, dt_compare_end);

                            chart_Main.AxisX[0].Sections.Clear();
                            chart_Main.AxisX[0].Sections.Add(AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));

                            // Fixed or Relative Range
                            if (!chk_FixedRange.Checked)
                            {
                                double yMax = 0;
                                double yMin = 0;
                                Utility.RetYAxis(chart_Main, out yMax, out yMin);

                                nmb_Y_Max.Value = (decimal)yMax;
                                nmb_Y_Min.Value = (decimal)yMin;

                                Utility.SetAxis(chart_Main, 0, hists.Count, (double)nmb_Y_Max.Value, (double)nmb_Y_Min.Value);
                            }
                            else
                            {
                                Utility.SetAxis(chart_Main, 0, hists.Count, (double)nmb_Y_Max.Value, (double)nmb_Y_Min.Value);
                            }

                            // 2nd Axis Y
                            var sec_axis_Y = new Axis();
                            chart_Main.AxisY.Add(sec_axis_Y);

                            //Volume Changes
                            var volume = stock.daily_volume.Where(r => r.symbol == symbol &&
                            r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                            chg = new List<double>();
                            for (int i = 0; i < volume.Count; i++)
                            {
                                if (volume[i].volume.HasValue)
                                    chg.Add(volume[i].volume.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Purple, "Volume", "volume", 1));

                            // Fixed or Relative Range
                            if (chk_FixedRange.Checked)
                            {
                                Utility.SetAxis(chart_Main, 1, hists.Count, (double)nmb_2nd_Axix_Y_Max.Value, (double)nmb_2nd_Axix_Y_Min.Value);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    query.Show_Message("Stock Chart Error:" + ex.Message);
                }
            }
        }

        #endregion

        #region + Capture Process
        private void btn_Capture_Click(object sender, EventArgs e)
        {
            if (capture_continue)
            {
                CaptureState(false);
                return;
            }

            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            if (dgv_List.Rows.Count < 1)
            {
                MessageBox.Show("Please List up Symbols First!");
                return;
            }

            CaptureState(true);
            thd_capture = new Thread(new ParameterizedThreadStart(Capture_Process));
            thd_capture.SetApartmentState(ApartmentState.STA);
            thd_capture.Start(new object[] { nmb_CaptureList.Value, chk_Analysis.Checked });
        }

        private void Capture_Process(object _params)
        {
            object[] _param = (object[])_params;
            int list = Convert.ToInt32(_param[0]);
            bool analysis_on = (bool)_param[1];
            for (int r = 2; r < list; r++)
            {
                if (!capture_continue)
                    break;

                Process1(r, analysis_on); 
                query.Show_Message("Processing " + r);
                if(analysis_on)
                    Thread.Sleep(3000);
                else
                    Thread.Sleep(1000);
                SaveChart(r, analysis_on);
            }
            CaptureState(false);
            query.Show_Message("Completed!!!");
        }
        private void Process1(int _r, bool _analysis_on)
        {
            if (this.chart_Main.InvokeRequired)
            {
                chart_Main.BeginInvoke(new MethodInvoker(delegate () { Process1(_r, _analysis_on); }));
            }
            else
            {
                is_processing = true;

                dgv_List[0, _r].Selected = true;
                DrawSymbolChart(_r);
                chart_Main.DisableAnimations = true;
                chart_Main.Refresh();
                is_processing = false;

            }
        }

        private void SaveChart(int _r, bool _analysis_on)
        {
            if (this.dgv_List.InvokeRequired)
            {
                dgv_List.BeginInvoke(new MethodInvoker(delegate () { SaveChart(_r, _analysis_on); }));
            }
            else
            {
                Rectangle bounds = this.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width - 20, bounds.Height - 10))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left + 10, bounds.Top), Point.Empty, bounds.Size);
                    }
                    var dir = Picture_Dir + "\\chart\\" + dt_Symbol_From.Value.ToString("yyyyMMdd") + "_" + dt_Symbol_To.Value.ToString("yyyyMMdd");

                    var filename = (_r + 1).ToString() + "." + dgv_List[0, _r].Value.ToString();
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    string fullpath = dir + "\\index_" + filename + ".png";

                    bitmap.Save(fullpath, ImageFormat.Png);
                    
                    if (_analysis_on)
                        query.Save_Analysis(dir, "\\chart_" + filename);

                }
            }
        }

        public void CaptureList(string _fileName)
        {
            if (this.dgv_List.InvokeRequired)
            {
                dgv_List.BeginInvoke(new MethodInvoker(delegate () { CaptureList(_fileName); }));
            }
            else
            {
                Rectangle bounds = this.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width - 20, bounds.Height - 10))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left + 10, bounds.Top), Point.Empty, bounds.Size);
                    }
                    var dir = Picture_Dir + "\\List\\" + dt_Symbol_From.Value.ToString("yyyyMMdd") + "_" + dt_Symbol_To.Value.ToString("yyyyMMdd");

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    string fullpath = dir + "\\" + _fileName + ".png";

                    bitmap.Save(fullpath, ImageFormat.Png);
                }
            }
        }


        private void CaptureState(bool _enabled)
        {
            if (btn_Capture.InvokeRequired)
            {
                btn_Capture.BeginInvoke(new MethodInvoker(delegate () { CaptureState(_enabled); }));
            }
            else
            {
                if(_enabled)
                {
                    capture_continue = true;
                    btn_Capture.Text = "Capturing";
                }
                else
                {
                    capture_continue = false;
                    btn_Capture.Text = "Capture";
                }
            }
        }
        #endregion

    }
}