using System;
using System.Collections.Generic;

namespace Apartment.Models
{
    public partial class Province
    {
        public Province()
        {
            Customer = new HashSet<Customer>();
            District = new HashSet<District>();
        }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public byte? ProvinceType { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<District> District { get; set; }
    }
}
