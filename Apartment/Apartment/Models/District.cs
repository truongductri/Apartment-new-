using System;
using System.Collections.Generic;

namespace Apartment.Models
{
    public partial class District
    {
        public District()
        {
            Customer = new HashSet<Customer>();
        }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public byte? DistrictType { get; set; }
        public int? ProvinceId { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual Province Province { get; set; }
    }
}
