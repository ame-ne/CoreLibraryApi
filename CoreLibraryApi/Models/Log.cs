using CoreLibraryApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibraryApi.Models
{
    public class Log : BaseEntity
    {
        public string ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        public string Url { get; set; }
        public LogActionEnum ActionResult { get; set; }
        public string Message { get; set; }
    }
}
