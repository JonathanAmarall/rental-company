using Xunit;

namespace RentalCompany.Domain.Tests.Entities.Location
{
    [CollectionDefinition(nameof(LocationCollection))]
    public class LocationCollection : ICollectionFixture<LocationTestsFixture> { }
}
