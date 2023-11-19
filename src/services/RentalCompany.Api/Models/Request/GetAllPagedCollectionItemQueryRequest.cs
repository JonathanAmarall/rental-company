using RentalCompany.Domain.Entities;

namespace RentalCompany.Api.Models.Request
{
    public class GetAllPagedCollectionItemQueryRequest : PagedListQueryBase
    {
        public ECollectionStatus? Status { get; set; }
        public EType? Type { get; set; }
    }
}
