using System.Collections.Generic;

namespace Application.Infrastructure.Settings
{
    /// <summary>
    /// Describes section of application settings file that is used for mongo database configuration.
    /// </summary>
    public sealed class MongoDbSettings
    {
        /// <summary>
        /// Gets or sets database connection string.
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets main database name.
        /// </summary>
        public string EatExerciseEnjoyDatabaseName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets ingredients database name.
        /// </summary>
        public string IngredientsDatabaseName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets exercise database name.
        /// </summary>
        public string ExerciseDatabaseName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets exercise collection name.
        /// </summary>
        public string ExercisesCollectionName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of meal component collection names.
        /// </summary>
        public List<string> MealComponentCollectionsNames { get; set; } = new();
    }
}