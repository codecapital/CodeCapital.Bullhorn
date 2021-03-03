namespace CodeCapital.Bullhorn.Dtos
{
    public class PlacementCommissionDto : EntityBaseDto
    {
        public double CommissionPercentage { get; set; }
        public PlacementDto Placement { get; set; }
        public UserDto User { get; set; }
        public string Status { get; set; } = "";
        public string Role { get; set; } = "";

        public PlacementCommissionDto()
        {
            Placement = new PlacementDto();
            User = new UserDto();
        }
    }
}
