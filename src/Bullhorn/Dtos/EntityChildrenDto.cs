namespace CodeCapital.Bullhorn.Dtos
{
    public class EntityChildrenDto
    {
        public int Total { get; set; }
        public DataId[] Data { get; set; }

        public class DataId
        {
            public int Id { get; set; }
            public long DateAdded { get; set; }
        }
    }
}