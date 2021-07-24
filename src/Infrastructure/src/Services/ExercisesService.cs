using Application.Core.Common.Entities;
using Application.Core.Interfaces;
using Application.Infrastructure.Constants;
using Application.Infrastructure.Settings;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Application.Infrastructure.Services
{
    /// <summary>
    /// Implementation of infrastructe exercise service.
    /// </summary>
    public sealed class ExercisesService : IExercisesService
    {
        /// <summary>
        /// Mongo collection name of the exercises.
        /// </summary>
        private readonly string _exercisesCollectionName;

        /// <summary>
        /// Exercises mongo database.
        /// </summary>
        private readonly IMongoDatabase _exercisesDatabase;

        /// <summary>
        /// Initializes a new instance of <see cref="ExercisesService" /> with the given mongo client and settings.
        /// </summary>
        /// <param name="client">client interface to mongoDb.</param>
        /// <param name="mongoSettings">database settings.</param>
        public ExercisesService(IMongoClient client, MongoDbSettings mongoSettings)
        {
            _exercisesDatabase = client.GetDatabase(mongoSettings.ExerciseDatabaseName);
            if (_exercisesDatabase is null)
            {
                throw new MongoException(string.Format(MongoExceptionMessages.DatabaseNotFound, _exercisesDatabase));
            }

            _exercisesCollectionName = mongoSettings.ExercisesCollectionName;
        }

        /// <summary>
        /// Returns readonly collection of exercises from database.
        /// </summary>
        public IReadOnlyList<IExercise> GetAllExercises()
        {
            IMongoCollection<IExercise>? exercises = _exercisesDatabase.GetCollection<IExercise>(_exercisesCollectionName);
            if (exercises is null)
            {
                throw new MongoException(string.Format(MongoExceptionMessages.CollectionNotFound, exercises));
            }

            return exercises.Find(_ => true)
                .ToList();
        }
    }
}