﻿using Common.Api;
using Common.Extensions;
using Entities.Model.Users;
using Entities.Response.Users;
using Entities.Response.Vocabularies;
using Entities.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading;

namespace WebApi.Controllers
{
    public class BaseController<TService>(TService service) : ControllerBase
    {
        protected readonly TService service = service;

        public VMUserMiniInfo CurrentUser
        {
            get
            {
                return new VMUserMiniInfo()
                {
                    Id = HttpContext.User.Identity.GetUserId<int>(),
                    NickName = HttpContext.User.Identity.GetNickName(),
                    UserName = HttpContext.User.Identity.GetUserName(),
                };
            }
        }
    }
}
