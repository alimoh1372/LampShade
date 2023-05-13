using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.RoleContracts;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IBaseRepository<long, Role>
    {
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}