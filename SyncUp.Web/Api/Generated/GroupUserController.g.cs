
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
    [Route("api/GroupUser")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class GroupUserController
        : BaseApiController<SyncUp.Data.Models.GroupUser, GroupUserParameter, GroupUserResponse, IntelliTect.SyncUp.Data.AppDbContext>
    {
        public GroupUserController(CrudContext<IntelliTect.SyncUp.Data.AppDbContext> context) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<SyncUp.Data.Models.GroupUser>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<GroupUserResponse>> Get(
            long id,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<GroupUserResponse>> List(
            [FromQuery] ListParameters parameters,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            [FromQuery] FilterParameters parameters,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        [Authorize]
        public virtual Task<ItemResult<GroupUserResponse>> Save(
            [FromForm] GroupUserParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource,
            IBehaviors<SyncUp.Data.Models.GroupUser> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("save")]
        [Consumes("application/json")]
        [Authorize]
        public virtual Task<ItemResult<GroupUserResponse>> SaveFromJson(
            [FromBody] GroupUserParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource,
            IBehaviors<SyncUp.Data.Models.GroupUser> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("bulkSave")]
        [Authorize]
        public virtual Task<ItemResult<GroupUserResponse>> BulkSave(
            [FromBody] BulkSaveRequest dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource,
            [FromServices] IDataSourceFactory dataSourceFactory,
            [FromServices] IBehaviorsFactory behaviorsFactory)
            => BulkSaveImplementation(dto, parameters, dataSource, dataSourceFactory, behaviorsFactory);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<GroupUserResponse>> Delete(
            long id,
            IBehaviors<SyncUp.Data.Models.GroupUser> behaviors,
            IDataSource<SyncUp.Data.Models.GroupUser> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
