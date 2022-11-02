using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class PagedResponse<T>:BaseResponse<T>
    {
        public int TotalRecords { get; set; }

        public PagedResponse(T data, int totalRecord)
        {
            this.TotalRecords = totalRecord;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
