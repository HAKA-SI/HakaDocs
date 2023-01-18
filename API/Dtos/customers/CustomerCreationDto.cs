using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CustomerCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
         public byte? Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string CustomerCode { get; set; }
        public string SecondPhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime Created { get; set; } =DateTime.Now;
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? BirthCountryId { get; set; }
        public int? BirthCityId { get; set; }
        public int? BirthDistrictId { get; set; }
        public int? MaritalStatusId { get; set; }
        public bool Active { get; set; } =true;
        public Boolean Validated { get; set; } =true;
        public string ToBeValidatedEmail { get; set; }
        public Boolean AccountDataValidated { get; set; } =false;    
        public string PostalBox { get; set; }
          public string Cni { get; set; }
        public string Passport { get; set; }
        public string Iddoc { get; set; }
        public string Email { get; set; }

    }
}