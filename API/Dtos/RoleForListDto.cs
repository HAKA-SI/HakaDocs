using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class RoleForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalUsers { get; set; }=0;
    }
}