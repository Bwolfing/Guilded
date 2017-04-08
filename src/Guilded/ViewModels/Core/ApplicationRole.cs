using Guilded.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataModel = Guilded.Identity.ApplicationRole;
using AutoMapper;

namespace Guilded.ViewModels.Core
{
    public class ApplicationRole
    {
        #region Properties
        #region Public Properties
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty("name")]
        [MinLength(5, ErrorMessage = "A role name must contain at least {0} characers")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("concurrencyStamp")]
        public string ConcurrencyStamp { get; set; }

        [JsonProperty("permissions")]
        public IList<Permission> Permissions { get; set; }
        #endregion
        #endregion

        public ApplicationRole()
        {
            Id = null;
            Name = null;
            ConcurrencyStamp = Guid.NewGuid().ToString();
            Permissions = new List<Permission>();
        }

        public ApplicationRole(DataModel role) : this()
        {
            if (role == null)
            {
                return;
            }

            Mapper.Map(role, this);
            Permissions = Permissions.OrderBy(p => p.PermissionType)
                .ToList();
        }
    }
}