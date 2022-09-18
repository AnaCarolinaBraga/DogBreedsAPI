namespace DogBreedsAPI.DTO
{
    public class FilterDogBreedsDTO
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
        public string DogType { get; set; }

        public string Origin { get; set; }

        public string Characteristic { get; set; }
    }
}
