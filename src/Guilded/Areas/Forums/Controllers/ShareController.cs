﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guilded.Areas.Forums.Constants;
using Guilded.Areas.Forums.DAL;
using Guilded.Areas.Forums.ViewModels;
using Guilded.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guilded.Areas.Forums.Controllers
{
    [AllowAnonymous]
    [Route("[area]/[controller]/[action]")]
    public class ShareController : ForumsController
    {
        public const int ThreadPreviewLength = 100;

        private readonly IConvertMarkdown _markdownConverter;

        public ShareController(IForumsDataContext dataContext,
            ILoggerFactory loggerFactory,
            IConvertMarkdown markdownConverter) : base(dataContext, loggerFactory)
        {
            _markdownConverter = markdownConverter;
        }

        [AllowAnonymous]
        [HttpGet("{slug}", Name = RouteNames.ThreadSharingRoute)]
        public async Task<IActionResult> Thread(string slug)
        {
            var thread = await DataContext.GetThreadBySlugAsync(slug);
            if (thread == null || thread.IsDeleted)
            {
                return NotFound();
            }

            var viewModel = new ThreadPreview
            {
                ShareLink = Url.RouteUrl(RouteNames.ThreadSharingRoute, new { slug }, "https"),
                Description = _markdownConverter.ConvertAndStripHtml(thread.Content),
                Title = thread.Title
            };

            if (viewModel.Description.Length > ThreadPreviewLength)
            {
                viewModel.Description = viewModel.Description.Substring(0, ThreadPreviewLength);
            }

            return View("ShareContent", viewModel);
        }
    }
}