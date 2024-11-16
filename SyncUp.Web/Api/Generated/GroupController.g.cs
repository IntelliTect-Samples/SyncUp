
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
    [Route("api/Group")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class GroupController
        : BaseApiController<IntelliTect.SyncUp.Data.Models.Group, GroupParameter, GroupResponse, IntelliTect.SyncUp.Data.AppDbContext>
    {
        public GroupController(CrudContext<IntelliTect.SyncUp.Data.AppDbContext> context) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<IntelliTect.SyncUp.Data.Models.Group>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<GroupResponse>> Get(
            long id,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<GroupResponse>> List(
            [FromQuery] ListParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            [FromQuery] FilterParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        [Authorize]
        public virtual Task<ItemResult<GroupResponse>> Save(
            [FromForm] GroupParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource,
            IBehaviors<IntelliTect.SyncUp.Data.Models.Group> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("save")]
        [Consumes("application/json")]
        [Authorize]
        public virtual Task<ItemResult<GroupResponse>> SaveFromJson(
            [FromBody] GroupParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource,
            IBehaviors<IntelliTect.SyncUp.Data.Models.Group> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("bulkSave")]
        [Authorize]
        public virtual Task<ItemResult<GroupResponse>> BulkSave(
            [FromBody] BulkSaveRequest dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource,
            [FromServices] IDataSourceFactory dataSourceFactory,
            [FromServices] IBehaviorsFactory behaviorsFactory)
            => BulkSaveImplementation(dto, parameters, dataSource, dataSourceFactory, behaviorsFactory);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<GroupResponse>> Delete(
            long id,
            IBehaviors<IntelliTect.SyncUp.Data.Models.Group> behaviors,
            IDataSource<IntelliTect.SyncUp.Data.Models.Group> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
