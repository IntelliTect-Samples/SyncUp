using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class ImageParameter : GeneratedParameterDto<SyncUp.Data.Models.Image>
    {
        public ImageParameter() { }

        private string _ImageId;

        public string ImageId
        {
            get => _ImageId;
            set { _ImageId = value; Changed(nameof(ImageId)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(SyncUp.Data.Models.Image entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(ImageId))) entity.ImageId = ImageId;
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override SyncUp.Data.Models.Image MapToNew(IMappingContext context)
        {
            var includes = context.Includes;

            var entity = new SyncUp.Data.Models.Image()
            {
                ImageId = ImageId,
            };

            if (OnUpdate(entity, context)) return entity;

            return entity;
        }
    }

    public partial class ImageResponse : GeneratedResponseDto<SyncUp.Data.Models.Image>
    {
        public ImageResponse() { }

        public string ImageId { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public System.DateTimeOffset? CreatedOn { get; set; }
        public SyncUp.Web.Models.UserResponse ModifiedBy { get; set; }
        public SyncUp.Web.Models.UserResponse CreatedBy { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(SyncUp.Data.Models.Image obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.ImageId = obj.ImageId;
            this.Color = obj.Color;
            this.ImageUrl = obj.ImageUrl;
            this.ModifiedById = obj.ModifiedById;
            this.ModifiedOn = obj.ModifiedOn;
            this.CreatedById = obj.CreatedById;
            this.CreatedOn = obj.CreatedOn;
            if (tree == null || tree[nameof(this.ModifiedBy)] != null)
                this.ModifiedBy = obj.ModifiedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.ModifiedBy)]);

            if (tree == null || tree[nameof(this.CreatedBy)] != null)
                this.CreatedBy = obj.CreatedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.CreatedBy)]);

        }
    }
}
