using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.ActivityPub.Interfaces;
using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.System.LiteDb.Repos;

namespace Naiad.Libraries.ActivityPub.LiteDb.Repos;

public class ActivityRepo : IActivityRepo
{
    private readonly BaseRepo<Post> _repo;

    public ActivityRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<Post>(
            database,
            "ap_activities");

        // _repo.EnsureIndex(x => x.Subject, true);
    }

    public Activity GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Save(Activity obj)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Activity> GetAll()
    {
        throw new NotImplementedException();
    }
}
