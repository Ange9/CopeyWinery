//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CopeyWinery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public int Task_Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Number_hours { get; set; }
        public string Hour_type { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Unit { get; set; }
        public Nullable<int> User { get; set; }
        public Nullable<int> Activity { get; set; }
        public Nullable<int> Labor { get; set; }
        public Nullable<int> Location { get; set; }
        public Nullable<int> Lane { get; set; }
    
        public virtual Activity Activity1 { get; set; }
        public virtual Labor Labor1 { get; set; }
        public virtual Lane Lane1 { get; set; }
        public virtual Location Location1 { get; set; }
        public virtual User User1 { get; set; }
    }
}
