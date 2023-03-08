using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class CaptchaModel
    {
        public string ImageBase64 { get; set; }
        public string code { get; set; }
    }
}
