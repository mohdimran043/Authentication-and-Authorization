using IdentityServer4.Validation;
using IdentityServer4.Models;
using MOI.Patrol.ORM_Auth;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace IdentityServer.IdentityServerExtensions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
             private MOI_ApplicationPermissionContext authContext { get; set; }
        public ResourceOwnerPasswordValidator()
        {
             authContext = new MOI_ApplicationPermissionContext();
        }

        //public async Task<CustomGrantValidationResult> ValidateAsync(string userName, string password, ValidatedTokenRequest request)
        //{
        //    var user = await _myUserManager.FindByNameAsync(userName);
        //    if (user != null && await _myUserManager.CheckPasswordAsync(user, password))
        //    {
        //        return new CustomGrantValidationResult(user.UserName, "password");
        //    }
        //    return new CustomGrantValidationResult("Invalid username or password");
        //}

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            MoiUser m = authContext.MoiUser.FirstOrDefault();
            context.Result = new GrantValidationResult(subject: context.UserName, authenticationMethod: "custom");
            //  context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);
        }
    }
}
