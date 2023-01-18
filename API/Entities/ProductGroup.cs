using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductGroup: BaseEntity
    {
        public enum ProductGroupIds{
            Physical =1,
            Digital=2
        }
     public string Name { get; set; }   
    }
}