using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class Room
    {
        public Room()
        {
            Contract = new HashSet<Contract>();
        }

        public int RoomID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? TypeID { get; set; }
        public long? PriceID { get; set; }
        public int? Capacity { get; set; }
        public int? Floor { get; set; }
        public string MetaTiTle { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public virtual Price Price { get; set; }
        public virtual Type Type { get; set; }
    }
}
