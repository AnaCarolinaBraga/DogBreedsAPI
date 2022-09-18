using System.Text.Json.Serialization;

namespace DogBreedsAPI.Models
{
    public class DogBreeds
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("raca_animal")]
        public string DogType { get; set; }

        [JsonPropertyName("origem")]
        public string Origin { get; set; }

        [JsonPropertyName("peso")]
        public string Weight { get; set; }

        [JsonPropertyName("altura")]
        public string Height { get; set; }

        [JsonPropertyName("expectativa_de_vida")]
        public string LifeExpectancy { get; set; }

        [JsonPropertyName("caracteristicas")]
        public string Characteristic { get; set; }

        public DogBreeds()
        {

        }

        public DogBreeds(int id, string type, string origin, string weight, string height, string life, string characteristic)
        {
            Id = id;
            DogType = type;
            Origin = origin;
            Weight = weight;
            Height = height;
            LifeExpectancy = life;
            Characteristic = characteristic;
        }
    }

}
