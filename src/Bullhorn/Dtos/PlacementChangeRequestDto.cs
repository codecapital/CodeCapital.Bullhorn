namespace CodeCapital.Bullhorn.Dtos
{
    public class PlacementChangeRequestDto : EntityBaseDto
    {
        public IdDto Placement { get; set; }
        public string RequestStatus { get; set; } = "";
        public string RequestType { get; set; } = "";
        public string CustomText12 { get; set; } = "";

        public PlacementChangeRequestDto()
        {
            Placement = new IdDto();
        }
    }
}
