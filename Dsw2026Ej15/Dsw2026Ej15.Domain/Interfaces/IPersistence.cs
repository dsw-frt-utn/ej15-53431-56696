using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<List<T>> GetAllAsync<T>() where T : BaseEntity;
        Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity;
        Task AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<bool> SaveChangesAsync();
    }
}