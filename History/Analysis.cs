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
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Drawing;
using System.Drawing.Imaging;

namespace History
{
    public partial class Analysis : Form
    {
        #region + Forms
        Query query = null;

        public Analysis()
        {
            InitializeComponent();
        }
        public Analysis(Query _query, Point _location)
        {
            query = _query;
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = _location;
        }

        private void Analysis_Load(object sender, EventArgs e)
        {
            splitContainer1_SizeChanged(this, null);
            dt_From.Value = DateTime.Today.AddDays(-30);
            dt_To.Value = DateTime.Today.AddDays(-1);

            cmb_Index.SelectedIndex = 0;

            splitContainer1.Panel2.Hide();
            splitContainer1.Panel2Collapsed = true;

            LoadSymbols();

            LoadLast();
        }
        private void Analysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLast();
            query.CloseChild(this);
        }

        private void LoadSymbols()
        {
            using(stockEntity stock = new stockEntity())
            {
                List<watch_list> symbols = new List<watch_list>();
                symbols = stock.watch_list.OrderBy(r => r.symbol).ToList();

                var source = new AutoCompleteStringCollection();
                foreach (var s in symbols)
                    source.Add(s.symbol);
                txt_Symbol.AutoCompleteCustomSource = source;
                txt_Symbol.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_Symbol.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        string ConfigFile = "Config.txt";
        private void LoadLast()
        {
            string config = System.Environment.CurrentDirectory + "\\" + ConfigFile;

            string[] lines = System.IO.File.ReadAllLines(config);
            foreach(var line in lines)
            {
                string[] l = line.Split(',');
                if (l[0] == "FROM_DTTM")
                    dt_From.Value = Convert.ToDateTime(l[1]);
                else if (l[0] == "TO_DTTM")
                    dt_To.Value = Convert.ToDateTime(l[1]);
                else if (l[0] == "SYMBOL")
                    txt_Symbol.Text = l[1];
            }
        }

        private void SaveLast()
        {
            string config = System.Environment.CurrentDirectory + "\\" + ConfigFile;

            File.WriteAllText(config, String.Empty);

            using (StreamWriter writer = new StreamWriter(config))
            {
                writer.WriteLine("FROM_DTTM," + dt_From.Value.ToString("MM/dd/yyyy"));
                writer.WriteLine("TO_DTTM," + dt_To.Value.ToString("MM/dd/yyyy"));
                writer.WriteLine("SYMBOL," + txt_Symbol.Text);
            }
        }
        #endregion

        #region + Button Graph
        public void btn_Symbol_Click(object sender, EventArgs e)
        {
            if (txt_Symbol.Text == "")
                MessageBox.Show("Enter Symbols");

            Utility.ResetChart(chart_Main);
            DrawGraph(chart_Main, txt_Symbol.Text, "Perc", dt_From.Value, dt_To.Value);
            DrawGraph(chart_Main, txt_Symbol.Text, "Candle", dt_From.Value, dt_To.Value);
            DrawGraph(chart_Main, txt_Symbol.Text, "SMA", dt_From.Value, dt_To.Value);

            Utility.ResetChart(chart_MainVol);
            DrawGraph(chart_MainVol, txt_Symbol.Text, "Volume", dt_From.Value, dt_To.Value);
            chart_MainVol.DisableAnimations = true;

            Utility.ResetChart(chart_Earning);
            DrawGraph(chart_Earning, txt_Symbol.Text, "Earning", dt_From.Value.AddYears(-1), dt_To.Value);
            chart_Earning.DisableAnimations = true;

            Utility.ResetChart(chart_Chg);
            DrawGraph(chart_Chg, txt_Symbol.Text, cmb_Index.Text, "Change", dt_From.Value, dt_To.Value);
            chart_Chg.DisableAnimations = true;

            txt_Remark.Text = "";
        }

        private void btn_Index_Click(object sender, EventArgs e)
        {
            Utility.ResetChart(chart_Index);
            DrawGraph(chart_Index, cmb_Index.Text, "Perc", dt_From.Value, dt_To.Value);
            DrawGraph(chart_Index, cmb_Index.Text, "Candle", dt_From.Value, dt_To.Value);
            DrawGraph(chart_Index, cmb_Index.Text, "SMA", dt_From.Value, dt_To.Value);
        }

        public void btn_Next_Click(object sender, EventArgs e)
        {
            if (txt_Symbol.Text == "")
                MessageBox.Show("Enter Symbols");

            Utility.ResetChart(chart_Index);
            DrawGraph(chart_Index, txt_Symbol.Text, "Perc", dt_To.Value.AddDays(1), dt_To.Value.AddMonths((int)nmb_Next.Value));
            DrawGraph(chart_Index, txt_Symbol.Text, "Candle", dt_To.Value.AddDays(1), dt_To.Value.AddMonths((int)nmb_Next.Value));
            DrawGraph(chart_Index, txt_Symbol.Text, "SMA", dt_To.Value.AddDays(1), dt_To.Value.AddMonths((int)nmb_Next.Value));

            Utility.ResetChart(chart_IndexVol);
            DrawGraph(chart_IndexVol, txt_Symbol.Text, "Volume", dt_To.Value.AddDays(1), dt_To.Value.AddMonths((int)nmb_Next.Value));
            chart_IndexVol.DisableAnimations = true;

            Utility.ResetChart(chart_IndexEarning); 
            DrawGraph(chart_IndexEarning, txt_Symbol.Text, "Earning", dt_To.Value.AddDays(1).AddYears(-1), dt_To.Value.AddMonths((int)nmb_Next.Value));
            chart_IndexEarning.DisableAnimations = true;

            Utility.ResetChart(chart_IndexChg);
            DrawGraph(chart_IndexChg, txt_Symbol.Text, cmb_Index.Text, "Change", dt_To.Value.AddDays(1), dt_To.Value.AddMonths((int)nmb_Next.Value));
            chart_IndexChg.DisableAnimations = true;

        }

        private void DrawGraph(LiveCharts.WinForms.CartesianChart _cht, string _symbol, string _type, DateTime _dt_from, DateTime _dt_to)
        {
            using (stockEntity stock = new stockEntity())
            {
                if (_type == "SMA")
                {
                    List<daily_close> hists = new List<daily_close>();
                    hists = stock.daily_close.Where(r => r.symbol == _symbol &&
                    r.date_hist >= _dt_from && r.date_hist <= _dt_to).OrderBy(r => r.date_hist).ToList();

                    _cht.Series.Add(Utility.AddLine(hists, 240));
                    _cht.Series.Add(Utility.AddLine(hists, 120));
                    _cht.Series.Add(Utility.AddLine(hists, 60));
                    _cht.Series.Add(Utility.AddLine(hists, 30));
                    _cht.Series.Add(Utility.AddLine(hists, 20));
                    _cht.Series.Add(Utility.AddLine(hists, 10));
                    _cht.Series.Add(Utility.AddLine(hists, 5));

                }
                else if (_type == "Perc")
                {
                    List<daily_close> hists = new List<daily_close>();
                    hists = stock.daily_close.Where(r => r.symbol == _symbol &&
                    r.date_hist >= _dt_from && r.date_hist <= _dt_to).OrderBy(r => r.date_hist).ToList();

                    double last_close = hists.OrderBy(r => r.date_hist).Last().price.Value;

                    _cht.Series.Add(Utility.AddStraightLine(hists,
                        last_close + (last_close * 0.05),
                        System.Windows.Media.Colors.Black,
                        "+5%"));

                    _cht.Series.Add(Utility.AddStraightLine(hists,
                        last_close - (last_close * 0.05),
                        System.Windows.Media.Colors.Black,
                        "-5%"));

                    _cht.Series.Add(Utility.AddStraightLine(hists,
                        last_close + (last_close * 0.1),
                        System.Windows.Media.Colors.Gray,
                        "+10%"));

                    _cht.Series.Add(Utility.AddStraightLine(hists,
                        last_close - (last_close * 0.1),
                        System.Windows.Media.Colors.Gray,
                        "-10%"));

                }
                else if (_type == "Volume")
                {
                    List<daily_volume> hists = new List<daily_volume>();
                    hists = stock.daily_volume.Where(r => r.symbol == _symbol &&
                    r.date_hist >= _dt_from && r.date_hist <= _dt_to).OrderBy(r => r.date_hist).ToList();

                    List<double> chg = new List<double>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        if (hists[i].volume.HasValue)
                            chg.Add(hists[i].volume.Value);
                    }

                    _cht.Series.Add(Utility.AddColumn(chg, System.Windows.Media.Colors.Blue, "Volume"));

                    chg = new List<double>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        if (hists[i].SMA_020.HasValue)
                            chg.Add(hists[i].SMA_020.Value);
                    }

                    _cht.Series.Add(Utility.AddLine(chg, System.Windows.Media.Colors.Tomato, "SMA 20"));

                    List<DateTime> dts = new List<DateTime>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        dts.Add(hists[i].date_hist.Date);
                    }

                    Utility.GetXLabels(_cht.AxisX, dts);
                    Utility.SetAxis(_cht, hists.Count);

                    if (chk_highlight.Checked)
                    {
                        var start_idx = hists.FindIndex(r => r.date_hist == dt_From_Highlight.Value);
                        var end_idx = hists.FindIndex(r => r.date_hist == dt_To_Highlight.Value);

                        _cht.AxisX[0].Sections.Clear();
                        _cht.AxisX[0].Sections.Add(Utility.AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));
                    }
                }
                else if (_type == "Earning")
                {
                    List<fundamental> hists = new List<fundamental>();
                    hists = stock.fundamentals.Where(r => r.symbol == _symbol &&
                    r.date_hist >= _dt_from && r.date_hist <= _dt_to).OrderBy(r => r.date_hist).ToList();

                    if (hists.Count < 1)
                        return;

                    List<double> chg = new List<double>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        if (hists[i].perc_est.HasValue)
                            chg.Add(hists[i].perc_est.Value);
                    }

                    _cht.Series.Add(Utility.AddColumn(chg, System.Windows.Media.Colors.Blue, "Est"));

                    chg = new List<double>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        if (hists[i].perc_eps.HasValue)
                            chg.Add(hists[i].perc_eps.Value);
                    }
                    _cht.Series.Add(Utility.AddColumn(chg, System.Windows.Media.Colors.Green, "EPS"));

                    chg = new List<double>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        if (hists[i].surprise.HasValue)
                            chg.Add(hists[i].surprise.Value);
                    }
                    _cht.Series.Add(Utility.AddColumn(chg, System.Windows.Media.Colors.Red, "Surprise"));

                    List<DateTime> dts = new List<DateTime>();
                    for (int i = 0; i < hists.Count; i++)
                    {
                        dts.Add(hists[i].date_hist.Date);
                    }

                    Utility.GetXLabels(_cht.AxisX, dts);
                    Utility.SetAxis(_cht, hists.Count);
                }
                else if (_type == "Candle")
                {
                    List<daily_history> hists = new List<daily_history>();
                    hists = stock.daily_history.Where(r => r.symbol == _symbol &&
                    r.date >= _dt_from && r.date <= _dt_to).OrderBy(r => r.date).ToList();

                    _cht.Series.Add(Utility.AddCandle(hists));
                    Utility.GetXLabels(_cht.AxisX, hists);
                    Utility.SetAxis(_cht, hists.Count);

                    if (chk_highlight.Checked)
                    {
                        var start_idx = hists.FindIndex(r => r.date == dt_From_Highlight.Value);
                        var end_idx = hists.FindIndex(r => r.date == dt_To_Highlight.Value);

                        _cht.AxisX[0].Sections.Clear();
                        _cht.AxisX[0].Sections.Add(Utility.AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));
                    }
                }
            }
        }

        private void DrawGraph(LiveCharts.WinForms.CartesianChart _cht, string _symbol, string _index, string _type, DateTime _dt_from, DateTime _dt_to)
        {
            using (stockEntity stock = new stockEntity())
            {
                if (_type == "Change")
                {
                    List<daily_strong> symbols = new List<daily_strong>();
                    symbols = stock.daily_strong.Where(r => r.symbol == _symbol &&
                    r.date_hist >= _dt_from && r.date_hist <= _dt_to).OrderBy(r => r.date_hist).ToList();

                    List<daily_strong> indexes = new List<daily_strong>();
                    indexes = stock.daily_strong.Where(r => r.symbol == _index &&
                    r.date_hist >= _dt_from && r.date_hist <= _dt_to).OrderBy(r => r.date_hist).ToList();

                    List<double> chg = new List<double>();
                    for(int i = 0; i < symbols.Count; i++)
                    {
                        if (symbols[i].price_chg_perc.HasValue)
                            chg.Add(symbols[i].price_chg_perc.Value - indexes[i].price_chg_perc.Value);
                        else
                            chg.Add(0);
                    }

                    _cht.Series.Add(Utility.AddColumn(chg, System.Windows.Media.Colors.Green, "%"));
                    Utility.GetXLabels(_cht.AxisX, indexes);
                    Utility.SetAxis(_cht, chg.Count);

                    if (chk_highlight.Checked)
                    {
                        var start_idx = symbols.FindIndex(r => r.date_hist == dt_From_Highlight.Value);
                        var end_idx = symbols.FindIndex(r => r.date_hist == dt_To_Highlight.Value);

                        _cht.AxisX[0].Sections.Clear();
                        _cht.AxisX[0].Sections.Add(Utility.AddSection(start_idx, end_idx - start_idx, System.Windows.Media.Colors.Purple, 0.4));
                    }
                }
            }
        }


        #endregion

        #region + User Event
        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer1.Size.Width > 0 && splitContainer1.Size.Height > 0)
            {
                splitContainer1.SplitterDistance = (splitContainer1.Size.Width / 2);
                splitContainer2.SplitterDistance = (splitContainer1.Size.Height / 10 * 6);
                splitContainer3.SplitterDistance = (splitContainer1.Size.Height / 10 * 6);
                splitContainer4.SplitterDistance = (splitContainer1.Size.Height / 10 * 2);
                splitContainer5.SplitterDistance = (splitContainer1.Size.Height / 10 * 2);
                splitContainer6.SplitterDistance = (splitContainer1.Size.Height / 10 * 1);
            }
        }

        private void txt_Symbol_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btn_Symbol_Click(this, null);
        }
        private void txt_Symbol_MouseClick(object sender, MouseEventArgs e)
        {
            txt_Symbol.SelectAll();
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

        private void btn_HideIndex_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            { 
                splitContainer1.Panel2.Show();
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                splitContainer1.Panel2.Hide();
                splitContainer1.Panel2Collapsed = true;
            }
        }
        public void Save_Screenshot(string _directory, string _file)
        {
            Rectangle bounds = this.Bounds;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                if (!Directory.Exists(_directory))
                    Directory.CreateDirectory(_directory);

                string fullpath = _directory + "\\" + _file + ".png";
                bitmap.Save(fullpath, ImageFormat.Png);
            }
        }
        #endregion

    }
}