﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity, string condition);
        T Get(T entity, string condition);
        List<T> GetAll(T entity);
        List<T>Find(T entity, string condition);
    }
}
