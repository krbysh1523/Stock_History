using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Analysis()
        {
            InitializeComponent();
            splitContainer1_SizeChanged(this, null);
        }

        private void Analysis_Load(object sender, EventArgs e)
        {
            dt_From.Value = DateTime.Today.AddDays(-30);
            dt_To.Value = DateTime.Today.AddDays(-1);

            cmb_Index.SelectedIndex = 0;

            LoadSymbols();
        }

        private void LoadSymbols()
        {
            using(StockEntity stock = new StockEntity())
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

        private void btn_Symbol_Click(object sender, EventArgs e)
        {
            if (txt_Symbol.Text == "")
                MessageBox.Show("Enter Symbols");

            DrawGraph(chart_Main, txt_Symbol.Text, "Candle");
            DrawGraph(chart_Main, txt_Symbol.Text, "SMA");
            DrawGraph(chart_MainVol, txt_Symbol.Text, "Volume");

            DrawGraph(chart_Chg, txt_Symbol.Text, cmb_Index.Text, "Change");
        }

        private void btn_Index_Click(object sender, EventArgs e)
        {
            DrawGraph(chart_Index, cmb_Index.Text, "Candle");
            DrawGraph(chart_Index, cmb_Index.Text, "SMA");
            DrawGraph(chart_IndexVol, cmb_Index.Text, "Volume");
        }
        private void DrawGraph(LiveCharts.WinForms.CartesianChart _cht, string _symbol, string _type)
        {
            using (StockEntity stock = new StockEntity())
            {
                if (_type == "SMA")
                {
                    List<daily_close> hists = new List<daily_close>();
                    hists = stock.daily_close.Where(r => r.symbol == _symbol &&
                    r.date_hist >= dt_From.Value && r.date_hist <= dt_To.Value).OrderBy(r => r.date_hist).ToList();

                    _cht.Series.Add(AddLine(hists, 240));
                    _cht.Series.Add(AddLine(hists, 120));
                    _cht.Series.Add(AddLine(hists, 60));
                    _cht.Series.Add(AddLine(hists, 30));
                    _cht.Series.Add(AddLine(hists, 20));
                    _cht.Series.Add(AddLine(hists, 10));
                    _cht.Series.Add(AddLine(hists, 5));

                }
                else if (_type == "Volume")
                {
                    _cht.AxisX.Clear();
                    _cht.Series.Clear();
                    List<daily_history> hists = new List<daily_history>();
                    hists = stock.daily_history.Where(r => r.symbol == _symbol &&
                    r.date >= dt_From.Value && r.date <= dt_To.Value).OrderBy(r => r.date).ToList();

                    _cht.Series.Add(AddColumn(hists));
                    GetXLabels(_cht.AxisX, hists);
                    SetAxis(_cht, hists.Count);
                }
                else if (_type == "Candle")
                {
                    _cht.AxisX.Clear();
                    _cht.Series.Clear();
                    List<daily_history> hists = new List<daily_history>();
                    hists = stock.daily_history.Where(r => r.symbol == _symbol &&
                    r.date >= dt_From.Value && r.date <= dt_To.Value).OrderBy(r => r.date).ToList();

                    _cht.Series.Add(AddCandle(hists));
                    GetXLabels(_cht.AxisX, hists);
                    SetAxis(_cht, hists.Count);
                }
            }
        }

        private void DrawGraph(LiveCharts.WinForms.CartesianChart _cht, string _symbol, string _index, string _type)
        {
            using (StockEntity stock = new StockEntity())
            {
                if (_type == "Change")
                {
                    _cht.AxisX.Clear();
                    _cht.Series.Clear();
                    List<daily_strong> symbols = new List<daily_strong>();
                    symbols = stock.daily_strong.Where(r => r.symbol == _symbol &&
                    r.date_hist >= dt_From.Value && r.date_hist <= dt_To.Value).OrderBy(r => r.date_hist).ToList();

                    List<daily_strong> indexes = new List<daily_strong>();
                    indexes = stock.daily_strong.Where(r => r.symbol == _index &&
                    r.date_hist >= dt_From.Value && r.date_hist <= dt_To.Value).OrderBy(r => r.date_hist).ToList();

                    List<double> chg = new List<double>();
                    for(int i = 0; i < symbols.Count; i++)
                    {
                        chg.Add(symbols[i].price_chg_perc.Value - indexes[i].price_chg_perc.Value);
                    }

                    _cht.Series.Add(AddColumn(chg));
                    GetXLabels(_cht.AxisX, indexes);
                    SetAxis(_cht, chg.Count);
                }
            }
        }

        private CandleSeries AddCandle(List<daily_history> _hist)
        {
            CandleSeries series = new CandleSeries();
            series.Title = "";
            ChartValues<OhlcPoint> points = new ChartValues<OhlcPoint>();
            foreach (var h in _hist)
            {
                points.Add(new OhlcPoint()
                {
                    Close = Math.Round(h.close.Value, 2),
                    Open = Math.Round(h.open.Value, 2),
                    High = Math.Round(h.high.Value, 2),
                    Low = Math.Round(h.low.Value, 2)
                });
            }
            series.Values = points;

            return series;
        }
        private ColumnSeries AddColumn(List<daily_history> _hist)
        {
            ColumnSeries series = new ColumnSeries();
            series.Title = "";

            ChartValues<double> points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                points.Add(h.volume.Value);
            }
            series.Values = points;

            return series;
        }
        private ColumnSeries AddColumn(List<double> _hist)
        {
            ColumnSeries series = new ColumnSeries();
            series.Title = "";

            ChartValues<double> points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                points.Add(h);
            }
            series.Values = points;

            return series;
        }
        private LineSeries AddLine(List<daily_close> _hist, int _sma)
        {
            LineSeries sma = new LineSeries();
            if (_sma == 5)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Fuchsia);
                sma.Title = "SMA " + _sma.ToString();
            }
            else if (_sma == 10)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Aqua);
                sma.Title = "SMA " + _sma.ToString();
            }
            else if (_sma == 20)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Tomato);
                sma.Title = "SMA " + _sma.ToString();
            }
            else if (_sma == 30)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                sma.Title = "SMA " + _sma.ToString();
            }
            else if (_sma == 60)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                sma.Title = "SMA " + _sma.ToString();
            }
            else if (_sma == 120)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                sma.Title = "SMA " + _sma.ToString();
            }
            else if (_sma == 240)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                sma.Title = "SMA " + _sma.ToString();
            }
            sma.Fill = System.Windows.Media.Brushes.Transparent;
            sma.PointGeometry = null;
            ChartValues<double> sma_points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                if (_sma == 5)
                    sma_points.Add(Math.Round(h.SMA_005.Value, 2));
                else if (_sma == 10)
                    sma_points.Add(Math.Round(h.SMA_010.Value, 2));
                else if (_sma == 20)
                    sma_points.Add(Math.Round(h.SMA_020.Value, 2));
                else if (_sma == 30)
                    sma_points.Add(Math.Round(h.SMA_030.Value, 2));
                else if (_sma == 60)
                    sma_points.Add(Math.Round(h.SMA_060.Value, 2));
                else if (_sma == 120)
                    sma_points.Add(Math.Round(h.SMA_120.Value, 2));
                else if (_sma == 240)
                    sma_points.Add(Math.Round(h.SMA_240.Value, 2));

            }
            sma.Values = sma_points;

            return sma;
        }

        private void GetXLabels(AxesCollection _x, List<daily_history> _hist)
        {
            Axis x = new Axis();
            List<string> labels = new List<string>();
            foreach (var h in _hist)
            {
                labels.Add(h.date.ToString("M/dd"));
            }
            x.Labels = labels;
            _x.Add(x);
        }
        private void GetXLabels(AxesCollection _x, List<daily_close> _hist)
        {
            Axis x = new Axis();
            List<string> labels = new List<string>();
            foreach (var h in _hist)
            {
                labels.Add(h.date_hist.ToString("M/dd"));
            }
            x.Labels = labels;
            _x.Add(x);
        }

        private void GetXLabels(AxesCollection _x, List<daily_strong> _hist)
        {
            Axis x = new Axis();
            List<string> labels = new List<string>();
            foreach (var h in _hist)
            {
                labels.Add(h.date_hist.ToString("M/dd"));
            }
            x.Labels = labels;
            _x.Add(x);
        }

        private void SetAxis(LiveCharts.WinForms.CartesianChart _cht, int _cnt)
        {
            int step = 2;
            if (_cnt > 40)
                step = 4;
            else if (_cnt > 60)
                step = 6;
            else if (_cnt > 80)
                step = 8;
            else if (_cnt > 100)
                step = 10;
            else if (_cnt > 140)
                step = 12;
            _cht.AxisX[0].Separator = new Separator
            {
                Step = step,
                StrokeThickness = 1,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(135, 206, 235))
            };
            _cht.AxisY[0].Separator = new Separator
            {
                StrokeThickness = 1,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(22, 255, 22))
            };
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer1.Size.Width > 0 && splitContainer1.Size.Height > 0)
            {
                splitContainer1.SplitterDistance = (splitContainer1.Size.Width / 2);
                splitContainer2.SplitterDistance = (splitContainer1.Size.Height / 10 * 6);
                splitContainer3.SplitterDistance = (splitContainer1.Size.Height / 10 * 6);
                splitContainer4.SplitterDistance = (splitContainer1.Size.Height / 10 * 2);
                splitContainer5.SplitterDistance = (splitContainer1.Size.Height / 10 * 2);
            }
        }

        private void txt_Symbol_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btn_Symbol_Click(this, null);
        }

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

        private void txt_Symbol_MouseClick(object sender, MouseEventArgs e)
        {
            txt_Symbol.SelectAll();
        }

        private void btn_Screenshot_Click(object sender, EventArgs e)
        {
            Rectangle bounds = this.Bounds;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                Clipboard.SetImage(bitmap);
            }
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
    }
}