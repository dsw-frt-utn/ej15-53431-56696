using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Data
{
    // Simple in-memory persistence for the exercise
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new();
        private readonly List<Speciality> _specialities = new();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");
                if (!File.Exists(path)) return;
                var json = File.ReadAllText(path);
                var items = JsonSerializer.Deserialize<List<Speciality>>(json);
                if (items != null) _specialities.AddRange(items);
            }
            catch
            {
                // ignore load errors; start with empty list
            }
        }

        public Task<List<T>> GetAllAsync<T>() where T : BaseEntity
        {
            if (typeof(T) == typeof(Doctor))
            {
                var list = _doctors.FindAll(x => x.IsActive).ConvertAll(x => (T)(object)x);
                return Task.FromResult(list);
            }
            if (typeof(T) == typeof(Speciality))
            {
                var list = _specialities.ConvertAll(x => (T)(object)x);
                return Task.FromResult(list);
            }
            throw new NotSupportedException($"Type {typeof(T).Name} is not supported.");
        }

        public Task<T?> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            if (typeof(T) == typeof(Doctor))
            {
                var d = _doctors.Find(x => x.Id == id && x.IsActive);
                return Task.FromResult((T)(object?)d);
            }
            if (typeof(T) == typeof(Speciality))
            {
                var s = _specialities.Find(x => x.Id == id);
                return Task.FromResult((T)(object?)s);
            }
            throw new NotSupportedException($"Type {typeof(T).Name} is not supported.");
        }

        public Task AddAsync<T>(T entity) where T : BaseEntity
        {
            entity.Id = Guid.NewGuid();
            if (typeof(T) == typeof(Doctor))
            {
                _doctors.Add((Doctor)(object)entity);
            }
            else if (typeof(T) == typeof(Speciality))
            {
                _specialities.Add((Speciality)(object)entity);
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(T).Name} is not supported for Add.");
            }
            return Task.CompletedTask;
        }

        public Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            if (typeof(T) == typeof(Doctor))
            {
                var doctor = (Doctor)(object)entity;
                var idx = _doctors.FindIndex(x => x.Id == doctor.Id);
                if (idx >= 0) _doctors[idx] = doctor;
            }
            else if (typeof(T) == typeof(Speciality))
            {
                var speciality = (Speciality)(object)entity;
                var idx = _specialities.FindIndex(x => x.Id == speciality.Id);
                if (idx >= 0) _specialities[idx] = speciality;
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(T).Name} is not supported for Update.");
            }
            return Task.CompletedTask;
        }

        public Task<bool> SaveChangesAsync() => Task.FromResult(true);
    }
}
