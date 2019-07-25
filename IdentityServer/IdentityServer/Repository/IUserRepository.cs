using MOI.Patrol.ORM_Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOI.IdentityServer.Repository
{
   public interface IUserRepository
    {
        bool ValidateCredentials(string username, string password);

        MoiUser FindBySubjectId(string subjectId);

        MoiUser FindByUsername(string username);
    }
}
