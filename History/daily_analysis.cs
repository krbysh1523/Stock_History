//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class daily_analysis
    {
        public string symbol { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<double> open_close_perc { get; set; }
        public Nullable<double> high_low_perc { get; set; }
        public string dir { get; set; }
        public Nullable<double> oc_by_hl_perc { get; set; }
        public Nullable<int> dir_on { get; set; }
        public Nullable<int> point1 { get; set; }
        public Nullable<int> point2 { get; set; }
        public Nullable<int> point3 { get; set; }
    }
}
