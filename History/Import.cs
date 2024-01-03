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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace History
{
    public partial class Import : Form
    {

        #region + Form
        Query query = null;
        int[] days = new int[] { 5, 10, 20, 30, 60, 120, 240 };
        List<lookup> data_types = new List<lookup>();
        int wait_sleep = 500;
        int down_sleep = 1000;

        public Import()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public Import(Query _query)
        {
            query = _query;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Import_FormClosing(object sender, FormClosingEventArgs e)
        {
            query.CloseChild(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //btn_Open_Click(this, null);
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
            foreach(var c in _chk_List.CheckedItems)
            {
                chkd.Add((string)c);
            }
            return chkd.ToArray();
        }
        #endregion

        #region + Symbols
        private void btn_Check_Not_Updated_Click(object sender, EventArgs e)
        {
            var max_dttm = DateTime.Today;
            using (stockEntity stock = new stockEntity())
            {
                max_dttm = stock.watch_list_v1.Max(r => r.last_updated).Value;
            }
            foreach (DataGridViewRow r in chk_Symbols.Rows)
            {
                if (r.Cells["last_updated"].Value == null)
                    r.Cells[0].Value = true;
                //else if( Convert.ToDateTime(r.Cells["last_updated"].Value) < max_dttm)
                //    r.Cells[0].Value = true;
            }
        }

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

        #region + Download
        private Double ret_Double(JToken _j, string _name)
        {
            double _t = -999999999999999999;
            try
            {
                if ((Newtonsoft.Json.Linq.JValue)_j[_name] != null)
                    Double.TryParse(((Newtonsoft.Json.Linq.JValue)_j[_name]).Value.ToString(), out _t);
            }
            catch(Exception)
            {
                show_Message(_name + " had exception (To Double)");
            }
            return _t;
        }
        private string ret_String(JToken _j, string _name)
        {
            string _t = "";
            try
            {
                if ((Newtonsoft.Json.Linq.JValue)_j[_name] != null)
                    _t = ((Newtonsoft.Json.Linq.JValue)_j[_name]).Value.ToString();
            }
            catch (Exception)
            {
                show_Message(_name + " had exception (To String)");
            }
            return _t;
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
            using (stockEntity stock = new stockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                var symbols = Ret_Checked(chk_Symbols, 1);
                for (int s=0; s < symbols.Length; s++)
                {
                    // Change of basic
                    stock.process_analysis(symbols[s]);
                    show_Message("Process Analysis " + symbols[s] + " Finished (" + (s+1).ToString() + "/" + (symbols.Length + 1).ToString());
                }
            }
        }
        private void btn_LoadAnalysis_Click(object sender, EventArgs e)
        {
            using (stockEntity stock = new stockEntity())
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
            using (stockEntity stock = new stockEntity())
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
            if(open.ShowDialog(this) == DialogResult.OK)
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
                Process_File(symbol, File.ReadAllLines(s));
            }
            show_Message("Completed");
        }

        private void Process_File(string _symbol, string[] _lines)
        {
            show_Message(_symbol + " starting");

            List<daily_history> hists = new List<daily_history>();

            foreach (var l in _lines)
            {
                string[] sss = l.Split(new char[] { ',' });
                if (sss.Length == 10 && sss[0].Contains("US"))
                {
                    var h = new daily_history();
                    h.symbol = _symbol;
                    h.date = DateTime.ParseExact(sss[2], "yyyyMMdd", CultureInfo.InvariantCulture);
                    h.open = double.Parse(sss[4]);
                    h.high = double.Parse(sss[5]);
                    h.low = double.Parse(sss[6]);
                    h.close = double.Parse(sss[7]);
                    h.volume = double.Parse(sss[8]);
                    hists.Add(h);
                }
            }

            using (stockEntity stock = new stockEntity())
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

                    using (stockEntity stock = new stockEntity())
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
