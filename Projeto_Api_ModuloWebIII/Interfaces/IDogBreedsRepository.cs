using DogBreedsAPI.Models;

namespace DogBreedsAPI.Interfaces
{
    public interface IDogBreedsRepository
    {
        Task<IQueryable<DogBreeds>> Get(int page, int maxResults);

        Task<DogBreeds?> GetByKey(int key);

        public Task<IQueryable<DogBreeds>> GetFilter(int page, int maxResults, string? dogType, string? country, string? characteristic);

        Task<DogBreeds> Insert(DogBreeds newBreed);

        Task<DogBreeds> Update(DogBreeds updateBreed);

        Task<int> Delete(int key);
        int LastId();
    }
}
