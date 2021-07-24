namespace Application.Core.Common.Enums
{
    /// <summary>
    /// Food allergens.
    /// </summary>
    public enum Allergen
    {
        /// <summary>
        /// Cereals containing gluten.
        /// </summary>
        Gluten = 1,

        /// <summary>
        /// Crabs, lobster, prawns and scampi.
        /// </summary>
        Crustaceans = 2,

        /// <summary>
        /// Refers to nuts growing underground. Biscuits, cakes, curries, desserts, sauces, oils.
        /// </summary>
        Peanuts = 5,

        /// <summary>
        /// Celery stalks, leaves, seed and the root called celeriac.
        /// </summary>
        Celery = 9,

        /// <summary>
        /// Liquid mustard, mustard powder and mustard seeds.
        /// </summary>
        Mustard = 10,

        /// <summary>
        /// Bread, bread sticks, humus, sesame oil and tahini.
        /// </summary>
        SesameSeeds = 11,

        /// <summary>
        /// Dried fruit such as raisins, dried apricots and prunes. Meat products, soft drinks, vegetables as well as
        /// vine and beer.
        /// </summary>
        Sulfites = 12,

        /// <summary>
        /// Mussels, land snails, oyster sauce, fish stews.
        /// </summary>
        Molluscs = 14
    }
}