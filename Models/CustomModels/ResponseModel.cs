using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class ResponseModel
    {
        public ResponseModel(bool status, string message, object data)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
        }

        public bool Status { get; set; }
        public string? Message { get; set; }
        public object Data { get; set; }
    }
}
