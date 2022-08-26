﻿using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{

    public interface IUserRepository : IRepository<User, int>
    {
        User ValidateCredentials(UserLoginDTO user);

        User ValidateCredentials(string username);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);
    }
}
