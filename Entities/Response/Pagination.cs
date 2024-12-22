using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class Pagination<TModel>
    {
        public List<TModel> Items { get; set; } = new List<TModel>();
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
    }
}
