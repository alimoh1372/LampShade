using System.Collections.Generic;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.RoleContracts
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleViewModel> List();
        EditRole GetDetails(long id);
    }
}