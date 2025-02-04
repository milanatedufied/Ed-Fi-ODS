﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EdFi.Ods.Common.Security.Authorization;
using EdFi.Ods.Common.Security.Claims;

namespace EdFi.Ods.Api.Security.Authorization.Repositories
{
    /// <summary>
    /// Provides an abstract base class for authorization decorators to use for invoking 
    /// authorization.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of the entity/request.</typeparam>
    public abstract class RepositoryOperationAuthorizationDecoratorBase<T>
    {
        private readonly IAuthorizationContextProvider _authorizationContextProvider;
        private readonly IEdFiAuthorizationProvider _authorizationProvider;

        protected RepositoryOperationAuthorizationDecoratorBase(
            IAuthorizationContextProvider authorizationContextProvider,
            IEdFiAuthorizationProvider authorizationProvider)
        {
            _authorizationContextProvider = authorizationContextProvider;
            _authorizationProvider = authorizationProvider;
        }

        /// <summary>
        /// Invokes authorization of the request using the resource/action currently in context.
        /// </summary>
        /// <param name="entity">The request/entity being authorized.</param>
        protected async Task AuthorizeSingleItemAsync(T entity, CancellationToken cancellationToken)
        {
            var action = _authorizationContextProvider.GetAction();

            await AuthorizeSingleItemAsync(entity, action, cancellationToken);
        }

        /// <summary>
        /// Invokes authorization of the request using the resource currently in context but wit 
        /// an override action (e.g. for converting the "Upsert" action to either "Create" or "Update").
        /// </summary>
        /// <param name="entity">The request/entity being authorized.</param>
        /// <param name="actionUri">The action being performed with the request/entity.</param>
        protected async Task AuthorizeSingleItemAsync(T entity, string actionUri, CancellationToken cancellationToken)
        {
            // Make sure Authorization context is present before proceeding
            _authorizationContextProvider.VerifyAuthorizationContextExists();

            // Build the AuthorizationContext
            EdFiAuthorizationContext authorizationContext = new EdFiAuthorizationContext(
                ClaimsPrincipal.Current,
                _authorizationContextProvider.GetResourceUris(),
                actionUri,
                entity);

            // Authorize the call
            await _authorizationProvider.AuthorizeSingleItemAsync(authorizationContext, cancellationToken);
        }

        protected IReadOnlyList<AuthorizationFilterDetails> GetAuthorizationFilters<TEntity>()
        {
            // Make sure Authorization context is present before proceeding
            _authorizationContextProvider.VerifyAuthorizationContextExists();

            // Build the AuthorizationContext
            var authorizationContext = new EdFiAuthorizationContext(
                ClaimsPrincipal.Current,
                _authorizationContextProvider.GetResourceUris(),
                _authorizationContextProvider.GetAction(),
                typeof(TEntity));

            // Get authorization filters
            return _authorizationProvider.GetAuthorizationFilters(authorizationContext);
        }

        protected void AuthorizeResourceActionOnly(T entity, string actionUri)
        {
            // Make sure Authorization context is present before proceeding
            _authorizationContextProvider.VerifyAuthorizationContextExists();

            // Build the AuthorizationContext
            EdFiAuthorizationContext authorizationContext = new EdFiAuthorizationContext(
                ClaimsPrincipal.Current,
                _authorizationContextProvider.GetResourceUris(),
                actionUri,
                entity);

            // Authorize the call
            _authorizationProvider.AuthorizeResourceActionOnly(authorizationContext);
        }
    }
}
