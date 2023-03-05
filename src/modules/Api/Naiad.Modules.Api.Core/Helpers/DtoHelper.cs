using System;
using System.Collections.Generic;
using Naiad.Libraries.System.Constants.System;
using Naiad.Libraries.System.Models.DataManagement;
using Naiad.Libraries.System.Models.System;
using Naiad.Libraries.System.Services;
using Naiad.Modules.Api.Core.Objects;

namespace Naiad.Modules.Api.Core.Helpers;

public static class DtoHelper
{
    public static StructuredDataDto ToStructuredDataDto(this StructuredDataDefinition sdd)
    {
        return new StructuredDataDto
        {
            MetadataId = sdd.MetadataId,
            Name = sdd.Name,
            Description = sdd.Description,
            MimeType = sdd.MimeType,
            IdentifierName = sdd.IdentifierName
        };
    }

    public static StructuredDataDetailDto ToStructuredDataDetailDto(this StructuredDataDefinition sdd)
    {
        return new StructuredDataDetailDto
        {
            MetadataId = sdd.MetadataId,
            Name = sdd.Name,
            Description = sdd.Description,
            MimeType = sdd.MimeType,
            IdentifierName = sdd.IdentifierName
        };
    }

    public static IEnumerable<StructuredDataDto> ToStructuredDataDtos(this IEnumerable<StructuredDataDefinition> sdds)
    {
        var output = new List<StructuredDataDto>();

        foreach (var sdd in sdds)
        {
            var sddto = new StructuredDataDto
            {
                MetadataId = sdd.MetadataId,
                Name = sdd.Name,
                Description = sdd.Description,
                MimeType = sdd.MimeType,
                IdentifierName = sdd.IdentifierName
            };

            output.Add(sddto);
        }

        return output;
    }

    public static StructuredDataDefinition ToStructuredDataDefinition(this StructuredDataDto sdd)
    {
        return new StructuredDataDefinition
        {
            MetadataId = sdd.MetadataId,
            Name = sdd.Name,
            Description = sdd.Description,
            MimeType = sdd.MimeType,
            IdentifierName = sdd.IdentifierName,
            CollectionName = MetadataService.GetNsdCollectionName(sdd.Name)
        };
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            GivenName = user.GivenName,
            FamilyName = user.FamilyName,
            UserRole = Enum.GetName(typeof(UserTypes), user.UserType),
            Username = user.Username,
            IsEnabled = user.IsEnabled,
            MustChangePassword = user.MustChangePassword
        };
    }

    public static IEnumerable<UserDto> ToUserDtos(this IEnumerable<User> users)
    {
        var output = new List<UserDto>();

        foreach (var user in users)
        {
            output.Add(user.ToUserDto());
        }

        return output;
    }

    public static User ToUser(this UserDto user)
    {
        return new User
        {
            Id = user.Id,
            Email = user.Email,
            FamilyName = user.FamilyName,
            GivenName = user.GivenName,
            IsEnabled = user.IsEnabled,
            MustChangePassword = user.MustChangePassword,
            UserType = Enum.Parse<UserTypes>(user.UserRole),
            Username = user.Username
        };
    }
}
