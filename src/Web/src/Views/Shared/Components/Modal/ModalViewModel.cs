namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for the modal component.
    /// </summary>
    public sealed class ModalViewModel
    {
        /// <summary>
        /// Gets or initializes modal id html attribute.
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// Gets or initializes label id html attribute in the modal div.
        /// </summary>
        public string LabelId { get; init; }

        /// <summary>
        /// Gets or initializes modal body id html attribtue in the modal div.
        /// </summary>
        public string BodyId { get; init; }

        /// <summary>
        /// Gets or initializes modal body css class.
        /// </summary>
        public string BodyClass { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalViewModel"/>
        /// with the given id, labelid, bodyid and body class.
        /// </summary>
        /// <param name="id">modal id html attribute</param>
        /// <param name="labelId">label id html attribute in the modal div.</param>
        /// <param name="bodyId">modal body id html attribtue in the modal div-</param>
        /// <param name="bodyClass">modal body css class.</param>
        public ModalViewModel(string id, string labelId, string bodyId, string bodyClass)
        {
            (Id, LabelId, BodyId, BodyClass) = (id, labelId, bodyId, bodyClass);
        }
    }
}