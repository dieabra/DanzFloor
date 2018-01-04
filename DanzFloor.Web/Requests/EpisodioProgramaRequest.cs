using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Requests
{
    public class EpisodioProgramaRequest: PaginatorRequest
    {
        public Guid ProgramaId { get; set; }
    }
}