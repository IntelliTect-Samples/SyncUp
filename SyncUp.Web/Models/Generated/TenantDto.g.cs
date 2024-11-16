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
        private bool? _IsPublic;

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
        public bool? IsPublic
        {
            get => _IsPublic;
            set { _IsPublic = value; Changed(nameof(IsPublic)); }
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
            if (ShouldMapTo(nameof(IsPublic))) entity.IsPublic = (IsPublic ?? entity.IsPublic);
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
            if (ShouldMapTo(nameof(IsPublic))) entity.IsPublic = (IsPublic ?? entity.IsPublic);

            return entity;
        }
    }

    public partial class TenantResponse : GeneratedResponseDto<IntelliTect.SyncUp.Data.Models.Tenant>
    {
        public TenantResponse() { }

        public string TenantId { get; set; }
        public string Name { get; set; }
        public bool? IsPublic { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(IntelliTect.SyncUp.Data.Models.Tenant obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.TenantId = obj.TenantId;
            this.Name = obj.Name;
            this.IsPublic = obj.IsPublic;
        }
    }
}
