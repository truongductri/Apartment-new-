//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BuildingApp.Models;
//using BuildingApp.Models.ModelView;
//using Microsoft.EntityFrameworkCore;

//namespace BuildingApp.Dao
//{
//    public class ContractDao
//    {

//        private readonly ApartmentDBContext _context;



//        public ContractDao(ApartmentDBContext context)
//        {
//            _context = context;
//        }
//        public List<ContractViewModel> ListContractView()
//        {

//            var model = (from c in _context.Contract
//                         join a in _context.Room on c.RoomID equals a.RoomID

//                         join b in _context.Customer on c.CustomerID equals b.CustomerID



//                         select new
//                         {
//                             CustomerID = b.CustomerID,
//                             CustomerName = b.Name,
//                             Age = b.Age,
//                             Identify = b.Identify,
//                             BirthDay = b.BirthDay,
//                             Phone = b.Phone,
//                             Email = b.Email,
//                             RoomID = a.RoomID,
//                             RoomName = a.Name,
//                             TypeID = a.TypeID,
//                             PriceID = a.PriceID,
//                             Capacity = a.Capacity,
//                             Floor = a.Floor,



//                         }).AsEnumerable().Select(x => new ContractViewModel()
//                         {
//                             CustomerID = x.CustomerID,
//                             CustomerName = x.CustomerName,
//                             Age = x.Age,
//                             Identify = x.Identify,
//                             BirthDay = x.BirthDay,
//                             Phone = x.Phone,
//                             Email = x.Email,
//                             RoomID = x.RoomID,


//                             RoomName = x.RoomName,
//                             TypeID = x.TypeID,
//                             PriceID = x.PriceID,
//                             Capacity = x.Capacity,
//                             Floor = x.Floor,
//                         });
          
//            return model;
//        }
//    }
//}
