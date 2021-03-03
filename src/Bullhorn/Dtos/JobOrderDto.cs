using System.Collections.Generic;

namespace CodeCapital.Bullhorn.Dtos
{
    public class JobOrderDto : EntityBaseDto
    {
        public string Title { get; set; } = "";
        public string Status { get; set; } = "";
        public string Source { get; set; } = "";
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public List<string> CustomText20 { get; set; } = new List<string>();

        public ClientContactDto ClientContact { get; set; }
        public ClientCorporationDto ClientCorporation { get; set; }
        public UserDto Owner { get; set; }

        public JobOrderDto()
        {
            ClientContact = new ClientContactDto();
            ClientCorporation = new ClientCorporationDto();
            Owner = new UserDto();
        }
    }
}
