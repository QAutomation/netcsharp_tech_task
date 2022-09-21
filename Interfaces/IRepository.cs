using PBITracker.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBITracker.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IEnumerable<T>> FindAfterDate(DateTime dateTime);
        Task<IEnumerable<T>> FindById(params int[] values);
        Task<IEnumerable<T>> FindAll();
    }
}