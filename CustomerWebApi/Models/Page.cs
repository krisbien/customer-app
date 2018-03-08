using CustomerWebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerWebApi.Models
{
    public class Page<T>: IPage<T> where T: class, IEntity
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int AllCount { get; set; }
        public IList<T> Items { get; set; }
    }
}
