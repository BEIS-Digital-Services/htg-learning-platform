﻿namespace Beis.LearningPlatform.Data.Repositories.Base
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IFeedbackGenericRepository<TEntity> : IRepositoryBase
        where TEntity : class
    {
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        IEnumerable<TEntity> GetAll(int page, int size);
        Task<IEnumerable<TEntity>> GetAllAsync(int page, int size);
        Task<IEnumerable<TEntity>> GetAllAsync();

        int Count(Func<TEntity, bool> filterExpression = null, string[] Includes = null);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}