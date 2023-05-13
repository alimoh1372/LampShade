using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.AccountContracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IBaseRepository<long,Account>
    {

        Account GetBy(string username);
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}