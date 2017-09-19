using System;
using System.Collections.Generic;

namespace Apartment.Models
{
    public partial class Room
    {
        public Room()
        {
            Contract = new HashSet<Contract>();
        }

        public int RoomId { get; set; }
        public string RoomNo { get; set; }
        public int? RoomTypeId { get; set; }
        public long? Price { get; set; }
        public string Area { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Contract> Contract { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
