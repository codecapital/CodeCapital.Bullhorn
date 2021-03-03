namespace CodeCapital.Bullhorn.Dtos
{
    public class DepartmentsDto
    {
        public int total { get; set; }
        public Data[] data { get; set; }

        public class Data
        {
            public int id { get; set; }
        }
    }
}