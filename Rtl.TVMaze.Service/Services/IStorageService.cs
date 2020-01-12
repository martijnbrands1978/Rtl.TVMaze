using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Rtl.TVMaze.Domain.Model;

namespace Rtl.TVMaze.Service.Services
{
    public interface IStorageService
    {
        Task<Document> CreateItemAsync(Show item);
        Task<IEnumerable<Show>> GetItemsAsync(int page);
    }
}
