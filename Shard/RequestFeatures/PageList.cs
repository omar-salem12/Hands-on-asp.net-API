using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.RequestFeatures
{
     public  class PageList<T> :List<T>
    {
        public MetaData metaData { get; set; }

        public PageList(List<T> items, int count, int pageNumber, int pageSize) 
        {
            metaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);
        } 

    }
}
