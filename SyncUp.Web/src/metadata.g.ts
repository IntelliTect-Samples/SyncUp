import { getEnumMeta, solidify } from 'coalesce-vue/lib/metadata'
import type {
  Domain, ModelType, ObjectType, HiddenAreas, BehaviorFlags, 
  PrimitiveProperty, ForeignKeyProperty, PrimaryKeyProperty,
  ModelCollectionNavigationProperty, ModelReferenceNavigationProperty
} from 'coalesce-vue/lib/metadata'


const domain: Domain = { enums: {}, types: {}, services: {} }
export const AuditEntryState = domain.enums.AuditEntryState = {
  name: "AuditEntryState" as const,
  displayName: "Audit Entry State",
  type: "enum",
  ...getEnumMeta<"EntityAdded"|"EntityDeleted"|"EntityModified">([
  {
    value: 0,
    strValue: "EntityAdded",
    displayName: "Entity Added",
  },
  {
    value: 1,
    strValue: "EntityDeleted",
    displayName: "Entity Deleted",
  },
  {
    value: 2,
    strValue: "EntityModified",
    displayName: "Entity Modified",
  },
  ]),
}
export const Permission = domain.enums.Permission = {
  name: "Permission" as const,
  displayName: "Permission",
  type: "enum",
  ...getEnumMeta<"Admin"|"UserAdmin"|"ViewAuditLogs">([
  {
    value: 1,
    strValue: "Admin",
    displayName: "Admin - General",
    description: "Modify application configuration and other administrative functions excluding user/role management.",
  },
  {
    value: 2,
    strValue: "UserAdmin",
    displayName: "Admin - Users",
    description: "Add and modify users accounts and their assigned roles. Edit roles and their permissions.",
  },
  {
    value: 3,
    strValue: "ViewAuditLogs",
    displayName: "View Audit Logs",
  },
  ]),
}
export const AuditLog = domain.types.AuditLog = {
  name: "AuditLog" as const,
  displayName: "Audit Log",
  get displayProp() { return this.props.type }, 
  type: "model",
  controllerRoute: "AuditLog",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 0 as BehaviorFlags,
  props: {
    userId: {
      name: "userId",
      displayName: "User Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.AuditLog as ModelType & { name: "AuditLog" }).props.user as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
    },
    user: {
      name: "user",
      displayName: "Changed By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.AuditLog as ModelType & { name: "AuditLog" }).props.userId as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
    type: {
      name: "type",
      displayName: "Type",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Type is required.",
        maxLength: val => !val || val.length <= 100 || "Type may not be more than 100 characters.",
      }
    },
    keyValue: {
      name: "keyValue",
      displayName: "Key Value",
      type: "string",
      role: "value",
    },
    description: {
      name: "description",
      displayName: "Description",
      type: "string",
      role: "value",
    },
    state: {
      name: "state",
      displayName: "Change Type",
      type: "enum",
      get typeDef() { return AuditEntryState },
      role: "value",
    },
    date: {
      name: "date",
      displayName: "Date",
      type: "date",
      dateKind: "datetime",
      role: "value",
    },
    properties: {
      name: "properties",
      displayName: "Properties",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.AuditLogProperty as ModelType & { name: "AuditLogProperty" }) },
      },
      role: "collectionNavigation",
      get foreignKey() { return (domain.types.AuditLogProperty as ModelType & { name: "AuditLogProperty" }).props.parentId as ForeignKeyProperty },
      dontSerialize: true,
    },
    clientIp: {
      name: "clientIp",
      displayName: "Client IP",
      type: "string",
      role: "value",
    },
    referrer: {
      name: "referrer",
      displayName: "Referrer",
      type: "string",
      role: "value",
    },
    endpoint: {
      name: "endpoint",
      displayName: "Endpoint",
      type: "string",
      role: "value",
    },
  },
  methods: {
  },
  dataSources: {
    tenantedDataSource: {
      type: "dataSource",
      name: "TenantedDataSource" as const,
      displayName: "Tenanted Data Source",
      isDefault: true,
      props: {
      },
    },
  },
}
export const AuditLogProperty = domain.types.AuditLogProperty = {
  name: "AuditLogProperty" as const,
  displayName: "Audit Log Property",
  get displayProp() { return this.props.propertyName }, 
  type: "model",
  controllerRoute: "AuditLogProperty",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 0 as BehaviorFlags,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
    parentId: {
      name: "parentId",
      displayName: "Parent Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.AuditLog as ModelType & { name: "AuditLog" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.AuditLog as ModelType & { name: "AuditLog" }) },
      rules: {
        required: val => val != null || "Parent Id is required.",
      }
    },
    propertyName: {
      name: "propertyName",
      displayName: "Property Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Property Name is required.",
        maxLength: val => !val || val.length <= 100 || "Property Name may not be more than 100 characters.",
      }
    },
    oldValue: {
      name: "oldValue",
      displayName: "Old Value",
      type: "string",
      role: "value",
    },
    oldValueDescription: {
      name: "oldValueDescription",
      displayName: "Old Value Description",
      type: "string",
      role: "value",
    },
    newValue: {
      name: "newValue",
      displayName: "New Value",
      type: "string",
      role: "value",
    },
    newValueDescription: {
      name: "newValueDescription",
      displayName: "New Value Description",
      type: "string",
      role: "value",
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const Comment = domain.types.Comment = {
  name: "Comment" as const,
  displayName: "Comment",
  get displayProp() { return this.props.body }, 
  type: "model",
  controllerRoute: "Comment",
  get keyProp() { return this.props.commentId }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    commentId: {
      name: "commentId",
      displayName: "Comment Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
      createOnly: true,
    },
    body: {
      name: "body",
      displayName: "Body",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Body is required.",
      }
    },
    postId: {
      name: "postId",
      displayName: "Post Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.Post as ModelType & { name: "Post" }).props.postId as PrimaryKeyProperty },
      get principalType() { return (domain.types.Post as ModelType & { name: "Post" }) },
      get navigationProp() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.post as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      rules: {
        required: val => val != null || "Post is required.",
      }
    },
    post: {
      name: "post",
      displayName: "Post",
      type: "model",
      get typeDef() { return (domain.types.Post as ModelType & { name: "Post" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.postId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Post as ModelType & { name: "Post" }).props.postId as PrimaryKeyProperty },
      get inverseNavigation() { return (domain.types.Post as ModelType & { name: "Post" }).props.comments as ModelCollectionNavigationProperty },
      dontSerialize: true,
    },
    modifiedById: {
      name: "modifiedById",
      displayName: "Modified By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.modifiedBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdById: {
      name: "createdById",
      displayName: "Created By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.createdBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdBy: {
      name: "createdBy",
      displayName: "Created By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.createdById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    createdOn: {
      name: "createdOn",
      displayName: "Created On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
    modifiedBy: {
      name: "modifiedBy",
      displayName: "Modified By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.modifiedById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedOn: {
      name: "modifiedOn",
      displayName: "Modified On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const Event = domain.types.Event = {
  name: "Event" as const,
  displayName: "Event",
  get displayProp() { return this.props.name }, 
  type: "model",
  controllerRoute: "Event",
  get keyProp() { return this.props.eventId }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    eventId: {
      name: "eventId",
      displayName: "Event Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
      createOnly: true,
    },
    name: {
      name: "name",
      displayName: "Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Name is required.",
        maxLength: val => !val || val.length <= 500 || "Name may not be more than 500 characters.",
      }
    },
    description: {
      name: "description",
      displayName: "Description",
      type: "string",
      role: "value",
    },
    time: {
      name: "time",
      displayName: "Time",
      type: "date",
      dateKind: "datetime",
      role: "value",
    },
    location: {
      name: "location",
      displayName: "Location",
      type: "string",
      role: "value",
    },
    groupId: {
      name: "groupId",
      displayName: "Group Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId as PrimaryKeyProperty },
      get principalType() { return (domain.types.Group as ModelType & { name: "Group" }) },
      get navigationProp() { return (domain.types.Event as ModelType & { name: "Event" }).props.group as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      createOnly: true,
      rules: {
        required: val => val != null || "Group is required.",
      }
    },
    group: {
      name: "group",
      displayName: "Group",
      type: "model",
      get typeDef() { return (domain.types.Group as ModelType & { name: "Group" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Event as ModelType & { name: "Event" }).props.groupId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId as PrimaryKeyProperty },
      get inverseNavigation() { return (domain.types.Group as ModelType & { name: "Group" }).props.events as ModelCollectionNavigationProperty },
      dontSerialize: true,
    },
    modifiedById: {
      name: "modifiedById",
      displayName: "Modified By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Event as ModelType & { name: "Event" }).props.modifiedBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdById: {
      name: "createdById",
      displayName: "Created By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Event as ModelType & { name: "Event" }).props.createdBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdBy: {
      name: "createdBy",
      displayName: "Created By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Event as ModelType & { name: "Event" }).props.createdById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    createdOn: {
      name: "createdOn",
      displayName: "Created On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
    modifiedBy: {
      name: "modifiedBy",
      displayName: "Modified By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Event as ModelType & { name: "Event" }).props.modifiedById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedOn: {
      name: "modifiedOn",
      displayName: "Modified On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
    eventsByDate: {
      type: "dataSource",
      name: "EventsByDate" as const,
      displayName: "Events By Date",
      props: {
        showPastEvents: {
          name: "showPastEvents",
          displayName: "Show Past Events",
          type: "boolean",
          role: "value",
        },
        userId: {
          name: "userId",
          displayName: "User Id",
          type: "string",
          role: "value",
        },
      },
    },
  },
}
export const Group = domain.types.Group = {
  name: "Group" as const,
  displayName: "Group",
  get displayProp() { return this.props.name }, 
  type: "model",
  controllerRoute: "Group",
  get keyProp() { return this.props.groupId }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    groupId: {
      name: "groupId",
      displayName: "Group Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
      createOnly: true,
    },
    name: {
      name: "name",
      displayName: "Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Name is required.",
        maxLength: val => !val || val.length <= 500 || "Name may not be more than 500 characters.",
      }
    },
    bannerImageId: {
      name: "bannerImageId",
      displayName: "Banner Image Id",
      type: "string",
      role: "value",
    },
    bannerImage: {
      name: "bannerImage",
      displayName: "Banner Image",
      type: "model",
      get typeDef() { return (domain.types.Image as ModelType & { name: "Image" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.bannerImageId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Image as ModelType & { name: "Image" }).props.imageId as PrimaryKeyProperty },
      dontSerialize: true,
    },
    description: {
      name: "description",
      displayName: "Description",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Description is required.",
      }
    },
    posts: {
      name: "posts",
      displayName: "Posts",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.Post as ModelType & { name: "Post" }) },
      },
      role: "collectionNavigation",
      get foreignKey() { return (domain.types.Post as ModelType & { name: "Post" }).props.groupId as ForeignKeyProperty },
      get inverseNavigation() { return (domain.types.Post as ModelType & { name: "Post" }).props.group as ModelReferenceNavigationProperty },
      dontSerialize: true,
    },
    events: {
      name: "events",
      displayName: "Events",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.Event as ModelType & { name: "Event" }) },
      },
      role: "collectionNavigation",
      get foreignKey() { return (domain.types.Event as ModelType & { name: "Event" }).props.groupId as ForeignKeyProperty },
      get inverseNavigation() { return (domain.types.Event as ModelType & { name: "Event" }).props.group as ModelReferenceNavigationProperty },
      dontSerialize: true,
    },
    modifiedById: {
      name: "modifiedById",
      displayName: "Modified By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Group as ModelType & { name: "Group" }).props.modifiedBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdById: {
      name: "createdById",
      displayName: "Created By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Group as ModelType & { name: "Group" }).props.createdBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdBy: {
      name: "createdBy",
      displayName: "Created By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.createdById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    createdOn: {
      name: "createdOn",
      displayName: "Created On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
    modifiedBy: {
      name: "modifiedBy",
      displayName: "Modified By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.modifiedById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedOn: {
      name: "modifiedOn",
      displayName: "Modified On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
    checkMembership: {
      name: "checkMembership",
      displayName: "Check Membership",
      transportType: "item",
      httpMethod: "POST",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "number",
          role: "value",
          get source() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId },
          rules: {
            required: val => val != null || "Primary Key is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "boolean",
        role: "value",
      },
    },
    toggleMembership: {
      name: "toggleMembership",
      displayName: "Toggle Membership",
      transportType: "item",
      httpMethod: "POST",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "number",
          role: "value",
          get source() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId },
          rules: {
            required: val => val != null || "Primary Key is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
  },
  dataSources: {
  },
}
export const GroupUser = domain.types.GroupUser = {
  name: "GroupUser" as const,
  displayName: "Group User",
  get displayProp() { return this.props.groupUserId }, 
  type: "model",
  controllerRoute: "GroupUser",
  get keyProp() { return this.props.groupUserId }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    groupUserId: {
      name: "groupUserId",
      displayName: "Group User Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
    isOwner: {
      name: "isOwner",
      displayName: "Is Owner",
      type: "boolean",
      role: "value",
    },
    userId: {
      name: "userId",
      displayName: "User Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.user as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      rules: {
        required: val => (val != null && val !== '') || "User is required.",
      }
    },
    user: {
      name: "user",
      displayName: "User",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.userId as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    groupId: {
      name: "groupId",
      displayName: "Group Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId as PrimaryKeyProperty },
      get principalType() { return (domain.types.Group as ModelType & { name: "Group" }) },
      get navigationProp() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.group as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      rules: {
        required: val => val != null || "Group is required.",
      }
    },
    group: {
      name: "group",
      displayName: "Group",
      type: "model",
      get typeDef() { return (domain.types.Group as ModelType & { name: "Group" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.groupId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedById: {
      name: "modifiedById",
      displayName: "Modified By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.modifiedBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdById: {
      name: "createdById",
      displayName: "Created By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.createdBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdBy: {
      name: "createdBy",
      displayName: "Created By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.createdById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    createdOn: {
      name: "createdOn",
      displayName: "Created On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
    modifiedBy: {
      name: "modifiedBy",
      displayName: "Modified By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.GroupUser as ModelType & { name: "GroupUser" }).props.modifiedById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedOn: {
      name: "modifiedOn",
      displayName: "Modified On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
    usersForGroup: {
      type: "dataSource",
      name: "UsersForGroup" as const,
      displayName: "Users For Group",
      props: {
        groupId: {
          name: "groupId",
          displayName: "Group Id",
          type: "number",
          role: "value",
        },
      },
    },
  },
}
export const Image = domain.types.Image = {
  name: "Image" as const,
  displayName: "Image",
  get displayProp() { return this.props.imageId }, 
  type: "model",
  controllerRoute: "Image",
  get keyProp() { return this.props.imageId }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    imageId: {
      name: "imageId",
      displayName: "Image Id",
      type: "string",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
    color: {
      name: "color",
      displayName: "Color",
      type: "string",
      role: "value",
      dontSerialize: true,
    },
    imageUrl: {
      name: "imageUrl",
      displayName: "Image Url",
      type: "string",
      role: "value",
      dontSerialize: true,
    },
    modifiedById: {
      name: "modifiedById",
      displayName: "Modified By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Image as ModelType & { name: "Image" }).props.modifiedBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdById: {
      name: "createdById",
      displayName: "Created By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Image as ModelType & { name: "Image" }).props.createdBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdBy: {
      name: "createdBy",
      displayName: "Created By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Image as ModelType & { name: "Image" }).props.createdById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    createdOn: {
      name: "createdOn",
      displayName: "Created On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
    modifiedBy: {
      name: "modifiedBy",
      displayName: "Modified By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Image as ModelType & { name: "Image" }).props.modifiedById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedOn: {
      name: "modifiedOn",
      displayName: "Modified On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const Post = domain.types.Post = {
  name: "Post" as const,
  displayName: "Post",
  get displayProp() { return this.props.title }, 
  type: "model",
  controllerRoute: "Post",
  get keyProp() { return this.props.postId }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    postId: {
      name: "postId",
      displayName: "Post Id",
      type: "number",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
      createOnly: true,
    },
    title: {
      name: "title",
      displayName: "Title",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Title is required.",
        maxLength: val => !val || val.length <= 500 || "Title may not be more than 500 characters.",
      }
    },
    body: {
      name: "body",
      displayName: "Body",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Body is required.",
      }
    },
    groupId: {
      name: "groupId",
      displayName: "Group Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId as PrimaryKeyProperty },
      get principalType() { return (domain.types.Group as ModelType & { name: "Group" }) },
      get navigationProp() { return (domain.types.Post as ModelType & { name: "Post" }).props.group as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      rules: {
        required: val => val != null || "Group is required.",
      }
    },
    group: {
      name: "group",
      displayName: "Group",
      type: "model",
      get typeDef() { return (domain.types.Group as ModelType & { name: "Group" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Post as ModelType & { name: "Post" }).props.groupId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Group as ModelType & { name: "Group" }).props.groupId as PrimaryKeyProperty },
      get inverseNavigation() { return (domain.types.Group as ModelType & { name: "Group" }).props.posts as ModelCollectionNavigationProperty },
      dontSerialize: true,
    },
    comments: {
      name: "comments",
      displayName: "Comments",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.Comment as ModelType & { name: "Comment" }) },
      },
      role: "collectionNavigation",
      get foreignKey() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.postId as ForeignKeyProperty },
      get inverseNavigation() { return (domain.types.Comment as ModelType & { name: "Comment" }).props.post as ModelReferenceNavigationProperty },
      dontSerialize: true,
    },
    modifiedById: {
      name: "modifiedById",
      displayName: "Modified By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Post as ModelType & { name: "Post" }).props.modifiedBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdById: {
      name: "createdById",
      displayName: "Created By Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.Post as ModelType & { name: "Post" }).props.createdBy as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    createdBy: {
      name: "createdBy",
      displayName: "Created By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Post as ModelType & { name: "Post" }).props.createdById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    createdOn: {
      name: "createdOn",
      displayName: "Created On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
    modifiedBy: {
      name: "modifiedBy",
      displayName: "Modified By",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Post as ModelType & { name: "Post" }).props.modifiedById as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    modifiedOn: {
      name: "modifiedOn",
      displayName: "Modified On",
      type: "date",
      dateKind: "datetime",
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
    postsForGroup: {
      type: "dataSource",
      name: "PostsForGroup" as const,
      displayName: "Posts For Group",
      isDefault: true,
      props: {
        groupId: {
          name: "groupId",
          displayName: "Group Id",
          type: "number",
          role: "value",
        },
      },
    },
  },
}
export const Role = domain.types.Role = {
  name: "Role" as const,
  displayName: "Role",
  description: "Roles are groups of permissions, analogous to job titles or functions.",
  get displayProp() { return this.props.name }, 
  type: "model",
  controllerRoute: "Role",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 7 as BehaviorFlags,
  props: {
    name: {
      name: "name",
      displayName: "Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Name is required.",
      }
    },
    permissions: {
      name: "permissions",
      displayName: "Permissions",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "enum",
        get typeDef() { return Permission },
      },
      role: "value",
    },
    id: {
      name: "id",
      displayName: "Id",
      type: "string",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const Tenant = domain.types.Tenant = {
  name: "Tenant" as const,
  displayName: "Organization",
  get displayProp() { return this.props.name }, 
  type: "model",
  controllerRoute: "Tenant",
  get keyProp() { return this.props.tenantId }, 
  behaviorFlags: 2 as BehaviorFlags,
  props: {
    tenantId: {
      name: "tenantId",
      displayName: "Tenant Id",
      type: "string",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
    name: {
      name: "name",
      displayName: "Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Name is required.",
      }
    },
    isPublic: {
      name: "isPublic",
      displayName: "Is Public",
      type: "boolean",
      role: "value",
    },
    bannerImageId: {
      name: "bannerImageId",
      displayName: "Banner Image Id",
      type: "string",
      role: "value",
    },
    bannerImage: {
      name: "bannerImage",
      displayName: "Banner Image",
      type: "model",
      get typeDef() { return (domain.types.Image as ModelType & { name: "Image" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Tenant as ModelType & { name: "Tenant" }).props.bannerImageId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Image as ModelType & { name: "Image" }).props.imageId as PrimaryKeyProperty },
      dontSerialize: true,
    },
  },
  methods: {
    create: {
      name: "create",
      displayName: "Create",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
        name: {
          name: "name",
          displayName: "Org Name",
          type: "string",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Org Name is required.",
          }
        },
        adminEmail: {
          name: "adminEmail",
          displayName: "Admin Email",
          type: "string",
          subtype: "email",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Admin Email is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
    isMemberOf: {
      name: "isMemberOf",
      displayName: "Is Member Of",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
        tenantId: {
          name: "tenantId",
          displayName: "Tenant Id",
          type: "string",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Tenant Id is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "boolean",
        role: "value",
      },
    },
    toggleMembership: {
      name: "toggleMembership",
      displayName: "Toggle Membership",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
        tenantId: {
          name: "tenantId",
          displayName: "Tenant Id",
          type: "string",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Tenant Id is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
  },
  dataSources: {
    defaultSource: {
      type: "dataSource",
      name: "DefaultSource" as const,
      displayName: "Default Source",
      isDefault: true,
      props: {
      },
    },
    globalAdminSource: {
      type: "dataSource",
      name: "GlobalAdminSource" as const,
      displayName: "Global Admin Source",
      props: {
      },
    },
  },
}
export const User = domain.types.User = {
  name: "User" as const,
  displayName: "User",
  description: "A user profile within the application.",
  get displayProp() { return this.props.fullName }, 
  type: "model",
  controllerRoute: "User",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 2 as BehaviorFlags,
  props: {
    fullName: {
      name: "fullName",
      displayName: "Full Name",
      type: "string",
      role: "value",
    },
    userName: {
      name: "userName",
      displayName: "User Name",
      type: "string",
      role: "value",
    },
    email: {
      name: "email",
      displayName: "Email",
      type: "string",
      role: "value",
      dontSerialize: true,
    },
    emailConfirmed: {
      name: "emailConfirmed",
      displayName: "Email Confirmed",
      type: "boolean",
      role: "value",
      dontSerialize: true,
    },
    photoHash: {
      name: "photoHash",
      displayName: "Photo Hash",
      type: "binary",
      base64: true,
      role: "value",
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    userRoles: {
      name: "userRoles",
      displayName: "User Roles",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.UserRole as ModelType & { name: "UserRole" }) },
      },
      role: "collectionNavigation",
      get foreignKey() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.userId as ForeignKeyProperty },
      get inverseNavigation() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.user as ModelReferenceNavigationProperty },
      manyToMany: {
        name: "roles",
        displayName: "Roles",
        get typeDef() { return (domain.types.Role as ModelType & { name: "Role" }) },
        get farForeignKey() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.roleId as ForeignKeyProperty },
        get farNavigationProp() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.role as ModelReferenceNavigationProperty },
        get nearForeignKey() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.userId as ForeignKeyProperty },
        get nearNavigationProp() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.user as ModelReferenceNavigationProperty },
      },
      hidden: 3 as HiddenAreas,
      dontSerialize: true,
    },
    roleNames: {
      name: "roleNames",
      displayName: "Roles",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "string",
      },
      role: "value",
      dontSerialize: true,
    },
    isGlobalAdmin: {
      name: "isGlobalAdmin",
      displayName: "Is Global Admin",
      description: "Global admins can perform some administrative actions against ALL tenants.",
      type: "boolean",
      role: "value",
      hidden: 3 as HiddenAreas,
    },
    id: {
      name: "id",
      displayName: "Id",
      type: "string",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
  },
  methods: {
    getPhoto: {
      name: "getPhoto",
      displayName: "Get Photo",
      transportType: "item",
      httpMethod: "GET",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "string",
          role: "value",
          get source() { return (domain.types.User as ModelType & { name: "User" }).props.id },
          rules: {
            required: val => (val != null && val !== '') || "Primary Key is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "file",
        role: "value",
      },
    },
    evict: {
      name: "evict",
      displayName: "Evict",
      transportType: "item",
      httpMethod: "POST",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "string",
          role: "value",
          get source() { return (domain.types.User as ModelType & { name: "User" }).props.id },
          rules: {
            required: val => (val != null && val !== '') || "Primary Key is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
    inviteUser: {
      name: "inviteUser",
      displayName: "Invite User",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
        email: {
          name: "email",
          displayName: "Email",
          type: "string",
          subtype: "email",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Email is required.",
          }
        },
        role: {
          name: "role",
          displayName: "Role",
          type: "model",
          get typeDef() { return (domain.types.Role as ModelType & { name: "Role" }) },
          role: "value",
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
    setEmail: {
      name: "setEmail",
      displayName: "Set Email",
      transportType: "item",
      httpMethod: "POST",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "string",
          role: "value",
          get source() { return (domain.types.User as ModelType & { name: "User" }).props.id },
          rules: {
            required: val => (val != null && val !== '') || "Primary Key is required.",
          }
        },
        newEmail: {
          name: "newEmail",
          displayName: "New Email",
          type: "string",
          subtype: "email",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "New Email is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
    sendEmailConfirmation: {
      name: "sendEmailConfirmation",
      displayName: "Send Email Confirmation",
      transportType: "item",
      httpMethod: "POST",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "string",
          role: "value",
          get source() { return (domain.types.User as ModelType & { name: "User" }).props.id },
          rules: {
            required: val => (val != null && val !== '') || "Primary Key is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
    setPassword: {
      name: "setPassword",
      displayName: "Set Password",
      transportType: "item",
      httpMethod: "POST",
      params: {
        id: {
          name: "id",
          displayName: "Primary Key",
          type: "string",
          role: "value",
          get source() { return (domain.types.User as ModelType & { name: "User" }).props.id },
          rules: {
            required: val => (val != null && val !== '') || "Primary Key is required.",
          }
        },
        currentPassword: {
          name: "currentPassword",
          displayName: "Current Password",
          type: "string",
          subtype: "password",
          role: "value",
        },
        newPassword: {
          name: "newPassword",
          displayName: "New Password",
          type: "string",
          subtype: "password",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "New Password is required.",
          }
        },
        confirmNewPassword: {
          name: "confirmNewPassword",
          displayName: "Confirm New Password",
          type: "string",
          subtype: "password",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Confirm New Password is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
  },
  dataSources: {
    defaultSource: {
      type: "dataSource",
      name: "DefaultSource" as const,
      displayName: "Default Source",
      isDefault: true,
      props: {
      },
    },
  },
}
export const UserRole = domain.types.UserRole = {
  name: "UserRole" as const,
  displayName: "User Role",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "UserRole",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 5 as BehaviorFlags,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "string",
      role: "primaryKey",
      hidden: 3 as HiddenAreas,
    },
    user: {
      name: "user",
      displayName: "User",
      type: "model",
      get typeDef() { return (domain.types.User as ModelType & { name: "User" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.userId as ForeignKeyProperty },
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get inverseNavigation() { return (domain.types.User as ModelType & { name: "User" }).props.userRoles as ModelCollectionNavigationProperty },
      dontSerialize: true,
    },
    role: {
      name: "role",
      displayName: "Role",
      type: "model",
      get typeDef() { return (domain.types.Role as ModelType & { name: "Role" }) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.roleId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Role as ModelType & { name: "Role" }).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    userId: {
      name: "userId",
      displayName: "User Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.User as ModelType & { name: "User" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.User as ModelType & { name: "User" }) },
      get navigationProp() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.user as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      rules: {
        required: val => (val != null && val !== '') || "User is required.",
      }
    },
    roleId: {
      name: "roleId",
      displayName: "Role Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.Role as ModelType & { name: "Role" }).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.Role as ModelType & { name: "Role" }) },
      get navigationProp() { return (domain.types.UserRole as ModelType & { name: "UserRole" }).props.role as ModelReferenceNavigationProperty },
      hidden: 3 as HiddenAreas,
      rules: {
        required: val => (val != null && val !== '') || "Role is required.",
      }
    },
  },
  methods: {
  },
  dataSources: {
    defaultSource: {
      type: "dataSource",
      name: "DefaultSource" as const,
      displayName: "Default Source",
      isDefault: true,
      props: {
      },
    },
  },
}
export const UserInfo = domain.types.UserInfo = {
  name: "UserInfo" as const,
  displayName: "User Info",
  type: "object",
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "string",
      role: "value",
    },
    userName: {
      name: "userName",
      displayName: "User Name",
      type: "string",
      role: "value",
    },
    email: {
      name: "email",
      displayName: "Email",
      type: "string",
      role: "value",
    },
    fullName: {
      name: "fullName",
      displayName: "Full Name",
      type: "string",
      role: "value",
    },
    roles: {
      name: "roles",
      displayName: "Roles",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "string",
      },
      role: "value",
    },
    permissions: {
      name: "permissions",
      displayName: "Permissions",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "string",
      },
      role: "value",
    },
    tenantId: {
      name: "tenantId",
      displayName: "Tenant Id",
      type: "string",
      role: "value",
      rules: {
        maxLength: val => !val || val.length <= 36 || "Tenant Id may not be more than 36 characters.",
      }
    },
    tenantName: {
      name: "tenantName",
      displayName: "Tenant Name",
      type: "string",
      role: "value",
    },
  },
}
export const ImageService = domain.services.ImageService = {
  name: "ImageService",
  displayName: "Image Service",
  type: "service",
  controllerRoute: "ImageService",
  methods: {
    upload: {
      name: "upload",
      displayName: "Upload",
      transportType: "item",
      httpMethod: "POST",
      params: {
        content: {
          name: "content",
          displayName: "Content",
          type: "binary",
          role: "value",
          rules: {
            required: val => val != null || "Content is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "model",
        get typeDef() { return (domain.types.Image as ModelType & { name: "Image" }) },
        role: "value",
      },
    },
    uploadFromUrl: {
      name: "uploadFromUrl",
      displayName: "Upload From Url",
      transportType: "item",
      httpMethod: "POST",
      params: {
        url: {
          name: "url",
          displayName: "Url",
          type: "string",
          role: "value",
          rules: {
            required: val => (val != null && val !== '') || "Url is required.",
          }
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "model",
        get typeDef() { return (domain.types.Image as ModelType & { name: "Image" }) },
        role: "value",
      },
    },
  },
}
export const SecurityService = domain.services.SecurityService = {
  name: "SecurityService",
  displayName: "Security Service",
  type: "service",
  controllerRoute: "SecurityService",
  methods: {
    whoAmI: {
      name: "whoAmI",
      displayName: "Who AmI",
      transportType: "item",
      httpMethod: "GET",
      params: {
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "object",
        get typeDef() { return (domain.types.UserInfo as ObjectType & { name: "UserInfo" }) },
        role: "value",
      },
    },
  },
}

interface AppDomain extends Domain {
  enums: {
    AuditEntryState: typeof AuditEntryState
    Permission: typeof Permission
  }
  types: {
    AuditLog: typeof AuditLog
    AuditLogProperty: typeof AuditLogProperty
    Comment: typeof Comment
    Event: typeof Event
    Group: typeof Group
    GroupUser: typeof GroupUser
    Image: typeof Image
    Post: typeof Post
    Role: typeof Role
    Tenant: typeof Tenant
    User: typeof User
    UserInfo: typeof UserInfo
    UserRole: typeof UserRole
  }
  services: {
    ImageService: typeof ImageService
    SecurityService: typeof SecurityService
  }
}

solidify(domain)

export default domain as unknown as AppDomain
