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
            
        }
    }
}
