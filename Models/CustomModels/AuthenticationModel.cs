using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class AuthenticationModel
    {
        public int UserId { get; set; }
        public string? AuthKey { get; set; }
        public string? ImageUrl { get; set; }
        public string? Code { get; set; }
    }
}
