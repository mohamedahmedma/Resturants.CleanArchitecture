using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Common
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items , int TotalCount  , int pagesize , int pagenumber) 
        {
            Items = items ;
            TotalItemscount = TotalCount;
            TotalPages = (int)Math.Ceiling(TotalCount/(double)pagesize );
            ItemsFrom = pagesize * (pagenumber - 1)+1;
            ItemsTo = ItemsFrom + pagesize -1;
        }
        public IEnumerable<T> Items { get; set; }
        public int TotalPages {  get; set; }
        public int TotalItemscount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
    }
}
