using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace History
{
    
    public static class SQL
    {
        public static string process_ema(string _symbol, string _days, string _src_table_name, string _table_name, string _src_col_name, string _dst_col_name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" BEGIN ");
            sb.Append("     DECLARE @symbol NVARCHAR(10), @days INT; ");
            sb.Append("     SET @symbol = '"+ _symbol + "'; ");
            sb.Append("     SET @days = "+ _days+"; ");
            sb.Append("     DECLARE @sql NVARCHAR(MAX), @col_name NVARCHAR(50), @sma_col_name NVARCHAR(50); ");
            sb.Append("     DECLARE @alpha FLOAT, @prev_ema FLOAT, @initial_sma FLOAT, @anchor INT; ");
            sb.Append("     SET @alpha = 2 / (1 + cast(@days as float)); ");


            sb.Append("     SELECT  ");
            sb.Append("            ROW_NUMBER() OVER( ");
            sb.Append("            ORDER BY  ");
            sb.Append("                     [date]) n,  ");
            sb.Append("            [date],  ");
            sb.Append("            ISNULL(["+ _src_col_name + "], 0) AS val,  ");
            sb.Append("            CAST(NULL AS FLOAT) ema ");
            sb.Append("     INTO  ");
            sb.Append("          #temp ");
            sb.Append("     FROM "+_src_table_name+" ");
            sb.Append("     WHERE symbol = @symbol; ");

            sb.Append("     SELECT  ");
            sb.Append("            @initial_sma = AVG(CASE ");
            sb.Append("                                   WHEN n < @days ");
            sb.Append("                                   THEN val ");
            sb.Append("                                   ELSE NULL ");
            sb.Append("                               END) ");
            sb.Append("     FROM #temp ");
            sb.Append("     WHERE n < @days; ");

            sb.Append("     UPDATE t1 ");
            sb.Append("       SET  ");
            sb.Append("           @prev_ema = CASE ");
            sb.Append("                           WHEN n < @days ");
            sb.Append("                           THEN NULL ");
            sb.Append("                           WHEN n = @days ");
            sb.Append("                           THEN t1.val * @alpha + @initial_sma * (1 - @alpha) ");
            sb.Append("                           WHEN n > @days ");
            sb.Append("                           THEN t1.val * @alpha + @prev_ema * (1 - @alpha) ");
            sb.Append("                       END,  ");
            sb.Append("           ema = @prev_ema,  ");
            sb.Append("           @anchor = n ");
            sb.Append("     FROM #temp t1 WITH(TABLOCKX) OPTION( ");
            sb.Append("                                         MAXDOP 1); ");

            sb.Append("     MERGE "+ _table_name + " v ");
            sb.Append("     USING ");
            sb.Append("     ( ");
            sb.Append("         SELECT  ");
            sb.Append("                * ");
            sb.Append("         FROM #temp ");
            sb.Append("     ) t ");
            sb.Append("     ON v.symbol = @symbol ");
            sb.Append("        AND v.date = t.date ");
            sb.Append("         WHEN MATCHED ");
            sb.Append("         THEN UPDATE SET  ");
            sb.Append("                         "+ _dst_col_name + " = t.ema ");
            sb.Append("         WHEN NOT MATCHED ");
            sb.Append("         THEN ");
            sb.Append("           INSERT( ");
            sb.Append("                  symbol,  ");
            sb.Append("                  [date],  ");
            sb.Append("                  " + _dst_col_name + ") ");
            sb.Append("           VALUES ");
            sb.Append("     ( ");
            sb.Append("          @symbol,  ");
            sb.Append("          t.date,  ");
            sb.Append("          t.ema ");
            sb.Append("     ); ");

            sb.Append("     DROP TABLE #temp; ");

            sb.Append(" END; ");

            return sb.ToString();
        }

        public static string process_rsi(string _symbol, string _days)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" BEGIN ");
            sb.Append("     DECLARE @symbol NVARCHAR(50), @last_ran DATE; ");
            sb.Append("     SET @symbol = '"+ _symbol + "'; ");

            sb.Append("     SELECT  ");
            sb.Append("            @last_ran = ISNULL(DATEADD(DAY, -5, MAX(t.date)), '1901-01-01') ");
            sb.Append("     FROM daily_volume t ");
            sb.Append("     WHERE t.symbol = @symbol ");
            sb.Append("           AND t.rsi_"+ _days + " IS NOT NULL; ");

            sb.Append("     EXEC process_rsi_detail  ");
            sb.Append("          @symbol,  ");
            sb.Append("          @last_ran,  ");
            sb.Append("          'daily_volume',  ");
            sb.Append("          'volume',  ");
            sb.Append("          "+ _days + "; ");

            sb.Append("     SELECT  ");
            sb.Append("            @last_ran = ISNULL(DATEADD(DAY, -5, MAX(t.date)), '1901-01-01') ");
            sb.Append("     FROM daily_open t ");
            sb.Append("     WHERE t.symbol = @symbol ");
            sb.Append("           AND t.rsi_" + _days + " IS NOT NULL; ");

            sb.Append("     EXEC process_rsi_detail  ");
            sb.Append("          @symbol,  ");
            sb.Append("          @last_ran,  ");
            sb.Append("          'daily_open',  ");
            sb.Append("          'open',  ");
            sb.Append("          " + _days + "; ");

            sb.Append("     SELECT  ");
            sb.Append("            @last_ran = ISNULL(DATEADD(DAY, -5, MAX(t.date)), '1901-01-01') ");
            sb.Append("     FROM daily_close t ");
            sb.Append("     WHERE t.symbol = @symbol ");
            sb.Append("           AND t.rsi_" + _days + " IS NOT NULL; ");

            sb.Append("     EXEC process_rsi_detail  ");
            sb.Append("          @symbol,  ");
            sb.Append("          @last_ran,  ");
            sb.Append("          'daily_close',  ");
            sb.Append("          'close',  ");
            sb.Append("          " + _days + "; ");

            sb.Append("     SELECT  ");
            sb.Append("            @last_ran = ISNULL(DATEADD(DAY, -5, MAX(t.date)), '1901-01-01') ");
            sb.Append("     FROM daily_high t ");
            sb.Append("     WHERE t.symbol = @symbol ");
            sb.Append("           AND t.rsi_" + _days + " IS NOT NULL; ");

            sb.Append("     EXEC process_rsi_detail  ");
            sb.Append("          @symbol,  ");
            sb.Append("          @last_ran,  ");
            sb.Append("          'daily_high',  ");
            sb.Append("          'high',  ");
            sb.Append("          " + _days + "; ");

            sb.Append("     SELECT  ");
            sb.Append("            @last_ran = ISNULL(DATEADD(DAY, -5, MAX(t.date)), '1901-01-01') ");
            sb.Append("     FROM daily_low t ");
            sb.Append("     WHERE t.symbol = @symbol ");
            sb.Append("           AND t.rsi_" + _days + " IS NOT NULL; ");

            sb.Append("     EXEC process_rsi_detail  ");
            sb.Append("          @symbol,  ");
            sb.Append("          @last_ran,  ");
            sb.Append("          'daily_low',  ");
            sb.Append("          'low',  ");
            sb.Append("          " + _days + "; ");

            sb.Append("     SELECT  ");
            sb.Append("            @last_ran = ISNULL(DATEADD(DAY, -5, MAX(t.date)), '1901-01-01') ");
            sb.Append("     FROM daily_avg t ");
            sb.Append("     WHERE t.symbol = @symbol ");
            sb.Append("           AND t.rsi_" + _days + " IS NOT NULL; ");

            sb.Append("     EXEC process_rsi_detail  ");
            sb.Append("          @symbol,  ");
            sb.Append("          @last_ran,  ");
            sb.Append("          'daily_avg',  ");
            sb.Append("          'avg',  ");
            sb.Append("          " + _days + "; ");

            sb.Append(" END; ");
            return sb.ToString();
        }

        public static string process_macd1(string _symbol, string _table_name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE t ");
            sb.Append("   SET  ");
            sb.Append("       t.macd_12_26 = ISNULL(t.ema_012, 0) - ISNULL(t.ema_026, 0) ");
            sb.Append(" FROM "+ _table_name + " t ");
            sb.Append(" WHERE  ");
            sb.Append("       t.symbol = '"+ _symbol + "'; ");

            return sb.ToString();
        }
        public static string process_macd2(string _symbol, string _table_name)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" UPDATE t ");
            sb.Append("   SET  ");
            sb.Append("       t.macd_12_26_hist = ISNULL(t.macd_12_26, 0) - ISNULL(t.macd_12_26_9, 0) ");
            sb.Append(" FROM " + _table_name + " t ");
            sb.Append(" WHERE  ");
            sb.Append("       t.symbol = '"+ _symbol + "'; ");

            return sb.ToString();
        }
        public static string update_change(string _symbol, string _table_name, string _src_col_name)
        { 
            StringBuilder sb = new StringBuilder();

            sb.Append(" MERGE "+ _table_name + " AS t ");
            sb.Append(" USING ");
            sb.Append(" ( ");
            sb.Append("     SELECT  ");
            sb.Append("            h.symbol,  ");
            sb.Append("            h.date,  ");
            sb.Append("            h.["+ _src_col_name + "],  ");
            sb.Append("            h.[" + _src_col_name + "] - ISNULL((LAG(h.[" + _src_col_name + "]) OVER( ");
            sb.Append("            ORDER BY  ");
            sb.Append("                     h.symbol,  ");
            sb.Append("                     h.date)), 0) AS change, ");
            sb.Append("            CASE ");
            sb.Append("                WHEN (LAG(h.[" + _src_col_name + "]) OVER( ");
            sb.Append("                     ORDER BY  ");
            sb.Append("                              h.symbol,  ");
            sb.Append("                              h.date)) IS NULL THEN 0 ");
            sb.Append("                WHEN h.[" + _src_col_name + "] <> 0 ");
            sb.Append("                THEN(h.[" + _src_col_name + "] - ISNULL((LAG(h.[" + _src_col_name + "]) OVER( ");
            sb.Append("                     ORDER BY  ");
            sb.Append("                              h.symbol,  ");
            sb.Append("                              h.date)), 0)) / h.[" + _src_col_name + "] ");
            sb.Append("                ELSE 0 ");
            sb.Append("            END change_perc ");
            sb.Append("     FROM daily_history h ");
            sb.Append("     WHERE h.symbol = '"+ _symbol + "' ");
            sb.Append(" ) u ");
            sb.Append(" ON t.symbol = u.symbol ");
            sb.Append("    AND t.date = u.date ");
            sb.Append("     WHEN MATCHED ");
            sb.Append("     THEN UPDATE SET  ");
            sb.Append("                     change_perc = u.change_perc; ");


            return sb.ToString();
        }
        public static string update_forecast(string _symbol, string _table_name, string _src_col_name, string _days)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" MERGE "+ _table_name + " v ");
            sb.Append(" USING ");
            sb.Append(" ( ");
            sb.Append("     SELECT  ");
            sb.Append("            h.[symbol],  ");
            sb.Append("            h.[date],  ");
            sb.Append("            h.[" + _src_col_name + "] as cur_value,  ");
            sb.Append("            LEAD(h.["+ _src_col_name + "], "+ _days + ") OVER( ");
            sb.Append("            ORDER BY  ");
            sb.Append("                     h.[date]) AS after_value ");
            sb.Append("     FROM daily_history h ");
            sb.Append("     WHERE h.symbol = '"+ _symbol+"' ");
            sb.Append(" ) u ");
            sb.Append(" ON v.symbol = u.symbol ");
            sb.Append("    AND v.date = u.date ");
            sb.Append("     WHEN MATCHED ");
            sb.Append("     THEN UPDATE SET  ");
            sb.Append("                     v.after_" + _days + "_perc = case when u.cur_value = 0 ");
            sb.Append("                       then 0 else (u.after_value - u.cur_value) / u.cur_value end; ");

            return sb.ToString();
        }
        public static string update_epr_fiscal(string _symbol, string _table_name, string _src_col_name)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" WITH eps ");
            sb.Append("      AS (SELECT  ");
            sb.Append("                 a.symbol,  ");
            sb.Append("                 a.fiscaldateending AS fiscaldateending_start,  ");
            sb.Append("                 LEAD(a.fiscaldateending) OVER(PARTITION BY a.symbol ");
            sb.Append("                 ORDER BY  ");
            sb.Append("                          a.fiscaldateending) AS fiscaldateending_end,  ");
            sb.Append("                 a.reporteddate AS reporteddate_start,  ");
            sb.Append("                 LEAD(a.reporteddate) OVER(PARTITION BY a.symbol ");
            sb.Append("                 ORDER BY  ");
            sb.Append("                          a.reporteddate) AS reporteddate_end,  ");
            sb.Append("                 a.reportedeps,  ");
            sb.Append("                 a.surprise,  ");
            sb.Append("                 a.surprisepercentage ");
            sb.Append("          FROM earning a) ");
            sb.Append("      UPDATE u ");
            sb.Append("        SET  ");
            sb.Append("            u.per_fiscal = CASE ");
            sb.Append("                               WHEN e.reportedeps <= 0 ");
            sb.Append("                               THEN 0 ");
            sb.Append("                               ELSE h.[" + _src_col_name + "] / e.reportedeps ");
            sb.Append("                           END ");
            sb.Append("      FROM  " + _table_name + " u, daily_history h, eps e ");
            sb.Append("      WHERE  ");
            sb.Append("            u.symbol = '"+ _symbol + "' ");
            sb.Append("            AND h.symbol = e.symbol ");
            sb.Append("            AND u.symbol = e.symbol ");
            sb.Append("            AND u.[date] = h.[date] ");
            sb.Append("            AND u.[date] BETWEEN e.fiscaldateending_start AND DATEADD(day, -1, e.fiscaldateending_end); ");

            return sb.ToString();
        }
        public static string update_epr_report(string _symbol, string _table_name, string _src_col_name)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" WITH eps ");
            sb.Append("      AS (SELECT  ");
            sb.Append("                 a.symbol,  ");
            sb.Append("                 a.fiscaldateending AS fiscaldateending_start,  ");
            sb.Append("                 LEAD(a.fiscaldateending) OVER(PARTITION BY a.symbol ");
            sb.Append("                 ORDER BY  ");
            sb.Append("                          a.fiscaldateending) AS fiscaldateending_end,  ");
            sb.Append("                 a.reporteddate AS reporteddate_start,  ");
            sb.Append("                 LEAD(a.reporteddate) OVER(PARTITION BY a.symbol ");
            sb.Append("                 ORDER BY  ");
            sb.Append("                          a.reporteddate) AS reporteddate_end,  ");
            sb.Append("                 a.reportedeps,  ");
            sb.Append("                 a.surprise,  ");
            sb.Append("                 a.surprisepercentage ");
            sb.Append("          FROM earning a) ");
            sb.Append("      UPDATE u ");
            sb.Append("        SET  ");
            sb.Append("            u.per_report = CASE ");
            sb.Append("                               WHEN e.reportedeps <= 0 ");
            sb.Append("                               THEN 0 ");
            sb.Append("                               ELSE h.[" + _src_col_name + "] / e.reportedeps ");
            sb.Append("                           END ");
            sb.Append("      FROM  " + _table_name + " u, daily_history h, eps e ");
            sb.Append("      WHERE  ");
            sb.Append("            u.symbol = '" + _symbol + "' ");
            sb.Append("            AND h.symbol = e.symbol ");
            sb.Append("            AND u.symbol = e.symbol ");
            sb.Append("            AND u.[date] = h.[date] ");
            sb.Append("            AND u.[date] BETWEEN e.reporteddate_start AND DATEADD(day, -1, e.reporteddate_end); ");

            return sb.ToString();
        }
    }
}
