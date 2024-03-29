﻿using System;
using System.Collections.Generic;
using System.Linq;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.System.Constants.DataManagement;
using Naiad.Libraries.System.Constants.MetadataManagement;
using Naiad.Libraries.System.Interfaces;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Models.DataManagement;
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

    public Categorization GetCategorizationByName(string name)
    {
        return _categorizationRepo.GetByName(name);
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

    public IEnumerable<MetadataProperty> GetMetadataPropertiesByKey(string key)
    {
        return _metadataPropertyRepo.GetByKey(key);
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

    public IEnumerable<Metadata> GetMetadataByCategorization(Guid categorizationId)
    {
        return _metadataRepo.Get(categorizationId);
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

    public Relationship GetRelationship(Guid parentId, Guid childId, string connectionContext)
    {
        return _relationshipRepo.GetRelationship(parentId, childId, connectionContext);
    }

    public Relationship GetStructuredDataRelationship(Guid dataPointerId, Guid metadataId)
    {
        return GetRelationship(dataPointerId, metadataId, StructuredDataConstants.NAIAD_STRUCTURED_DATA);
    }


    // Metadata

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

    public void DefineStructuredData(StructuredDataDefinition sdd)
    {
        if (!sdd.Name.IsOnlyAlphaNumeric())
        {
            throw new ArgumentException("Structured data definition names can only be alpha numeric.");
        }

        var categorization = EnsureCategorization(StructuredDataConstants.NAIAD_STRUCTURED_DATA);

        var existingSdd = FindStructuredDataDefinition(categorization.Id, sdd.Name);

        if (existingSdd != null)
        {
            SetMetadataPropertyValue(
                sdd.MetadataId, 
                StructuredDataConstants.NSD_DESCRIPTION, 
                sdd.Description);
            return;
        }

        // Build Metadata record
        var metadata = new Metadata
        {
            CategorizationId = categorization.Id
        };
        _metadataRepo.Save(metadata);

        // Create Metadata properties needed
        SetMetadataPropertyValue(
            metadata.Id, 
            StructuredDataConstants.NSD_NAME, 
            sdd.Name);

        SetMetadataPropertyValue(
            metadata.Id,
            StructuredDataConstants.NSD_DESCRIPTION,
            sdd.Description);

        SetMetadataPropertyValue(
            metadata.Id, 
            StructuredDataConstants.NSD_MIME_TYPE, 
            sdd.MimeType);

        SetMetadataPropertyValue(
            metadata.Id, 
            StructuredDataConstants.NSD_IDENTIFIER_NAME, 
            sdd.IdentifierName);

        SetMetadataPropertyValue(
            metadata.Id, 
            StructuredDataConstants.NSD_COLLECTION_NAME,
            GetNsdCollectionName(sdd.Name));
    }

    public static string GetNsdCollectionName(string name)
    {
        return $"nsd_{name.ToLower()}";
    }

    private Categorization EnsureCategorization(string name)
    {
        var categorization = _categorizationRepo.GetByName(name);

        if (categorization == null)
        {
            categorization = new Categorization
            {
                Name = name
            };

            _categorizationRepo.Save(categorization);
        }

        return categorization;
    }

    private StructuredDataDefinition FindStructuredDataDefinition(
        Guid categorizationId,
        string name)
    {
        var metadatas = _metadataRepo.Get(categorizationId);

        foreach (var metadata in metadatas)
        {
            var metadataPropertyValue = GetMetadataPropertyValue(metadata.Id, StructuredDataConstants.NSD_NAME);

            if (metadataPropertyValue != null
                && metadataPropertyValue.Equals(name))
            {
                var sdd = GetStructuredDataDefinition(metadata.Id);
                return sdd;
            }
        }

        return null;
    }

    private IEnumerable<StructuredDataDefinition> FindStructuredDataDefinitions(Guid categorizationId)
    {
        var output = new List<StructuredDataDefinition>();

        var metadatas = _metadataRepo.Get(categorizationId);

        foreach (var metadata in metadatas)
        {
            var sdd = GetStructuredDataDefinition(metadata.Id);

            if (sdd != null)
            {
                output.Add(sdd);
            }
        }

        return output;
    }

    public StructuredDataDefinition GetStructuredDataDefinition(Guid metadataId)
    {
        var metadataPropertyValue = GetMetadataPropertyValue(metadataId, StructuredDataConstants.NSD_NAME);

        if (metadataPropertyValue != null)
        {
            var sdd = new StructuredDataDefinition
            {
                MetadataId = metadataId,
                Name = metadataPropertyValue,
                Description = GetMetadataPropertyValue(metadataId, StructuredDataConstants.NSD_DESCRIPTION),
                MimeType = GetMetadataPropertyValue(metadataId, StructuredDataConstants.NSD_MIME_TYPE),
                IdentifierName = GetMetadataPropertyValue(metadataId, StructuredDataConstants.NSD_IDENTIFIER_NAME),
                CollectionName = GetMetadataPropertyValue(metadataId, StructuredDataConstants.NSD_COLLECTION_NAME)
            };

            return sdd;
        }

        return null;
    }

    public string GetMetadataPropertyValue(Guid metadataId, string key)
    {
        var metadataProperty = _metadataPropertyRepo.GetByIdAndKey(metadataId, key);

        return metadataProperty?.Value;
    }

    public void SetMetadataPropertyValue(Guid metadataId, string key, string value)
    {
        var metadataProperty = _metadataPropertyRepo.GetByIdAndKey(metadataId, key);

        if (metadataProperty == null)
        {
            metadataProperty = new MetadataProperty
            {
                MetadataId = metadataId,
                Key = key
            };
        }

        metadataProperty.Value = value;
        _metadataPropertyRepo.Save(metadataProperty);
    }

    public StructuredDataDefinition GetStructuredDataDefinition(string name)
    {
        var categorization = EnsureCategorization(StructuredDataConstants.NAIAD_STRUCTURED_DATA);

        return FindStructuredDataDefinition(categorization.Id, name);
    }

    public IEnumerable<StructuredDataDefinition> GetStructuredDataDefinitions()
    {
        var categorization = EnsureCategorization(StructuredDataConstants.NAIAD_STRUCTURED_DATA);

        return FindStructuredDataDefinitions(categorization.Id);
    }

    public IEnumerable<StructuredDataDefinition> GetStructuredDataDefinitionsTaggedToData(Guid dataPointerId)
    {
        var metadataIds = new List<Guid>();

        var childRelationships = _relationshipRepo.GetChildren(dataPointerId);
        foreach (var childRelationship in childRelationships)
        {
            if (childRelationship.ConnectionContext.Equals(StructuredDataConstants.NAIAD_STRUCTURED_DATA)
                && childRelationship.ChildType == EntityType.Metadata)
            {
                metadataIds.Add(childRelationship.ChildId);
            }
        }

        var parentRelationships = _relationshipRepo.GetParents(dataPointerId);
        foreach (var parentRelationship in parentRelationships)
        {
            if (parentRelationship.ConnectionContext.Equals(StructuredDataConstants.NAIAD_STRUCTURED_DATA)
                && parentRelationship.ParentType == EntityType.Metadata)
            {
                metadataIds.Add(parentRelationship.ParentId);
            }
        }

        var distinctIds = metadataIds.Distinct();

        var output = new List<StructuredDataDefinition>();

        foreach (var metadataId in distinctIds)
        {
            var sdd = GetStructuredDataDefinition(metadataId);

            if (sdd != null)
            {
                output.Add(sdd);
            }
        }

        return output;
    }
}

