using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SyncUp.Web.Models
{
    public partial class EventParameter : GeneratedParameterDto<IntelliTect.SyncUp.Data.Models.Event>
    {
        public EventParameter() { }

        private long? _EventId;
        private string _Name;
        private string _Description;
        private System.DateTimeOffset? _Time;
        private string _Location;
        private long? _GroupId;

        public long? EventId
        {
            get => _EventId;
            set { _EventId = value; Changed(nameof(EventId)); }
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
        public System.DateTimeOffset? Time
        {
            get => _Time;
            set { _Time = value; Changed(nameof(Time)); }
        }
        public string Location
        {
            get => _Location;
            set { _Location = value; Changed(nameof(Location)); }
        }
        public long? GroupId
        {
            get => _GroupId;
            set { _GroupId = value; Changed(nameof(GroupId)); }
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(IntelliTect.SyncUp.Data.Models.Event entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(Name))) entity.Name = Name;
            if (ShouldMapTo(nameof(Description))) entity.Description = Description;
            if (ShouldMapTo(nameof(Time))) entity.Time = Time;
            if (ShouldMapTo(nameof(Location))) entity.Location = Location;
        }

        /// <summary>
        /// Map from the current DTO instance to a new instance of the domain object.
        /// </summary>
        public override IntelliTect.SyncUp.Data.Models.Event MapToNew(IMappingContext context)
        {
            var includes = context.Includes;

            var entity = new IntelliTect.SyncUp.Data.Models.Event()
            {
                EventId = (EventId ?? default),
                GroupId = (GroupId ?? default),
            };

            if (OnUpdate(entity, context)) return entity;
            if (ShouldMapTo(nameof(Name))) entity.Name = Name;
            if (ShouldMapTo(nameof(Description))) entity.Description = Description;
            if (ShouldMapTo(nameof(Time))) entity.Time = Time;
            if (ShouldMapTo(nameof(Location))) entity.Location = Location;

            return entity;
        }
    }

    public partial class EventResponse : GeneratedResponseDto<IntelliTect.SyncUp.Data.Models.Event>
    {
        public EventResponse() { }

        public long? EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTimeOffset? Time { get; set; }
        public string Location { get; set; }
        public long? GroupId { get; set; }
        public string ModifiedById { get; set; }
        public System.DateTimeOffset? ModifiedOn { get; set; }
        public string CreatedById { get; set; }
        public System.DateTimeOffset? CreatedOn { get; set; }
        public SyncUp.Web.Models.GroupResponse Group { get; set; }
        public SyncUp.Web.Models.UserResponse ModifiedBy { get; set; }
        public SyncUp.Web.Models.UserResponse CreatedBy { get; set; }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(IntelliTect.SyncUp.Data.Models.Event obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            this.EventId = obj.EventId;
            this.Name = obj.Name;
            this.Description = obj.Description;
            this.Time = obj.Time;
            this.Location = obj.Location;
            this.GroupId = obj.GroupId;
            this.ModifiedById = obj.ModifiedById;
            this.ModifiedOn = obj.ModifiedOn;
            this.CreatedById = obj.CreatedById;
            this.CreatedOn = obj.CreatedOn;
            if (tree == null || tree[nameof(this.Group)] != null)
                this.Group = obj.Group.MapToDto<IntelliTect.SyncUp.Data.Models.Group, GroupResponse>(context, tree?[nameof(this.Group)]);

            if (tree == null || tree[nameof(this.ModifiedBy)] != null)
                this.ModifiedBy = obj.ModifiedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.ModifiedBy)]);

            if (tree == null || tree[nameof(this.CreatedBy)] != null)
                this.CreatedBy = obj.CreatedBy.MapToDto<IntelliTect.SyncUp.Data.Models.User, UserResponse>(context, tree?[nameof(this.CreatedBy)]);

        }
    }
}
