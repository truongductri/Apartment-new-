using System;
using System.Collections.Generic;

namespace Apartment.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contract = new HashSet<Contract>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? IdentityCardNo { get; set; }
        public int? PhoneNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool? Status { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public virtual District District { get; set; }
        public virtual Province Province { get; set; }
    }
}
