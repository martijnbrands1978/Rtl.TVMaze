using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace Rtl.TVMaze.Service.Services
{
    public interface IStorageService<T> where T : class
    {
        Task<Document> CreateItemAsync(T item);
        Task<IEnumerable<T>> GetItemsAsync(int page);
    }
}
