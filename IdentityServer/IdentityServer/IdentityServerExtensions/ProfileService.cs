using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using MOI.Patrol.ORM_Auth;
namespace IdentityServer.IdentityServerExtensions
{
    public class ProfileService : IProfileService
    {
        private MOI_ApplicationPermissionContext _repository;

        public ProfileService(MOI_ApplicationPermissionContext rep)
        {
            this._repository = rep;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //{
                //    var subjectId = context.Subject.GetSubjectId();
                //    var user = _repository.GetUserById(subjectId);

                //    var claims = new List<Claim>
                //    {
                //        new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                //        //new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                //        //new Claim(JwtClaimTypes.GivenName, user.FirstName),
                //        //new Claim(JwtClaimTypes.FamilyName, user.LastName),
                //        new Claim(JwtClaimTypes.Email, user.Email),
                //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                //    };

                //    context.IssuedClaims = claims;
                return Task.FromResult(0);
            }
            catch (Exception x)
            {
                return Task.FromResult(0);
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //var user = _repository.GetUserById(context.Subject.GetSubjectId());
            //context.IsActive = (user != null) && user.Active;
            return Task.FromResult(0);
        }
    }
}
