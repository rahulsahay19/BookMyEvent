// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace BookMyEvent.Services.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(), // subject id will be returned
                new IdentityResources.Profile(), //profiles like names will be returned
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                //each microservice will be an API Resource
            };
            
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               //For more granualar access, scopes will be added.
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               //will be added
            };
    }
}