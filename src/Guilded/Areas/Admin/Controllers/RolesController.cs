﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Guilded.Areas.Admin.ViewModels.Roles;
using Guilded.Data.DAL.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guilded.Areas.Admin.Controllers
{
    using DataModel = Identity.ApplicationRole;
    using ViewModel = ApplicationRole;

    public class RolesController : BaseController
    {
        public const int PageSize = 20;

        private readonly IAdminDataContext _db;
        private readonly ILogger _log;

        public RolesController(IAdminDataContext db,
            ILoggerFactory loggerFactory)
        {
            _db = db;
            _log = loggerFactory.CreateLogger<RolesController>();
        }

        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return RedirectToAction(nameof(Index), new {page = 1});
            }

            var viewModel = GetRoles(page);

            if (viewModel.LastPage != 0 && page > viewModel.LastPage)
            {
                return RedirectToAction(nameof(Index), new {page = viewModel.LastPage});
            }

            return View(viewModel);
        }

        private PaginatedRolesViewModel GetRoles(int page)
        {
            int zeroIndexPage = page - 1;

            var allRoles = _db.GetRoles();
            var rolesForPage = allRoles.Skip(PageSize * zeroIndexPage).Take(PageSize);

            return new PaginatedRolesViewModel
            {
                CurrentPage = page,
                LastPage = (int)Math.Ceiling(allRoles.Count() / (double)PageSize),
                Roles = Mapper.Map<IQueryable<DataModel>, List<ViewModel>>(rolesForPage)
            };
        }
    }
}
