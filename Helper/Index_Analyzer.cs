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

namespace Helper
{
    public partial class Index_Analyzer : Form
    {
        #region + Forms
        List<daily_history> hists = new List<daily_history>();
        List<daily_analysis> anal = new List<daily_analysis>();
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

        public Index_Analyzer(Point _location)
        {
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
            dt_Symbol_From.Value = DateTime.Today.AddDays(-30);
            dt_Symbol_To.Value = DateTime.Today.AddDays(-1);
            btn_Fixed_Click(this, null);
            LoadLast();
        }

        private void Index_Analyzer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLast();
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
                    if (l[0] == "FROM_STOCK_DTTM")
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
                writer.WriteLine("FROM_STOCK_DTTM," + dt_Symbol_From.Value.ToString("MM/dd/yyyy"));
                writer.WriteLine("TO_STOCK_DTTM," + dt_Symbol_To.Value.ToString("MM/dd/yyyy"));
            }
        }
        #endregion

        #region + User Button
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
        private void btn_SP_Click(object sender, EventArgs e)
        {
            Get_List_Start();
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width > 500)
                splitContainer1.SplitterDistance = this.Width - 500;
        }
        private void chart_Main_DataClick(object sender, ChartPoint chartPoint)
        {
            if (is_1st_click_chart)
            {
                dt_Symbol_From.Value = hists[chartPoint.Key].date;
                is_1st_click_chart = false;
                click_key_1st = chartPoint.Key;
            }
            else
            {
                if (dt_Symbol_From.Value <= hists[chartPoint.Key].date)
                {
                    dt_Symbol_To.Value = hists[chartPoint.Key].date;
                    is_1st_click_chart = true;
                    double pre = Math.Round(hists[click_key_1st].close.Value, 2);
                    double nex = Math.Round(hists[chartPoint.Key].close.Value, 2);
                }
            }
        }
        private void dgv_List_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

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
                    if (!dgv_List[2, r].Value.ToString().StartsWith("_"))
                    {
                        sb.Append(dgv_List[2, r].Value.ToString());
                        if (r < dgv_List.Rows.Count - 1)
                            sb.Append(",");
                    }
                }
            }
            catch (Exception)
            {
            }
            return sb.ToString();
        }

        private void btn_CopyList_Click(object sender, EventArgs e)
        {
            Utility.Copy_DGV_to_Clipboard(dgv_List, 2);
        }

        #endregion

        #region + Button List
        public void Get_List_Start()
        {
            List<object> param = new List<object>();
            param.Add(dt_Symbol_From.Value);
            param.Add(dt_Symbol_To.Value);
            param.Add(Utility.RetChecked(this.chk_PassingList));
            param.Add(GetSymbolList());
            param.Add(filter_option);
            param.Add(filter_att1);
            param.Add(filter_att2);
            param.Add(filter_att3);
            param.Add(filter_att4);
            param.Add(filter_att5);

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
                show_Message("Get List Error:" + ex.Message);
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

                dgv_List.DataSource = GetData_List(_params);

                Set_DGV_List();

                SetHeader(_params);

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

                
                show_Message("Stat History : Completed");

                is_processing = false;
                Monitor.Exit(process);
                chart_Main.BringToFront();

            }
        }
        public SortableBindingList<get_Stat_history_Result> GetStatHistory_List(DateTime _start_dttm, DateTime _end_dttm, int _hist_count, string _final_only)
        {
            SortableBindingList<get_Stat_history_Result> ranks = new SortableBindingList<get_Stat_history_Result>();

            show_Message("Stat History : Retrieving");
            using (stockEntities stock = new stockEntities())
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

                show_Message("History " + _hist_id.ToString() + " : Completed");

                is_processing = false;
                Monitor.Exit(process);
            }
        }
        public SortableBindingList<sp_get_prediction_history_Result> GetDataHistory_List(string _hist_id)
        {
            SortableBindingList<sp_get_prediction_history_Result> ranks = new SortableBindingList<sp_get_prediction_history_Result>();

            int hist_id = Convert.ToInt32(_hist_id);

            show_Message("History " + _hist_id.ToString() + " : Retrieving");
            using (stockEntities stock = new stockEntities())
            {
                List<sp_get_prediction_history_Result> results = stock.sp_get_prediction_history(hist_id).ToList();

                foreach (var r in results)
                    ranks.Add(r);
            }
            return ranks;
        }

        #endregion

        #region + Draw Symbol Graph
        private void dgv_List_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex > 1)
            {
                Monitor.Enter(process);

                DrawSymbolChart(e.RowIndex, -1, 3);

                Monitor.Exit(process);
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
                    DateTime dt_from = dt_Symbol_From.Value;
                    DateTime dt_to = dt_Symbol_To.Value;
                    string symbol = dgv_List[2, _rowIndex].Value.ToString();

                    using (stockEntities stock = new stockEntities())
                    {

                        hists = stock.daily_history.Where(r => r.symbol == symbol &&
                        r.date >= dt_from && r.date <= dt_to).OrderBy(r => r.date).ToList();

                        var end_idx = hists.FindIndex(r => r.date == dt_to);

                        List<double> chg = new List<double>();

                        // Candle
                        var candle_axis_Y = new Axis();
                        candle_axis_Y.Name = "Candle";
                        chart_Main.AxisY.Add(candle_axis_Y);

                        List<daily_history> candles = new List<daily_history>();
                        candles = stock.daily_history.Where(r => r.symbol == symbol &&
                        r.date >= dt_from && r.date <= dt_to).OrderBy(r => r.date).ToList();

                        CandleSeries candle = Utility.AddCandle(candles);
                        candle.ScalesYAt = Utility.FindAxis_Name(chart_Main, "Candle");
                        chart_Main.Series.Add(candle);

                        Utility.GetXLabels(chart_Main.AxisX, candles);
                        chart_Main.AxisY[Utility.FindAxis_Name(chart_Main, "Candle")].ShowLabels = false;

                        // Symbol Pric
                        if (chk_S_Price.Checked)
                        {
                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].close.HasValue)
                                    chg.Add(hists[i].close.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            Utility.AddLine_Axis(chart_Main, chg, System.Windows.Media.Colors.Black, "Price");
                            // Fixed
                            if (chk_FixedRange.Checked)
                                Utility.SetAxis_Fixed(chart_Main, "Price", (double)nmb_Y_Max.Value, (double)nmb_Y_Min.Value);
                        }

                        // Symbol SMA5
                        if (chk_S_SMA5.Checked)
                        {
                            var sma = stock.daily_analysis.Where(r => r.symbol == symbol &&
                            r.date_hist >= dt_from && r.date_hist <= dt_to).OrderBy(r => r.date_hist).ToList();

                            chg = new List<double>();
                            for (int i = 0; i < sma.Count; i++)
                            {
                                if (sma[i].sma5.HasValue)
                                    chg.Add(sma[i].sma5.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            Utility.AddLine_Axis(chart_Main, chg, System.Windows.Media.Colors.Silver, "SMA5");
                            // Fixed
                            if (chk_FixedRange.Checked)
                                Utility.SetAxis_Fixed(chart_Main, "SMA5", (double)nmb_Y_Max.Value, (double)nmb_Y_Min.Value);
                        }

                        // Volume
                        if (chk_S_Volume.Checked)
                        {
                            //Volume Changes

                            chg = new List<double>();
                            for (int i = 0; i < hists.Count; i++)
                            {
                                if (hists[i].volume.HasValue)
                                    chg.Add(hists[i].volume.Value);
                            }
                            chg = Utility.ConverToPerc(chg, end_idx, 1);

                            Utility.AddLine_Axis(chart_Main, chg, System.Windows.Media.Colors.Purple, "Volume");
                            // Fixed
                            if (chk_FixedRange.Checked)
                                Utility.SetAxis_Fixed(chart_Main, "Volume", (double)nmb_2nd_Axix_Y_Max.Value, (double)nmb_2nd_Axix_Y_Min.Value);
                        }

                    }
                }
                catch (Exception ex)
                {
                    show_Message("Stock Chart Error:" + ex.Message);
                }

                chart_Main.BringToFront();
            }
        }

        public void DrawScatterChart(string _hist_id)
        {
            if (this.chart_Main.InvokeRequired)
            {
                chart_Main.BeginInvoke(new MethodInvoker(delegate () { DrawScatterChart(_hist_id); }));
            }
            else
            {
                try
                {
                    Utility.ResetChart(chart_Main);

                    using (stockEntities stock = new stockEntities())
                    {
                        List<sp_get_prediction_history_Result> results = stock.sp_get_prediction_history(Convert.ToInt32(_hist_id)).ToList();

                        List<double> list1 = new List<double>();
                        List<double> list2 = new List<double>();

                        if (results.Count > 950)
                        {
                            show_Message("More than 999 Rows"); 
                            return;
                        }
                        foreach(var r in results)
                        {
                            if (!r.symbol.StartsWith("_"))
                            {
                                list1.Add(Convert.ToDouble(r.ratio1));
                                list2.Add(Convert.ToDouble(r.ratio2));
                            }
                        }

                        Utility.AddScatter(chart_Main, "Scatter", list1, list2, System.Windows.Media.Colors.Blue);

                    }
                }
                catch (Exception ex)
                {
                    show_Message("Scatter Chart Error:" + ex.Message);
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
                //thd_capture.Start(new object[] { count, chk_Analysis.Checked, nmb_Sleep.Value, _option, _pre_month, _nex_month });
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
                if (analysis_on)
                    Thread.Sleep(sleep * 1000);
                else
                    Thread.Sleep(sleep * 1000);
                SaveChart(r, analysis_on, option);
            }
            pic_Chart.BringToFront();

            CaptureState(false);
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

        private void CaptureChart(int _r, bool _analysis_on, int _option, bool _is_auto = true)
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

                    if (!_is_auto)
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
                if (txt_Log.Lines.Length > 50)
                {
                    txt_Log.Text = string.Empty;
                }
                txt_Log.Text = DateTime.Now.ToString("hh:mm:ss") + "-" + _log + "\r\n" + txt_Log.Text;
            }
        }

        #endregion

    }
}