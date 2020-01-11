using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Extensions
{
    public static class LdapExtensions
    {
        public static IList<string> FindAllAdGroups(this HttpContext context, string q)
        {
            var groups = new List<string>();
            var domain = context.User.Identity.Name.Split("\\")[0];

            using (var ctx = new PrincipalContext(ContextType.Domain, domain))
            {
                var principal = new GroupPrincipal(ctx);
                using (var search = new PrincipalSearcher(principal))
                {
                    foreach (var group in search.FindAll())
                    {
                        groups.Add($"{group.Context.Name.ToUpper()}\\{group.Name.ToUpper()}");
                    }
                }
            }

            var busca = q?.ToUpper();

            return groups
                .Where(x => string.IsNullOrEmpty(busca) || x.Contains(busca))
                .OrderBy(x => x)
                .ToList();
        }

        public static IList<string> GetAllAdGroupsFromUserLoggedIn(this HttpContext context)
        {
            var identity = (WindowsIdentity)context.User.Identity;
            var groups = new List<string>();

            if (identity.Groups?.Count > 0)
            {
                foreach (var group in identity.Groups)
                {
                    try
                    {
                        groups.Add(group.Translate(typeof(NTAccount)).ToString());
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            return groups;
        }
    }
}