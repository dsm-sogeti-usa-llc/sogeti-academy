using System;
using Sogeti.Academy.Infrastructure.Models;
using Sogeti.Academy.Persistence.Storage;

namespace Sogeti.Academy.Application.Storage
{
    public interface IDocumentContext : IDisposable
    {
        IDocumentCollection<T> GetCollection<T>() where T : IModel<string>;
    }
}