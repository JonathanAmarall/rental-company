﻿using RentalCompany.Core.Contracts;
using RentalCompany.Core.Models;

namespace RentalCompany.Domain.Entities
{
    public class Location : AggregateRoot, IAuditableEntity
    {
        private List<Location> _childrens = new();
        private List<CollectionItem>? _colletionItems = new();

        public Location(string initials, string description, Guid? parentId, int level)
        {
            Initials = initials.ToUpper();
            Description = description;
            ParentId = parentId;
            Level = level;
        }

        public string Initials { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? UpdatedAt { get; }

        // EF Rel.
        public ICollection<Location>? Childrens
        {
            get => _childrens;
        }

        public ICollection<CollectionItem>? CollectionItems
        {
            get => _colletionItems;
        }

        public Guid? ParentId { get; private set; }
        public Location? Parent { get; private set; }

        public bool HasChildren()
        {
            return _childrens.Count > 0;
        }

        public bool HasParent()
        {
            return ParentId != Guid.Empty;
        }

        public void AddChildrens(ICollection<Location> itemLocations)
        {
            _childrens.AddRange(itemLocations);
        }

        public void LinkACollectionItem(CollectionItem item)
        {
            _colletionItems!.Add(item);
        }

        public bool HasCollectionItem()
        {
            return _colletionItems?.Count > 0;
        }
    }
}