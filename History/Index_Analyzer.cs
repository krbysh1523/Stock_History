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
        ResultUI result = null;
        List<daily_strong> hists = new List<daily_strong>();
        List<daily_close> hist2 = new List<daily_close>();
        bool is_1st_click_chart = true;
        int click_key_1st = 0;
        Thread thd_capture;
        Thread thd_list;
        public bool capture_continue = false;
        public object process = new object();
        public bool is_processing = false;
        string filter_option;
        string filter_att1;
        string filter_att2;
        string filter_att3;
        string filter_att4;
        string filter_att5;
        string Picture_Dir = @"C:\Users\Sukho\OneDrive\투자\Program\Capture";
        string Result_ID = "";
        string Result_Symbol = "";

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
            cmb_Type.SelectedIndex = 1;
            btn_Fixed_Click(this, null);
            LoadLast();
            Load_Result_Menu();
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

        #region + Result Menu

        private void Load_Result_Menu()
        { 
            using(stockEntity stock = new stockEntity())
            {
                var results = stock.lookups.Where(r => r.l_type == "Result").ToList();

                foreach(var r in results)
                {
                    ToolStripButton btn = new ToolStripButton();
                    btn.Text = r.l_id + "." + r.l_name;
                    btn.Name = "menu_" + r.l_id;
                    btn.Click += menu_Result_Click;
                    menu_Result.Items.Add(btn);
                }

                ToolStripTextBox desc = new ToolStripTextBox();
                desc.Name = "menu_DESC";
                menu_Result.Items.Add(desc);

            }
        }

        private void menu_Result_Click(object sender, EventArgs e)
        {
            var btn = ((ToolStripButton)sender);

            int ResultID = Convert.ToInt32(Result_ID);
            using (stockEntity stock = new stockEntity())
            {
                var result = stock.prediction_history.FirstOrDefault(r => r.result_id == ResultID);
                result.result = btn.Text;
                result.result_desc = menu_Result_Get_Desc();

                stock.SaveChanges();
            }
        }

        private void menu_Result_Set_Desc(string _Desc)
        {
            foreach (var i in menu_Result.Items)
            {
                if (i.GetType().Name == "ToolStripTextBox")
                {
                    ((ToolStripTextBox)i).Text = _Desc;
                }
            }

        }
        private string menu_Result_Get_Desc()
        {
            foreach(var i in menu_Result.Items)
            {
                if (i.GetType().Name == "ToolStripTextBox")
                {
                    return ((ToolStripTextBox)i).Text;
                }
            }
            return "";
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

            using (stockEntity stock = new stockEntity())
            {
                hists = stock.daily_strong.Where(r => r.symbol == "AAPL" &&
r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                if (cmb_Type.SelectedIndex == 0) // Percent
                {
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

                    double yMax = 0;
                    double yMin = 0;
                    Utility.RetYAxis(chart_Main, out yMax, out yMin);

                    Utility.SetAxis(chart_Main, hists.Count, (double)yMax, (double)yMin);

                    //Draw Section 
                    var start_idx = hists.FindIndex(r => r.date_hist == dt_compare_start);
                    var end_idx = hists.FindIndex(r => r.date_hist == dt_compare_end);
                    chart_Main.AxisX[0].Sections.Add(AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));

                }
                else if (cmb_Type.SelectedIndex == 1) //SMA
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
                    chart_Main.Series.Add(Utility.AddLine(hist2, 1, "circle", 10));

                    double yMax = 0;
                    double yMin = 0;
                    Utility.RetYAxis(chart_Main, out yMax, out yMin);

                    Utility.SetAxis(chart_Main, hists.Count, (double)yMax, (double)yMin);

                    //Draw Section 
                    var start_idx = hist2.FindIndex(r => r.date_hist == dt_compare_start);
                    var end_idx = hist2.FindIndex(r => r.date_hist == dt_compare_end);
                    chart_Main.AxisX[0].Sections.Add(AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));

                }
            }
            chart_Main.BringToFront();

            SaveLast();
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
            catch (Exception ex)
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

                //chart_Main.DisableAnimations = true;

                query.Show_Message("Option " + _params[4].ToString() + " : Completed");
                query.Refresh_History();

                chk_Simul_CheckedChanged(this, null);

                is_processing = false;
                Monitor.Exit(process);
                chart_Main.BringToFront();

            }
        }

        public void Set_DGV_List()
        {
            dgv_List.Columns[0].Visible = false;
            dgv_List.Columns[1].Visible = false;

            dgv_List.Columns[2].Width = 50;
            dgv_List.Columns[3].Width = 40;
            dgv_List.Columns[4].Width = 40;
            dgv_List.Columns[5].Width = 40;
            dgv_List.Columns[6].Width = 40;
            dgv_List.Columns[7].Width = 40;
            dgv_List.Columns[8].Width = 40;
            dgv_List.Columns[9].Width = 40;
            dgv_List.Columns[10].Width = 40;
            dgv_List.Columns[11].Width = 40;
            dgv_List.Columns[12].Width = 100;
            dgv_List.Columns[13].Width = 100;

            dgv_List.Columns[2].HeaderText = "Symbol";
            dgv_List.Columns[3].HeaderText = "Rank";
            dgv_List.Columns[4].HeaderText = "R 1";
            dgv_List.Columns[5].HeaderText = "1month";
            dgv_List.Columns[6].HeaderText = "+5";
            dgv_List.Columns[7].HeaderText = "+10";
            dgv_List.Columns[8].HeaderText = "+15";
            dgv_List.Columns[9].HeaderText = "+20";
            dgv_List.Columns[10].HeaderText = "+40";
            dgv_List.Columns[11].HeaderText = "+60";
            dgv_List.Columns[12].HeaderText = "Result";
            dgv_List.Columns[13].HeaderText = "Description";

            dgv_List.Columns[6].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[7].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[8].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[9].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[10].SortMode = DataGridViewColumnSortMode.Automatic;
            dgv_List.Columns[11].SortMode = DataGridViewColumnSortMode.Automatic;

            dgv_List.Columns[3].Resizable = DataGridViewTriState.True;
            dgv_List.Columns[4].Resizable = DataGridViewTriState.True;
            dgv_List.Columns[5].Resizable = DataGridViewTriState.True;

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
            using (stockEntity stock = new stockEntity())
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

            using (stockEntity stock = new stockEntity())
            {
                List<filter_simul_header_Result> header = stock.filter_simul_header(dt_to).ToList();

                dgv_List.Columns[6].HeaderText = header[0].d5.Value.ToString("M/dd");
                dgv_List.Columns[7].HeaderText = header[0].d10.Value.ToString("M/dd");
                dgv_List.Columns[8].HeaderText = header[0].d15.Value.ToString("M/dd");
                dgv_List.Columns[9].HeaderText = header[0].d20.Value.ToString("M/dd");
                dgv_List.Columns[10].HeaderText = header[0].d40.Value.ToString("M/dd");
                dgv_List.Columns[11].HeaderText = header[0].d60.Value.ToString("M/dd");
            }
        }

        public void GetStatHistory(int _hist_count, string _final_only)
        {
            if (this.dgv_List.InvokeRequired)
            {
                dgv_List.BeginInvoke(new MethodInvoker(delegate () { GetStatHistory(_hist_count, _final_only); }));
            }
            else
            {
                Monitor.Enter(process);

                is_processing = true;

                dgv_List.DataSource = GetStatHistory_List(dt_Symbol_From.Value, dt_Symbol_To.Value, _hist_count, _final_only);

                Set_DGV_List();

                SetHeader(new object[] { "", dt_Symbol_To.Value.ToString() });

                //chart_Main.DisableAnimations = true;

                chk_Simul_CheckedChanged(this, null);

                query.Show_Message("Stat History : Completed");

                is_processing = false;
                Monitor.Exit(process);
                chart_Main.BringToFront();

            }
        }
        public SortableBindingList<get_Stat_history_Result> GetStatHistory_List(DateTime _start_dttm, DateTime _end_dttm, int _hist_count, string _final_only)
        {
            SortableBindingList<get_Stat_history_Result> ranks = new SortableBindingList<get_Stat_history_Result>();

            query.Show_Message("Stat History : Retrieving");
            using (stockEntity stock = new stockEntity())
            {
                List<get_Stat_history_Result> results = stock.get_Stat_history(
                    _start_dttm, _end_dttm, _hist_count, _final_only).ToList();

                foreach (var r in results)
                    ranks.Add(r);
            }
            return ranks;
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

                //chart_Main.DisableAnimations = true;

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
            using (stockEntity stock = new stockEntity())
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
            btn_Index_Click(this, null);
        }

        private void btn_N1_Click(object sender, EventArgs e)
        {
            dt_From.Value = dt_From.Value.AddMonths(1);
            dt_To.Value = dt_To.Value.AddMonths(1);
            btn_Index_Click(this, null);
        }
        private void btn_PD1_Click(object sender, EventArgs e)
        {
            dt_Symbol_From.Value = dt_Symbol_From.Value.AddDays(-7);
            dt_Symbol_To.Value = dt_Symbol_To.Value.AddDays(-7);
        }

        private void btn_ND1_Click(object sender, EventArgs e)
        {
            dt_Symbol_From.Value = dt_Symbol_From.Value.AddDays(7);
            dt_Symbol_To.Value = dt_Symbol_To.Value.AddDays(7);
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
        private void btn_Fixed_Click(object sender, EventArgs e)
        {
            nmb_Y_Min.Value = -50;
            nmb_Y_Max.Value = 50;

            nmb_2nd_Axix_Y_Min.Value = -200;
            nmb_2nd_Axix_Y_Max.Value = 500;
        }

        #endregion

        #region + User Event
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width > 500)
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
                if (dt_Symbol_From.Value <= hists[chartPoint.Key].date_hist)
                {
                    dt_Symbol_To.Value = hists[chartPoint.Key].date_hist;
                    is_1st_click_chart = true;
                    double pre = Math.Round(hists[click_key_1st].price.Value, 2);
                    double nex = Math.Round(hists[chartPoint.Key].price.Value, 2);
                    query.Show_Message(string.Format("Price: {0} → {1} Change: {2}%",
                        pre.ToString(),
                        nex.ToString(),
                        Math.Round((nex - pre) / nex * 100, 1).ToString()));
                }
            }
        }
        private void dgv_List_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 4 || e.ColumnIndex > 10)
                return;

            try
            {
                var dg_type = dgv_List.DataSource.GetType();
                if (dg_type.FullName.Contains("sp_get_prediction_history_Result"))
                {
                    List<sp_get_prediction_history_Result> ranks = ((System.Collections.ObjectModel.Collection<History.sp_get_prediction_history_Result>)dgv_List.DataSource).ToList();
                    List<sp_get_prediction_history_Result> _ranks = new List<sp_get_prediction_history_Result>();
                    if (e.ColumnIndex == 4)
                        _ranks = ranks.OrderByDescending(r => r.ratio1).ToList();
                    else if (e.ColumnIndex == 5)
                        _ranks = ranks.OrderByDescending(r => r.ratio2).ToList();
                    else if (e.ColumnIndex == 6)
                        _ranks = ranks.OrderByDescending(r => r.rate_5).ToList();
                    else if (e.ColumnIndex == 7)
                        _ranks = ranks.OrderByDescending(r => r.rate_10).ToList();
                    else if (e.ColumnIndex == 8)
                        _ranks = ranks.OrderByDescending(r => r.rate_15).ToList();
                    else if (e.ColumnIndex == 9)
                        _ranks = ranks.OrderByDescending(r => r.rate_20).ToList();
                    else if (e.ColumnIndex == 10)
                        _ranks = ranks.OrderByDescending(r => r.rate_40).ToList();
                    else if (e.ColumnIndex == 11)
                        _ranks = ranks.OrderByDescending(r => r.rate_60).ToList();

                    dgv_List.DataSource = _ranks;
                }
                else
                {
                    List<filter_main_Result> ranks = (List<filter_main_Result>)dgv_List.DataSource;
                    List<filter_main_Result> _ranks = new List<filter_main_Result>();
                    if (e.ColumnIndex == 4)
                        _ranks = ranks.OrderByDescending(r => r.ratio1).ToList();
                    else if (e.ColumnIndex == 5)
                        _ranks = ranks.OrderByDescending(r => r.ratio2).ToList();
                    else if (e.ColumnIndex == 6)
                        _ranks = ranks.OrderByDescending(r => r.rate_5).ToList();
                    else if (e.ColumnIndex == 7)
                        _ranks = ranks.OrderByDescending(r => r.rate_10).ToList();
                    else if (e.ColumnIndex == 8)
                        _ranks = ranks.OrderByDescending(r => r.rate_15).ToList();
                    else if (e.ColumnIndex == 9)
                        _ranks = ranks.OrderByDescending(r => r.rate_20).ToList();
                    else if (e.ColumnIndex == 10)
                        _ranks = ranks.OrderByDescending(r => r.rate_40).ToList();
                    else if (e.ColumnIndex == 11)
                        _ranks = ranks.OrderByDescending(r => r.rate_60).ToList();

                    dgv_List.DataSource = _ranks;
                }
            }
            catch (Exception ex)
            {
                query.Show_Message("Error:" + ex.Message);
            }
        }


        private void dgv_List_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow dr in dgv_List.Rows)
            {
                if (dr.Index > 1)
                {
                    for (int c = 6; c < dgv_List.ColumnCount - 2; c++)
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

                    if (dgv_List[12, dr.Index].Value == null)
                        dr.Cells[12].Style.ForeColor = Color.White;
                    else if (dgv_List[12, dr.Index].Value.ToString() == "UP")
                        dr.Cells[12].Style.ForeColor = Color.Green;
                    else if (dgv_List[12, dr.Index].Value.ToString() == "DOWN")
                        dr.Cells[12].Style.ForeColor = Color.Red;
                    else if (dgv_List[12, dr.Index].Value.ToString() == "EXCLUDE")
                        dr.Cells[12].Style.ForeColor = Color.Gray;

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
                    sb.Append(dgv_List[2, r].Value.ToString());
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
                for (int c = 5; c < dgv_List.ColumnCount - 2; c++)
                    dgv_List.Columns[c].Visible = true;
            }
            else
            {
                for (int c = 5; c < dgv_List.ColumnCount - 2; c++)
                    dgv_List.Columns[c].Visible = false;
            }
        }

        private void btn_CopyList_Click(object sender, EventArgs e)
        {
            var newline = System.Environment.NewLine;
            var tab = "\t";
            var clipboard_string = new StringBuilder();
            for (int c = 0; c < dgv_List.Columns.Count; c++)
            {
                clipboard_string.Append(dgv_List.Columns[c].HeaderText + tab);
            }
            clipboard_string.Append(newline);
            for (int r = 2; r < dgv_List.Rows.Count; r++)
            {
                for (int c = 0; c < dgv_List.Columns.Count; c++)
                {
                        clipboard_string.Append(dgv_List[c, r].Value + tab);
                }
                clipboard_string.Append(newline);
            }

            Clipboard.SetText(clipboard_string.ToString());
        }

        #endregion

        #region + Draw Symbol Graph
        private void dgv_List_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex > 1)
            {
                Monitor.Enter(process);

                var filename = dt_Symbol_From.Value.ToString("yyyyMMdd") + "_" + dt_Symbol_To.Value.ToString("yyyyMMdd") + "_" + dgv_List[2, e.RowIndex].Value.ToString();
                string fullpath = Picture_Dir + "\\chart\\" + filename + "_1.png";
                query.LoadPicture(fullpath);

                filename = dt_Symbol_From.Value.ToString("yyyyMMdd") + "_" + dt_Symbol_To.Value.ToString("yyyyMMdd") + "_" + dgv_List[2, e.RowIndex].Value.ToString();
                fullpath = Picture_Dir + "\\chart\\" + filename + "_2.png";
                if (File.Exists(fullpath) && chk_Show_Pic.Checked)
                {
                    Load_Picture(fullpath);
                }
                else
                    DrawSymbolChart(e.RowIndex, -1, 3);

                Monitor.Exit(process);
            }
            if ((e.ColumnIndex == 12 || e.ColumnIndex == 13) && e.RowIndex > 1)
            {
                //Load Picture
                var filename = dt_Symbol_From.Value.ToString("yyyyMMdd") + "_" + dt_Symbol_To.Value.ToString("yyyyMMdd") + "_" + dgv_List[2, e.RowIndex].Value.ToString();
                string fullpath = Picture_Dir + "\\chart\\" + filename + "_1.png";
                if (File.Exists(fullpath) && chk_Show_Pic.Checked)
                {
                    Load_Picture(fullpath);
                }
                else
                    DrawSymbolChart(e.RowIndex, -4, 0);

                Result_ID = dgv_List[0, e.RowIndex].Value.ToString();
                Result_Symbol = dgv_List[2, e.RowIndex].Value.ToString();
                if(dgv_List[13, e.RowIndex].Value != null)
                    menu_Result_Set_Desc(dgv_List[13, e.RowIndex].Value.ToString());
                else
                    menu_Result_Set_Desc("");

                //Show Menu
                DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
                ContextMenuStrip cms = currentCell.ContextMenuStrip;
                Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
                Point p = new Point(r.X, r.Y + r.Height);
                menu_Result.Show(currentCell.DataGridView, p);
            }
        }

        private void Load_Picture(string _filename)
        {
            if (File.Exists(_filename))
            {
                pic_Chart.Image = Image.FromFile(_filename);
            }
            else
            {
                pic_Chart.Image = pic_Chart.ErrorImage;
            }

            pic_Chart.BringToFront();
        }

        private void DrawSymbolChart(int _rowIndex, int _past_month = 999, int _future_month = 999, int _past_week = 0, int _future_week = 4)
        {
            if (this.chart_Main.InvokeRequired)
            {
                chart_Main.BeginInvoke(new MethodInvoker(delegate () { DrawSymbolChart(_rowIndex, _past_month, _future_month); }));
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
                    if (_past_month != 999 && _future_month != 999)
                    {
                        dt_from = dt_compare_end.AddMonths(_past_month);
                        dt_to = dt_compare_end.AddMonths(_future_month);
                    }
                    string symbol = dgv_List[2, _rowIndex].Value.ToString();

                    //If Date is outside of Range
                    if (dt_from > dt_compare_start || dt_to < dt_compare_end)
                    {
                        MessageBox.Show("Date is not within Range to display Chart!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    query.Show_Message(symbol + ": " + dt_compare_start.ToString("MM/dd") + "~" + dt_compare_end.ToString("MM/dd"));

                    // Send to Analysis Page
                    if (chk_Analysis.Checked)
                        query.SendAnalysis(symbol, dt_from, dt_to, dt_compare_start, dt_compare_end);

                    using (stockEntity stock = new stockEntity())
                    {

                        hists = stock.daily_strong.Where(r => r.symbol == symbol &&
r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                        var start_idx = hists.FindIndex(r => r.date_hist == dt_compare_start);
                        var end_idx = hists.FindIndex(r => r.date_hist == dt_compare_end);

                        List<double> chg = new List<double>();

                        // Index Price
                        if (chk_IndexPrice.Checked)
                        {
                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].idx_price.HasValue)
                                    chg.Add(hists[i].idx_price.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Tomato, "Index", "index"));
                        }

                        // Index SMA5
                        if (chk_IndexSMA5.Checked)
                        {
                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].idx_sma5.HasValue)
                                    chg.Add(hists[i].idx_sma5.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Red, "Index SMA5", "index_sma5"));
                        }

                        // Symbol Pric
                        if (chk_S_Price.Checked)
                        {
                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].price.HasValue)
                                    chg.Add(hists[i].price.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.Black, "Price", "price"));
                        }

                        // Symbol SMA5
                        if (chk_S_SMA5.Checked)
                        {
                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].sma5.HasValue)
                                    chg.Add(hists[i].sma5.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            chart_Main.Series.Add(AddLine(chg, System.Windows.Media.Colors.LightSkyBlue, "SMA5", "sma5"));
                        }

                        //Axis Label
                        List<DateTime> dts = new List<DateTime>();
                        for (int i = 0; i < hists.Count; i++)
                        {
                            dts.Add(hists[i].date_hist.Date);
                        }
                        Utility.GetXLabels(chart_Main.AxisX, dts, dt_compare_end);

                        chart_Main.AxisX[0].Sections.Clear();

                            int buy_week = end_idx + (5 * _past_week);
                            int sell_week = end_idx + (5 * _future_week);
                            chart_Main.AxisX[0].Sections.Add(AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));
                            chart_Main.AxisX[0].Sections.Add(AddSection(buy_week + 1, 5, System.Windows.Media.Colors.Red, 0.4));
                            chart_Main.AxisX[0].Sections.Add(AddSection(sell_week + 1, 5, System.Windows.Media.Colors.Blue, 0.4));

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

                        // Volume
                        if (chk_S_Volume.Checked)
                        {
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

                        // Candle
                        if (chk_S_Candle.Checked)
                        {
                            // 3rd Axis Y
                            var candle_axis_Y = new Axis();
                            chart_Main.AxisY.Add(candle_axis_Y);

                            List<daily_history> candles = new List<daily_history>();
                            candles = stock.daily_history.Where(r => r.symbol == symbol &&
                            r.date >= dt_from && r.date <= dt_to).OrderBy(r => r.date).ToList();

                            CandleSeries candle = Utility.AddCandle(candles);
                            candle.ScalesYAt = chart_Main.AxisY.Count - 1;
                            chart_Main.Series.Add(candle);

                        }
                    }
                }
                catch (Exception ex)
                {
                    query.Show_Message("Stock Chart Error:" + ex.Message);
                }

                chart_Main.BringToFront();
            }
        }

        #endregion

        #region + Capture Process
        private void btn_Capture_One_Click(object sender, EventArgs e)
        {
            if (dgv_List.SelectedCells[0].RowIndex > 1)
            {

                if (this.WindowState != FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }

                SaveChart(dgv_List.SelectedCells[0].RowIndex, chk_Analysis.Checked, 0, false);
            }
        }

        private void btn_Capture_Click(object sender, EventArgs e)
        {
            Start_Capture(0);
        }

        /// <summary>
        /// Thread to Start Capture
        /// </summary>
        /// <param name="_option">0=Default, Greater than 0 is Date Option</param>
        public void Start_Capture(int _option, int _pre_month = 0, int _nex_month = 0)
        {
            if (this.nmb_Sleep.InvokeRequired)
            {
                nmb_Sleep.BeginInvoke(new MethodInvoker(delegate () { Start_Capture(_option, _pre_month, _nex_month); }));
            }
            else
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

                int count = dgv_List.Rows.Count;
                if (nmb_CaptureList.Value < count)
                    count = Convert.ToInt32(nmb_CaptureList.Value);

                CaptureState(true);
                thd_capture = new Thread(new ParameterizedThreadStart(Capture_Process));
                thd_capture.SetApartmentState(ApartmentState.STA);
                thd_capture.Start(new object[] { count, chk_Analysis.Checked, nmb_Sleep.Value, _option, _pre_month, _nex_month });
            }
        }

        private void Capture_Process(object _params)
        {
            object[] _param = (object[])_params;
            int list = Convert.ToInt32(_param[0]);
            bool analysis_on = (bool)_param[1];
            int sleep = Convert.ToInt32(_param[2]);
            int option = Convert.ToInt32(_param[3]);
            int pre_month = Convert.ToInt32(_param[4]);
            int nex_month = Convert.ToInt32(_param[5]);
            for (int r = 2; r < list; r++)
            {
                if (!capture_continue)
                    break;

                Process1(r, analysis_on, option, pre_month, nex_month); 
                query.Show_Message("Processing " + r);
                if (analysis_on)
                    Thread.Sleep(sleep * 1000);
                else
                    Thread.Sleep(sleep * 1000);
                SaveChart(r, analysis_on, option);
            }
            pic_Chart.BringToFront();

            CaptureState(false);
            query.Show_Message("Completed!!!");
        }
        private void Process1(int _r, bool _analysis_on, int _option, int _pre_month, int _nex_month)
        {
            if (this.chart_Main.InvokeRequired)
            {
                chart_Main.BeginInvoke(new MethodInvoker(delegate () { Process1(_r, _analysis_on, _option, _pre_month, _nex_month); }));
            }
            else
            {
                is_processing = true;

                dgv_List[2, _r].Selected = true;
                if(_option == 0)
                    DrawSymbolChart(_r);
                else if(_option == 1)
                    DrawSymbolChart(_r, _pre_month, _nex_month);
                else if (_option == 2)
                    DrawSymbolChart(_r, _pre_month, _nex_month, 0, 4);
                //chart_Main.DisableAnimations = true;
                chart_Main.Refresh();
                is_processing = false;

            }
        }

        private void SaveChart(int _r, bool _analysis_on, int _option, bool _is_auto = true)
        {
            if (this.dgv_List.InvokeRequired)
            {
                dgv_List.BeginInvoke(new MethodInvoker(delegate () { SaveChart(_r, _analysis_on, _option, _is_auto); }));
            }
            else
            {

                Rectangle bounds = this.Bounds;

                int pic_width = bounds.Width - 20;
                int pic_height = bounds.Height - 10;
                int start_left = bounds.Left + 10;
                int start_top = bounds.Top;

                if (_option == 1 || _option == 2)
                {
                    bounds = this.chart_Main.Bounds;
                    pic_width = bounds.Width - 20;
                    pic_height = bounds.Height;
                    start_left = bounds.Left;
                    start_top = bounds.Top + 22;
                }

                using (Bitmap bitmap = new Bitmap(pic_width, pic_height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(start_left, start_top), Point.Empty, bounds.Size);
                    }
                    var dir = Picture_Dir + "\\chart\\";

                    var filename = dt_Symbol_From.Value.ToString("yyyyMMdd") + "_" 
                        + dt_Symbol_To.Value.ToString("yyyyMMdd") + "_" 
                        + dgv_List[2, _r].Value.ToString() + "_"
                        + _option.ToString();
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    string fullpath = dir + filename + ".png";

                    if (File.Exists(fullpath))
                        File.Delete(fullpath);

                    bitmap.Save(fullpath, ImageFormat.Png);
                    
                    if (_analysis_on)
                        query.Save_Analysis(dir, "\\chart_" + filename);

                    if(!_is_auto)
                    {
                        using (Process myProcess = new Process())
                        {
                            myProcess.StartInfo.FileName = @"C:\Program Files\TechSmith\Snagit 2021\SnagitEditor.exe";
                            myProcess.StartInfo.Arguments = fullpath;
                            myProcess.Start();
                        }
                    }

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


        public void CaptureState(bool _enabled)
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