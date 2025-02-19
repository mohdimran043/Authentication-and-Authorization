﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        //Defining the InMemory Clients
        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();

            //Client1
            clients.Add(new Client()
            {
                ClientId = "Client1",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "api1", IdentityServerConstants.StandardScopes.OfflineAccess },
                AllowOfflineAccess = true

            });
            clients.Add(new Client
            {
                ClientId = "mvc-client-hybrid",
                ClientName = "MVC Client Code",
                ClientSecrets = { new Secret("segredo".Sha256()) },

                AllowedGrantTypes = GrantTypes.Hybrid,

                RedirectUris = { "http://localhost:62026/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:62026/signout-callback-oidc" },


                AllowedScopes = { "openid", "profile", "email", "financial.read", "offline_access" },
                AllowAccessTokensViaBrowser = true,
                //Refresh Token Settings
                AllowOfflineAccess = true,
                RequirePkce = true,
                RequireConsent = false,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 60 * 60 * 24 * 365
            });
            clients.Add(new Client
            {
                ClientId = "mvc-client-code",
                ClientName = "MVC Client Code",
                ClientSecrets = { new Secret("segredo".Sha256()) },

                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                RedirectUris = { "http://localhost:62026/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:62026/signout-callback-oidc" },


                AllowedScopes = { "openid", "profile", "email", "financial.read", "offline_access" },

                //Refresh Token Settings
                AllowOfflineAccess = true,
                RequireConsent = false,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 60 * 60 * 24 * 365
            });
            //Client for MVC
            clients.Add(new Client()
            {
                ClientId = "mvc",
                ClientName = "MVC",
                AllowedGrantTypes = GrantTypes.Implicit,
                RequireConsent = false,
                RedirectUris = { "http://localhost:12021/signin-oidc" },
                PostLogoutRedirectUris = { "http://localhost:12021/signout-callback-oidc" },
                AllowedScopes = new List<string>
                 {
                     IdentityServerConstants.StandardScopes.OpenId,
                     IdentityServerConstants.StandardScopes.Profile,
                      IdentityServerConstants.StandardScopes.OfflineAccess,
                      "roles"
                 },
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true
            });

            return clients;
        }

        //Defining the InMemory API's
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> apiResources = new List<ApiResource>();

            apiResources.Add(new ApiResource("api1", "First API"));

            return apiResources;
        }

        //Defining the InMemory User's
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId="1",
                    Username="admin",
                    Password="password",

                    Claims = new List<Claim>
                     {
                         new Claim("role", "admin"),
                     }
                 },
                new TestUser()
                {
                    SubjectId="2",
                    Username="editor",
                    Password="password",

                     Claims = new List<Claim>
                     {
                         new Claim("role", "editor"),
                     }
                }
            };
        }

        //Support for OpenId connectivity scopes
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            List<IdentityResource> resources = new List<IdentityResource>();

            resources.Add(new IdentityResources.OpenId()); // Support for the OpenId
            resources.Add(new IdentityResources.Profile()); // To get the profile information
            resources.Add(new IdentityResource("roles", new[] { "role" }));//To Support Roles

            return resources;
        }
    }
}
