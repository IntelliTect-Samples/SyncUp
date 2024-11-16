import * as metadata from './metadata.g'
import { convertToModel, mapToModel, reactiveDataSource } from 'coalesce-vue/lib/model'
import type { Model, DataSource } from 'coalesce-vue/lib/model'

export enum AuditEntryState {
  EntityAdded = 0,
  EntityDeleted = 1,
  EntityModified = 2,
}


export enum Permission {
  
  /** Modify application configuration and other administrative functions excluding user/role management. */
  Admin = 1,
  
  /** Add and modify users accounts and their assigned roles. Edit roles and their permissions. */
  UserAdmin = 2,
  ViewAuditLogs = 3,
}


export interface AuditLog extends Model<typeof metadata.AuditLog> {
  userId: string | null
  user: User | null
  id: number | null
  type: string | null
  keyValue: string | null
  description: string | null
  state: AuditEntryState | null
  date: Date | null
  properties: AuditLogProperty[] | null
  clientIp: string | null
  referrer: string | null
  endpoint: string | null
}
export class AuditLog {
  
  /** Mutates the input object and its descendents into a valid AuditLog implementation. */
  static convert(data?: Partial<AuditLog>): AuditLog {
    return convertToModel(data || {}, metadata.AuditLog) 
  }
  
  /** Maps the input object and its descendents to a new, valid AuditLog implementation. */
  static map(data?: Partial<AuditLog>): AuditLog {
    return mapToModel(data || {}, metadata.AuditLog) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.AuditLog; }
  
  /** Instantiate a new AuditLog, optionally basing it on the given data. */
  constructor(data?: Partial<AuditLog> | {[k: string]: any}) {
    Object.assign(this, AuditLog.map(data || {}));
  }
}
export namespace AuditLog {
  export namespace DataSources {
    
    export class TenantedDataSource implements DataSource<typeof metadata.AuditLog.dataSources.tenantedDataSource> {
      readonly $metadata = metadata.AuditLog.dataSources.tenantedDataSource
    }
  }
}


export interface AuditLogProperty extends Model<typeof metadata.AuditLogProperty> {
  id: number | null
  parentId: number | null
  propertyName: string | null
  oldValue: string | null
  oldValueDescription: string | null
  newValue: string | null
  newValueDescription: string | null
}
export class AuditLogProperty {
  
  /** Mutates the input object and its descendents into a valid AuditLogProperty implementation. */
  static convert(data?: Partial<AuditLogProperty>): AuditLogProperty {
    return convertToModel(data || {}, metadata.AuditLogProperty) 
  }
  
  /** Maps the input object and its descendents to a new, valid AuditLogProperty implementation. */
  static map(data?: Partial<AuditLogProperty>): AuditLogProperty {
    return mapToModel(data || {}, metadata.AuditLogProperty) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.AuditLogProperty; }
  
  /** Instantiate a new AuditLogProperty, optionally basing it on the given data. */
  constructor(data?: Partial<AuditLogProperty> | {[k: string]: any}) {
    Object.assign(this, AuditLogProperty.map(data || {}));
  }
}


export interface Comment extends Model<typeof metadata.Comment> {
  commentId: number | null
  body: string | null
  postId: number | null
  post: Post | null
  modifiedBy: User | null
  modifiedById: string | null
  modifiedOn: Date | null
  createdBy: User | null
  createdById: string | null
  createdOn: Date | null
}
export class Comment {
  
  /** Mutates the input object and its descendents into a valid Comment implementation. */
  static convert(data?: Partial<Comment>): Comment {
    return convertToModel(data || {}, metadata.Comment) 
  }
  
  /** Maps the input object and its descendents to a new, valid Comment implementation. */
  static map(data?: Partial<Comment>): Comment {
    return mapToModel(data || {}, metadata.Comment) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Comment; }
  
  /** Instantiate a new Comment, optionally basing it on the given data. */
  constructor(data?: Partial<Comment> | {[k: string]: any}) {
    Object.assign(this, Comment.map(data || {}));
  }
}


export interface Event extends Model<typeof metadata.Event> {
  eventId: number | null
  name: string | null
  description: string | null
  time: Date | null
  location: string | null
  groupId: number | null
  group: Group | null
  modifiedBy: User | null
  modifiedById: string | null
  modifiedOn: Date | null
  createdBy: User | null
  createdById: string | null
  createdOn: Date | null
}
export class Event {
  
  /** Mutates the input object and its descendents into a valid Event implementation. */
  static convert(data?: Partial<Event>): Event {
    return convertToModel(data || {}, metadata.Event) 
  }
  
  /** Maps the input object and its descendents to a new, valid Event implementation. */
  static map(data?: Partial<Event>): Event {
    return mapToModel(data || {}, metadata.Event) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Event; }
  
  /** Instantiate a new Event, optionally basing it on the given data. */
  constructor(data?: Partial<Event> | {[k: string]: any}) {
    Object.assign(this, Event.map(data || {}));
  }
}
export namespace Event {
  export namespace DataSources {
    
    export class EventsByDate implements DataSource<typeof metadata.Event.dataSources.eventsByDate> {
      readonly $metadata = metadata.Event.dataSources.eventsByDate
      showPastEvents: boolean | null = null
      userId: string | null = null
      
      constructor(params?: Omit<Partial<EventsByDate>, '$metadata'>) {
        if (params) Object.assign(this, params);
        return reactiveDataSource(this);
      }
    }
  }
}


export interface Group extends Model<typeof metadata.Group> {
  groupId: number | null
  name: string | null
  bannerImageId: string | null
  bannerImage: Image | null
  avatarImageId: string | null
  avatarImage: Image | null
  description: string | null
  posts: Post[] | null
  events: Event[] | null
  modifiedBy: User | null
  modifiedById: string | null
  modifiedOn: Date | null
  createdBy: User | null
  createdById: string | null
  createdOn: Date | null
}
export class Group {
  
  /** Mutates the input object and its descendents into a valid Group implementation. */
  static convert(data?: Partial<Group>): Group {
    return convertToModel(data || {}, metadata.Group) 
  }
  
  /** Maps the input object and its descendents to a new, valid Group implementation. */
  static map(data?: Partial<Group>): Group {
    return mapToModel(data || {}, metadata.Group) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Group; }
  
  /** Instantiate a new Group, optionally basing it on the given data. */
  constructor(data?: Partial<Group> | {[k: string]: any}) {
    Object.assign(this, Group.map(data || {}));
  }
}


export interface GroupUser extends Model<typeof metadata.GroupUser> {
  groupUserId: number | null
  isOwner: boolean | null
  userId: string | null
  user: User | null
  groupId: number | null
  group: Group | null
  modifiedBy: User | null
  modifiedById: string | null
  modifiedOn: Date | null
  createdBy: User | null
  createdById: string | null
  createdOn: Date | null
}
export class GroupUser {
  
  /** Mutates the input object and its descendents into a valid GroupUser implementation. */
  static convert(data?: Partial<GroupUser>): GroupUser {
    return convertToModel(data || {}, metadata.GroupUser) 
  }
  
  /** Maps the input object and its descendents to a new, valid GroupUser implementation. */
  static map(data?: Partial<GroupUser>): GroupUser {
    return mapToModel(data || {}, metadata.GroupUser) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.GroupUser; }
  
  /** Instantiate a new GroupUser, optionally basing it on the given data. */
  constructor(data?: Partial<GroupUser> | {[k: string]: any}) {
    Object.assign(this, GroupUser.map(data || {}));
  }
}
export namespace GroupUser {
  export namespace DataSources {
    
    export class UsersForGroup implements DataSource<typeof metadata.GroupUser.dataSources.usersForGroup> {
      readonly $metadata = metadata.GroupUser.dataSources.usersForGroup
      groupId: number | null = null
      
      constructor(params?: Omit<Partial<UsersForGroup>, '$metadata'>) {
        if (params) Object.assign(this, params);
        return reactiveDataSource(this);
      }
    }
  }
}


export interface Image extends Model<typeof metadata.Image> {
  imageId: string | null
  color: string | null
  imageUrl: string | null
  modifiedBy: User | null
  modifiedById: string | null
  modifiedOn: Date | null
  createdBy: User | null
  createdById: string | null
  createdOn: Date | null
}
export class Image {
  
  /** Mutates the input object and its descendents into a valid Image implementation. */
  static convert(data?: Partial<Image>): Image {
    return convertToModel(data || {}, metadata.Image) 
  }
  
  /** Maps the input object and its descendents to a new, valid Image implementation. */
  static map(data?: Partial<Image>): Image {
    return mapToModel(data || {}, metadata.Image) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Image; }
  
  /** Instantiate a new Image, optionally basing it on the given data. */
  constructor(data?: Partial<Image> | {[k: string]: any}) {
    Object.assign(this, Image.map(data || {}));
  }
}


export interface Post extends Model<typeof metadata.Post> {
  postId: number | null
  title: string | null
  body: string | null
  groupId: number | null
  group: Group | null
  comments: Comment[] | null
  modifiedBy: User | null
  modifiedById: string | null
  modifiedOn: Date | null
  createdBy: User | null
  createdById: string | null
  createdOn: Date | null
}
export class Post {
  
  /** Mutates the input object and its descendents into a valid Post implementation. */
  static convert(data?: Partial<Post>): Post {
    return convertToModel(data || {}, metadata.Post) 
  }
  
  /** Maps the input object and its descendents to a new, valid Post implementation. */
  static map(data?: Partial<Post>): Post {
    return mapToModel(data || {}, metadata.Post) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Post; }
  
  /** Instantiate a new Post, optionally basing it on the given data. */
  constructor(data?: Partial<Post> | {[k: string]: any}) {
    Object.assign(this, Post.map(data || {}));
  }
}
export namespace Post {
  export namespace DataSources {
    
    export class PostsForGroup implements DataSource<typeof metadata.Post.dataSources.postsForGroup> {
      readonly $metadata = metadata.Post.dataSources.postsForGroup
      groupId: number | null = null
      
      constructor(params?: Omit<Partial<PostsForGroup>, '$metadata'>) {
        if (params) Object.assign(this, params);
        return reactiveDataSource(this);
      }
    }
  }
}


export interface Role extends Model<typeof metadata.Role> {
  name: string | null
  permissions: Permission[] | null
  id: string | null
}
export class Role {
  
  /** Mutates the input object and its descendents into a valid Role implementation. */
  static convert(data?: Partial<Role>): Role {
    return convertToModel(data || {}, metadata.Role) 
  }
  
  /** Maps the input object and its descendents to a new, valid Role implementation. */
  static map(data?: Partial<Role>): Role {
    return mapToModel(data || {}, metadata.Role) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Role; }
  
  /** Instantiate a new Role, optionally basing it on the given data. */
  constructor(data?: Partial<Role> | {[k: string]: any}) {
    Object.assign(this, Role.map(data || {}));
  }
}


export interface Tenant extends Model<typeof metadata.Tenant> {
  tenantId: string | null
  name: string | null
  isPublic: boolean | null
  bannerImageId: string | null
  bannerImage: Image | null
}
export class Tenant {
  
  /** Mutates the input object and its descendents into a valid Tenant implementation. */
  static convert(data?: Partial<Tenant>): Tenant {
    return convertToModel(data || {}, metadata.Tenant) 
  }
  
  /** Maps the input object and its descendents to a new, valid Tenant implementation. */
  static map(data?: Partial<Tenant>): Tenant {
    return mapToModel(data || {}, metadata.Tenant) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.Tenant; }
  
  /** Instantiate a new Tenant, optionally basing it on the given data. */
  constructor(data?: Partial<Tenant> | {[k: string]: any}) {
    Object.assign(this, Tenant.map(data || {}));
  }
}
export namespace Tenant {
  export namespace DataSources {
    
    export class DefaultSource implements DataSource<typeof metadata.Tenant.dataSources.defaultSource> {
      readonly $metadata = metadata.Tenant.dataSources.defaultSource
    }
    
    export class GlobalAdminSource implements DataSource<typeof metadata.Tenant.dataSources.globalAdminSource> {
      readonly $metadata = metadata.Tenant.dataSources.globalAdminSource
    }
  }
}


export interface User extends Model<typeof metadata.User> {
  fullName: string | null
  userName: string | null
  email: string | null
  emailConfirmed: boolean | null
  photoHash: string | null
  userRoles: UserRole[] | null
  roleNames: string[] | null
  
  /** Global admins can perform some administrative actions against ALL tenants. */
  isGlobalAdmin: boolean | null
  id: string | null
}
export class User {
  
  /** Mutates the input object and its descendents into a valid User implementation. */
  static convert(data?: Partial<User>): User {
    return convertToModel(data || {}, metadata.User) 
  }
  
  /** Maps the input object and its descendents to a new, valid User implementation. */
  static map(data?: Partial<User>): User {
    return mapToModel(data || {}, metadata.User) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.User; }
  
  /** Instantiate a new User, optionally basing it on the given data. */
  constructor(data?: Partial<User> | {[k: string]: any}) {
    Object.assign(this, User.map(data || {}));
  }
}
export namespace User {
  export namespace DataSources {
    
    export class DefaultSource implements DataSource<typeof metadata.User.dataSources.defaultSource> {
      readonly $metadata = metadata.User.dataSources.defaultSource
    }
  }
}


export interface UserRole extends Model<typeof metadata.UserRole> {
  id: string | null
  user: User | null
  role: Role | null
  userId: string | null
  roleId: string | null
}
export class UserRole {
  
  /** Mutates the input object and its descendents into a valid UserRole implementation. */
  static convert(data?: Partial<UserRole>): UserRole {
    return convertToModel(data || {}, metadata.UserRole) 
  }
  
  /** Maps the input object and its descendents to a new, valid UserRole implementation. */
  static map(data?: Partial<UserRole>): UserRole {
    return mapToModel(data || {}, metadata.UserRole) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.UserRole; }
  
  /** Instantiate a new UserRole, optionally basing it on the given data. */
  constructor(data?: Partial<UserRole> | {[k: string]: any}) {
    Object.assign(this, UserRole.map(data || {}));
  }
}
export namespace UserRole {
  export namespace DataSources {
    
    export class DefaultSource implements DataSource<typeof metadata.UserRole.dataSources.defaultSource> {
      readonly $metadata = metadata.UserRole.dataSources.defaultSource
    }
  }
}


export interface UserInfo extends Model<typeof metadata.UserInfo> {
  id: string | null
  userName: string | null
  email: string | null
  fullName: string | null
  roles: string[] | null
  permissions: string[] | null
  tenantId: string | null
  tenantName: string | null
}
export class UserInfo {
  
  /** Mutates the input object and its descendents into a valid UserInfo implementation. */
  static convert(data?: Partial<UserInfo>): UserInfo {
    return convertToModel(data || {}, metadata.UserInfo) 
  }
  
  /** Maps the input object and its descendents to a new, valid UserInfo implementation. */
  static map(data?: Partial<UserInfo>): UserInfo {
    return mapToModel(data || {}, metadata.UserInfo) 
  }
  
  static [Symbol.hasInstance](x: any) { return x?.$metadata === metadata.UserInfo; }
  
  /** Instantiate a new UserInfo, optionally basing it on the given data. */
  constructor(data?: Partial<UserInfo> | {[k: string]: any}) {
    Object.assign(this, UserInfo.map(data || {}));
  }
}


declare module "coalesce-vue/lib/model" {
  interface EnumTypeLookup {
    AuditEntryState: AuditEntryState
    Permission: Permission
  }
  interface ModelTypeLookup {
    AuditLog: AuditLog
    AuditLogProperty: AuditLogProperty
    Comment: Comment
    Event: Event
    Group: Group
    GroupUser: GroupUser
    Image: Image
    Post: Post
    Role: Role
    Tenant: Tenant
    User: User
    UserInfo: UserInfo
    UserRole: UserRole
  }
}
