using System;
using System.Collections.Generic;

namespace BuildingApp.Models
{
    public partial class Price
    {
        public Price()
        {
            Room = new HashSet<Room>();
            Type = new HashSet<Type>();
        }

        public long PriceID { get; set; }
        public long? Price1 { get; set; }

        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<Type> Type { get; set; }
        
    }
}
