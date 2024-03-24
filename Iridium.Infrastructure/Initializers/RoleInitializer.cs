using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Attributes;
using Iridium.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Iridium.Infrastructure.Initializers
{
    public class RoleInitializer
    {
        private readonly ApplicationDbContext dbContext;

        public RoleInitializer(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async void InitializeRoles()
        {
            var roleTypes = AppDomain.CurrentDomain.GetAssemblies()
                                                   .SelectMany(a => a.GetTypes())
                                                   .Where(t => typeof(IRole).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                                                   .ToList();

            var allRolesDb = await dbContext.Role.Where(w => w.Deleted != true)
                                                 .ToListAsync();

            var allRolesDomain = new List<Role>();

            foreach (Type roleType in roleTypes)
            {
                RoleAreaAttribute roleAreaAttribute = roleType.GetCustomAttribute<RoleAreaAttribute>();

                string roleArea = roleAreaAttribute.Area;

                FieldInfo[] fieldInfos = roleType.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    RoleNameAttribute roleNameAttribute = fieldInfo.GetCustomAttribute<RoleNameAttribute>();

                    if (roleNameAttribute == null)
                        continue;

                    string roleName = roleNameAttribute.Name;

                    string paramCode = (string)fieldInfo.GetValue(null);

                    var role = new Role
                    {
                        GuidId = Guid.NewGuid(),
                        Name = roleName,
                        Area = roleArea,
                        ParamCode = paramCode,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                    };

                    allRolesDomain.Add(role);
                }
            }

            var rolesWillBeAdd = allRolesDomain.Except(allRolesDb);
            var rolesWillBeRemoved = allRolesDb.Except(allRolesDomain);

            await dbContext.AddRangeAsync(rolesWillBeAdd);
            dbContext.RemoveRange(rolesWillBeRemoved);

            await dbContext.SaveChangesAsync();
            
        }
    }
}
