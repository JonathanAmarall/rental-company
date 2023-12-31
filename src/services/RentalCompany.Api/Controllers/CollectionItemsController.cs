﻿using Microsoft.AspNetCore.Mvc;
using RentalCompany.Api.Models.Request;
using RentalCompany.Core.DTOs;
using RentalCompany.Core.Messages.Commands;
using RentalCompany.Domain;
using RentalCompany.Domain.Commands;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Handler;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Api.Controllers
{
    [Route("api/v1/collection-items")]
    [ApiController]
    public class CollectionItemsController : MainController
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<CollectionItem>>> Get(
            [FromServices] ICollectionItemRepository collectionItemRepository,
            [FromQuery] GetAllPagedCollectionItemQueryRequest query)
        {
            var items = await collectionItemRepository.GetAllPagedAsync(
                query.GlobalFilter,
                query.SortOrder,
                query.SortField,
                query.Status,
                query.Type,
                query.PageNumber,
                query.PageSize);

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCollectionItemCommand command,
            [FromServices] CreateCollectionItemCommandHandler handler)
        {
            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);
            if (result.IsFailure)
            {
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
                AddProcessingError(result.Message);
            }

            return CustomReponse(new { result.Message });
        }

        [HttpPost("{id:guid}/lend")]
        public async Task<ActionResult> Post(Guid id, [FromBody] RentItemCommand command,
            [FromServices] LendCollectionItemCommandHandler handler)
        {
            if (id != command.CollectionItemId)
                return BadRequest();

            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);
            if (result.IsFailure)
            {
                AddProcessingError(result.Message);
                AddProcessingErrors(result.ValidationResult!);
            }

            return CustomReponse(new { result.Message });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(Guid id, [FromServices] ICollectionItemRepository repository) => 
             Ok(await repository.GetByIdAsync(id));

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> AddLocation(Guid id, [FromBody] AddLocationInCollectionItemCommand command,
            [FromServices] AddLocationInCollectionCommandHandler handler, [FromServices] ICollectionItemRepository repository)
        {
            if (id != command.CollectionItemId)
            {
                return NotFound();
            }

            var item = await repository.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound();
            }

            var result = (CommandResult<CollectionItem>)await handler.HandleAsync(command);
            if (result.IsFailure)
            {
                result.ValidationResult?.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
                AddProcessingError(result.Message);
            }

            return CustomReponse(new { result.Message });
        }

        [HttpGet("{id:guid}/location")]
        public async Task<ActionResult<string>> GetFullLocation(Guid id,
            [FromServices] ILocationRepository locationRepository)
        {
            return Ok(new { Location = await locationRepository.GetFullLocationTag(id) });
        }
    }
}