﻿namespace WebApiOwinMiddleware.Repositories
{
    using Models;

    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity[] GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}