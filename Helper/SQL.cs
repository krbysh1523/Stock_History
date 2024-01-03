using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    
    public static class SQL
    {
        public static string get_sp_filter_name()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT o.[name]                    ");
            sb.Append("   FROM sys.objects o               ");
            sb.Append("  WHERE o.[type] = 'P'              ");
            sb.Append("        AND o.[name] LIKE 'filter%' ");
            sb.Append(" ORDER BY o.[name]                  ");
            return sb.ToString();
        }
    }
}
