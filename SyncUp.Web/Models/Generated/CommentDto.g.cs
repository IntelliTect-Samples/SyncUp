using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class CommentParameter : GeneratedParameterDto<IntelliTect.SyncUp.Data.Models.Comment>
    {
        public CommentParameter() { }

        private long? _CommentId;
        private string _Body;
        private long? _PostId;

        public long? CommentId
        {
            get => _CommentId;
            set { _CommentId = value; Changed(nameof(CommentId)); }
        }
        public string Body
        {
            get => _Body;
            set { _Body = value; Changed(nameof(Body)); }
        }
        public long? PostId
        {
            get => _PostId;
            set { _PostId = value; Changed(nameof(PostId)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(IntelliTect.SyncUp.Data.Models.Comment entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Body))) entity.Body = Body;
            if (ShouldMapTo(nameof(PostId))) entity.PostId = (PostId ?? entity.PostId);
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override IntelliTect.SyncUp.Data.Models.Comment MapToNew(IMappingContext context)
        {
            var includes = context.Includes;

            var entity = new IntelliTect.SyncUp.Data.Models.Comment()
            {
                CommentId = (CommentId ?? default),
                Body = Body,
            };

            if (OnUpdate(entity, context)) return entity;
            if (ShouldMapTo(nameof(PostId))) entity.PostId = (PostId ?? entity.PostId);

            return entity;
        }
    }

    public partial class CommentResponse : GeneratedResponseDto<IntelliTect.SyncUp.Data.Models.Comment>
    {
        public CommentResponse() { }

        public long? CommentId { get; set; }
        public string Body { get; set; }
        public long? PostId { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public System.DateTimeOffset? CreatedOn { get; set; }
        public SyncUp.Web.Models.PostResponse Post { get; set; }
        public SyncUp.Web.Models.UserResponse ModifiedBy { get; set; }
        public SyncUp.Web.Models.UserResponse CreatedBy { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(IntelliTect.SyncUp.Data.Models.Comment obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.CommentId = obj.CommentId;
            this.Body = obj.Body;
            this.PostId = obj.PostId;
            this.ModifiedById = obj.ModifiedById;
            this.ModifiedOn = obj.ModifiedOn;
            this.CreatedById = obj.CreatedById;
            this.CreatedOn = obj.CreatedOn;
            if (tree == null || tree[nameof(this.Post)] != null)
                this.Post = obj.Post.MapToDto<IntelliTect.SyncUp.Data.Models.Post, PostResponse>(context, tree?[nameof(this.Post)]);

            if (tree == null || tree[nameof(this.ModifiedBy)] != null)
                this.ModifiedBy = obj.ModifiedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.ModifiedBy)]);

            if (tree == null || tree[nameof(this.CreatedBy)] != null)
                this.CreatedBy = obj.CreatedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.CreatedBy)]);

        }
    }
}
