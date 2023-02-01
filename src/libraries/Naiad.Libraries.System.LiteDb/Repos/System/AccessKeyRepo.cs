using LiteDB;
using Naiad.Libraries.System.Interfaces.System;
using Naiad.Libraries.System.Models.System;

namespace Naiad.Libraries.System.LiteDb.Repos.System
{
    public class AccessKeyRepo : IAccessKeyRepo
    {
        private readonly InternalRepo<AccessKey> _repo;

        public AccessKeyRepo(ILiteDatabase database)
        {
            _repo = new InternalRepo<AccessKey>(database, "useraccesses");

            _repo.EnsureIndex(x => x.UserId, false);
        }

        public AccessKey GetById(Guid accessKeyId)
        {
            return _repo.GetById(accessKeyId);
        }

        public IEnumerable<AccessKey> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Save(AccessKey accessKey)
        {
            _repo.Save(accessKey);
        }
    }
}
