using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Paging
{
    public class PageParameters
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SearchText { get; set; }
        public string SortColumn { get; set; }
    }
}
