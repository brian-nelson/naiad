using System;
using Naiad.Libraries.Core.Helpers;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Helpers
{
    public static class AccessKeyHelper
    {
        public static AccessKeyResult GenerateAccessKey(
            this SystemService systemService,
            Guid userId)
        {
            string key;
            AccessKey existing = null;

            do
            {
                key = PasswordHelper.GeneratePassword(20);
                existing = systemService.GetAccessKey(key);
            } while (existing != null);

            var secretKey = PasswordHelper.GeneratePassword(40);

            var accessKey = new AccessKeyResult
            {
                Key = key,
                SecretKey = secretKey
            };

            systemService.CreateAccessKey(userId, key, secretKey);

            return accessKey;
        }
    }
}
