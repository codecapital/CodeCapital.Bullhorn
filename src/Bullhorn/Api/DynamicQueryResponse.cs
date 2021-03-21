using System.Collections.Generic;

namespace CodeCapital.Bullhorn.Api
{
    public class DynamicQueryResponse : DynamicResponse
    {
        public int Start { get; set; }
        public int Count { get; set; }

        //ToDo If not used remove
        //Check this https://github.com/dotnet/runtime/issues/29690
        //public List<dynamic> Data { get; set; }
        public List<dynamic> DynamicData { get; set; }
    }
}
