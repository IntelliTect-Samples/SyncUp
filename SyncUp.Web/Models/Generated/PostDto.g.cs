using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class PostParameter : GeneratedParameterDto<IntelliTect.SyncUp.Data.Models.Post>
    {
        public PostParameter() { }

        private long? _PostId;
        private string _Title;
        private string _Body;
        private long? _GroupId;
        private int? _LikeCount;

        public long? PostId
        {
            get => _PostId;
            set { _PostId = value; Changed(nameof(PostId)); }
        }
        public string Title
        {
            get => _Title;
            set { _Title = value; Changed(nameof(Title)); }
        }
        public string Body
        {
            get => _Body;
            set { _Body = value; Changed(nameof(Body)); }
        }
        public long? GroupId
        {
            get => _GroupId;
            set { _GroupId = value; Changed(nameof(GroupId)); }
        }
        public int? LikeCount
        {
            get => _LikeCount;
            set { _LikeCount = value; Changed(nameof(LikeCount)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(IntelliTect.SyncUp.Data.Models.Post entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Title))) entity.Title = Title;
            if (ShouldMapTo(nameof(Body))) entity.Body = Body;
            if (ShouldMapTo(nameof(GroupId))) entity.GroupId = (GroupId ?? entity.GroupId);
            if (ShouldMapTo(nameof(LikeCount))) entity.LikeCount = (LikeCount ?? entity.LikeCount);
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override IntelliTect.SyncUp.Data.Models.Post MapToNew(IMappingContext context)
        {
            var includes = context.Includes;

            var entity = new IntelliTect.SyncUp.Data.Models.Post()
            {
                PostId = (PostId ?? default),
                Title = Title,
                Body = Body,
            };

            if (OnUpdate(entity, context)) return entity;
            if (ShouldMapTo(nameof(GroupId))) entity.GroupId = (GroupId ?? entity.GroupId);
            if (ShouldMapTo(nameof(LikeCount))) entity.LikeCount = (LikeCount ?? entity.LikeCount);

            return entity;
        }
    }

    public partial class PostResponse : GeneratedResponseDto<IntelliTect.SyncUp.Data.Models.Post>
    {
        public PostResponse() { }

        public long? PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public long? GroupId { get; set; }
        public int? LikeCount { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public System.DateTimeOffset? CreatedOn { get; set; }
        public SyncUp.Web.Models.GroupResponse Group { get; set; }
        public System.Collections.Generic.ICollection<SyncUp.Web.Models.CommentResponse> Comments { get; set; }
        public SyncUp.Web.Models.UserResponse ModifiedBy { get; set; }
        public SyncUp.Web.Models.UserResponse CreatedBy { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(IntelliTect.SyncUp.Data.Models.Post obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.PostId = obj.PostId;
            this.Title = obj.Title;
            this.Body = obj.Body;
            this.GroupId = obj.GroupId;
            this.LikeCount = obj.LikeCount;
            this.ModifiedById = obj.ModifiedById;
            this.ModifiedOn = obj.ModifiedOn;
            this.CreatedById = obj.CreatedById;
            this.CreatedOn = obj.CreatedOn;
            if (tree == null || tree[nameof(this.Group)] != null)
                this.Group = obj.Group.MapToDto<IntelliTect.SyncUp.Data.Models.Group, GroupResponse>(context, tree?[nameof(this.Group)]);

            var propValComments = obj.Comments;
            if (propValComments != null && (tree == null || tree[nameof(this.Comments)] != null))
            {
                this.Comments = propValComments
                    .OrderBy(f => f.CommentId)
                    .Select(f => f.MapToDto<IntelliTect.SyncUp.Data.Models.Comment, CommentResponse>(context, tree?[nameof(this.Comments)])).ToList();
            }
            else if (propValComments == null && tree?[nameof(this.Comments)] != null)
            {
                this.Comments = new CommentResponse[0];
            }

            if (tree == null || tree[nameof(this.ModifiedBy)] != null)
                this.ModifiedBy = obj.ModifiedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.ModifiedBy)]);

            if (tree == null || tree[nameof(this.CreatedBy)] != null)
                this.CreatedBy = obj.CreatedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.CreatedBy)]);

        }
    }
}
