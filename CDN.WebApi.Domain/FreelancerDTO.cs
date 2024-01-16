using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDN.WebApi.Domain
{
    public class FreelancerDTO
    {
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Mail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Skillsets { get; set; }
        public string? Hobby { get; set; }

    }
}
