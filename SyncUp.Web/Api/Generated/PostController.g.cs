
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
    [Route("api/Post")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class PostController
        : BaseApiController<IntelliTect.SyncUp.Data.Models.Post, PostParameter, PostResponse, IntelliTect.SyncUp.Data.AppDbContext>
    {
        public PostController(CrudContext<IntelliTect.SyncUp.Data.AppDbContext> context) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<IntelliTect.SyncUp.Data.Models.Post>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<PostResponse>> Get(
            long id,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<PostResponse>> List(
            [FromQuery] ListParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            [FromQuery] FilterParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        [Authorize]
        public virtual Task<ItemResult<PostResponse>> Save(
            [FromForm] PostParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource,
            IBehaviors<IntelliTect.SyncUp.Data.Models.Post> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("save")]
        [Consumes("application/json")]
        [Authorize]
        public virtual Task<ItemResult<PostResponse>> SaveFromJson(
            [FromBody] PostParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource,
            IBehaviors<IntelliTect.SyncUp.Data.Models.Post> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("bulkSave")]
        [Authorize]
        public virtual Task<ItemResult<PostResponse>> BulkSave(
            [FromBody] BulkSaveRequest dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource,
            [FromServices] IDataSourceFactory dataSourceFactory,
            [FromServices] IBehaviorsFactory behaviorsFactory)
            => BulkSaveImplementation(dto, parameters, dataSource, dataSourceFactory, behaviorsFactory);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<PostResponse>> Delete(
            long id,
            IBehaviors<IntelliTect.SyncUp.Data.Models.Post> behaviors,
            IDataSource<IntelliTect.SyncUp.Data.Models.Post> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
