using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace PadTime.Identity;

public static class Config
{
    // Custom claim types for Pad'Time
    public static class CustomClaimTypes
    {
        public const string Matricule = "matricule";
        public const string MemberCategory = "member_category";
        public const string SiteId = "site_id";
        public const string Role = "role";
    }

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            // Custom identity resource for Pad'Time specific claims
            new IdentityResource(
                name: "padtime_profile",
                displayName: "Pad'Time Profile",
                userClaims: new[]
                {
                    CustomClaimTypes.Matricule,
                    CustomClaimTypes.MemberCategory,
                    CustomClaimTypes.SiteId,
                    CustomClaimTypes.Role
                })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("padel_api", "Pad'Time API")
            {
                UserClaims = new[]
                {
                    CustomClaimTypes.Matricule,
                    CustomClaimTypes.MemberCategory,
                    CustomClaimTypes.SiteId,
                    CustomClaimTypes.Role
                }
            },
            new ApiScope("padel_admin", "Pad'Time Admin API")
            {
                UserClaims = new[]
                {
                    CustomClaimTypes.Role,
                    CustomClaimTypes.SiteId
                }
            },
            new ApiScope("padel_analytics", "Pad'Time Analytics API")
            {
                UserClaims = new[]
                {
                    CustomClaimTypes.Role
                }
            }
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("padtime-api", "Pad'Time Backend API")
            {
                Scopes = { "padel_api", "padel_admin", "padel_analytics" },
                UserClaims = new[]
                {
                    CustomClaimTypes.Matricule,
                    CustomClaimTypes.MemberCategory,
                    CustomClaimTypes.SiteId,
                    CustomClaimTypes.Role
                }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // Angular SPA client using Authorization Code flow + PKCE
            new Client
            {
                ClientId = "padtime-web",
                ClientName = "Pad'Time Web Application",

                // No secret for public SPA client
                RequireClientSecret = false,

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                // Where to redirect after login
                RedirectUris =
                {
                    "http://localhost:4200/callback",
                    "https://localhost:4200/callback"
                },

                // Where to redirect after logout
                PostLogoutRedirectUris =
                {
                    "http://localhost:4200",
                    "https://localhost:4200"
                },

                // Allowed CORS origins for token endpoint
                AllowedCorsOrigins =
                {
                    "http://localhost:4200",
                    "https://localhost:4200"
                },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "padtime_profile",
                    "padel_api",
                    "padel_admin",
                    "padel_analytics"
                },

                // Token settings
                AccessTokenLifetime = 3600, // 1 hour
                IdentityTokenLifetime = 3600,

                // Allow refresh tokens
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.ReUse,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 86400, // 24 hours

                // Always include user claims in ID token
                AlwaysIncludeUserClaimsInIdToken = true
            },

            // Machine-to-machine client for backend services
            new Client
            {
                ClientId = "padtime-api",
                ClientName = "Pad'Time API Service",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("padtime-api-secret".Sha256()) },

                AllowedScopes = { "padel_api", "padel_admin", "padel_analytics" }
            }
        };
}
