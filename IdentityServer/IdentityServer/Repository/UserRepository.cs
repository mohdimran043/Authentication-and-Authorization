using MOI.Patrol.ORM_Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOI.IdentityServer.Repository
{
    public class UserRepository : IUserRepository
    {
        MOI_ApplicationPermissionContext _context = new MOI_ApplicationPermissionContext();

        public bool ValidateCredentials(string username, string password)
        {
            var user = FindByUsername(username);
            if (user != null)
            {
                return true;
            }

            return false;
        }

        public MoiUser FindBySubjectId(string subjectId)
        {
            return _context.MoiUser.FirstOrDefault();
        }

        public MoiUser FindByUsername(string username)
        {
            return _context.MoiUser.FirstOrDefault();
        }
    }
}
