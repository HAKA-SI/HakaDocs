using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Dtos
{
    public class SubProductAddingDto
    {
        public string Name { get; set; }
    // public int? TypeId { get; set; }
    public int? CategoryId { get; set; }
    public int ProductId { get; set; }
    public bool WithSerialNumber { get; set; }
    // public bool Discontinued { get; set; }= false;
    public int UnitPrice { get; set; } = 0;
    public int QuantityPerUnite { get; set; } = 1;
    public IFormFile MainPhotoFile { get; set; }
    public List<IFormFile> OtherPhotoFiles { get; set; }
    public int ReorderLevel { get; set; } 

    public int Quantity { get; set; }
    public string Note { get; set; }
    }
}