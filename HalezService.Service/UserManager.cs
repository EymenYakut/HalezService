using HalezService.Entities;
using HalezService.Model.Dtos.User;
using HalezService.Model.Enums;
using HalezService.Model.Shared;
using HallezService.Data.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HalezService.Service
{
    public class UserManager
    {
        private readonly IUnitOfWork unitOfWork;

        public UserManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public UserManager()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public LoginResult LoginControl(string username, string password)
        {
            User userQuery = unitOfWork.Users.Get(u => u.Code == username && u.Password == password).FirstOrDefault();

            LoginResult loginResult =  new LoginResult()
                                       {
                                           Id = userQuery.Id,
                                           NameSurname = userQuery.Name,
                                           SecurityLevel = "",
                                           UserTypes = userQuery.UserType
                                       };

            return loginResult;
        }
        public void Insert(AddUserInput addUserInput, int userId)
        {
            try
            {
                unitOfWork.BeginTransaction();
                User user = new()
                {
                    Code = addUserInput.Code,
                    Name = addUserInput.Name,
                    Password = addUserInput.Password,
                    Gsm = addUserInput.Gsm,
                    Mail = addUserInput.Email,
                    Status = "1",
                    Adress = addUserInput.Adress,
                    Surname = addUserInput.Surname,
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };
                unitOfWork.Users.Add(user);
                unitOfWork.SaveChanges();

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception(MyExceptionHandler.GetAllExceptionMessages(exception: ex));
            }
        }
        public void Delete(int id, int userId)
        {
            try
            {
                User user = unitOfWork.Users.Get(id: id);
                user.IsDeleted = true;
                user.DeletedBy = userId;
                user.DeletedDate = DateTime.Now;
                unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(MyExceptionHandler.GetAllExceptionMessages(exception: ex));
            }
        }
        public void Update(UpdateUserInput updateUserInput, int updateUserId, int userId)
        {
            try
            {
                unitOfWork.BeginTransaction();
                User user = unitOfWork.Users.Get(u => u.Id == updateUserId).First();
                user.Code = updateUserInput.Code;
                user.Name = updateUserInput.Name;
                user.Surname = updateUserInput.Surname;
                user.Adress = updateUserInput.Adress;
                user.Gsm = updateUserInput.Gsm;
                user.Mail = updateUserInput.Email;
                user.ModifyBy = userId;
                user.ModifyDate = DateTime.Now;
                unitOfWork.SaveChanges();

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new Exception(MyExceptionHandler.GetAllExceptionMessages(exception: ex));
            }
        }
        
        public UserFullResult Get(int id)
        {
            UserFullResult userFullResult = unitOfWork.Users.Get(u => u.Id == id).Select(ToFullResult).First();
            return userFullResult;
        }
        public List<UserResult> GetList()
        {
            return unitOfWork.Users.Get(u => u.IsDeleted == false).Select(ToResult).ToList();
        }


        private Expression<Func<User, UserResult>> ToResult => user =>
          new UserResult()
          {
              Code = user.Code,
              Email = user.Mail,
              Gsm = user.Gsm,
              Id = user.Id,
              Name = user.Name,
              Status = user.Status,
              Adress = user.Adress,
              Surname = user.Surname
          };
        private Expression<Func<User, UserFullResult>> ToFullResult => user =>
          new UserFullResult()
          {
              Code = user.Code,
              Email = user.Mail,
              Gsm = user.Gsm,
              Id = user.Id,
              Name = user.Name,
              Status = user.Status,
              Adress = user.Adress,
              Surname = user.Surname
          };
    }
}
