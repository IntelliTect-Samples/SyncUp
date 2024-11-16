using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class GroupUserParameter : GeneratedParameterDto<SyncUp.Data.Models.GroupUser>
    {
        public GroupUserParameter() { }

        private long? _GroupUserId;
        private bool? _IsOwner;
        private string _UserId;
        private long? _GroupId;

        public long? GroupUserId
        {
            get => _GroupUserId;
            set { _GroupUserId = value; Changed(nameof(GroupUserId)); }
        }
        public bool? IsOwner
        {
            get => _IsOwner;
            set { _IsOwner = value; Changed(nameof(IsOwner)); }
        }
        public string UserId
        {
            get => _UserId;
            set { _UserId = value; Changed(nameof(UserId)); }
        }
        public long? GroupId
        {
            get => _GroupId;
            set { _GroupId = value; Changed(nameof(GroupId)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(SyncUp.Data.Models.GroupUser entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(GroupUserId))) entity.GroupUserId = (GroupUserId ?? entity.GroupUserId);
            if (ShouldMapTo(nameof(IsOwner))) entity.IsOwner = (IsOwner ?? entity.IsOwner);
            if (ShouldMapTo(nameof(UserId))) entity.UserId = UserId;
            if (ShouldMapTo(nameof(GroupId))) entity.GroupId = (GroupId ?? entity.GroupId);
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override SyncUp.Data.Models.GroupUser MapToNew(IMappingContext context)
        {
            var entity = new SyncUp.Data.Models.GroupUser();
            MapTo(entity, context);
            return entity;
        }
    }

    public partial class GroupUserResponse : GeneratedResponseDto<SyncUp.Data.Models.GroupUser>
    {
        public GroupUserResponse() { }

        public long? GroupUserId { get; set; }
        public bool? IsOwner { get; set; }
        public string UserId { get; set; }
        public long? GroupId { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public System.DateTimeOffset? CreatedOn { get; set; }
        public SyncUp.Web.Models.UserResponse User { get; set; }
        public SyncUp.Web.Models.GroupResponse Group { get; set; }
        public SyncUp.Web.Models.UserResponse ModifiedBy { get; set; }
        public SyncUp.Web.Models.UserResponse CreatedBy { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(SyncUp.Data.Models.GroupUser obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.GroupUserId = obj.GroupUserId;
            this.IsOwner = obj.IsOwner;
            this.UserId = obj.UserId;
            this.GroupId = obj.GroupId;
            this.ModifiedById = obj.ModifiedById;
            this.ModifiedOn = obj.ModifiedOn;
            this.CreatedById = obj.CreatedById;
            this.CreatedOn = obj.CreatedOn;
            if (tree == null || tree[nameof(this.User)] != null)
                this.User = obj.User.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.User)]);

            if (tree == null || tree[nameof(this.Group)] != null)
                this.Group = obj.Group.MapToDto<IntelliTect.SyncUp.Data.Models.Group, GroupResponse>(context, tree?[nameof(this.Group)]);

            if (tree == null || tree[nameof(this.ModifiedBy)] != null)
                this.ModifiedBy = obj.ModifiedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.ModifiedBy)]);

            if (tree == null || tree[nameof(this.CreatedBy)] != null)
                this.CreatedBy = obj.CreatedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.CreatedBy)]);

        }
    }
}
