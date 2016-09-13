//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndicoPacking.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public int ID { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileTelephoneNumber { get; set; }
        public string OfficeTelephoneNumber { get; set; }
        public Nullable<System.DateTime> DateLastLogin { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool GenderMale { get; set; }
    
        public virtual Role Role1 { get; set; }
        public virtual UserStatu UserStatu { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}