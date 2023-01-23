using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InventopForListDto
    {
         public int Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        // public string Version { get; set; }
          public int? InventOpTypeId { get; set; }
        public DateTime OpDate { get; set; }
        public int? FromStoreId { get; set; }
        public int? FromEmployeeId { get; set; }
        public int? ToStoreId { get; set; }
        public int? ToEmployeeId { get; set; }
         public string FormNum { get; set; }
        public byte Status { get; set; }
              public int? InsertUserId { get; set; }

        public  UserDto FromEmployee { get; set; }
        public  StoreListDto FromStore { get; set; }
        public  string InventOpType { get; set; }
        public  UserDto ToEmployee { get; set; }
        //  public  UserDto InsertUser { get; set; }
        public  StoreListDto ToStore { get; set; }
        public int? SubProductId { get; set; }
        public SubProductListDto SubProduct { get; set; }
        public int? Quantity { get; set; }
    }
}