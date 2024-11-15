
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
    [Route("api/SecurityService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class SecurityServiceController : BaseApiController
    {
        protected IntelliTect.SyncUp.Data.Auth.SecurityService Service { get; }

        public SecurityServiceController(CrudContext context, IntelliTect.SyncUp.Data.Auth.SecurityService service) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<IntelliTect.SyncUp.Data.Auth.SecurityService>();
            Service = service;
        }

        /// <summary>
        /// Method: WhoAmI
        /// </summary>
        [HttpGet("WhoAmI")]
        [Authorize]
        public virtual ItemResult<UserInfoResponse> WhoAmI(
            [FromServices] IntelliTect.SyncUp.Data.AppDbContext db)
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = Service.WhoAmI(
                User,
                db
            );
            var _result = new ItemResult<UserInfoResponse>();
            _result.Object = Mapper.MapToDto<IntelliTect.SyncUp.Data.Auth.UserInfo, UserInfoResponse>(_methodResult, _mappingContext, includeTree);
            return _result;
        }
    }
}
