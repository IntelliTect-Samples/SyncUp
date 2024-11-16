
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
    [Route("api/Image")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class ImageController
        : BaseApiController<SyncUp.Data.Models.Image, ImageParameter, ImageResponse, IntelliTect.SyncUp.Data.AppDbContext>
    {
        public ImageController(CrudContext<IntelliTect.SyncUp.Data.AppDbContext> context) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<SyncUp.Data.Models.Image>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<ImageResponse>> Get(
            string id,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.Image> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<ImageResponse>> List(
            [FromQuery] ListParameters parameters,
            IDataSource<SyncUp.Data.Models.Image> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            [FromQuery] FilterParameters parameters,
            IDataSource<SyncUp.Data.Models.Image> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        [Authorize]
        public virtual Task<ItemResult<ImageResponse>> Save(
            [FromForm] ImageParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.Image> dataSource,
            IBehaviors<SyncUp.Data.Models.Image> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("save")]
        [Consumes("application/json")]
        [Authorize]
        public virtual Task<ItemResult<ImageResponse>> SaveFromJson(
            [FromBody] ImageParameter dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.Image> dataSource,
            IBehaviors<SyncUp.Data.Models.Image> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("bulkSave")]
        [Authorize]
        public virtual Task<ItemResult<ImageResponse>> BulkSave(
            [FromBody] BulkSaveRequest dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SyncUp.Data.Models.Image> dataSource,
            [FromServices] IDataSourceFactory dataSourceFactory,
            [FromServices] IBehaviorsFactory behaviorsFactory)
            => BulkSaveImplementation(dto, parameters, dataSource, dataSourceFactory, behaviorsFactory);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<ImageResponse>> Delete(
            string id,
            IBehaviors<SyncUp.Data.Models.Image> behaviors,
            IDataSource<SyncUp.Data.Models.Image> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
