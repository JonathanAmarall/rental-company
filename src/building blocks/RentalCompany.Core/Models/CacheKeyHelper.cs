namespace RentalCompany.Core.Models
{
    public static class CacheKeyHelper
    {
        public static readonly string CollectionItemKey = $"{Guid.NewGuid()}-collection-item";
    }
}
