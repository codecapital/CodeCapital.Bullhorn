using Newtonsoft.Json;

namespace CodeCapital.Bullhorn.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }

        [JsonProperty("_subtype")]
        public string? Subtype { get; set; }

        public DepartmentsDto Departments { get; set; } = new DepartmentsDto();

        public UserDto()
        {

        }

        public UserDto(int id) => Id = id;
    }
}
