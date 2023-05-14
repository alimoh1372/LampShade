using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.RoleContracts;
using AccountManagement.Domain.PermissionAgg;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class RoleRepository : BaseRepository<long, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;

        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public EditRole GetDetails(long id)
        {
            var role = _accountContext.Roles.Select(x => new EditRole
            {
                Name = x.Name,
                Id = x.Id,
                MappedPermissions = MapPermissions(x.Permissions)
            }).AsNoTracking()
                .FirstOrDefault(x=>x.Id==id);
            
            role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();
            return role;
        }

        private static List<PermissionDto> MapPermissions(List<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }

        public List<RoleViewModel> List()
        {
            return _accountContext.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }
    }
}