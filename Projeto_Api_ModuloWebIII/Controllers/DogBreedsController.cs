using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DogBreedsAPI.DTO;
using DogBreedsAPI.FIlters;
using DogBreedsAPI.Interfaces;
using DogBreedsAPI.Models;
using System.Net.Mime;

namespace DogBreedsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogBreedsController : ControllerBase
    {

        private readonly IDogBreedsRepository _repository;

        public DogBreedsController(IDogBreedsRepository repository, ILogger<DogBreeds> logger, IConfiguration configuration)
        {
            _repository = repository;
        }

        private DogBreeds UpdateBreeds(DogBreeds breed, DogBreedsDTO breedDto)
        {
            breed.DogType = breedDto.DogType;
            breed.Origin = breedDto.Origin;
            breed.Weight = breedDto.Weight;
            breed.Height = breedDto.Height;
            breed.LifeExpectancy = breedDto.LifeExpectancy;
            breed.Characteristic = breedDto.Characteristic;
            return breed;
        }

        private DogBreeds UpdateBreedsPatch(DogBreeds breed, DogBreedsPatchDTO patchDto)
        {
            breed.Characteristic = patchDto.Characteristic;
            return breed;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<DogBreeds>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromQuery] int page, int maxResults)
        {
           var breeds = await _repository.Get(page, maxResults);
            return Ok(breeds);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var breeds = await _repository.GetByKey(id);
            if (breeds == null)
            {
                Console.WriteLine(breeds?.Id);
                return NotFound("Id Inexistente");
            }
            return Ok(breeds);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] DogBreedsDTO entity)
        {
            var databaseBreeds = await _repository.GetByKey(id);

            if (databaseBreeds == null)
            {
                var lastId = _repository.LastId();
                var breedToInsert = new DogBreeds(lastId, entity.DogType, entity.Origin, entity.Weight, entity.Height, entity.LifeExpectancy, entity.Characteristic);
                var inserted = await _repository.Insert(breedToInsert);
                return Created(string.Empty, inserted);
            }
            var updating = UpdateBreeds(databaseBreeds, entity);
            var updated = await _repository.Update(updating);
            return Ok(updated);
        }

        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> Post([FromBody] DogBreedsDTO entity)
        {
            var lastId = _repository.LastId();
            var breedToInsert = new DogBreeds(lastId, entity.DogType, entity.Origin, entity.Weight, entity.Height, entity.LifeExpectancy, entity.Characteristic);
            var inserted = await _repository.Insert(breedToInsert);
            return Created(string.Empty, inserted);
        }

        [HttpPost("/query")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> GetFilter([FromBody] FilterDogBreedsDTO filter)
        {
            var breeds = await _repository.GetFilter(filter.Page, filter.PageSize, filter.DogType, filter.Origin, filter.Characteristic);
            if (breeds == null)
            {
                return NoContent();
            }
            return Ok(breeds);
        }

        [HttpPatch("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] DogBreedsPatchDTO entity)
        {
            var databaseBreeds = await _repository.GetByKey(id);

            if (databaseBreeds == null)
            {
                return NoContent();
            }
            var updating = UpdateBreedsPatch(databaseBreeds, entity);
            var updated = await _repository.Update(updating);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DogBreeds), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var databaseBreed = await _repository.GetByKey(id);

            if (databaseBreed == null)
            {
                return NoContent();
            }

            var deleted = await _repository.Delete(id);
            return Ok(deleted);
        }
    }
}
