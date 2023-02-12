using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Constants.MetadataManagement;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;

namespace Naiad.Libraries.System.Services;

public class MetadataService
{
    private readonly ICategorizationRepo _categorizationRepo;
    private readonly IDataPointerRepo _dataPointerRepo;
    private readonly IGranularityRepo _granularityRepo;
    private readonly IMetadataPropertyRepo _metadataPropertyRepo;
    private readonly IMetadataRepo _metadataRepo;
    private readonly IRelationshipRepo _relationshipRepo;
    private readonly IZoneRepo _zoneRepo;

    public MetadataService(
        IRepositoryProvider repositoryProvider)
    {
        _categorizationRepo = repositoryProvider.GetCategorizationRepo();
        _dataPointerRepo = repositoryProvider.GetDataPointerRepo();
        _granularityRepo = repositoryProvider.GetGranularityRepo();
        _metadataPropertyRepo = repositoryProvider.GetMetadataPropertyRepo();
        _metadataRepo = repositoryProvider.GetMetadataRepo();
        _relationshipRepo = repositoryProvider.GetRelationshipRepo();
        _zoneRepo = repositoryProvider.GetZonRepo();
    }

    /* Categorization */
    public Categorization GetCategorization(Guid id)
    {
        return _categorizationRepo.GetById(id);
    }

    public void Save(Categorization categorization)
    {
        _categorizationRepo.Save(categorization);
    }

    public IEnumerable<Categorization> GetCategorizations()
    {
        return _categorizationRepo.GetAll();
    }

    /* Data Pointer */
    public DataPointer GetDataPointer(Guid id)
    {
        return _dataPointerRepo.GetById(id);
    }

    public DataPointer GetDataPointer(string fileId)
    {
        return _dataPointerRepo.GetByLocation(fileId);
    }

    public void Save(DataPointer dataPointer)
    {
        _dataPointerRepo.Save(dataPointer);
    }

    /* Granularity */
    public Granularity GetGranularity(Guid id)
    {
        return _granularityRepo.GetById(id);
    }

    public IEnumerable<Granularity> GetGranularities()
    {
        return _granularityRepo.GetAll();
    }

    public void Save(Granularity granularity)
    {
        _granularityRepo.Save(granularity);
    }

    /* Metadata Property */
    public MetadataProperty GetMetadataProperty(Guid metadataPropertyId)
    {
        return _metadataPropertyRepo.GetById(metadataPropertyId);
    }

    public IEnumerable<MetadataProperty> GetMetadataProperties(Guid metadataId)
    {
        return _metadataPropertyRepo.Get(metadataId);
    }

    public void Save(MetadataProperty metadataProperty)
    {
        _metadataPropertyRepo.Save(metadataProperty);
    }

    /* Metadata */
    public Metadata GetMetadata(Guid metadataId)
    {
        return _metadataRepo.GetById(metadataId);
    }

    public void Save(Metadata metadata)
    {
        _metadataRepo.Save(metadata);
    }

    /* Relationship */
    public Relationship GetRelationship(Guid relationshipId)
    {
        return _relationshipRepo.GetById(relationshipId);
    }

    public IEnumerable<Relationship> GetChildRelationships(Guid parentId)
    {
        return _relationshipRepo.GetChildren(parentId);
    }

    public IEnumerable<Relationship> GetParentRelationships(Guid childId)
    {
        return _relationshipRepo.GetParents(childId);
    }

    public void Save(Relationship relationship)
    {
        _relationshipRepo.Save(relationship);
    }

    public IEnumerable<Metadata> GetChildrenMetadata(Guid parentId)
    {
        var output = new List<Metadata>();

        var relationships = GetChildRelationships(parentId);

        foreach (var relationship in relationships)
        {
            if (relationship.ChildType == EntityType.Metadata)
            {
                var metadata = GetMetadata(relationship.ChildId);
                output.Add(metadata);
            }
        }

        return output;
    }

    public IEnumerable<Metadata> GetParentMetadata(Guid childId)
    {
        var output = new List<Metadata>();

        var relationships = GetParentRelationships(childId);

        foreach (var relationship in relationships)
        {
            if (relationship.ParentType == EntityType.Metadata)
            {
                var metadata = GetMetadata(relationship.ParentId);
                output.Add(metadata);
            }
        }

        return output;
    }

    public IEnumerable<DataPointer> GetChildrenDataPointers(Guid parentId)
    {
        var output = new List<DataPointer>();

        var relationships = GetChildRelationships(parentId);

        foreach (var relationship in relationships)
        {
            if (relationship.ChildType == EntityType.DataPointer)
            {
                var dataPointer = GetDataPointer(relationship.ChildId);
                output.Add(dataPointer);
            }
        }

        return output;
    }

    public IEnumerable<DataPointer> GetParentDataPointers(Guid childId)
    {
        var output = new List<DataPointer>();

        var relationships = GetParentRelationships(childId);

        foreach (var relationship in relationships)
        {
            if (relationship.ParentType == EntityType.DataPointer)
            {
                var dataPointer = GetDataPointer(relationship.ParentId);
                output.Add(dataPointer);
            }
        }

        return output;
    }

    /* Zones */
    public Zone GetZone(Guid zoneId)
    {
        return _zoneRepo.GetById(zoneId);
    }

    public IEnumerable<Zone> GetZones()
    {
        return _zoneRepo.GetAll();
    }

    public void Save(Zone zone)
    {
        _zoneRepo.Save(zone);
    }

}

