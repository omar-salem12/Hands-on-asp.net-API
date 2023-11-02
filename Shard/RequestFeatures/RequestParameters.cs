using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.RequestFeatures
{
    public class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public  int pageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                if (_pageSize > maxPageSize)
                    _pageSize = maxPageSize;
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
