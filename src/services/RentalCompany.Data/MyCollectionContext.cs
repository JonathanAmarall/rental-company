using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RentalCompany.Core.Contracts;
using RentalCompany.Core.Data;
using RentalCompany.Core.Models;
using RentalCompany.Domain.Entities;
using System.Reflection;

namespace RentalCompany.Data
{
    public class RentalCompanyContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<CollectionItem>? CollectionItems { get; set; }
        public DbSet<Location>? Locations { get; set; }
        public DbSet<Borrower>? Borrowers { get; set; }


        public RentalCompanyContext(DbContextOptions<RentalCompanyContext> options, IMediator mediator) :
            base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalCompanyContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit(CancellationToken cancellationToken = default)
        {
            DateTime utcNow = DateTime.UtcNow;

            UpdateAuditableEntities(utcNow);

            await PublishDomainEvents(cancellationToken);

            return await SaveChangesAsync(cancellationToken) > 0;
        }

        private void UpdateAuditableEntities(DateTime utcNow)
        {
            foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = utcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(nameof(IAuditableEntity.UpdatedAt)).CurrentValue = utcNow;
                }
            }
        }

        private async Task PublishDomainEvents(CancellationToken cancellationToken)
        {
            List<EntityEntry<AggregateRoot>> aggregateRoots = ChangeTracker
                .Entries<AggregateRoot>()
                .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
                .ToList();

            List<IDomainEvent> domainEvents = aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();

            aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

            IEnumerable<Task> tasks = domainEvents.Select(domainEvent => _mediator.Publish(domainEvent, cancellationToken));

            await Task.WhenAll(tasks);
        }
    }
}