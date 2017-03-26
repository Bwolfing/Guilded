﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guilded.Security.Claims
{
    public static class RoleClaimTypes
    {
        #region Properties
        #region Public properties
        public static readonly RoleClaim RoleManagement = new RoleClaim
        {
            ClaimType = "Guilded:Admin:Roles",
            Description = "Gives permission to create, edit, and apply roles",
        };

        public static readonly RoleClaim ForumsPinning = new RoleClaim
        {
            ClaimType = "Guilded:Forums:Pin Posts",
            Description = "Gives permission to pin posts in the forums",
        };
        public static readonly RoleClaim ForumsLocking = new RoleClaim
        {
            ClaimType = "Guilded:Forums:Lock Posts",
            Description = "Gives permission to lock posts in the forums",
        };
        public static readonly RoleClaim ForumsReader = new RoleClaim
        {
            ClaimType = "Guilded:Forums:Read Posts",
            Description = "Gives permission to read posts in the forums",
        };
        public static readonly RoleClaim ForumsWriter = new RoleClaim
        {
            ClaimType = "Guilded:Forums:Write Posts",
            Description = "Gives permission to create and reply to posts in the forums",
        };

        public static readonly IEnumerable<RoleClaim> RoleClaims = new List<RoleClaim>
        {
            RoleManagement,
            ForumsPinning,
            ForumsLocking,
            ForumsReader,
            ForumsWriter,
        };
        #endregion

        #region Private properties
        private static Dictionary<string, RoleClaim> TypesToRoleClaims
        {
            get
            {
                if (_typesToRolesClaims == null)
                {
                    _typesToRolesClaims = new Dictionary<string, RoleClaim>();
                    foreach (RoleClaim rc in RoleClaims)
                    {
                        _typesToRolesClaims.Add(rc.ClaimType, rc);
                    }
                }
                return _typesToRolesClaims;
            }
        }

        private static Dictionary<string, RoleClaim> _typesToRolesClaims;
        #endregion
        #endregion

        #region Methods
        #region Public methods
        public static RoleClaim LookUpRoleClaimByIdentityRoleClaim(IdentityRoleClaim<string> identityRoleClaim)
        {
            if (!TypesToRoleClaims.ContainsKey(identityRoleClaim.ClaimType))
            {
                throw new KeyNotFoundException($"Unable to fined role claim {identityRoleClaim.ClaimType}");
            }
            return TypesToRoleClaims[identityRoleClaim.ClaimType];
        }
        #endregion
        #endregion
    }
}