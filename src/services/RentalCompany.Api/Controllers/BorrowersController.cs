using Microsoft.AspNetCore.Mvc;
using RentalCompany.Api.Models.Request;
using RentalCompany.Application.Borrower.Commands.CreateBorrower;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Core.Models;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Api.Controllers
{
    [Route("api/v1/borrowers")]
    [ApiController]
    public class BorrowersController : MainController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(
            [FromServices] IBorrowerRepository borrowerRepository,
            [FromQuery] GetAllBorrowersPagedQueryRequest queryRequest)
        {
            var query = await borrowerRepository.GetAllPagedAsync(
                queryRequest.GlobalFilter,
                queryRequest.SortOrder,
                queryRequest.SortField,
                queryRequest.PageNumber,
                queryRequest.PageSize);

            return Ok(query);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> Create(
            [FromBody] CreateBorrowerCommand command,
            [FromServices] CreateBorrowerCommandHandler handler)
        {
            var result = (CommandResult<Borrower>)await handler.HandleAsync(command);

            if (result.IsFailure)
            {
                AddProcessingErrors(command.ValidationResult!);
            }

            return CustomReponse();
        }
    }
}
