﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using EdFi.Ods.Api.Security.AuthorizationStrategies.Relationships;

namespace EdFi.Ods.Api.Security.Authorization
{
    public interface IConcreteEducationOrganizationIdAuthorizationContextDataTransformer<TContextData>
        where TContextData : RelationshipsAuthorizationContextData
    {
        TContextData GetConcreteAuthorizationContextData(TContextData authorizationContextData);
    }
}
