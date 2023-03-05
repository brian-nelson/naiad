using System;
using System.Collections.Generic;
using LiteDB;
using Naiad.Libraries.ActivityPub.Interfaces;
using Naiad.Libraries.ActivityPub.Models.General;
using Naiad.Libraries.System.LiteDb.Repos;

namespace Naiad.Libraries.ActivityPub.LiteDb.Repos
{
    public class WebfingerRepo : IWebfingerRepo
    {
        private readonly BaseRepo<Webfinger> _repo;

        public WebfingerRepo(ILiteDatabase database)
        {
            _repo = new BaseRepo<Webfinger>(
                database, 
                "webfingers");

            _repo.EnsureIndex(x => x.Subject, true);
        }


        public Webfinger GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        public Webfinger GetBySubject(string subject)
        {
            return _repo.GetItem(Query.EQ("Subject", subject));
        }

        public void Save(Webfinger obj)
        {
            _repo.Save(obj);
        }

        public IEnumerable<Webfinger> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
