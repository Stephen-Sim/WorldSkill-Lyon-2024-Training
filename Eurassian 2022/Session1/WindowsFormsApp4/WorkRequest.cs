//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp4
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkRequest
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime FullfilledDate { get; set; }
        public string Address { get; set; }
        public string TaskDescription { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
        public Nullable<System.DateTime> BeginWorkTime { get; set; }
        public Nullable<System.DateTime> CompleteWorkTime { get; set; }
        public int WorkTypeId { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual WorkType WorkType { get; set; }
    }
}
