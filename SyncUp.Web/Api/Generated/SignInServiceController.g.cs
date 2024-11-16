
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Api;
using IntelliTect.Coalesce.Api.Behaviors;
using IntelliTect.Coalesce.Api.Controllers;
using IntelliTect.Coalesce.Api.DataSources;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Mapping.IncludeTrees;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.TypeDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncUp.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SyncUp.Web.Api
{
    [Route("api/SignInService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class SignInServiceController : BaseApiController
    {
        protected IntelliTect.SyncUp.Data.Auth.ISignInService Service { get; }

        public SignInServiceController(CrudContext context, IntelliTect.SyncUp.Data.Auth.ISignInService service) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<IntelliTect.SyncUp.Data.Auth.ISignInService>();
            Service = service;
        }

        /// <summary>
        /// Method: SignIn
        /// </summary>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult> SignIn(
            [FromForm(Name = "username")] string username,
            [FromForm(Name = "password")] string password)
        {
            var _params = new
            {
                Username = username,
                Password = password
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("SignIn"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.SignIn(
                _params.Username,
                _params.Password
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        public class SignInParameters
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Method: SignIn
        /// </summary>
        [HttpPost("SignIn")]
        [AllowAnonymous]
        [Consumes("application/json")]
        public virtual async Task<ItemResult> SignIn(
            [FromBody] SignInParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("SignIn"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.SignIn(
                _params.Username,
                _params.Password
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        /// <summary>
        /// Method: SignOut
        /// </summary>
        [HttpPost("SignOut")]
        [Authorize]
        public virtual async Task<ItemResult> SignOut()
        {
            var _methodResult = await Service.SignOut();
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        /// <summary>
        /// Method: ResetPassword
        /// </summary>
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult> ResetPassword(
            [FromForm(Name = "email")] string email,
            [FromForm(Name = "password")] string password,
            [FromForm(Name = "token")] string token)
        {
            var _params = new
            {
                Email = email,
                Password = password,
                Token = token
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("ResetPassword"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.ResetPassword(
                _params.Email,
                _params.Password,
                _params.Token
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        public class ResetPasswordParameters
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }

        /// <summary>
        /// Method: ResetPassword
        /// </summary>
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        [Consumes("application/json")]
        public virtual async Task<ItemResult> ResetPassword(
            [FromBody] ResetPasswordParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("ResetPassword"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.ResetPassword(
                _params.Email,
                _params.Password,
                _params.Token
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        /// <summary>
        /// Method: ForgotPassword
        /// </summary>
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult> ForgotPassword(
            [FromForm(Name = "username")] string username)
        {
            var _params = new
            {
                Username = username
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("ForgotPassword"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.ForgotPassword(
                _params.Username
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        public class ForgotPasswordParameters
        {
            public string Username { get; set; }
        }

        /// <summary>
        /// Method: ForgotPassword
        /// </summary>
        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        [Consumes("application/json")]
        public virtual async Task<ItemResult> ForgotPassword(
            [FromBody] ForgotPasswordParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("ForgotPassword"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.ForgotPassword(
                _params.Username
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        /// <summary>
        /// Method: Register
        /// </summary>
        [HttpPost("Register")]
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult> Register(
            [FromForm(Name = "email")] string email,
            [FromForm(Name = "password")] string password)
        {
            var _params = new
            {
                Email = email,
                Password = password
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("Register"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.Register(
                _params.Email,
                _params.Password
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        public class RegisterParameters
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Method: Register
        /// </summary>
        [HttpPost("Register")]
        [AllowAnonymous]
        [Consumes("application/json")]
        public virtual async Task<ItemResult> Register(
            [FromBody] RegisterParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("Register"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.Register(
                _params.Email,
                _params.Password
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        /// <summary>
        /// Method: LoadTenants
        /// </summary>
        [HttpPost("LoadTenants")]
        [Authorize]
        public virtual async Task<ItemResult<System.Collections.Generic.ICollection<TenantResponse>>> LoadTenants()
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = await Service.LoadTenants(
                User
            );
            var _result = new ItemResult<System.Collections.Generic.ICollection<TenantResponse>>(_methodResult);
            _result.Object = _methodResult.Object?.ToList().Select(o => Mapper.MapToDto<IntelliTect.SyncUp.Data.Models.Tenant, TenantResponse>(o, _mappingContext, includeTree ?? _methodResult.IncludeTree)).ToList();
            return _result;
        }

        /// <summary>
        /// Method: SetTenant
        /// </summary>
        [HttpPost("SetTenant")]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult> SetTenant(
            [FromForm(Name = "tenantId")] string tenantId,
            [FromForm(Name = "tenantName")] string tenantName = default)
        {
            var _params = new
            {
                TenantId = tenantId,
                TenantName = tenantName
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("SetTenant"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.SetTenant(
                User,
                _params.TenantId,
                _params.TenantName
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        public class SetTenantParameters
        {
            public string TenantId { get; set; }
            public string TenantName { get; set; } = default;
        }

        /// <summary>
        /// Method: SetTenant
        /// </summary>
        [HttpPost("SetTenant")]
        [Authorize]
        [Consumes("application/json")]
        public virtual async Task<ItemResult> SetTenant(
            [FromBody] SetTenantParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("SetTenant"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.SetTenant(
                User,
                _params.TenantId,
                _params.TenantName
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }
    }
}
