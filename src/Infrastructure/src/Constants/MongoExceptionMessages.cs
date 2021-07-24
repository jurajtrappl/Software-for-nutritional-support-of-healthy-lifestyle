namespace Application.Infrastructure.Constants
{
    /// <summary>
    /// Messages for exceptions that can occur with MongoDB.
    /// </summary>
    internal static class MongoExceptionMessages
    {
        /// <summary>
        /// The given collection was not found.
        /// </summary>
        internal const string CollectionNotFound = "{0} collection was not found.";

        /// <summary>
        /// The given database was not found.
        /// </summary>
        internal const string DatabaseNotFound = "{0} database was not found.";
    }
}