using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class District
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public byte? DistrictType { get; set; }
        public int? ProvinceID { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
