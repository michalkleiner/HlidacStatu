//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HlidacStatu.Lib.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class WatchDog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Expires { get; set; }
        public int StatusId { get; set; }
        public string SearchTerm { get; set; }
        public string SearchRawQuery { get; set; }
        public int RunCount { get; set; }
        public int SentCount { get; set; }
        public int PeriodId { get; set; }
        public Nullable<System.DateTime> LastSent { get; set; }
        public Nullable<System.DateTime> LastSearched { get; set; }
        public int ToEmail { get; set; }
        public int ShowPublic { get; set; }
        public string Name { get; set; }
        public int FocusId { get; set; }
        public string dataType { get; set; }
        public string SpecificContact { get; set; }
        public Nullable<System.DateTime> LatestRec { get; set; }
    }
}