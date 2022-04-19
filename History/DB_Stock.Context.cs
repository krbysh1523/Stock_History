﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace History
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class stockEntities : DbContext
    {
        public stockEntities()
            : base("name=stockEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<daily_history> daily_history { get; set; }
        public virtual DbSet<daily_strong> daily_strong { get; set; }
        public virtual DbSet<daily_close> daily_close { get; set; }
        public virtual DbSet<lookup> lookups { get; set; }
        public virtual DbSet<daily_analysis> daily_analysis { get; set; }
        public virtual DbSet<daily_sma5> daily_sma5 { get; set; }
        public virtual DbSet<daily_source> daily_source { get; set; }
        public virtual DbSet<daily_volume> daily_volume { get; set; }
        public virtual DbSet<earning> earnings { get; set; }
        public virtual DbSet<fundamental> fundamentals { get; set; }
        public virtual DbSet<income> incomes { get; set; }
        public virtual DbSet<symbol_att> symbol_att { get; set; }
        public virtual DbSet<watch_list> watch_list { get; set; }
        public virtual DbSet<api> apis { get; set; }
        public virtual DbSet<daily_avg> daily_avg { get; set; }
        public virtual DbSet<daily_high> daily_high { get; set; }
        public virtual DbSet<daily_industry> daily_industry { get; set; }
        public virtual DbSet<daily_low> daily_low { get; set; }
        public virtual DbSet<daily_open> daily_open { get; set; }
        public virtual DbSet<daily_sector> daily_sector { get; set; }
        public virtual DbSet<daily_analysis_v1> daily_analysis_v1 { get; set; }
        public virtual DbSet<daily_candle_v1> daily_candle_v1 { get; set; }
        public virtual DbSet<daily_close_v1> daily_close_v1 { get; set; }
        public virtual DbSet<daily_sma5_v1> daily_sma5_v1 { get; set; }
        public virtual DbSet<daily_strong_v1> daily_strong_v1 { get; set; }
        public virtual DbSet<daily_v1> daily_v1 { get; set; }
        public virtual DbSet<daily_vol_sma10_v1> daily_vol_sma10_v1 { get; set; }
        public virtual DbSet<daily_vol_sma5_v1> daily_vol_sma5_v1 { get; set; }
        public virtual DbSet<daily_volume_v1> daily_volume_v1 { get; set; }
        public virtual DbSet<fundamental_v1> fundamental_v1 { get; set; }
        public virtual DbSet<industry_v1> industry_v1 { get; set; }
        public virtual DbSet<sector_v1> sector_v1 { get; set; }
        public virtual DbSet<watch_list_v1> watch_list_v1 { get; set; }
        public virtual DbSet<test_plan> test_plan { get; set; }
        public virtual DbSet<list_history> list_history { get; set; }
        public virtual DbSet<prediction> predictions { get; set; }
        public virtual DbSet<list_history_v1> list_history_v1 { get; set; }
    
        public virtual ObjectResult<filter_intro_Result> filter_intro(Nullable<System.DateTime> start_dttm, Nullable<System.DateTime> end_dttm, string filter_used, Nullable<int> filter_option, Nullable<int> filter_limit, string filter_symbols, Nullable<int> rank_option, Nullable<int> rank_limit, string is_simul)
        {
            var start_dttmParameter = start_dttm.HasValue ?
                new ObjectParameter("start_dttm", start_dttm) :
                new ObjectParameter("start_dttm", typeof(System.DateTime));
    
            var end_dttmParameter = end_dttm.HasValue ?
                new ObjectParameter("end_dttm", end_dttm) :
                new ObjectParameter("end_dttm", typeof(System.DateTime));
    
            var filter_usedParameter = filter_used != null ?
                new ObjectParameter("filter_used", filter_used) :
                new ObjectParameter("filter_used", typeof(string));
    
            var filter_optionParameter = filter_option.HasValue ?
                new ObjectParameter("filter_option", filter_option) :
                new ObjectParameter("filter_option", typeof(int));
    
            var filter_limitParameter = filter_limit.HasValue ?
                new ObjectParameter("filter_limit", filter_limit) :
                new ObjectParameter("filter_limit", typeof(int));
    
            var filter_symbolsParameter = filter_symbols != null ?
                new ObjectParameter("filter_symbols", filter_symbols) :
                new ObjectParameter("filter_symbols", typeof(string));
    
            var rank_optionParameter = rank_option.HasValue ?
                new ObjectParameter("rank_option", rank_option) :
                new ObjectParameter("rank_option", typeof(int));
    
            var rank_limitParameter = rank_limit.HasValue ?
                new ObjectParameter("rank_limit", rank_limit) :
                new ObjectParameter("rank_limit", typeof(int));
    
            var is_simulParameter = is_simul != null ?
                new ObjectParameter("is_simul", is_simul) :
                new ObjectParameter("is_simul", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<filter_intro_Result>("filter_intro", start_dttmParameter, end_dttmParameter, filter_usedParameter, filter_optionParameter, filter_limitParameter, filter_symbolsParameter, rank_optionParameter, rank_limitParameter, is_simulParameter);
        }
    
        public virtual int process_analysis(string symbol)
        {
            var symbolParameter = symbol != null ?
                new ObjectParameter("symbol", symbol) :
                new ObjectParameter("symbol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("process_analysis", symbolParameter);
        }
    
        public virtual int process_history(string symbol)
        {
            var symbolParameter = symbol != null ?
                new ObjectParameter("symbol", symbol) :
                new ObjectParameter("symbol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("process_history", symbolParameter);
        }
    
        public virtual int process_rsi_detail(string symbol, Nullable<System.DateTime> last_ran, string table_name, string col_name, Nullable<int> days)
        {
            var symbolParameter = symbol != null ?
                new ObjectParameter("symbol", symbol) :
                new ObjectParameter("symbol", typeof(string));
    
            var last_ranParameter = last_ran.HasValue ?
                new ObjectParameter("last_ran", last_ran) :
                new ObjectParameter("last_ran", typeof(System.DateTime));
    
            var table_nameParameter = table_name != null ?
                new ObjectParameter("table_name", table_name) :
                new ObjectParameter("table_name", typeof(string));
    
            var col_nameParameter = col_name != null ?
                new ObjectParameter("col_name", col_name) :
                new ObjectParameter("col_name", typeof(string));
    
            var daysParameter = days.HasValue ?
                new ObjectParameter("days", days) :
                new ObjectParameter("days", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("process_rsi_detail", symbolParameter, last_ranParameter, table_nameParameter, col_nameParameter, daysParameter);
        }
    
        public virtual int process_sma_detail(string symbol, Nullable<System.DateTime> last_ran, string table_name, string col_name, Nullable<int> days)
        {
            var symbolParameter = symbol != null ?
                new ObjectParameter("symbol", symbol) :
                new ObjectParameter("symbol", typeof(string));
    
            var last_ranParameter = last_ran.HasValue ?
                new ObjectParameter("last_ran", last_ran) :
                new ObjectParameter("last_ran", typeof(System.DateTime));
    
            var table_nameParameter = table_name != null ?
                new ObjectParameter("table_name", table_name) :
                new ObjectParameter("table_name", typeof(string));
    
            var col_nameParameter = col_name != null ?
                new ObjectParameter("col_name", col_name) :
                new ObjectParameter("col_name", typeof(string));
    
            var daysParameter = days.HasValue ?
                new ObjectParameter("days", days) :
                new ObjectParameter("days", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("process_sma_detail", symbolParameter, last_ranParameter, table_nameParameter, col_nameParameter, daysParameter);
        }
    
        public virtual int process_volume(string symbol)
        {
            var symbolParameter = symbol != null ?
                new ObjectParameter("symbol", symbol) :
                new ObjectParameter("symbol", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("process_volume", symbolParameter);
        }
    
        public virtual int sp_simulation(string symbols, Nullable<System.DateTime> start_dttm, Nullable<double> drop_limit_perc, Nullable<double> sell_limit_perc, Nullable<int> max_days_hold, string show_detail)
        {
            var symbolsParameter = symbols != null ?
                new ObjectParameter("symbols", symbols) :
                new ObjectParameter("symbols", typeof(string));
    
            var start_dttmParameter = start_dttm.HasValue ?
                new ObjectParameter("start_dttm", start_dttm) :
                new ObjectParameter("start_dttm", typeof(System.DateTime));
    
            var drop_limit_percParameter = drop_limit_perc.HasValue ?
                new ObjectParameter("drop_limit_perc", drop_limit_perc) :
                new ObjectParameter("drop_limit_perc", typeof(double));
    
            var sell_limit_percParameter = sell_limit_perc.HasValue ?
                new ObjectParameter("sell_limit_perc", sell_limit_perc) :
                new ObjectParameter("sell_limit_perc", typeof(double));
    
            var max_days_holdParameter = max_days_hold.HasValue ?
                new ObjectParameter("max_days_hold", max_days_hold) :
                new ObjectParameter("max_days_hold", typeof(int));
    
            var show_detailParameter = show_detail != null ?
                new ObjectParameter("show_detail", show_detail) :
                new ObjectParameter("show_detail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_simulation", symbolsParameter, start_dttmParameter, drop_limit_percParameter, sell_limit_percParameter, max_days_holdParameter, show_detailParameter);
        }
    
        public virtual int sp_weekly_v1(string symbols, Nullable<System.DateTime> start_dttm, Nullable<int> week_count)
        {
            var symbolsParameter = symbols != null ?
                new ObjectParameter("symbols", symbols) :
                new ObjectParameter("symbols", typeof(string));
    
            var start_dttmParameter = start_dttm.HasValue ?
                new ObjectParameter("start_dttm", start_dttm) :
                new ObjectParameter("start_dttm", typeof(System.DateTime));
    
            var week_countParameter = week_count.HasValue ?
                new ObjectParameter("week_count", week_count) :
                new ObjectParameter("week_count", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_weekly_v1", symbolsParameter, start_dttmParameter, week_countParameter);
        }
    
        public virtual int update_daily_analysis_v1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_daily_analysis_v1");
        }
    
        public virtual int update_daily_v1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("update_daily_v1");
        }
    
        public virtual ObjectResult<filter_main_Result> filter_main(Nullable<System.DateTime> start_dttm, Nullable<System.DateTime> end_dttm, Nullable<int> option, string passing_list, string passing_symbols, string att1, string att2, string att3, string att4, string att5, string description)
        {
            var start_dttmParameter = start_dttm.HasValue ?
                new ObjectParameter("start_dttm", start_dttm) :
                new ObjectParameter("start_dttm", typeof(System.DateTime));
    
            var end_dttmParameter = end_dttm.HasValue ?
                new ObjectParameter("end_dttm", end_dttm) :
                new ObjectParameter("end_dttm", typeof(System.DateTime));
    
            var optionParameter = option.HasValue ?
                new ObjectParameter("option", option) :
                new ObjectParameter("option", typeof(int));
    
            var passing_listParameter = passing_list != null ?
                new ObjectParameter("passing_list", passing_list) :
                new ObjectParameter("passing_list", typeof(string));
    
            var passing_symbolsParameter = passing_symbols != null ?
                new ObjectParameter("passing_symbols", passing_symbols) :
                new ObjectParameter("passing_symbols", typeof(string));
    
            var att1Parameter = att1 != null ?
                new ObjectParameter("att1", att1) :
                new ObjectParameter("att1", typeof(string));
    
            var att2Parameter = att2 != null ?
                new ObjectParameter("att2", att2) :
                new ObjectParameter("att2", typeof(string));
    
            var att3Parameter = att3 != null ?
                new ObjectParameter("att3", att3) :
                new ObjectParameter("att3", typeof(string));
    
            var att4Parameter = att4 != null ?
                new ObjectParameter("att4", att4) :
                new ObjectParameter("att4", typeof(string));
    
            var att5Parameter = att5 != null ?
                new ObjectParameter("att5", att5) :
                new ObjectParameter("att5", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<filter_main_Result>("filter_main", start_dttmParameter, end_dttmParameter, optionParameter, passing_listParameter, passing_symbolsParameter, att1Parameter, att2Parameter, att3Parameter, att4Parameter, att5Parameter, descriptionParameter);
        }
    
        public virtual int sp_generate_idx()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_generate_idx");
        }
    
        public virtual ObjectResult<sp_get_prediction_history_Result> sp_get_prediction_history(Nullable<int> hist_id)
        {
            var hist_idParameter = hist_id.HasValue ?
                new ObjectParameter("hist_id", hist_id) :
                new ObjectParameter("hist_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_get_prediction_history_Result>("sp_get_prediction_history", hist_idParameter);
        }
    
        public virtual ObjectResult<filter_simul_header_Result> filter_simul_header(Nullable<System.DateTime> end_dttm)
        {
            var end_dttmParameter = end_dttm.HasValue ?
                new ObjectParameter("end_dttm", end_dttm) :
                new ObjectParameter("end_dttm", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<filter_simul_header_Result>("filter_simul_header", end_dttmParameter);
        }
    }
}
