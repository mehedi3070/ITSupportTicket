//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITSupportTicket
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAssigned
    {
        public int AssignedId { get; set; }
        public Nullable<int> TicketId { get; set; }
        public string StaffName { get; set; }
        public string Email { get; set; }
    
        public virtual tblTicket tblTicket { get; set; }
    }
}
