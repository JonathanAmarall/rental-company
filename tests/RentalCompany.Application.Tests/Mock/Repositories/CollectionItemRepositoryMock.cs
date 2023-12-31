﻿using AutoFixture;
using RentalCompany.Core.Data;
using RentalCompany.Core.DTOs;
using RentalCompany.Domain.Entities;
using RentalCompany.Domain.Repositories;

namespace RentalCompany.Application.Tests.Mock.Repositories;

public class CollectionItemRepositoryMock : ICollectionItemRepository
{
    public void Dispose()
    {
    }

    public IUnitOfWork UnitOfWork { get; }
    public Task CreateAsync(Domain.Entities.CollectionItem item)
    {
        throw new NotImplementedException();
    }

    public void Delete(Domain.Entities.CollectionItem item)
    {
        throw new NotImplementedException();
    }

    public void Update(Domain.Entities.CollectionItem item)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entities.CollectionItem?> GetByIdAsync(Guid collectionItemId)
    {
        var item = new Domain.Entities.CollectionItem("Livro ABC", "Jonhn Doe", 10, string.Empty, EType.BOOK);
        return Task.FromResult(item);
    }

    public Task<Domain.Entities.Borrower?> GetContactByIdAsync(Guid contactId)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<RentalCompany.Domain.Entities.CollectionItem>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status,
        EType? type, int pageNumber = 1, int pageSize = 5)
    {
        var fixture = new Fixture();
        var itens = fixture.Create<PagedList<RentalCompany.Domain.Entities.CollectionItem>>();
        return Task.FromResult(itens);
    }

    public Task<PagedList<Domain.Entities.Borrower>> GetAllContactsPagedAsync(string? globalFilter, int pageNumber = 1, int pageSize = 5)
    {
        throw new NotImplementedException();
    }
}