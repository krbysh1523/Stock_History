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
    public partial class GetData : Form
    {

        #region + Form
        int[] days = new int[] { 5, 10, 20, 30, 60, 120, 200 };
        string API_KEY = "YFMAAM655H11S0KV";
        //string API_KEY = "BWN5J2GY9613F0D0";
        List<lookup> data_types = new List<lookup>();
        int wait_sleep = 500;
        int down_sleep = 100;

        public GetData()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Symbols();
            Load_Lookup();
        }

        private void Load_Symbols()
        {
            using (StockEntity stock = new StockEntity())
            {
                List<watch_list_v1> watches = new List<watch_list_v1>();
                watches = stock.watch_list_v1.ToList();
                chk_Symbols.DataSource = watches;
                var chk_col = new DataGridViewCheckBoxColumn();
                chk_col.ReadOnly = false;
                chk_Symbols.Columns.Insert(0, chk_col);
                chk_Symbols.Columns[0].Width = 50;
            }
        }
        private void Load_Lookup()
        {
            using (StockEntity stock = new StockEntity())
            {
                data_types = stock.lookups.Where(r => r.l_type == "data_type").ToList();
            }
        }

        private string[] Ret_Checked(DataGridView _chk_List)
        {
            List<string> chkd = new List<string>();
            foreach (DataGridViewRow r in _chk_List.Rows)
            {
                bool isSelected = Convert.ToBoolean(r.Cells[0].Value);
                if (isSelected)
                {
                    chkd.Add((string)r.Cells[1].Value.ToString());
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
            using (StockEntity stock = new StockEntity())
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
        #endregion

        #region + Download
        private void chk_CheckAll_Download_Click(object sender, EventArgs e)
        {
            Check_All(chk_Downloads);
        }
        private void btn_Download_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(Process_Download));
            t.Start();
        }
        private void Process_Download()
        {
            if (Ret_Checked(chk_Downloads).Contains("Download Income"))
                ProcIncome();
            if (Ret_Checked(chk_Downloads).Contains("Download Earning"))
                ProcEarning();
            if (Ret_Checked(chk_Downloads).Contains("Download Latest"))
                Download_Daily_Last();
            if (Ret_Checked(chk_Downloads).Contains("Download All History"))
                Download_Daily_Full();
        }
        private void Download_Daily_Full()
        {
            foreach (var s in Ret_Checked(chk_Symbols))
            {
                string json_result = Get_API_Data("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=" + s + "&outputsize=full&apikey=" + API_KEY, s);

                DailyHistoryAdjusted(json_result, s);
                show_Message("Downloaded " + s);

                Thread.Sleep(down_sleep);
            }
            show_Message("Completed");
        }
        private void Download_Daily_Last()
        {
            foreach (var s in Ret_Checked(chk_Symbols))
            {
                string json_result = Get_API_Data("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=" + s + "&outputsize=compact&apikey=" + API_KEY, s);

                DailyHistoryAdjusted(json_result, s);
                show_Message("Downloaded " + s);

                Thread.Sleep(down_sleep);
            }
            show_Message("Completed");
        }
        public void DailyHistoryAdjusted(string _json_result, string _symbol)
        {
            var jsearch = Newtonsoft.Json.Linq.JObject.Parse(_json_result);
            if (jsearch == null)
                return;
            IList<JToken> results = jsearch["Time Series (Daily)"].Children().ToList();
            using (StockEntity api = new StockEntity())
            {
                List<daily_source> history = new List<daily_source>();
                history = api.daily_source.Where(r => r.symbol == _symbol).ToList();

                foreach (JToken j in results)
                {
                    DateTime timestamp = Convert.ToDateTime(((Newtonsoft.Json.Linq.JProperty)j).Name);

                    IList<JToken> values = ((Newtonsoft.Json.Linq.JProperty)j).Value.Children().ToList();

                    if (!history.Any(r => r.date == timestamp))
                    {
                        api.daily_source.Add(new daily_source
                        {
                            symbol = _symbol,
                            date = timestamp,
                            open = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[0]).Value),
                            high = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[1]).Value),
                            low = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[2]).Value),
                            close = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[3]).Value),
                            adjusted_close = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[4]).Value),
                            volume = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[5]).Value),
                            dividend_amount = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[6]).Value),
                            split_coefficient = Convert.ToDouble(((Newtonsoft.Json.Linq.JProperty)values[7]).Value)
                        });
                    }

                }
                api.SaveChanges();
                show_Message("Updated Database for " + _symbol);
            }
        }
        public void ProcEarning()
        {
            foreach (var s in Ret_Checked(chk_Symbols))
            {
                string json_result = Get_API_Data("https://www.alphavantage.co/query?function=EARNINGS&symbol=" + s + "&apikey=" + API_KEY, s);

                var jsearch = Newtonsoft.Json.Linq.JObject.Parse(json_result);
                IList<JToken> results = jsearch["quarterlyEarnings"].Children().ToList();
                using (StockEntity api = new StockEntity())
                {
                    List<earning> history = new List<earning>();
                    history = api.earnings.Where(r => r.symbol == s).ToList();

                    foreach (JToken j in results)
                    {
                        DateTime fiscalDateEnding = Convert.ToDateTime(((Newtonsoft.Json.Linq.JValue)j["fiscalDateEnding"]).Value);

                        if (!history.Any(r => r.fiscalDateEnding == fiscalDateEnding))
                        {
                            api.earnings.Add(new earning
                            {
                                symbol = s,
                                fiscalDateEnding = fiscalDateEnding,
                                reportedDate = Convert.ToDateTime(((Newtonsoft.Json.Linq.JValue)j["reportedDate"]).Value),
                                reportedEPS = ret_Double(((Newtonsoft.Json.Linq.JValue)j["reportedEPS"]).Value),
                                estimatedEPS = ret_Double(((Newtonsoft.Json.Linq.JValue)j["estimatedEPS"]).Value),
                                surprise = ret_Double(((Newtonsoft.Json.Linq.JValue)j["surprise"]).Value),
                                surprisePercentage = ret_Double(((Newtonsoft.Json.Linq.JValue)j["surprisePercentage"]).Value),
                            });
                        }

                    }
                    api.SaveChanges();
                    show_Message("Updated Database for " + s);
                }
                show_Message("Downloaded Earnings for " + s);

                Thread.Sleep(down_sleep);
            }
            show_Message("Completed");
        }
        private Double ret_Double(object _value)
        {
            double _t = 0;
            Double.TryParse(_value.ToString(), out _t);
            return _t;
        }

        public void ProcIncome()
        {
            foreach (var s in Ret_Checked(chk_Symbols))
            {
                string json_result = Get_API_Data("https://www.alphavantage.co/query?function=INCOME_STATEMENT&symbol=" + s + "&apikey=" + API_KEY, s);

                var jsearch = Newtonsoft.Json.Linq.JObject.Parse(json_result);
                IList<JToken> results = jsearch["quarterlyReports"].Children().ToList();
                using (StockEntity api = new StockEntity())
                {
                    List<income> history = new List<income>();
                    history = api.incomes.Where(r => r.symbol == s).ToList();

                    foreach (JToken j in results)
                    {
                        DateTime fiscalDateEnding = Convert.ToDateTime(((Newtonsoft.Json.Linq.JValue)j["fiscalDateEnding"]).Value);

                        if (!history.Any(r => r.fiscalDateEnding == fiscalDateEnding))
                        {
                            api.incomes.Add(new income
                            {
                                symbol = s,
                                fiscalDateEnding = fiscalDateEnding,
                                reportedCurrency = ((Newtonsoft.Json.Linq.JValue)j["reportedCurrency"]).Value.ToString(),
                                totalRevenue = ret_Double(((Newtonsoft.Json.Linq.JValue)j["totalRevenue"]).Value),
                                totalOperatingExpense = ret_Double(((Newtonsoft.Json.Linq.JValue)j["totalOperatingExpense"]).Value),
                                costOfRevenue = ret_Double(((Newtonsoft.Json.Linq.JValue)j["costOfRevenue"]).Value),
                                grossProfit = ret_Double(((Newtonsoft.Json.Linq.JValue)j["grossProfit"]).Value),
                                ebit = ret_Double(((Newtonsoft.Json.Linq.JValue)j["ebit"]).Value),
                                netIncome = ret_Double(((Newtonsoft.Json.Linq.JValue)j["netIncome"]).Value),
                                researchAndDevelopment = ret_Double(((Newtonsoft.Json.Linq.JValue)j["researchAndDevelopment"]).Value),
                                effectOfAccountingCharges = ret_Double(((Newtonsoft.Json.Linq.JValue)j["effectOfAccountingCharges"]).Value),
                                incomeBeforeTax = ret_Double(((Newtonsoft.Json.Linq.JValue)j["incomeBeforeTax"]).Value),
                                minorityInterest = ret_Double(((Newtonsoft.Json.Linq.JValue)j["minorityInterest"]).Value),
                                sellingGeneralAdministrative = ret_Double(((Newtonsoft.Json.Linq.JValue)j["sellingGeneralAdministrative"]).Value),
                                otherNonOperatingIncome = ret_Double(((Newtonsoft.Json.Linq.JValue)j["otherNonOperatingIncome"]).Value),
                                operatingIncome = ret_Double(((Newtonsoft.Json.Linq.JValue)j["operatingIncome"]).Value),
                                otherOperatingExpense = ret_Double(((Newtonsoft.Json.Linq.JValue)j["otherOperatingExpense"]).Value),
                                interestExpense = ret_Double(((Newtonsoft.Json.Linq.JValue)j["interestExpense"]).Value),
                                taxProvision = ((Newtonsoft.Json.Linq.JValue)j["taxProvision"]).Value.ToString(),
                                interestIncome = ((Newtonsoft.Json.Linq.JValue)j["interestIncome"]).Value.ToString(),
                                netInterestIncome = ((Newtonsoft.Json.Linq.JValue)j["netInterestIncome"]).Value.ToString(),
                                extraordinaryItems = ret_Double(((Newtonsoft.Json.Linq.JValue)j["extraordinaryItems"]).Value),
                                nonRecurring = ret_Double(((Newtonsoft.Json.Linq.JValue)j["nonRecurring"]).Value),
                                otherItems = ret_Double(((Newtonsoft.Json.Linq.JValue)j["otherItems"]).Value),
                                incomeTaxExpense = ret_Double(((Newtonsoft.Json.Linq.JValue)j["incomeTaxExpense"]).Value),
                                totalOtherIncomeExpense = ret_Double(((Newtonsoft.Json.Linq.JValue)j["totalOtherIncomeExpense"]).Value),
                                discontinuedOperations = ret_Double(((Newtonsoft.Json.Linq.JValue)j["discontinuedOperations"]).Value),
                                netIncomeFromContinuingOperations = ret_Double(((Newtonsoft.Json.Linq.JValue)j["netIncomeFromContinuingOperations"]).Value),
                                netIncomeApplicableToCommonShares = ret_Double(((Newtonsoft.Json.Linq.JValue)j["netIncomeApplicableToCommonShares"]).Value),
                                preferredStockAndOtherAdjustments = ((Newtonsoft.Json.Linq.JValue)j["preferredStockAndOtherAdjustments"]).Value.ToString()
                            });
                        }
                    }
                    api.SaveChanges();
                    show_Message("Updated Database");
                }
                show_Message("Downloaded Income for " + s);

                Thread.Sleep(down_sleep);
            }
            show_Message("Completed");
        }

        #endregion

        #region + Process
        private void btn_ProcAll_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ProcAll));
            t.Start();
        }
        private void ProcAll()
        {
            if (chk_Processing.CheckedItems.Contains("History"))
            {
                ProcHistory();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("SMA"))
            {
                ProcSMA();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("EMA"))
            {
                ProcEMA();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("Change"))
            {
                ProcChange();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("RSI"))
            {
                ProcRSI();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("MACD"))
            {
                ProcMACD();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("Forecast"))
            {
                ProcForecast();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("Analysis"))
            {
                ProcAnalysis();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("EPR"))
            {
                ProcEPR();
                Thread.Sleep(wait_sleep);
            }
            if (chk_Processing.CheckedItems.Contains("Volume"))
            {
                ProcVolume();
                Thread.Sleep(wait_sleep);
            }
        }
        private void btn_CheckAllProc_Click(object sender, EventArgs e)
        {
            Check_All(chk_Processing);
        }
        private void ProcHistory()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;
                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    show_Message("Process History " + s + " Started");
                    stock.process_history(s);
                    show_Message("Process History " + s + " Finished");

                    Thread.Sleep(300);
                }
            }
        }
        private void ProcSMA()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;
                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (lookup l in data_types)
                    {
                        foreach (var i in days)
                        {
                            show_Message("Process SMA " + s + ":" + l.l_att1 + "-" + i.ToString() + " Started");
                            stock.process_sma_detail(s, DateTime.Parse("2000-01-01"), l.l_att1, l.l_att2, i);
                            show_Message("Process SMA " + s + ":" + l.l_att1 + "-" + i.ToString() + " Finished");
                        }
                    }
                    Thread.Sleep(300);
                }
            }
        }
        private void ProcEMA()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (lookup l in data_types)
                    {
                        foreach (var i in days)
                        {
                            show_Message("Process EMA " + s + ":" + l.l_att1 + "-" + i.ToString() + " Started");
                            stock.Database.ExecuteSqlCommand(SQL.process_ema(s,
                                i.ToString(),
                                "daily_history",
                                l.l_att1,
                                l.l_att2,
                                "ema_" + i.ToString("000")),
                                new object[] { });
                            show_Message("Process EMA " + s + ":" + l.l_att1 + "-" + i.ToString() + " Finished");
                        }
                        Thread.Sleep(300);
                    }
                }
            }
        }
        private void ProcRSI()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (var i in days)
                    {
                        show_Message("Process RSI " + s + ":" + i.ToString() + " Started");
                        stock.Database.ExecuteSqlCommand(SQL.process_rsi(s,
                            i.ToString("000")),
                            new object[] { });
                        show_Message("Process RSI " + s + ":" + i.ToString() + " Finished");
                        Thread.Sleep(300);
                    }
                }
            }
        }

        private void ProcMACD()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (lookup l in data_types)
                    {
                        show_Message("Process MACD " + s + ":" + l.l_att1 + " Started");
                        // macd
                        stock.Database.ExecuteSqlCommand(SQL.process_macd1(s,
                            l.l_att1),
                            new object[] { });

                        // Signal
                        stock.Database.ExecuteSqlCommand(SQL.process_ema(s,
                            "9",
                            l.l_att1,
                            l.l_att1,
                            "macd_12_26",
                            "macd_12_26_9"),
                            new object[] { });

                        // Histogram
                        stock.Database.ExecuteSqlCommand(SQL.process_macd2(s,
                            l.l_att1),
                            new object[] { });

                        show_Message("Process MACD" + s + ":" + l.l_att1 + " Finished");
                        Thread.Sleep(300);
                    }
                }
            }
        }
        private void ProcChange()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (lookup l in data_types)
                    {
                        show_Message("Process Change " + s + ":" + l.l_att1 + " Started");
                        // Change of basic
                        stock.Database.ExecuteSqlCommand(SQL.update_change(s,
                            l.l_att1,
                            l.l_att2),
                            new object[] { });
                        show_Message("Process Change " + s + ":" + l.l_att1 + " Finished");
                    }
                    Thread.Sleep(300);
                }
            }
        }
        private void ProcForecast()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (lookup l in data_types)
                    {
                        foreach (var i in days)
                        {
                            show_Message("Process Forecast " + s + ":" + l.l_att1 + "-" + i.ToString() + " Started");
                            // Change of basic
                            stock.Database.ExecuteSqlCommand(SQL.update_forecast(s,
                                l.l_att1,
                                l.l_att2,
                                i.ToString("000")),
                                new object[] { });
                            show_Message("Process Forecast " + s + ":" + l.l_att1 + "-" + i.ToString() + " Finished");
                        }
                        Thread.Sleep(300);
                    }
                }
            }
        }
        private void ProcAnalysis()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    show_Message("Process Analysis " + s + " Started");
                    // Change of basic
                    stock.process_analysis(s);
                    show_Message("Process Analysis " + s + " Finished");
                    Thread.Sleep(300);
                }
            }
        }
        private void ProcEPR()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    foreach (lookup l in data_types)
                    {
                        show_Message("Process EPR " + s + ":" + l.l_att1 + " Started");
                        // Change of basic
                        stock.Database.ExecuteSqlCommand(SQL.update_epr_fiscal(s,
                            l.l_att1,
                            l.l_att2),
                            new object[] { });
                        stock.Database.ExecuteSqlCommand(SQL.update_epr_report(s,
                            l.l_att1,
                            l.l_att2),
                            new object[] { });
                        show_Message("Process EPR " + s + ":" + l.l_att1 + " Finished");
                    }
                    Thread.Sleep(300);
                }
            }
        }
        private void ProcVolume()
        {
            using (StockEntity stock = new StockEntity())
            {
                stock.Database.CommandTimeout = 300000;

                foreach (var s in Ret_Checked(chk_Symbols))
                {
                    show_Message("Process Volume " + s + " Started");
                    // Change of basic
                    stock.process_volume(s);
                    show_Message("Process Volume " + s + " Finished");
                }
                Thread.Sleep(300);
            }
        }
        #endregion

        #region + API
        public string Get_API_Data(string _URL, string _symbol)
        {
            string result;
            using (var client = new WebClient())
            {
                result = client.DownloadString(_URL);
            }
            show_Message("Downloaded!");
            return result;
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
            using (StockEntity stock = new StockEntity())
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

        private void btn_Test_Click(object sender, EventArgs e)
        {
            Analysis dlg = new Analysis();
            dlg.Show();
        }
    }
}
