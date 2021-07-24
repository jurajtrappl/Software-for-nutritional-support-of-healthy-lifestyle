using MongoDB.Bson.Serialization.Conventions;

namespace Application.Infrastructure.Config
{
    /// <summary>
    /// Provides methods that configures Mongo default conventions.
    /// </summary>
    public static class ConfigureConventions
    {
        /// <summary>
        /// Creating and adding default convention packs for element naming, data type representation and handling null values.
        /// </summary>
        public static void AddConventionsPacks()
        {
            // property names using camel case
            ConventionPack camelCasePack = new();
            camelCasePack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register(nameof(CamelCaseElementNameConvention), camelCasePack, t => true);

            // enum values serialized as strings
            ConventionPack enumAsStringPack = new();
            enumAsStringPack.Add(new EnumRepresentationConvention(MongoDB.Bson.BsonType.String));
            ConventionRegistry.Register(nameof(EnumRepresentationConvention), enumAsStringPack, t => true);

            // if property is null, it will not be serialized
            ConventionPack ignoreIfNullPack = new();
            ignoreIfNullPack.Add(new IgnoreIfNullConvention(true));
            ConventionRegistry.Register(nameof(IgnoreIfNullConvention), ignoreIfNullPack, t => true);
        }
    }
}