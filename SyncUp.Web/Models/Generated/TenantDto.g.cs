using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class TenantParameter : GeneratedParameterDto<IntelliTect.SyncUp.Data.Models.Tenant>
    {
        public TenantParameter() { }

        private string _TenantId;
        private string _Name;
        private string _Description;
        private bool? _IsPublic;
        private string _BannerImageId;

        public string TenantId
        {
            get => _TenantId;
            set { _TenantId = value; Changed(nameof(TenantId)); }
        }
        public string Name
        {
            get => _Name;
            set { _Name = value; Changed(nameof(Name)); }
        }
        public string Description
        {
            get => _Description;
            set { _Description = value; Changed(nameof(Description)); }
        }
        public bool? IsPublic
        {
            get => _IsPublic;
            set { _IsPublic = value; Changed(nameof(IsPublic)); }
        }
        public string BannerImageId
        {
            get => _BannerImageId;
            set { _BannerImageId = value; Changed(nameof(BannerImageId)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(IntelliTect.SyncUp.Data.Models.Tenant entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(TenantId))) entity.TenantId = TenantId;
            if (ShouldMapTo(nameof(Name))) entity.Name = Name;
            if (ShouldMapTo(nameof(Description))) entity.Description = Description;
            if (ShouldMapTo(nameof(IsPublic))) entity.IsPublic = (IsPublic ?? entity.IsPublic);
            if (ShouldMapTo(nameof(BannerImageId))) entity.BannerImageId = BannerImageId;
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override IntelliTect.SyncUp.Data.Models.Tenant MapToNew(IMappingContext context)
        {
            var includes = context.Includes;

            var entity = new IntelliTect.SyncUp.Data.Models.Tenant()
            {
                Name = Name,
            };

            if (OnUpdate(entity, context)) return entity;
            if (ShouldMapTo(nameof(TenantId))) entity.TenantId = TenantId;
            if (ShouldMapTo(nameof(Description))) entity.Description = Description;
            if (ShouldMapTo(nameof(IsPublic))) entity.IsPublic = (IsPublic ?? entity.IsPublic);
            if (ShouldMapTo(nameof(BannerImageId))) entity.BannerImageId = BannerImageId;

            return entity;
        }
    }

    public partial class TenantResponse : GeneratedResponseDto<IntelliTect.SyncUp.Data.Models.Tenant>
    {
        public TenantResponse() { }

        public string TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsPublic { get; set; }
        public string BannerImageId { get; set; }
        public SyncUp.Web.Models.ImageResponse BannerImage { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(IntelliTect.SyncUp.Data.Models.Tenant obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.TenantId = obj.TenantId;
            this.Name = obj.Name;
            this.Description = obj.Description;
            this.IsPublic = obj.IsPublic;
            this.BannerImageId = obj.BannerImageId;
            if (tree == null || tree[nameof(this.BannerImage)] != null)
                this.BannerImage = obj.BannerImage.MapToDto<SyncUp.Data.Models.Image, ImageResponse>(context, tree?[nameof(this.BannerImage)]);

        }
    }
}
