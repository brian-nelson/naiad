using System;
using System.Collections.Generic;
using Naiad.Libraries.ActivityPub.Interfaces;
using Naiad.Libraries.ActivityPub.Models.Pub;

namespace Naiad.Libraries.ActivityPub.LiteDb.Repos
{
    public class InboxRepo : IInboxRepo
    {
        public Post GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Post obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
