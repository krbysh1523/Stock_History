using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;

namespace Helper
{
    public partial class Import : Form
    {

        #region + Form
        public Import()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Import_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt_Weekly.Value = get_Sunday(DateTime.Today);
        }

        private DateTime get_Sunday(DateTime _dttm)
        {
            DateTime dttm = _dttm;
            while (true)
            {
                if (dttm.DayOfWeek == DayOfWeek.Sunday)
                    return dttm;
                else
                    dttm = dttm.AddDays(-1);
            }
        }

        private string[] Ret_Checked(DataGridView _chk_List, int _index = 2)
        {
            List<string> chkd = new List<string>();
            foreach (DataGridViewRow r in _chk_List.Rows)
            {
                bool isSelected = Convert.ToBoolean(r.Cells[0].Value);
                if (isSelected)
                {
                    chkd.Add((string)r.Cells[_index].Value.ToString());
                }
            }
            return chkd.ToArray();
        }

        private string[] Ret_Checked(CheckedListBox _chk_List)
        {
            List<string> chkd = new List<string>();
            foreach (var c in _chk_List.CheckedItems)
            {
                chkd.Add((string)c);
            }
            return chkd.ToArray();
        }
        #endregion

        #region + Symbols
        private void chk_CheckAll_Symbol_Click(object sender, EventArgs e)
        {
            Check_All(chk_Symbols);
        }

        private void Check_All(DataGridView _chk_all)
        {
            DataGridViewCheckBoxCell chkchecking = _chk_all.Rows[0].Cells[0] as DataGridViewCheckBoxCell;

            if (Convert.ToBoolean(chkchecking.Value) == true)
            {
                foreach (DataGridViewRow r in _chk_all.Rows)
                    r.Cells[0].Value = false;
            }
            else
            {
                foreach (DataGridViewRow r in _chk_all.Rows)
                    r.Cells[0].Value = true;
            }
        }

        private void Check_All(CheckedListBox _chk_all)
        {
            if (_chk_all.CheckedItems.Count > 0)
            {
                foreach (var i in _chk_all.CheckedIndices)
                    _chk_all.SetItemChecked((int)i, false);
            }
            else
            {
                for (int i = 0; i < _chk_all.Items.Count; i++)
                    _chk_all.SetItemChecked(i, true);
            }
        }
        private void chk_Selected_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell c in chk_Symbols.SelectedCells)
            {
                int row = c.RowIndex;
                DataGridViewCheckBoxCell chkchecking = chk_Symbols.Rows[row].Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chkchecking.Value) == true)
                {
                    chk_Symbols.Rows[row].Cells[0].Value = false;
                }
                else
                {
                    chk_Symbols.Rows[row].Cells[0].Value = true;
                }
            }
        }
        private void btn_Test_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in chk_Symbols.Rows)
            {
                if (r.Cells["last_earning"].Value == null)
                    r.Cells[0].Value = true;
                //else if( Convert.ToDateTime(r.Cells["last_updated"].Value) < max_dttm)
                //    r.Cells[0].Value = true;
            }
        }

        #endregion

        #region + Process
        private void btn_ProcAnalysis_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ProcAnalysis));
            t.Start();
        }
        private void ProcAnalysis()
        {
            using (stockEntities stock = new stockEntities())
            {
                stock.Database.CommandTimeout = 300000;

                var symbols = Ret_Checked(chk_Symbols, 1);
                for (int s = 0; s < symbols.Length; s++)
                {
                    // Change of basic
                    stock.process_analysis(symbols[s]);
                    show_Message("Process Analysis " + symbols[s] + " Finished (" + (s + 1).ToString() + "/" + (symbols.Length + 1).ToString());
                }
            }
        }
        private void btn_LoadAnalysis_Click(object sender, EventArgs e)
        {
            using (stockEntities stock = new stockEntities())
            {
                try
                {
                    SortableBindingList<Symbol> symbols = new SortableBindingList<Symbol>();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" select distinct dh.symbol       ");
                    sb.Append(" FROM   dbo.daily_history dh     ");
                    sb.Append(" WHERE  NOT EXISTS (SELECT 1     ");
                    sb.Append(" FROM   dbo.daily_analysis da    ");
                    sb.Append(" WHERE dh.symbol = da.symbol     ");
                    sb.Append(" AND   dh.[date] = da.date_hist) ");
                    var results = stock.Database.SqlQuery<string>(sb.ToString()).ToList();

                    foreach (var r in results)
                        symbols.Add(new Symbol(r));

                    chk_Symbols.DataSource = symbols;
                    chk_Symbols.ReadOnly = false;

                    Add_CheckColumn();

                    chk_CheckAll_Symbol_Click(this, null);

                }
                catch (Exception ex)
                {
                    show_Message("URL Error:" + ex.Message);
                }
            }
        }
        #endregion

        #region + Database Format
        private void btn_View_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(UpdateView));
            t.Start();
        }
        private void UpdateView()
        {
            using (stockEntities stock = new stockEntities())
            {
                stock.Database.CommandTimeout = 300000;
                stock.Database.ExecuteSqlCommand(" exec update_daily_v1 ", new object[] { });
                stock.Database.ExecuteSqlCommand(" exec update_daily_analysis_v1 ", new object[] { });
                show_Message("Updating View Finished");
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

        #region + Import Full History
        private void btn_Open_Click(object sender, EventArgs e)
        {
            SortableBindingList<TextFile> symbols = new SortableBindingList<TextFile>();

            var open = new FolderBrowserDialog();
            open.SelectedPath = @"C:\Users\Sukho\Downloads";
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                var files = Directory.GetFiles(open.SelectedPath, "*us*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var file_name = Path.GetFileNameWithoutExtension(file).Replace(".us", "").ToUpper();
                    symbols.Add(new TextFile(file_name, file));
                }
            }
            chk_Symbols.DataSource = symbols;
            chk_Symbols.ReadOnly = false;

            Add_CheckColumn();
        }
        private void btn_Download_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Import_Full));
            t.Start();
        }
        private void Import_Full()
        {
            foreach (var s in Ret_Checked(chk_Symbols))
            {
                var symbol = Path.GetFileNameWithoutExtension(s).Replace(".us", "").ToUpper();
                Process_CSV(symbol, s);

                //Process_File(symbol, File.ReadAllLines(s));
            }
            show_Message("Completed");
        }

        private void Process_CSV(string _symbol, string _fileName)
        {
            show_Message(_symbol + " starting");
            List<daily_history> hists = new List<daily_history>();
            var Max_Date = SQL_GetMaxDate(_symbol);
            var line_date = DateTime.Now;
            using (var csv = new CachedCsvReader(new StreamReader(_fileName), true))
            {
                foreach(var line in csv)
                {
                    line_date = DateTime.ParseExact(line[2], "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (line_date > Max_Date)
                    {
                        var h = new daily_history();
                        h.symbol = _symbol;
                        h.date = line_date;
                        h.open = double.Parse(line[4]);
                        h.high = double.Parse(line[5]);
                        h.low = double.Parse(line[6]);
                        h.close = double.Parse(line[7]);
                        h.volume = double.Parse(line[8]);
                        hists.Add(h);
                    }
                }
            }

            using (stockEntities stock = new stockEntities())
            {
                stock.daily_history.AddRange(hists);
                try
                {
                    stock.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    show_Message(_symbol + " : Error - " + ex.InnerException.InnerException.Message);
                }
                show_Message(_symbol + " has been imported");
            }
        }

        //private void Process_File(string _symbol, string[] _lines)
        //{
        //    show_Message(_symbol + " starting");

        //    List<daily_history> hists = new List<daily_history>();

        //    foreach (var l in _lines)
        //    {
        //        string[] sss = l.Split(new char[] { ',' });
        //        if (sss.Length == 10 && sss[0].Contains("US"))
        //        {
        //            var h = new daily_history();
        //            h.symbol = _symbol;
        //            h.date = DateTime.ParseExact(sss[2], "yyyyMMdd", CultureInfo.InvariantCulture);
        //            h.open = double.Parse(sss[4]);
        //            h.high = double.Parse(sss[5]);
        //            h.low = double.Parse(sss[6]);
        //            h.close = double.Parse(sss[7]);
        //            h.volume = double.Parse(sss[8]);
        //            hists.Add(h);
        //        }
        //    }

        //    using (stockEntities stock = new stockEntities())
        //    {
        //        stock.daily_history.AddRange(hists);
        //        try
        //        {
        //            stock.SaveChanges();
        //        }
        //        catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
        //        {
        //            show_Message(_symbol + " : Error - " + ex.InnerException.InnerException.Message);
        //        }
        //        show_Message(_symbol + " has been imported");
        //    }
        //}
                
        private DateTime SQL_GetMaxDate(string _symbol)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT MAX(dh.date) AS max_date ");
            sb.Append(" FROM   dbo.daily_history dh     ");
            sb.Append(" WHERE dh.symbol = '" + _symbol + "' ");
            using (stockEntities stock = new stockEntities())
            {
                try
                {
                    var results = stock.Database.SqlQuery<DateTime>(sb.ToString()).ToList();

                    return results[0];
                }
                catch(Exception ex)
                {
                    return DateTime.Parse("2001/1/1");
                }
            }
        }
        #endregion

        #region + Import Daily History
        private void btn_Open_Daily_Click(object sender, EventArgs e)
        {
            SortableBindingList<TextFile> symbols = new SortableBindingList<TextFile>();

            var open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Users\Sukho\Downloads";
            open.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                symbols.Add(new TextFile("DAILY", open.FileName));
            }
            chk_Symbols.DataSource = symbols;
            chk_Symbols.ReadOnly = false;

            Add_CheckColumn();

            chk_CheckAll_Symbol_Click(this, null);
        }

        private void btn_ImportDaily_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Import_Daily));
            t.Start();
        }
        private void Import_Daily()
        {
            foreach (var s in Ret_Checked(chk_Symbols))
            {
                var file = Path.GetFileNameWithoutExtension(s);
                Process_Line(file, File.ReadAllLines(s));
            }
            show_Message("Completed");
        }

        private void Process_Line(string _file, string[] _lines)
        {
            show_Message(_file + " starting");

            foreach (var l in _lines)
            {
                string[] sss = l.Split(new char[] { ',' });
                if (sss.Length == 10 && sss[0].Contains("US") &&
                    !sss[0].StartsWith("<") && !sss[0].StartsWith("^"))
                {
                    var h = new daily_history();
                    h.symbol = sss[0].Replace(".US", "").ToUpper();
                    h.date = DateTime.ParseExact(sss[2], "yyyyMMdd", CultureInfo.InvariantCulture);
                    h.open = double.Parse(sss[4]);
                    h.high = double.Parse(sss[5]);
                    h.low = double.Parse(sss[6]);
                    h.close = double.Parse(sss[7]);
                    h.volume = double.Parse(sss[8]);

                    using (stockEntities stock = new stockEntities())
                    {
                        stock.daily_history.Add(h);
                        try
                        {
                            stock.SaveChanges();
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                        {
                            show_Message(h.symbol + " : Error - " + ex.InnerException.InnerException.Message);
                        }
                    }
                }
            }

            show_Message(_file + " has been imported");
        }
        #endregion

        #region + Util
        private void Add_CheckColumn()
        {
            if (chk_Symbols.Columns.Count > 0 && chk_Symbols.Columns[0].Name != "columnChecks")
            {
                var chk_col = new DataGridViewCheckBoxColumn();
                chk_col.Name = "columnChecks";
                chk_Symbols.Columns.Insert(0, chk_col);
                chk_Symbols.Columns[0].Width = 50;
                chk_col.ReadOnly = false;
                for (int c = 1; c < chk_Symbols.Columns.Count; c++)
                {
                    chk_Symbols.Columns[c].SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
        }
        #endregion

        #region + Weekly
        private void btn_Weekly_Click(object sender, EventArgs e)
        {
            Weekly_Analysis(dt_Weekly.Value);
        }


        private void btn_Week_Long_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Run Long Weekly?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Start_Long_Week_Analysis));
                t.Start(new object[] { dt_Weekly.Value });
            }
        }
        private void Start_Long_Week_Analysis(object _param)
        {
            object[] param = (object[])_param;
            DateTime dttm = DateTime.Today;
            DateTime start = Convert.ToDateTime(param[0]); 
            while (start <= dttm)
            {
                Weekly_Analysis(start);
                start = start.AddDays(7);
            }
            show_Message("Completed");
        }
        private void Weekly_Analysis(DateTime _dttm)
        {
            using (stockEntities stock = new stockEntities())
            {
                show_Message("Starting Weekly Processing of " + _dttm.ToString());
                stock.process_weekly(_dttm);
                stock.process_weekly_chg(_dttm);
                show_Message("Processed " + _dttm.ToString());
            }
        }

        #endregion
    }

    public class TextFile
    {
        public string Symbol { get; set; }
        public string File_Path { get; set; }
        public TextFile(string _Symbol, string _File_Path)
        {
            Symbol = _Symbol;
            File_Path = _File_Path;
        }
    }

    public class Symbol
    {
        public string symbol { get; set; }
        public Symbol(string _Symbol)
        {
            symbol = _Symbol;
        }
    }
}
