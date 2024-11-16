
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
    [Route("api/TenantsService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class TenantsServiceController : BaseApiController
    {
        protected SyncUp.Data.Services.TenantsService Service { get; }

        public TenantsServiceController(CrudContext context, SyncUp.Data.Services.TenantsService service) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<SyncUp.Data.Services.TenantsService>();
            Service = service;
        }

        /// <summary>
        /// Method: LoadTenants
        /// </summary>
        [HttpPost("LoadTenants")]
        [Authorize]
        public virtual ItemResult<System.Collections.Generic.ICollection<TenantResponse>> LoadTenants()
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = Service.LoadTenants();
            var _result = new ItemResult<System.Collections.Generic.ICollection<TenantResponse>>();
            _result.Object = _methodResult?.ToList().Select(o => Mapper.MapToDto<IntelliTect.SyncUp.Data.Models.Tenant, TenantResponse>(o, _mappingContext, includeTree ?? (_methodResult as IQueryable)?.GetIncludeTree())).ToList();
            return _result;
        }

        /// <summary>
        /// Method: SwitchTenant
        /// </summary>
        [HttpPost("SwitchTenant")]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult> SwitchTenant(
            [FromForm(Name = "tenantId")] string tenantId)
        {
            var _params = new
            {
                TenantId = tenantId
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("SwitchTenant"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.SwitchTenant(
                User,
                _params.TenantId
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }

        public class SwitchTenantParameters
        {
            public string TenantId { get; set; }
        }

        /// <summary>
        /// Method: SwitchTenant
        /// </summary>
        [HttpPost("SwitchTenant")]
        [Authorize]
        [Consumes("application/json")]
        public virtual async Task<ItemResult> SwitchTenant(
            [FromBody] SwitchTenantParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("SwitchTenant"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return _validationResult;
            }

            var _methodResult = await Service.SwitchTenant(
                User,
                _params.TenantId
            );
            var _result = new ItemResult(_methodResult);
            return _result;
        }
    }
}
