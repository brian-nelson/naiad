using LiteDB;
using System;
using System.Collections.Generic;
using Naiad.Libraries.ActivityPub.Interfaces;
using Naiad.Libraries.ActivityPub.Models.Pub;
using Naiad.Libraries.System.LiteDb.Repos;

namespace Naiad.Libraries.ActivityPub.LiteDb.Repos;

public class PostRepo : IPostRepo
{
    private readonly BaseRepo<Post> _repo;

    public PostRepo(ILiteDatabase database)
    {
        _repo = new BaseRepo<Post>(
            database,
            "ap_posts");

        // _repo.EnsureIndex(x => x.Subject, true);
    }

    public Post GetById(Guid id)
    {
        return _repo.GetById(id);
    }

    public void Save(Post obj)
    {
        _repo.Save(obj);
    }

    public IEnumerable<Post> GetAll()
    {
        return _repo.GetAll();
    }
}
