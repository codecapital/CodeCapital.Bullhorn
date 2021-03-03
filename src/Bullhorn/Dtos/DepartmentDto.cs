namespace CodeCapital.Bullhorn.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool? Enabled { get; set; }
        public string Name { get; set; } = null!;
    }
}
