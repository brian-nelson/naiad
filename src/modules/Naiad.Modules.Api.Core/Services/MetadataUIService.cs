using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Constants.MetadataManagement;
using Naiad.Libraries.System.Models.MetadataManagement;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Services;

public class MetadataUIService
{
    private readonly DataService _dataService;
    private readonly MetadataService _metadataService;
    private readonly SystemService _systemService;

    public MetadataUIService(
        DataService dataService,
        MetadataService metadataService,
        SystemService systemService)
    {
        _dataService = dataService;
        _metadataService = metadataService;
        _systemService = systemService;
    }

    public MetadataUI GetMetadata(Guid metadataId)
    {
        var metadata = _metadataService.GetMetadata(metadataId);
        if (metadata != null)
        {
            var metadataProperties = _metadataService.GetMetadataProperties(metadataId);

            var output = new MetadataUI
            {
                Id = metadata.Id,
                CategorizationId = metadata.CategorizationId
            };

            foreach (var metadataProperty in metadataProperties)
            {
                output.Properties.Add(metadataProperty.Key, metadataProperty.Value);
            }

            return output;
        }

        return null;
    }

    public IEnumerable<DataPointer> GetRelatedData(string fileId)
    {
        var dataPointer = _metadataService.GetDataPointer(fileId);
        var output = new List<DataPointer>();

        if (dataPointer == null)
        {
            return output;
        }

        var parentRelationships = _metadataService.GetParentRelationships(dataPointer.Id);
        foreach (var relationship in parentRelationships)
        {
            if (relationship.ParentType == EntityType.DataPointer)
            {
                var parent = _metadataService.GetDataPointer(relationship.ParentId);
                output.Add(parent);
            }
        }

        var childrenRelationships = _metadataService.GetChildRelationships(dataPointer.Id);
        foreach (var relationship in childrenRelationships)
        {
            if (relationship.ChildType == EntityType.DataPointer)
            {
                var child = _metadataService.GetDataPointer(relationship.ChildId);
                output.Add(child);
            }
        }

        return output;
    }

    public IEnumerable<MetadataUI> GetRelatedMetadata(string fileId)
    {
        var dataPointer = _metadataService.GetDataPointer(fileId);
        var output = new List<MetadataUI>();

        if (dataPointer == null)
        {
            return output;
        }

        var parentRelationships = _metadataService.GetParentRelationships(dataPointer.Id);
        foreach (var relationship in parentRelationships)
        {
            if (relationship.ParentType == EntityType.Metadata)
            {
                var metadataUI = GetMetadata(relationship.ParentId);
                metadataUI.ConnectionContext = relationship.ConnectionContext;

                output.Add(metadataUI);
            }
        }

        var childrenRelationships = _metadataService.GetChildRelationships(dataPointer.Id);
        foreach (var relationship in childrenRelationships)
        {
            if (relationship.ChildType == EntityType.Metadata)
            {
                var metadataUI = GetMetadata(relationship.ChildId);
                metadataUI.ConnectionContext = relationship.ConnectionContext;

                output.Add(metadataUI);
            }
        }

        return output;
    }

    public IEnumerable<MetadataUI> GetRelatedMetadata(Guid metadataId)
    {
        var output = new List<MetadataUI>();

        var parentRelationships = _metadataService.GetParentRelationships(metadataId);
        foreach (var relationship in parentRelationships)
        {
            if (relationship.ParentType == EntityType.Metadata)
            {
                var metadataUI = GetMetadata(relationship.ParentId);
                metadataUI.ConnectionContext = relationship.ConnectionContext;

                output.Add(metadataUI);
            }
        }

        var childrenRelationships = _metadataService.GetChildRelationships(metadataId);
        foreach (var relationship in childrenRelationships)
        {
            if (relationship.ChildType == EntityType.Metadata)
            {
                var metadataUI = GetMetadata(relationship.ChildId);
                metadataUI.ConnectionContext = relationship.ConnectionContext;

                output.Add(metadataUI);
            }
        }

        return output;
    }
}
