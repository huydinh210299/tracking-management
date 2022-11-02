using AutoFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTO;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Utils;

namespace TrackingManagement.Services
{
    public class UserService : IUserService
    {
        private readonly BKContext _db;

        public UserService(BKContext db)
        {
            _db = db;
        }

        public int createUser(CreateUserModal user)
        {
            var createdUser = new User() { UserName = user.UserName, Password = BCrypt.Net.BCrypt.HashPassword(user.Password), ScopeId = user.ScopeId };
            _db.Users.Add(createdUser);
            _db.SaveChanges();
            foreach(int unitId in user.UnitIds)
            {
                UserUnit userUnit = new UserUnit() { UserId = createdUser.Id, UnitId = unitId };
                _db.UserUnits.Add(userUnit);
            }
            return _db.SaveChanges();
        }

        public int deleteUser(int userId)
        {
            User u = _db.Users.Where(item => item.Id == userId).FirstOrDefault();
            u.Status = false;
            return _db.SaveChanges();
        }

        public async Task<PagedResponse<List<User>>> getListUser(UserFilter userFilter, PaginationFilter paginationFilter)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.Users.AutoFilter(userFilter)
                                .Include(u => u.Scope)
                                .Include(u => u.UserUnits);
            var total = await  _db.Users
                        .AutoFilter(userFilter).CountAsync();
            List<User> userList = new List<User>();
            if (paginationFilter.Paging == true)
            {
                userList = await query.Skip((page - 1) * record).Take(record).ToListAsync();
            }
            else
            {
                userList = await query.ToListAsync();
            }
            return new PagedResponse<List<User>>(userList, total);
        }

        public User getUserById(int id)
        {
            var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public Tuple<User, Scope> getUserScope(UserLoginModel userInfo)
        {
            var userScope = (from u in _db.Users
                             join sc in _db.Scopes on
                             u.ScopeId equals sc.Id
                             where u.UserName == userInfo.UserName && u.Status == true
                             select new { u, sc, u.Password }).FirstOrDefault();
            if (userScope != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(userInfo.Password, userScope.Password);
                if (validPassword)
                {
                    return new Tuple<User, Scope>(userScope.u, userScope.sc);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<Unit> getUserUnit(int userId)
        {
            var userUnits = (from uu in _db.UserUnits
                             join u in _db.Units
                             on uu.UnitId equals u.Id
                             where uu.UserId == userId
                             select u).ToList();
            return userUnits;
        }

        public int updateUser(UpdateUser updateUser, int userId)
        {
            var user = _db.Users.Where(u => u.Id == userId).Include(u => u.UserUnits).FirstOrDefault();
            if (updateUser.Password != null) {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUser.Password);
            }
            user.ScopeId = updateUser.ScopeId;
            List<int> unitIds = new List<int>();
            foreach(UserUnit uu in user.UserUnits)
            {
                unitIds.Add((int)uu.UnitId);
            }
            bool isUserUnitModify = !Enumerable.SequenceEqual(unitIds.OrderBy(e => e), updateUser.UnitIds.OrderBy(e => e));
            if (isUserUnitModify)
            {
                _db.UserUnits.RemoveRange(user.UserUnits);
                foreach(int unitId in updateUser.UnitIds)
                {
                    UserUnit uu = new UserUnit() { UnitId = unitId, UserId = user.Id };
                    user.UserUnits.Add(uu);
                }
            }
            return _db.SaveChanges();
        }

        public int updateUserPassword(UpdateUserPassword updateUserPassword)
        {
            User user = _db.Users.Where(u => u.Id == updateUserPassword.UserId).FirstOrDefault();
            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(updateUserPassword.OldPassword, user.Password);
            if (isOldPasswordValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserPassword.NewPassword);
                return _db.SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
