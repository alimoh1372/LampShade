using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.RoleContracts;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication:IRoleApplication
    {

        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.IsExists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessage.Duplication);

            var role = new Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (role == null)
                return operation.Failed(ApplicationMessage.NotFound);

            if (_roleRepository.IsExists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.NotFound);

            role.Edit(command.Name);
            _roleRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> List()
        {
            return _roleRepository.List();
        }

    }
}