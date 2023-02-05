using Naiad.Libraries.System.Interfaces.DataManagement;
using Naiad.Libraries.System.Interfaces.MetadataManagement;
using Naiad.Libraries.System.Interfaces.System;

namespace Naiad.Libraries.System.Interfaces;

public interface IRepositoryProvider
{
    // Data Management
    public IStorageProvider GetStorageProvider();

    // Metadata Management
    public ICategorizationRepo GetCategorizationRepo();

    public IDataPointerRepo GetDataPointerRepo();

    public IGranularityRepo GetGranularityRepo();

    public IMetadataPropertyRepo GetMetadataPropertyRepo();

    public IMetadataRepo GetMetadataRepo();

    public IRelationshipRepo GetRelationshipRepo();

    public IZoneRepo GetZonRepo();

    // System
    public IAccessKeyRepo GetAccessKeyRepo();

    public ILogEntryRepo GetLogEntryRepo();

    public IUserAccessRepo GetUserAccessRepo();

    public IUserRepo GetUserRepo();

}