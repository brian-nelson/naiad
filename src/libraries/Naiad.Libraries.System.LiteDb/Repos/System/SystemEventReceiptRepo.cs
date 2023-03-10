using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System;

public class SystemEventReceiptRepo
    : ISystemEventReceiptRepo
{
    private readonly BaseRepo<SystemEventReceipt> _repo;

    public SystemEventReceiptRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<SystemEventReceipt>(database, "eventreceipts");
    }

    public SystemEventReceipt GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(SystemEventReceipt obj)
    {
        _repo.Save(obj);
    }

    public IEnumerable<SystemEventReceipt> GetAll()
    {
        return _repo.GetAll();
    }
}
