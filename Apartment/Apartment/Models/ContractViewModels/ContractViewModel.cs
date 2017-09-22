using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apartment.Models.ContractViewModels
{
    public class ContractViewModel
    {
        public int ContractId { get; set; }
        public string ContractNo { get; set; }

        public string CustomerName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }
        public int? IdentityCardNo { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }
        public int? PhoneNo { get; set; }
        public string Email { get; set; }

        public string RoomNo { get; set; }
        public int? RoomTypeId { get; set; }
        public long? Price { get; set; }
        public string Area { get; set; }
        public int? EngageMonth { get; set; }
        public long? ContractValue { get; set; }

    }
}
