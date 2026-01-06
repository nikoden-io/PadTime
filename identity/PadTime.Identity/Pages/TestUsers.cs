using System.Security.Claims;
using Duende.IdentityModel;
using Duende.IdentityServer.Test;

namespace PadTime.Identity.Pages;

public static class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            return new List<TestUser>
            {
                // Standard user - Free member (L category, J-5 booking window)
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Martin"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Martin"),
                        new Claim(JwtClaimTypes.Email, "alice.martin@example.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        // Pad'Time custom claims
                        new Claim(Config.CustomClaimTypes.Matricule, "L0001"),
                        new Claim(Config.CustomClaimTypes.MemberCategory, "free"),
                        new Claim(Config.CustomClaimTypes.Role, "user"),
                        // No site_id for free members
                    }
                },

                // Global member (G category, J-21 booking window)
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Dupont"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Dupont"),
                        new Claim(JwtClaimTypes.Email, "bob.dupont@example.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        // Pad'Time custom claims
                        new Claim(Config.CustomClaimTypes.Matricule, "G0001"),
                        new Claim(Config.CustomClaimTypes.MemberCategory, "global"),
                        new Claim(Config.CustomClaimTypes.Role, "user"),
                        // No site_id for global members (can access all sites)
                    }
                },

                // Site member (S category, J-14 booking window, restricted to site)
                new TestUser
                {
                    SubjectId = "3",
                    Username = "charlie",
                    Password = "charlie",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Charlie Bernard"),
                        new Claim(JwtClaimTypes.GivenName, "Charlie"),
                        new Claim(JwtClaimTypes.FamilyName, "Bernard"),
                        new Claim(JwtClaimTypes.Email, "charlie.bernard@example.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        // Pad'Time custom claims
                        new Claim(Config.CustomClaimTypes.Matricule, "S0001"),
                        new Claim(Config.CustomClaimTypes.MemberCategory, "site"),
                        new Claim(Config.CustomClaimTypes.Role, "user"),
                        new Claim(Config.CustomClaimTypes.SiteId, "site-paris-01"),
                    }
                },

                // Site administrator
                new TestUser
                {
                    SubjectId = "4",
                    Username = "diana",
                    Password = "diana",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Diana Leroy"),
                        new Claim(JwtClaimTypes.GivenName, "Diana"),
                        new Claim(JwtClaimTypes.FamilyName, "Leroy"),
                        new Claim(JwtClaimTypes.Email, "diana.leroy@padtime.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        // Pad'Time custom claims
                        new Claim(Config.CustomClaimTypes.Matricule, "A0001"),
                        new Claim(Config.CustomClaimTypes.MemberCategory, "global"),
                        new Claim(Config.CustomClaimTypes.Role, "admin_site"),
                        new Claim(Config.CustomClaimTypes.SiteId, "site-paris-01"),
                    }
                },

                // Global administrator
                new TestUser
                {
                    SubjectId = "5",
                    Username = "admin",
                    Password = "admin",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Admin System"),
                        new Claim(JwtClaimTypes.GivenName, "Admin"),
                        new Claim(JwtClaimTypes.FamilyName, "System"),
                        new Claim(JwtClaimTypes.Email, "admin@padtime.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        // Pad'Time custom claims
                        new Claim(Config.CustomClaimTypes.Matricule, "A0000"),
                        new Claim(Config.CustomClaimTypes.MemberCategory, "global"),
                        new Claim(Config.CustomClaimTypes.Role, "admin_global"),
                        // No site_id for global admin (has access to all sites)
                    }
                },

                // User with debt (for testing debt blocking)
                new TestUser
                {
                    SubjectId = "6",
                    Username = "debtor",
                    Password = "debtor",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Pierre Dettes"),
                        new Claim(JwtClaimTypes.GivenName, "Pierre"),
                        new Claim(JwtClaimTypes.FamilyName, "Dettes"),
                        new Claim(JwtClaimTypes.Email, "pierre.dettes@example.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        // Pad'Time custom claims
                        new Claim(Config.CustomClaimTypes.Matricule, "L0002"),
                        new Claim(Config.CustomClaimTypes.MemberCategory, "free"),
                        new Claim(Config.CustomClaimTypes.Role, "user"),
                    }
                }
            };
        }
    }
}
