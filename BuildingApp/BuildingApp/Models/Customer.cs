using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
        }

        public int CustomerID { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public int? ProvinceID { get; set; }
        public int? DistrictID { get; set; }
        public int? Identify { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public string MetaTitle { get; set; }

        public virtual Province Province { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        
    }
}
