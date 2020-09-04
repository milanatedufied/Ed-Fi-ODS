﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

#if NETFRAMEWORK
using System.Web;
using System.Web.Mvc;
using EdFi.Ods.Admin.Extensions;

namespace EdFi.Ods.Admin.Services
{
    public class RouteService : IRouteService
    {
        private UrlHelper Url
        {
            get { return new UrlHelper(HttpContext.Current.Request.RequestContext); }
        }

        public string GetRouteForPasswordReset(string email, string marker)
        {
            return Url.Action(
                           "ResetPassword",
                           "Account",
                           new
                           {
                               Email = email,
                               Marker = marker
                           })
                      .ToAbsolutePath();
        }

        public string GetRouteForActivation(string email, string marker)
        {
            return Url.Action(
                           "ActivateAccount",
                           "Account",
                           new
                           {
                               Email = email,
                               Marker = marker
                           })
                      .ToAbsolutePath();
        }
    }
}
#endif