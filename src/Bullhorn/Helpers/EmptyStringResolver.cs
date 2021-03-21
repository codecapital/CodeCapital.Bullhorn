namespace CodeCapital.Bullhorn.Helpers
{
    //ToDo Do we need this?
    //public class EmptyStringResolver : DefaultContractResolver
    //{
    //    public EmptyStringResolver()
    //    {
    //        NamingStrategy = new CamelCaseNamingStrategy();
    //    }

    //    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    //    {
    //        var property = base.CreateProperty(member, memberSerialization);

    //        if (property.PropertyType != typeof(string)) return property;

    //        property.ShouldSerialize = instance =>
    //        {
    //            var value = (string)property.ValueProvider.GetValue(instance);

    //            return !string.IsNullOrWhiteSpace(value);
    //        };

    //        return property;
    //    }
    //}
}
