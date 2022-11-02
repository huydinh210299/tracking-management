using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Succeeded = true;
        }
        public BaseResponse(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Errors { get; set; }
        public string Message { get; set; }
    }
}
