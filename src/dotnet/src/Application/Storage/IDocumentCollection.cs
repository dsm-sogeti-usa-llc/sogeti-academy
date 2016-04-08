using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sogeti.Academy.Infrastructure.Models;

namespace Sogeti.Academy.Application.Storage
{
    public interface IDocumentCollection<T> where T : IModel<string>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<string> CreateAsync(T item);
        Task UpdateAsync(T item);
        Task<T> GetByIdAsync(string id);
        Task RemoveById(string id);
    }
}