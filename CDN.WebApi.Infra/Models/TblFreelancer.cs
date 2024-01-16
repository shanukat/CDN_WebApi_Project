using System;
using System.Collections.Generic;

namespace CDN.WebApi.Infra.Models
{
    public partial class TblFreelancer
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Mail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Skillsets { get; set; }
        public string? Hobby { get; set; }
    }
}
