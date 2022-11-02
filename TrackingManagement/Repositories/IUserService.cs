using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingManagement.DTO;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IUserService
    {
        public Tuple<User, Scope> getUserScope(UserLoginModel userInfo);
        public List<Unit> getUserUnit(int userId);
        public Task<PagedResponse<List<User>>> getListUser(UserFilter userFilter, PaginationFilter paginationFilter);
        public User getUserById(int id);
        public int createUser(CreateUserModal createUserModal);
        public int updateUser(UpdateUser updateUser, int userId);
        public int deleteUser(int userId);
        public int updateUserPassword(UpdateUserPassword updateUserPassword);

    }
}
