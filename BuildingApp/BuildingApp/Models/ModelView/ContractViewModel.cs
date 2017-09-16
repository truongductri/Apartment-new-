using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingApp.Models.ModelView
{
    public class ContractViewModel
    {
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int? Age { get; set; }
        public int? Identify { get; set; }
        public DateTime? BirthDay{ get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        

        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int? TypeID { get; set; }
        public long? PriceID { get; set; }
        public int? Capacity { get; set; }
        public int? Floor { get; set; }
        public bool? Status { get; set; }
       
    }
}
