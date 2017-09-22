﻿using System;
using System.Collections.Generic;

namespace Apartment.Models
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }
        public int? EngageMonth { get; set; }
        public DateTime? BeginDate { get; set; }
        public long? ContractValue { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
