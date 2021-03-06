﻿using System;
using System.Collections.Generic;

namespace Sc.Blog.Abstractions.Repositories
{
    public interface IRepository<T, TKey> where T : class, new()
    {
        IEnumerable<T> GetAll();
        T Get(TKey id);
        bool Update(T entity);
        bool Delete(TKey id);
        bool Create(T entity);
        IList<Exception> RepositoryErrors { get; }
    }
}
