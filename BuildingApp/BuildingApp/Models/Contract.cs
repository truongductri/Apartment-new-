using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class Contract
    {
        public int ConstractID{ get; set; }
        public string Name { get; set; }
        public int? RoomID { get; set; }
        public int? CustomerID { get; set; }
        public int? NumOfMonth { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? AmountOfMoney { get; set; }
        public string MetaTitle { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
