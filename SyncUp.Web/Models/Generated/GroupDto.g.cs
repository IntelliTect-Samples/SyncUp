using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class GroupParameter : GeneratedParameterDto<IntelliTect.SyncUp.Data.Models.Group>
    {
        public GroupParameter() { }

        private long? _GroupId;
        private string _Name;
        private string _BannerImageId;
        private string _Description;

        public long? GroupId
        {
            get => _GroupId;
            set { _GroupId = value; Changed(nameof(GroupId)); }
        }
        public string Name
        {
            get => _Name;
            set { _Name = value; Changed(nameof(Name)); }
        }
        public string BannerImageId
        {
            get => _BannerImageId;
            set { _BannerImageId = value; Changed(nameof(BannerImageId)); }
        }
        public string Description
        {
            get => _Description;
            set { _Description = value; Changed(nameof(Description)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(IntelliTect.SyncUp.Data.Models.Group entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Name))) entity.Name = Name;
            if (ShouldMapTo(nameof(BannerImageId))) entity.BannerImageId = BannerImageId;
            if (ShouldMapTo(nameof(Description))) entity.Description = Description;
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override IntelliTect.SyncUp.Data.Models.Group MapToNew(IMappingContext context)
        {
            var includes = context.Includes;

            var entity = new IntelliTect.SyncUp.Data.Models.Group()
            {
                GroupId = (GroupId ?? default),
                Name = Name,
                Description = Description,
            };

            if (OnUpdate(entity, context)) return entity;
            if (ShouldMapTo(nameof(BannerImageId))) entity.BannerImageId = BannerImageId;

            return entity;
        }
    }

    public partial class GroupResponse : GeneratedResponseDto<IntelliTect.SyncUp.Data.Models.Group>
    {
        public GroupResponse() { }

        public long? GroupId { get; set; }
        public string Name { get; set; }
        public string BannerImageId { get; set; }
        public string Description { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public System.DateTimeOffset? CreatedOn { get; set; }
        public SyncUp.Web.Models.ImageResponse BannerImage { get; set; }
        public System.Collections.Generic.ICollection<SyncUp.Web.Models.PostResponse> Posts { get; set; }
        public System.Collections.Generic.ICollection<SyncUp.Web.Models.EventResponse> Events { get; set; }
        public SyncUp.Web.Models.UserResponse ModifiedBy { get; set; }
        public SyncUp.Web.Models.UserResponse CreatedBy { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(IntelliTect.SyncUp.Data.Models.Group obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.GroupId = obj.GroupId;
            this.Name = obj.Name;
            this.BannerImageId = obj.BannerImageId;
            this.Description = obj.Description;
            this.ModifiedById = obj.ModifiedById;
            this.ModifiedOn = obj.ModifiedOn;
            this.CreatedById = obj.CreatedById;
            this.CreatedOn = obj.CreatedOn;
            if (tree == null || tree[nameof(this.BannerImage)] != null)
                this.BannerImage = obj.BannerImage.MapToDto<SyncUp.Data.Models.Image, ImageResponse>(context, tree?[nameof(this.BannerImage)]);

            var propValPosts = obj.Posts;
            if (propValPosts != null && (tree == null || tree[nameof(this.Posts)] != null))
            {
                this.Posts = propValPosts
                    .OrderBy(f => f.PostId)
                    .Select(f => f.MapToDto<IntelliTect.SyncUp.Data.Models.Post, PostResponse>(context, tree?[nameof(this.Posts)])).ToList();
            }
            else if (propValPosts == null && tree?[nameof(this.Posts)] != null)
            {
                this.Posts = new PostResponse[0];
            }

            var propValEvents = obj.Events;
            if (propValEvents != null && (tree == null || tree[nameof(this.Events)] != null))
            {
                this.Events = propValEvents
                    .OrderBy(f => f.Name)
                    .Select(f => f.MapToDto<IntelliTect.SyncUp.Data.Models.Event, EventResponse>(context, tree?[nameof(this.Events)])).ToList();
            }
            else if (propValEvents == null && tree?[nameof(this.Events)] != null)
            {
                this.Events = new EventResponse[0];
            }

            if (tree == null || tree[nameof(this.ModifiedBy)] != null)
                this.ModifiedBy = obj.ModifiedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.ModifiedBy)]);

            if (tree == null || tree[nameof(this.CreatedBy)] != null)
                this.CreatedBy = obj.CreatedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.CreatedBy)]);

        }
    }
}
