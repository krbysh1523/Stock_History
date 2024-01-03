using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Forms;

namespace History
{
    #region + DG Custom List
    /// <summary>
    /// Provides a generic collection that supports data binding and additionally supports sorting.
    /// See http://msdn.microsoft.com/en-us/library/ms993236.aspx
    /// If the elements are IComparable it uses that; otherwise compares the ToString()
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class SortableBindingList<T> : BindingList<T> where T : class
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor _sortProperty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        public SortableBindingList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortableBindingList{T}"/> class.
        /// </summary>
        /// <param name="list">An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the <see cref="T:System.ComponentModel.BindingList`1" />.</param>
        public SortableBindingList(IList<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the list supports sorting.
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the list is sorted.
        /// </summary>
        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }

        /// <summary>
        /// Gets the direction the list is sorted.
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirection; }
        }

        /// <summary>
        /// Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class; otherwise, returns null
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortProperty; }
        }

        /// <summary>
        /// Removes any sort applied with ApplySortCore if sorting is implemented
        /// </summary>
        protected override void RemoveSortCore()
        {
            _sortDirection = ListSortDirection.Ascending;
            _sortProperty = null;
            _isSorted = false; //thanks Luca
        }

        /// <summary>
        /// Sorts the items if overridden in a derived class
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="direction"></param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            List<T> list = Items as List<T>;
            if (list == null) return;

            list.Sort(Compare);

            _isSorted = true;
            //fire an event that the list has been changed.
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }


        private int Compare(T lhs, T rhs)
        {
            var result = OnComparison(lhs, rhs);
            //invert if descending
            if (_sortDirection == ListSortDirection.Descending)
                result = -result;
            return result;
        }

        private int OnComparison(T lhs, T rhs)
        {
            object lhsValue = lhs == null ? null : _sortProperty.GetValue(lhs);
            object rhsValue = rhs == null ? null : _sortProperty.GetValue(rhs);
            if (lhsValue == null)
            {
                return (rhsValue == null) ? 0 : -1; //nulls are equal
            }
            if (rhsValue == null)
            {
                return 1; //first has value, second doesn't
            }
            if (lhsValue is IComparable)
            {
                return ((IComparable)lhsValue).CompareTo(rhsValue);
            }
            if (lhsValue.Equals(rhsValue))
            {
                return 0; //both are the same
            }
            //not comparable, compare ToString
            return lhsValue.ToString().CompareTo(rhsValue.ToString());
        }
    }
    #endregion

    #region + Utility
    public static class Utility
    {
        #region + Graph Function
        public static void RetYAxis(LiveCharts.WinForms.CartesianChart _chart_Main, out double _YMax, out double _YMin)
        {
            _YMax = 10;
            _YMin = -10;

            for (int s = 0; s < _chart_Main.Series.Count; s++)
            {
                if (s == 0)
                {
                    _YMax = ((LiveCharts.Helpers.NoisyCollection<double>)_chart_Main.Series[s].Values).Max();
                    _YMin = ((LiveCharts.Helpers.NoisyCollection<double>)_chart_Main.Series[s].Values).Min();
                }
                else
                {
                    if (_YMax < ((LiveCharts.Helpers.NoisyCollection<double>)_chart_Main.Series[s].Values).Max())
                        _YMax = ((LiveCharts.Helpers.NoisyCollection<double>)_chart_Main.Series[s].Values).Max();
                    if (_YMin > ((LiveCharts.Helpers.NoisyCollection<double>)_chart_Main.Series[s].Values).Min())
                        _YMin = ((LiveCharts.Helpers.NoisyCollection<double>)_chart_Main.Series[s].Values).Min();
                }
            }
            Func<double, string> formatFunc = (x) => string.Format("{0:0}", x);
            _chart_Main.AxisY[0].LabelFormatter = formatFunc;
        }

        public static LineSeries AddLine(List<daily_close> _hist, int _sma,
            string _point_shape = "", double _point_size = 0
            )
        {
            LineSeries sma = new LineSeries();
            if (_sma == 1)
            {
                sma.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                sma.Title = "Price";
            }
            else if (_sma == 5)
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
            
            if (_point_shape.ToLower() == "circle")
                sma.PointGeometry = DefaultGeometries.Circle;
            else if (_point_shape.ToLower() == "diamond")
                sma.PointGeometry = DefaultGeometries.Diamond;
            else if (_point_shape.ToLower() == "cross")
                sma.PointGeometry = DefaultGeometries.Cross;
            else if (_point_shape.ToLower() == "square")
                sma.PointGeometry = DefaultGeometries.Square;
            else
                sma.PointGeometry = DefaultGeometries.None;

            sma.PointGeometrySize = _point_size;
            ChartValues<double> sma_points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                if (_sma == 1)
                    sma_points.Add(Math.Round(h.price.Value, 2));
                else if (_sma == 5)
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

        public static void GetXLabels(AxesCollection _x, List<daily_history> _hist)
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
        public static void GetXLabels(AxesCollection _x, List<daily_close> _hist)
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

        public static void GetXLabels(AxesCollection _x, List<daily_strong> _hist)
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
        public static void GetXLabels(AxesCollection _x, List<DateTime> _hist)
        {
            Axis x = new Axis();
            List<string> labels = new List<string>();
            foreach (var h in _hist)
            {
                labels.Add(h.ToString("M/dd"));
            }
            x.Labels = labels;
            _x.Add(x);
        }

        public static void GetXLabels(AxesCollection _x, List<DateTime> _hist, DateTime _end_dttm)
        {
            Axis x = new Axis();
            List<string> labels = new List<string>();
            foreach (var h in _hist)
            {
                labels.Add(h.ToString("M/dd") + "(" + Get_Week_Diff(_end_dttm, h).ToString() + ")");
            }
            x.Labels = labels;
            _x.Add(x);
        }
        public static int Get_Week_Diff(DateTime d1, DateTime d2)
        {
            var diff = StartOfWeek(d2, DayOfWeek.Monday).Subtract(StartOfWeek(d1, DayOfWeek.Monday));

            var weeks = (int)diff.Days / 7;

            return weeks;
        }
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        public static void SetAxis_LineSeries(LiveCharts.WinForms.CartesianChart _cht, LineSeries _series, string _Axis_Name)
        {
            for(int i = 0; i < _cht.AxisY.Count; i++)
            {
                if(_cht.AxisY[i].Name == _Axis_Name)
                {
                    _series.ScalesYAt = i;
                }
            }
        }

        public static void SetAxis_Name(LiveCharts.WinForms.CartesianChart _cht, int _index, string _Axis_Name)
        {
            _cht.AxisY[_index].Name = _Axis_Name;
        }

        public static bool AxisY_null(LiveCharts.WinForms.CartesianChart _cht)
        {
            if (_cht.AxisY.Count > 0)
                return false;
            else
                return true;
        }

        public static int FindAxis_Name(LiveCharts.WinForms.CartesianChart _cht, string _Axis_Name)
        {
            for (int i = 0; i < _cht.AxisY.Count; i++)
            {
                if (_cht.AxisY[i].Name == "")
                {
                    _cht.AxisY.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < _cht.AxisY.Count; i++)
            {
                if (_cht.AxisY[i].Name == _Axis_Name)
                {
                    return i;
                }
            }

            var new_axis_Y = new Axis();
            new_axis_Y.Name = _Axis_Name;
            _cht.AxisY.Add(new_axis_Y);
            return _cht.AxisY.Count - 1;
        }

        public static void SetAxis_Fixed(LiveCharts.WinForms.CartesianChart _cht, string _series_name, double _Max, double _Min, bool _LabelVisible = false)
        {
            _cht.AxisY[FindAxis_Name(_cht, _series_name)].MaxValue = _Max;
            _cht.AxisY[FindAxis_Name(_cht, _series_name)].MinValue = _Min;
            _cht.AxisY[FindAxis_Name(_cht, _series_name)].ShowLabels = _LabelVisible;
        }

        public static void SetAxis(LiveCharts.WinForms.CartesianChart _cht, int _cnt)
        {
            int step = 1;
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
            if (_cht.AxisX.Count > 0)
            {
                _cht.AxisX[0].Separator = new Separator
                {
                    Step = step,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Silver)
                };
            }
            if (_cht.AxisY.Count > 0)
            {
                _cht.AxisY[0].Separator = new Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Silver)
                };
            }
        }

        public static void SetAxis(LiveCharts.WinForms.CartesianChart _cht, int _cnt, double _y_Max, double _y_Min)
        {
            int step = 1;
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
            if (_cht.AxisX.Count > 0)
            {
                foreach (Axis x in _cht.AxisX)
                {
                    x.Separator = new Separator
                    {
                        Step = step,
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gainsboro)
                    };
                }
            }
            if (_cht.AxisY.Count > 0)
            {
                foreach (Axis y in _cht.AxisY)
                {
                    y.Separator = new Separator
                    {
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gainsboro)
                    };
                    y.MaxValue = _y_Max;
                    y.MinValue = _y_Min;
                }
            }
        }
        public static void SetAxis(LiveCharts.WinForms.CartesianChart _cht, int _index, int _cnt, double _y_Max, double _y_Min)
        {
            int step = 1;
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
            if (_cht.AxisX.Count - 1 >= _index)
            {
                _cht.AxisX[_index].Separator = new Separator
                {
                    Step = step,
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gainsboro)
                };
            }
            if (_cht.AxisY.Count - 1 >= _index)
            {
                _cht.AxisY[_index].Separator = new Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gainsboro)
                };
                _cht.AxisY[_index].MaxValue = _y_Max;
                _cht.AxisY[_index].MinValue = _y_Min;
            }
        }
        public static CandleSeries AddCandle(List<daily_history> _hist)
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
        public static ColumnSeries AddColumn(List<double> _hist, System.Windows.Media.Color _color, string _title)
        {
            ColumnSeries series = new ColumnSeries();
            series.Fill = new System.Windows.Media.SolidColorBrush(_color);
            series.Title = _title;

            ChartValues<double> points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                points.Add(h);
            }
            series.Values = points;

            return series;
        }
        public static LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title)
        {
            LineSeries series = new LineSeries();
            series.Stroke = new System.Windows.Media.SolidColorBrush(_color);
            series.Title = _title;
            series.Fill = System.Windows.Media.Brushes.Transparent;
            series.PointGeometry = null;
            ChartValues<double> points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                points.Add(h);
            }
            series.Values = points;

            return series;
        }
        public static LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title, string _name, string _point_shape, double _point_size)
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
        public static LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title, string _name)
        {
            return AddLine(_hist, _color, _title, _name, "circle", 10);
        }
        public static LineSeries AddLine(List<double> _hist, System.Windows.Media.Color _color, string _title, string _name, int _AxisY)
        {
            LineSeries line = AddLine(_hist, _color, _title, _name, "circle", 5);
            line.ScalesYAt = _AxisY;
            return line;
        }

        public static void AddLine_Axis(LiveCharts.WinForms.CartesianChart _cht, List<double> _hist, System.Windows.Media.Color _color, string _series_name)
        {
            var series = AddLine(_hist, _color, _series_name, _series_name);
            series.ScalesYAt = FindAxis_Name(_cht, _series_name);
            _cht.Series.Add(series);
        }

        public static AxisSection AddSection(double _start, double _width, System.Windows.Media.Color _color, double _opacity)
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

        public static LineSeries AddStraightLine(List<DateTime> _hist, double _value, System.Windows.Media.Color _color, string _title)
        {
            LineSeries line = new LineSeries();
            line.Stroke = new System.Windows.Media.SolidColorBrush(_color);
            line.StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 });
            line.Title = _title;

            line.Fill = System.Windows.Media.Brushes.Transparent;
            line.PointGeometry = null;
            ChartValues<double> line_points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                line_points.Add(Math.Round(_value, 2));
            }
            line.Values = line_points;

            return line;
        }

        public static LineSeries AddStraightLine(List<daily_close> _hist, double _value, System.Windows.Media.Color _color, string _title)
        {
            LineSeries line = new LineSeries();
            line.Stroke = new System.Windows.Media.SolidColorBrush(_color);
            line.StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 1 });
            line.Title = _title;

            line.Fill = System.Windows.Media.Brushes.Transparent;
            line.PointGeometry = null;
            ChartValues<double> line_points = new ChartValues<double>();
            foreach (var h in _hist)
            {
                line_points.Add(Math.Round(_value, 2));
            }
            line.Values = line_points;

            return line;
        }
        public static void AddScatter(LiveCharts.WinForms.CartesianChart _cht, string _name, List<double> _list1, List<double> _list2, System.Windows.Media.Color _color)
        {
            ScatterSeries series = new ScatterSeries();
            series.Values = new ChartValues<ObservablePoint>();
            series.Name = _name;
            series.Stroke = new System.Windows.Media.SolidColorBrush(_color);
            series.Title = _name;
            
            for(int i = 0; i < _list1.Count; i++)
            {
                series.Values.Add(new ObservablePoint(_list1[i], _list2[i]));
            }

            _cht.Series.Add(series);

            //Axis
            AxisSection zeroX = new AxisSection();
            zeroX.Value = 0;
            zeroX.SectionWidth =0.1;
            zeroX.Fill = new System.Windows.Media.SolidColorBrush
            {
                Color = _color
            };
            AxisSection zeroY = new AxisSection();
            zeroY.Value = 0;
            zeroY.SectionWidth = 0.1;
            zeroY.Fill = new System.Windows.Media.SolidColorBrush
            {
                Color = _color
            };

            var new_axis_Y = new Axis();
            new_axis_Y.Name = "Scatter_Y";
            new_axis_Y.Sections.Add(zeroY);
            _cht.AxisY.Add(new_axis_Y);

            var new_axis_X = new Axis();
            new_axis_X.Name = "Scatter_X";
            new_axis_X.Sections.Add(zeroX);
            _cht.AxisX.Add(new_axis_X);

            SetAxis(_cht, _list1.Count);

        }
        public static void ResetChart(LiveCharts.WinForms.CartesianChart _cht)
        {
            _cht.AxisX.Clear();

            _cht.AxisY.Clear();
            //while(_cht.AxisY.Count > 1)
            //        _cht.AxisY.RemoveAt(1);

            _cht.Series.Clear();
        }


        public static List<double> ConverToPerc(List<double> _list, int _zero_index, int _decimal)
        {
            List<double> ret = new List<double>();

            double zero = _list[_zero_index];
            if (zero != 0)
            {
                foreach (double d in _list)
                {
                    if ((d - zero) == 0)
                        ret.Add(0);
                    else
                        ret.Add(Math.Round(((d - zero) / zero) * 100, _decimal));
                }
            }
            return ret;
        }
        #endregion

        #region + Utility
        public static string RetChecked(CheckBox _chk_Box)
        {
            if (_chk_Box.Checked)
                return "Y";
            else
                return "N";
        }

        public static void Copy_DGV_to_Clipboard(DataGridView dgv, int _exclue_row, bool Copy_Header = true)
        {

            var newline = System.Environment.NewLine;
            var tab = "\t";
            var clipboard_string = new StringBuilder();
            if (Copy_Header)
            {
                for (int c = 0; c < dgv.Columns.Count; c++)
                {
                    clipboard_string.Append(dgv.Columns[c].HeaderText + tab);
                }
                clipboard_string.Append(newline);
            }
            for (int r = _exclue_row; r < dgv.Rows.Count; r++)
            {
                for (int c = 0; c < dgv.Columns.Count; c++)
                {
                    clipboard_string.Append(dgv[c, r].Value + tab);
                }
                clipboard_string.Append(newline);
            }

            Clipboard.SetText(clipboard_string.ToString());

        }
        #endregion
    }
    #endregion

    #region + Test Generator   
    public class Test_Generator
    {
        List<Test_Plan> plans = new List<Test_Plan>();
       public Test_Generator()
        {
        }

        public void Generate(int _start_no, int _total)
        {
            Test_Plan plan = new Test_Plan(_total);
            plan.set_plan(0, _start_no);
            for(int i = 1;i<_total;i++)
            {
                plan.set_plan(i, plan.ret_smallest_unused());
            }

            int init = 1;
            int rev = _total - 1;
            while(!is_exist(plan))
            {
                plan.swap(rev);
                rev--;
            }
        }

        private bool is_exist(Test_Plan _plan)
        {
            foreach(var p in plans)
            {
                if(p.is_match(_plan.plan))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Test_Plan
    {
        public List<int> unused = new List<int>();
        public int[] plan;

        public Test_Plan(int _total)
        {
            plan = new int[_total];
            for (int i = 1; i <= _total; i++)
                unused.Add(i);
        }

        public int ret_smallest_unused()
        {
            return unused.Min();
        }
        public int ret_smallest_unused(int _min_value)
        {
            return unused.Where(r => r > _min_value).Min();
        }

        public void set_plan(int _position, int _option)
        {
            plan[_position] = _option;
            unused.Remove(_option);
        }

        public void remove(int _position)
        {
            unused.Add(plan[_position]);
        }

        public int get_plan(int _position)
        {
            return plan[_position];
        }

        public void swap(int _position)
        {
            int t = plan[_position -1];
            plan[_position - 1] = plan[_position];
            plan[_position] = t;
        }

        public bool is_match(int[] _plan)
        {
            if (this.plan == _plan)
                return true;
            else
                return false;
        }

    }
    #endregion
}
