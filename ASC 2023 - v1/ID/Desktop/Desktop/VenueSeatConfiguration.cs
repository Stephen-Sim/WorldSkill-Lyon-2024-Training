//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Desktop
{
    using System;
    using System.Collections.Generic;
    
    public partial class VenueSeatConfiguration
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public int SeatClassId { get; set; }
        public int Capacity { get; set; }
    
        public virtual SeatClass SeatClass { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
