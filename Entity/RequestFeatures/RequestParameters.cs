using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pagesSize;

        public int PageSize
        {
            get { return _pagesSize; }
            set { _pagesSize = value > maxPageSize ? maxPageSize : value; }
        }
        public string? OrderBy { get; set; }
    }
}
