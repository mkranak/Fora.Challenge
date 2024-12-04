namespace Fora.Challenge.Application.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T : class
    {
        /// <summary>
        /// Gets the entity by the identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity inside a task.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Get all entities asynchronously.
        /// </summary>
        /// <returns>The entities inside a task.</returns>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Adds the entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates the entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes the entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A task.</returns>
        Task DeleteAsync(T entity);
    }
}
