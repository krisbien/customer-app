using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerWebApi.Interfaces
{
    public interface IPage<T> where T : class, IEntity
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int AllCount { get; set; }
        IList<T> Items { get; set; }
    }
}
