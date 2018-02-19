using BAuth.DAL;
using BAuth.Models;
using System;
using System.Linq;

namespace BAuth.Hellpers
{
    public class UserHeppler
    {
        public static VMUser GetUserById(string userID)
        {
            VMUser user = null;
            using (BAuthDBContext context = new BAuthDBContext())
            {
                var userDB = context.Users.Where(u => u.UserId == userID).SingleOrDefault();
                if (userDB != null)
                {
                    user = new VMUser()
                    {
                        UserId = userDB.UserId,
                        FirstName = userDB.FirstName,
                        LastName = userDB.LastName,
                        Email = userDB.Email,
                        PictureUrl = userDB.PictureUrl,
                        Role = userDB.Role
                    };
                }
            }
            return user;
        }

        public static VMUser SyncUserToDatabase(VMUser user)
        {
            user.PictureUrl = user.PictureUrl ?? "";
            user.FirstName = user.FirstName ?? "";
            user.LastName = user.LastName ?? "";
            user.Email = user.Email ?? "";
            user.Role = user.Role ?? "";

            using (BAuthDBContext context = new BAuthDBContext())
            {
                var userDB = context.Users.Where(u => u.UserId == user.UserId).SingleOrDefault();
                if (userDB != null)
                {
                    user.Role = userDB.Role;
                    return user;
                }
                user.Role = "";
                var newUserBD = new User()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PictureUrl = user.PictureUrl,
                    CreateDate = DateTime.Now,
                    Role = user.Role
                };

                context.Users.Add(newUserBD);
                context.SaveChanges();
                return user;
            }
        }
        
    }
}