using Microsoft.EntityFrameworkCore;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using FinBY.Domain.Data.DTO;

namespace FinBY.Infra.Repository
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DbContext context)
           : base(context)
        {
        }

        public User ValidateCredentials(UserLoginDTO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _dataset.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User ValidateCredentials(string userName)
        {
            return _dataset.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _dataset.SingleOrDefault(u => (u.UserName == userName));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_dataset.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _dataset.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
