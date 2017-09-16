using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class Province
    {
        public Province()
        {
            District = new HashSet<District>();
        }

        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public byte? ProvinceType { get; set; }

        public virtual ICollection<District> District { get; set; }
        public virtual ICollection<Customer> Customer{ get; set; }
    }
}
