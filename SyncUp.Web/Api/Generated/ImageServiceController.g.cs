
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
    [Route("api/ImageService")]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class ImageServiceController : BaseApiController
    {
        protected IntelliTect.SyncUp.Data.Services.ImageService Service { get; }

        public ImageServiceController(CrudContext context, IntelliTect.SyncUp.Data.Services.ImageService service) : base(context)
        {
            GeneratedForClassViewModel = context.ReflectionRepository.GetClassViewModel<IntelliTect.SyncUp.Data.Services.ImageService>();
            Service = service;
        }

        /// <summary>
        /// Method: Upload
        /// </summary>
        [HttpPost("Upload")]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult<ImageResponse>> Upload(
            [FromForm(Name = "content")] byte[] content)
        {
            var _params = new
            {
                Content = content ?? await ((await Request.ReadFormAsync()).Files[nameof(content)]?.OpenReadStream().ReadAllBytesAsync(true) ?? Task.FromResult<byte[]>(null))
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("Upload"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return new ItemResult<ImageResponse>(_validationResult);
            }

            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = await Service.Upload(
                _params.Content
            );
            var _result = new ItemResult<ImageResponse>(_methodResult);
            _result.Object = Mapper.MapToDto<SyncUp.Data.Models.Image, ImageResponse>(_methodResult.Object, _mappingContext, includeTree ?? _methodResult.IncludeTree);
            return _result;
        }

        public class UploadParameters
        {
            public byte[] Content { get; set; }
        }

        /// <summary>
        /// Method: Upload
        /// </summary>
        [HttpPost("Upload")]
        [Authorize]
        [Consumes("application/json")]
        public virtual async Task<ItemResult<ImageResponse>> Upload(
            [FromBody] UploadParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("Upload"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return new ItemResult<ImageResponse>(_validationResult);
            }

            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = await Service.Upload(
                _params.Content
            );
            var _result = new ItemResult<ImageResponse>(_methodResult);
            _result.Object = Mapper.MapToDto<SyncUp.Data.Models.Image, ImageResponse>(_methodResult.Object, _mappingContext, includeTree ?? _methodResult.IncludeTree);
            return _result;
        }

        /// <summary>
        /// Method: UploadFromUrl
        /// </summary>
        [HttpPost("UploadFromUrl")]
        [Authorize]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public virtual async Task<ItemResult<ImageResponse>> UploadFromUrl(
            [FromForm(Name = "url")] string url)
        {
            var _params = new
            {
                Url = url
            };

            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("UploadFromUrl"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return new ItemResult<ImageResponse>(_validationResult);
            }

            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = await Service.UploadFromUrl(
                _params.Url
            );
            var _result = new ItemResult<ImageResponse>(_methodResult);
            _result.Object = Mapper.MapToDto<SyncUp.Data.Models.Image, ImageResponse>(_methodResult.Object, _mappingContext, includeTree ?? _methodResult.IncludeTree);
            return _result;
        }

        public class UploadFromUrlParameters
        {
            public string Url { get; set; }
        }

        /// <summary>
        /// Method: UploadFromUrl
        /// </summary>
        [HttpPost("UploadFromUrl")]
        [Authorize]
        [Consumes("application/json")]
        public virtual async Task<ItemResult<ImageResponse>> UploadFromUrl(
            [FromBody] UploadFromUrlParameters _params
        )
        {
            if (Context.Options.ValidateAttributesForMethods)
            {
                var _validationResult = ItemResult.FromParameterValidation(
                    GeneratedForClassViewModel!.MethodByName("UploadFromUrl"), _params, HttpContext.RequestServices);
                if (!_validationResult.WasSuccessful) return new ItemResult<ImageResponse>(_validationResult);
            }

            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(Context);
            var _methodResult = await Service.UploadFromUrl(
                _params.Url
            );
            var _result = new ItemResult<ImageResponse>(_methodResult);
            _result.Object = Mapper.MapToDto<SyncUp.Data.Models.Image, ImageResponse>(_methodResult.Object, _mappingContext, includeTree ?? _methodResult.IncludeTree);
            return _result;
        }
    }
}
