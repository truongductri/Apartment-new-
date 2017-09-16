using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class Type
    {
        public Type()
        {
            Room = new HashSet<Room>();
        }

        public int TypeID { get; set; }
        public string Name { get; set; }
        public long PriceID { get; set; }

        public virtual ICollection<Room> Room { get; set; }
        public virtual Price Price { get; set; }
    }
}
