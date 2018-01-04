using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web
{
    public class PaginatorRequest
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}