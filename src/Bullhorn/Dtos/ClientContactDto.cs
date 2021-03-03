namespace CodeCapital.Bullhorn.Dtos
{
    public class ClientContactDto : EntityBaseDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public UserDto Owner { get; set; }
        public ClientCorporationDto ClientCorporation { get; set; } = new ClientCorporationDto();
        public bool IsDeleted { get; set; }

        public ClientContactDto()
        {
            Owner = new UserDto();
        }
    }
}
