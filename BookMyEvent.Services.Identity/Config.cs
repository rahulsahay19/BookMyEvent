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
                new ApiResource("eventcatalog", "Event Catalog API")
                {
                    Scopes = { "eventcatalog.read", "eventcatalog.write" }
                },
                new ApiResource("shoppingbasket", "Shopping Basket API")
                {
                    Scopes = { "shoppingbasket.fullaccess" }
                },
                new ApiResource("discount", "Discount API")
                {
                    Scopes = { "discount.fullaccess" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               //For more granualar access, scopes will be added.
               new ApiScope("eventcatalog.fullaccess"),
               new ApiScope("eventcatalog.read"),
               new ApiScope("eventcatalog.write"),
               new ApiScope("shoppingbasket.fullaccess"),
               new ApiScope("discount.fullaccess")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               //will be added
               new Client
               {
                   ClientName="Book My Event Machine 2 Machine Client",
                   ClientId="bookmyeventm2m",
                   ClientSecrets = { new Secret("c89e03c5-7b84-456d-822c-edaca84ed7f4".Sha256()) },
                   AllowedGrantTypes = GrantTypes.ClientCredentials,
                   AllowedScopes = { "eventcatalog.fullaccess" }
               },
                new Client
                {
                    ClientName = "Book My Event Interactive Client",
                    ClientId = "bookmyeventinteractive",
                    ClientSecrets = { new Secret("d1ee8859-6e00-45f9-b749-3ae4124299a0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "shoppingbasket.fullaccess" }
                },
                new Client
                {
                    ClientName = "Book My Client",
                    ClientId = "bookmyevent",
                    ClientSecrets = { new Secret("d1ee8859-6e00-45f9-b749-3ae4124299a0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RedirectUris = { "https://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "shoppingbasket.fullaccess", "eventcatalog.read", "eventcatalog.write" }
                },
                new Client
                {
                    ClientId = "shoppingbaskettodownstreamtokenexchangeclient",
                    ClientName = "Shopping Basket Token Exchange Client",
                    AllowedGrantTypes = new[] { "urn:ietf:params:oauth:grant-type:token-exchange" },
                    ClientSecrets = { new Secret("0cdea0bc-779e-4368-b46b-09956f70712c".Sha256()) },
                    AllowedScopes = {
                         "openid", "profile", "discount.fullaccess" }
                }
            };
    }
}