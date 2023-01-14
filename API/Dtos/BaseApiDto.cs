using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Dtos
{
    public class BaseApiDto
    {
        public string PropValue { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}