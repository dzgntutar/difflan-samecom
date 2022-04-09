using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttar.Common.Models
{
    public class Response<T>
    {
        public  T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
    }
}
