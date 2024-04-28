namespace EasyLibby.Api.Entities
{
    public abstract class BaseSearchableEntity : BaseEntity
    {
        public abstract string GetEntitySearchableName();
    }
}
