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
    
    public partial class daily_analysis_v1
    {
        public string symbol { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<double> open_close_perc { get; set; }
        public Nullable<double> high_low_perc { get; set; }
        public string dir { get; set; }
        public Nullable<double> oc_by_hl_perc { get; set; }
        public Nullable<int> dir_on { get; set; }
        public string TMB { get; set; }
        public string tmb_no { get; set; }
        public Nullable<double> VT { get; set; }
        public Nullable<double> VM { get; set; }
        public Nullable<double> VB { get; set; }
        public Nullable<double> TMB_R1 { get; set; }
        public Nullable<double> TMB_R2 { get; set; }
        public Nullable<double> TMB_R3 { get; set; }
        public double reportedEPS { get; set; }
        public double surprisePercentage { get; set; }
    }
}
