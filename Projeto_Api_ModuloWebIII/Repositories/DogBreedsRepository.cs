using DogBreedsAPI.Interfaces;
using DogBreedsAPI.Models;
using System.Text.Json;

namespace DogBreedsAPI.Repositories
{
    public class DogBreedsRepository : IDogBreedsRepository
    {
        private List<DogBreeds> breeds;

        public DogBreedsRepository()
        {
            using var reader = new StreamReader("./racas.json");
            var json = reader.ReadToEnd();
            breeds = JsonSerializer.Deserialize<List<DogBreeds>>(json);
            reader.Dispose();
        }

        public Task<int> Delete(int key)
        {
            return Task.Run(() =>
            {
                var entity = breeds.Any(x => x.Id == key);

                if (entity == false)
                {
                    throw new Exception("ID inexistente.");
                }
                var delete = breeds.Find(x => x.Id == key);
                breeds.Remove(delete);
                var content = JsonSerializer.Serialize(breeds);
                System.IO.File.WriteAllText("./racas.json", content);
                return key;
            });
        }

        public Task<IQueryable<DogBreeds>> Get(int page, int maxResults)
        {
            return Task.Run(() =>
            {
                var data = breeds.AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                return data.Any() ? data : new List<DogBreeds>().AsQueryable(); ;
            });
        }

        public Task<IQueryable<DogBreeds>> GetFilter(int page, int maxResults, string? dogType, string? country, string? characteristic)
        {
            return Task.Run(() =>
            {
                if(string.IsNullOrWhiteSpace(dogType) && !(string.IsNullOrWhiteSpace(country)) && !(string.IsNullOrWhiteSpace(characteristic)))
                {
                    var data1 = breeds.Where(x => x.Origin.ToLower().Equals(country.ToLower())).Where(x => x.Characteristic.ToLower().Contains(characteristic.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data1.Any() ? data1 : new List<DogBreeds>().AsQueryable();
                }
                else if (!(string.IsNullOrWhiteSpace(dogType)) && string.IsNullOrWhiteSpace(country) && !(string.IsNullOrWhiteSpace(characteristic)))
                {
                    var data2 = breeds.Where(x => x.DogType.ToLower().Equals(dogType.ToLower())).Where(x => x.Characteristic.ToLower().Contains(characteristic.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data2.Any() ? data2 : new List<DogBreeds>().AsQueryable();
                }
                else if (!(string.IsNullOrWhiteSpace(dogType)) && !(string.IsNullOrWhiteSpace(country)) && string.IsNullOrWhiteSpace(characteristic))
                {
                    var data3 = breeds.Where(x => x.DogType.ToLower().Equals(dogType.ToLower())).Where(x => x.Origin.ToLower().Equals(country.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data3.Any() ? data3 : new List<DogBreeds>().AsQueryable();
                }
                else if(string.IsNullOrWhiteSpace(dogType) && string.IsNullOrWhiteSpace(country) && !(string.IsNullOrWhiteSpace(characteristic)))
                {
                    var data4 = breeds.Where(x => x.Characteristic.ToLower().Contains(characteristic.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data4.Any() ? data4 : new List<DogBreeds>().AsQueryable();
                }
                else if(string.IsNullOrWhiteSpace(dogType) && !(string.IsNullOrWhiteSpace(country)) && string.IsNullOrWhiteSpace(characteristic))
                {
                    var data5 = breeds.Where(x => x.Origin.ToLower().Equals(country.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data5.Any() ? data5 : new List<DogBreeds>().AsQueryable();
                }
                else if (!(string.IsNullOrWhiteSpace(dogType)) && string.IsNullOrWhiteSpace(country) && string.IsNullOrWhiteSpace(characteristic))
                {
                    var data6 = breeds.Where(x => x.DogType.ToLower().Equals(dogType.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data6.Any() ? data6 : new List<DogBreeds>().AsQueryable();
                }
                else if (string.IsNullOrWhiteSpace(dogType) && string.IsNullOrWhiteSpace(country) && string.IsNullOrWhiteSpace(characteristic))
                {
                    Get(page, maxResults);
                }
                var data7 = breeds.Where(x => x.DogType.ToLower().Equals(dogType.ToLower())).Where(x => x.Origin.ToLower().Equals(country.ToLower())).Where(x => x.Characteristic.ToLower().Contains(characteristic.ToLower())).OrderBy(x => x.DogType).AsQueryable().Skip((page - 1) * maxResults).Take(maxResults);
                    return data7.Any() ? data7 : new List<DogBreeds>().AsQueryable();
            });
        }

        public Task<DogBreeds?> GetByKey(int key)
        {
            return Task.Run(() =>
            {
                return breeds.Find(x => x.Id == key);
            });
        }

        public Task<DogBreeds> Insert(DogBreeds newBreed)
        {
            return Task.Run(() =>
            {
                breeds.Add(newBreed);
                var content = JsonSerializer.Serialize(breeds);
                System.IO.File.WriteAllText("./racas.json", content);
                return newBreed;
            });
        }

        public Task<DogBreeds> Update(DogBreeds updateBreed)
        {
            return Task.Run(() =>
            {
                var searchDog = breeds.Any(x => x.Id == updateBreed.Id);

                if (searchDog == false)
                {
                    throw new Exception("Raça inexistente.");
                }
                breeds.FirstOrDefault(x => x.Id == updateBreed.Id).DogType = updateBreed.DogType;
                breeds.FirstOrDefault(x => x.Id == updateBreed.Id).Origin = updateBreed.Origin;
                breeds.FirstOrDefault(x => x.Id == updateBreed.Id).Weight = updateBreed.Weight;
                breeds.FirstOrDefault(x => x.Id == updateBreed.Id).Height = updateBreed.Height;
                breeds.FirstOrDefault(x => x.Id == updateBreed.Id).LifeExpectancy = updateBreed.LifeExpectancy;
                breeds.FirstOrDefault(x => x.Id == updateBreed.Id).Characteristic = updateBreed.Characteristic;
                var content = JsonSerializer.Serialize(breeds);
                System.IO.File.WriteAllText("./racas.json", content);
                return updateBreed;
            });
        }

        public int LastId()
        {
            int lastId = breeds.Max(x => x.Id);
            return lastId + 1;
        }
    }
}
