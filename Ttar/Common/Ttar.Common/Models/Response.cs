using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttar.Common.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public short HtmlStatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }



        public static Response<T> Success(T Data, short statusCode)
        {
            return new Response<T> { Data = Data, HtmlStatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(short statusCode)
        {
            return new Response<T> { Data = default(T), HtmlStatusCode = statusCode, IsSuccessful = true };
        }



        public static Response<T> Fail(List<string> errors, short statusCode)
        {
            return new Response<T> { Errors = errors, HtmlStatusCode = statusCode, IsSuccessful = false };
        }

        public static Response<T> Fail(string error, short statusCode)
        {
            return new Response<T> { Errors = new List<string> { error }, HtmlStatusCode = statusCode, IsSuccessful = false };
        }
    }
}
