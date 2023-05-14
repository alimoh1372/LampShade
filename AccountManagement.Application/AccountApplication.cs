using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.AccountContracts;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{

    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUpload _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
            IFileUpload fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _authHelper = authHelper;
            _roleRepository = roleRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _accountRepository = accountRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessage.NotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessage.PasswordsNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.IsExists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessage.Duplication);

            var password = _passwordHasher.Hash(command.Password);
            var path = $"profilePhotos";
            var picturePath = _fileUploader.UploadFile(command.ProfilePhoto, path);
            var account = new Account(command.Fullname, command.Username, password, command.Mobile, command.RoleId,
                picturePath);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessage.NotFound);

            if (_accountRepository.IsExists(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.Duplication);

            var path = $"profilePhotos";
            var picturePath = _fileUploader.UploadFile(command.ProfilePhoto, path);
            account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, picturePath);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account == null)
                return operation.Failed(ApplicationMessage.WrongUserPass);

            var result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.Verified)
                return operation.Failed(ApplicationMessage.WrongUserPass);

            var permissions = _roleRepository.Get(account.RoleId)
                .Permissions
                .Select(x => x.Code)
                .ToList();


            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username,
                permissions);

            _authHelper.Signin(authViewModel);

            return operation.Succedded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }
    }

}