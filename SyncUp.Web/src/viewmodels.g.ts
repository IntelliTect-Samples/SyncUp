import * as $metadata from './metadata.g'
import * as $models from './models.g'
import * as $apiClients from './api-clients.g'
import { ViewModel, ListViewModel, ViewModelCollection, ServiceViewModel, type DeepPartial, defineProps } from 'coalesce-vue/lib/viewmodel'

export interface AuditLogViewModel extends $models.AuditLog {
  userId: string | null;
  get user(): UserViewModel | null;
  set user(value: UserViewModel | $models.User | null);
  id: number | null;
  type: string | null;
  keyValue: string | null;
  description: string | null;
  state: $models.AuditEntryState | null;
  date: Date | null;
  get properties(): ViewModelCollection<AuditLogPropertyViewModel, $models.AuditLogProperty>;
  set properties(value: (AuditLogPropertyViewModel | $models.AuditLogProperty)[] | null);
  clientIp: string | null;
  referrer: string | null;
  endpoint: string | null;
}
export class AuditLogViewModel extends ViewModel<$models.AuditLog, $apiClients.AuditLogApiClient, number> implements $models.AuditLog  {
  static DataSources = $models.AuditLog.DataSources;
  
  
  public addToProperties(initialData?: DeepPartial<$models.AuditLogProperty> | null) {
    return this.$addChild('properties', initialData) as AuditLogPropertyViewModel
  }
  
  constructor(initialData?: DeepPartial<$models.AuditLog> | null) {
    super($metadata.AuditLog, new $apiClients.AuditLogApiClient(), initialData)
  }
}
defineProps(AuditLogViewModel, $metadata.AuditLog)

export class AuditLogListViewModel extends ListViewModel<$models.AuditLog, $apiClients.AuditLogApiClient, AuditLogViewModel> {
  static DataSources = $models.AuditLog.DataSources;
  
  constructor() {
    super($metadata.AuditLog, new $apiClients.AuditLogApiClient())
  }
}


export interface AuditLogPropertyViewModel extends $models.AuditLogProperty {
  id: number | null;
  parentId: number | null;
  propertyName: string | null;
  oldValue: string | null;
  oldValueDescription: string | null;
  newValue: string | null;
  newValueDescription: string | null;
}
export class AuditLogPropertyViewModel extends ViewModel<$models.AuditLogProperty, $apiClients.AuditLogPropertyApiClient, number> implements $models.AuditLogProperty  {
  
  constructor(initialData?: DeepPartial<$models.AuditLogProperty> | null) {
    super($metadata.AuditLogProperty, new $apiClients.AuditLogPropertyApiClient(), initialData)
  }
}
defineProps(AuditLogPropertyViewModel, $metadata.AuditLogProperty)

export class AuditLogPropertyListViewModel extends ListViewModel<$models.AuditLogProperty, $apiClients.AuditLogPropertyApiClient, AuditLogPropertyViewModel> {
  
  constructor() {
    super($metadata.AuditLogProperty, new $apiClients.AuditLogPropertyApiClient())
  }
}


export interface CommentViewModel extends $models.Comment {
  commentId: number | null;
  body: string | null;
  postId: number | null;
  get post(): PostViewModel | null;
  set post(value: PostViewModel | $models.Post | null);
  get modifiedBy(): UserViewModel | null;
  set modifiedBy(value: UserViewModel | $models.User | null);
  modifiedById: string | null;
  modifiedOn: Date | null;
  get createdBy(): UserViewModel | null;
  set createdBy(value: UserViewModel | $models.User | null);
  createdById: string | null;
  createdOn: Date | null;
}
export class CommentViewModel extends ViewModel<$models.Comment, $apiClients.CommentApiClient, number> implements $models.Comment  {
  
  constructor(initialData?: DeepPartial<$models.Comment> | null) {
    super($metadata.Comment, new $apiClients.CommentApiClient(), initialData)
  }
}
defineProps(CommentViewModel, $metadata.Comment)

export class CommentListViewModel extends ListViewModel<$models.Comment, $apiClients.CommentApiClient, CommentViewModel> {
  
  constructor() {
    super($metadata.Comment, new $apiClients.CommentApiClient())
  }
}


export interface EventViewModel extends $models.Event {
  eventId: number | null;
  name: string | null;
  description: string | null;
  time: Date | null;
  location: string | null;
  groupId: number | null;
  get group(): GroupViewModel | null;
  set group(value: GroupViewModel | $models.Group | null);
  get modifiedBy(): UserViewModel | null;
  set modifiedBy(value: UserViewModel | $models.User | null);
  modifiedById: string | null;
  modifiedOn: Date | null;
  get createdBy(): UserViewModel | null;
  set createdBy(value: UserViewModel | $models.User | null);
  createdById: string | null;
  createdOn: Date | null;
}
export class EventViewModel extends ViewModel<$models.Event, $apiClients.EventApiClient, number> implements $models.Event  {
  
  constructor(initialData?: DeepPartial<$models.Event> | null) {
    super($metadata.Event, new $apiClients.EventApiClient(), initialData)
  }
}
defineProps(EventViewModel, $metadata.Event)

export class EventListViewModel extends ListViewModel<$models.Event, $apiClients.EventApiClient, EventViewModel> {
  
  constructor() {
    super($metadata.Event, new $apiClients.EventApiClient())
  }
}


export interface GroupViewModel extends $models.Group {
  groupId: number | null;
  name: string | null;
  bannerImageId: string | null;
  get bannerImage(): ImageViewModel | null;
  set bannerImage(value: ImageViewModel | $models.Image | null);
  description: string | null;
  get posts(): ViewModelCollection<PostViewModel, $models.Post>;
  set posts(value: (PostViewModel | $models.Post)[] | null);
  get events(): ViewModelCollection<EventViewModel, $models.Event>;
  set events(value: (EventViewModel | $models.Event)[] | null);
  get modifiedBy(): UserViewModel | null;
  set modifiedBy(value: UserViewModel | $models.User | null);
  modifiedById: string | null;
  modifiedOn: Date | null;
  get createdBy(): UserViewModel | null;
  set createdBy(value: UserViewModel | $models.User | null);
  createdById: string | null;
  createdOn: Date | null;
}
export class GroupViewModel extends ViewModel<$models.Group, $apiClients.GroupApiClient, number> implements $models.Group  {
  
  
  public addToPosts(initialData?: DeepPartial<$models.Post> | null) {
    return this.$addChild('posts', initialData) as PostViewModel
  }
  
  
  public addToEvents(initialData?: DeepPartial<$models.Event> | null) {
    return this.$addChild('events', initialData) as EventViewModel
  }
  
  public get checkMembership() {
    const checkMembership = this.$apiClient.$makeCaller(
      this.$metadata.methods.checkMembership,
      (c) => c.checkMembership(this.$primaryKey),
      () => ({}),
      (c, args) => c.checkMembership(this.$primaryKey))
    
    Object.defineProperty(this, 'checkMembership', {value: checkMembership});
    return checkMembership
  }
  
  public get toggleMembership() {
    const toggleMembership = this.$apiClient.$makeCaller(
      this.$metadata.methods.toggleMembership,
      (c) => c.toggleMembership(this.$primaryKey),
      () => ({}),
      (c, args) => c.toggleMembership(this.$primaryKey))
    
    Object.defineProperty(this, 'toggleMembership', {value: toggleMembership});
    return toggleMembership
  }
  
  constructor(initialData?: DeepPartial<$models.Group> | null) {
    super($metadata.Group, new $apiClients.GroupApiClient(), initialData)
  }
}
defineProps(GroupViewModel, $metadata.Group)

export class GroupListViewModel extends ListViewModel<$models.Group, $apiClients.GroupApiClient, GroupViewModel> {
  
  constructor() {
    super($metadata.Group, new $apiClients.GroupApiClient())
  }
}


export interface GroupUserViewModel extends $models.GroupUser {
  groupUserId: number | null;
  isOwner: boolean | null;
  userId: string | null;
  get user(): UserViewModel | null;
  set user(value: UserViewModel | $models.User | null);
  groupId: number | null;
  get group(): GroupViewModel | null;
  set group(value: GroupViewModel | $models.Group | null);
  get modifiedBy(): UserViewModel | null;
  set modifiedBy(value: UserViewModel | $models.User | null);
  modifiedById: string | null;
  modifiedOn: Date | null;
  get createdBy(): UserViewModel | null;
  set createdBy(value: UserViewModel | $models.User | null);
  createdById: string | null;
  createdOn: Date | null;
}
export class GroupUserViewModel extends ViewModel<$models.GroupUser, $apiClients.GroupUserApiClient, number> implements $models.GroupUser  {
  static DataSources = $models.GroupUser.DataSources;
  
  constructor(initialData?: DeepPartial<$models.GroupUser> | null) {
    super($metadata.GroupUser, new $apiClients.GroupUserApiClient(), initialData)
  }
}
defineProps(GroupUserViewModel, $metadata.GroupUser)

export class GroupUserListViewModel extends ListViewModel<$models.GroupUser, $apiClients.GroupUserApiClient, GroupUserViewModel> {
  static DataSources = $models.GroupUser.DataSources;
  
  constructor() {
    super($metadata.GroupUser, new $apiClients.GroupUserApiClient())
  }
}


export interface ImageViewModel extends $models.Image {
  imageId: string | null;
  color: string | null;
  imageUrl: string | null;
  get modifiedBy(): UserViewModel | null;
  set modifiedBy(value: UserViewModel | $models.User | null);
  modifiedById: string | null;
  modifiedOn: Date | null;
  get createdBy(): UserViewModel | null;
  set createdBy(value: UserViewModel | $models.User | null);
  createdById: string | null;
  createdOn: Date | null;
}
export class ImageViewModel extends ViewModel<$models.Image, $apiClients.ImageApiClient, string> implements $models.Image  {
  
  constructor(initialData?: DeepPartial<$models.Image> | null) {
    super($metadata.Image, new $apiClients.ImageApiClient(), initialData)
  }
}
defineProps(ImageViewModel, $metadata.Image)

export class ImageListViewModel extends ListViewModel<$models.Image, $apiClients.ImageApiClient, ImageViewModel> {
  
  constructor() {
    super($metadata.Image, new $apiClients.ImageApiClient())
  }
}


export interface PostViewModel extends $models.Post {
  postId: number | null;
  title: string | null;
  body: string | null;
  groupId: number | null;
  get group(): GroupViewModel | null;
  set group(value: GroupViewModel | $models.Group | null);
  get comments(): ViewModelCollection<CommentViewModel, $models.Comment>;
  set comments(value: (CommentViewModel | $models.Comment)[] | null);
  get modifiedBy(): UserViewModel | null;
  set modifiedBy(value: UserViewModel | $models.User | null);
  modifiedById: string | null;
  modifiedOn: Date | null;
  get createdBy(): UserViewModel | null;
  set createdBy(value: UserViewModel | $models.User | null);
  createdById: string | null;
  createdOn: Date | null;
}
export class PostViewModel extends ViewModel<$models.Post, $apiClients.PostApiClient, number> implements $models.Post  {
  static DataSources = $models.Post.DataSources;
  
  
  public addToComments(initialData?: DeepPartial<$models.Comment> | null) {
    return this.$addChild('comments', initialData) as CommentViewModel
  }
  
  constructor(initialData?: DeepPartial<$models.Post> | null) {
    super($metadata.Post, new $apiClients.PostApiClient(), initialData)
  }
}
defineProps(PostViewModel, $metadata.Post)

export class PostListViewModel extends ListViewModel<$models.Post, $apiClients.PostApiClient, PostViewModel> {
  static DataSources = $models.Post.DataSources;
  
  constructor() {
    super($metadata.Post, new $apiClients.PostApiClient())
  }
}


export interface RoleViewModel extends $models.Role {
  name: string | null;
  permissions: $models.Permission[] | null;
  id: string | null;
}
export class RoleViewModel extends ViewModel<$models.Role, $apiClients.RoleApiClient, string> implements $models.Role  {
  
  constructor(initialData?: DeepPartial<$models.Role> | null) {
    super($metadata.Role, new $apiClients.RoleApiClient(), initialData)
  }
}
defineProps(RoleViewModel, $metadata.Role)

export class RoleListViewModel extends ListViewModel<$models.Role, $apiClients.RoleApiClient, RoleViewModel> {
  
  constructor() {
    super($metadata.Role, new $apiClients.RoleApiClient())
  }
}


export interface TenantViewModel extends $models.Tenant {
  tenantId: string | null;
  name: string | null;
  isPublic: boolean | null;
  bannerImageId: string | null;
  get bannerImage(): ImageViewModel | null;
  set bannerImage(value: ImageViewModel | $models.Image | null);
}
export class TenantViewModel extends ViewModel<$models.Tenant, $apiClients.TenantApiClient, string> implements $models.Tenant  {
  static DataSources = $models.Tenant.DataSources;
  
  constructor(initialData?: DeepPartial<$models.Tenant> | null) {
    super($metadata.Tenant, new $apiClients.TenantApiClient(), initialData)
  }
}
defineProps(TenantViewModel, $metadata.Tenant)

export class TenantListViewModel extends ListViewModel<$models.Tenant, $apiClients.TenantApiClient, TenantViewModel> {
  static DataSources = $models.Tenant.DataSources;
  
  public get create() {
    const create = this.$apiClient.$makeCaller(
      this.$metadata.methods.create,
      (c, name: string | null, adminEmail: string | null) => c.create(name, adminEmail),
      () => ({name: null as string | null, adminEmail: null as string | null, }),
      (c, args) => c.create(args.name, args.adminEmail))
    
    Object.defineProperty(this, 'create', {value: create});
    return create
  }
  
  public get isMemberOf() {
    const isMemberOf = this.$apiClient.$makeCaller(
      this.$metadata.methods.isMemberOf,
      (c, tenantId: string | null) => c.isMemberOf(tenantId),
      () => ({tenantId: null as string | null, }),
      (c, args) => c.isMemberOf(args.tenantId))
    
    Object.defineProperty(this, 'isMemberOf', {value: isMemberOf});
    return isMemberOf
  }
  
  public get toggleMembership() {
    const toggleMembership = this.$apiClient.$makeCaller(
      this.$metadata.methods.toggleMembership,
      (c, tenantId: string | null) => c.toggleMembership(tenantId),
      () => ({tenantId: null as string | null, }),
      (c, args) => c.toggleMembership(args.tenantId))
    
    Object.defineProperty(this, 'toggleMembership', {value: toggleMembership});
    return toggleMembership
  }
  
  constructor() {
    super($metadata.Tenant, new $apiClients.TenantApiClient())
  }
}


export interface UserViewModel extends $models.User {
  fullName: string | null;
  userName: string | null;
  email: string | null;
  emailConfirmed: boolean | null;
  photoHash: string | null;
  get userRoles(): ViewModelCollection<UserRoleViewModel, $models.UserRole>;
  set userRoles(value: (UserRoleViewModel | $models.UserRole)[] | null);
  roleNames: string[] | null;
  
  /** Global admins can perform some administrative actions against ALL tenants. */
  isGlobalAdmin: boolean | null;
  id: string | null;
}
export class UserViewModel extends ViewModel<$models.User, $apiClients.UserApiClient, string> implements $models.User  {
  static DataSources = $models.User.DataSources;
  
  
  public addToUserRoles(initialData?: DeepPartial<$models.UserRole> | null) {
    return this.$addChild('userRoles', initialData) as UserRoleViewModel
  }
  
  get roles(): ReadonlyArray<RoleViewModel> {
    return (this.userRoles || []).map($ => $.role!).filter($ => $)
  }
  
  public get getPhoto() {
    const getPhoto = this.$apiClient.$makeCaller(
      this.$metadata.methods.getPhoto,
      (c) => c.getPhoto(this.$primaryKey),
      () => ({}),
      (c, args) => c.getPhoto(this.$primaryKey))
    
    Object.defineProperty(this, 'getPhoto', {value: getPhoto});
    return getPhoto
  }
  
  public get evict() {
    const evict = this.$apiClient.$makeCaller(
      this.$metadata.methods.evict,
      (c) => c.evict(this.$primaryKey),
      () => ({}),
      (c, args) => c.evict(this.$primaryKey))
    
    Object.defineProperty(this, 'evict', {value: evict});
    return evict
  }
  
  public get setEmail() {
    const setEmail = this.$apiClient.$makeCaller(
      this.$metadata.methods.setEmail,
      (c, newEmail: string | null) => c.setEmail(this.$primaryKey, newEmail),
      () => ({newEmail: null as string | null, }),
      (c, args) => c.setEmail(this.$primaryKey, args.newEmail))
    
    Object.defineProperty(this, 'setEmail', {value: setEmail});
    return setEmail
  }
  
  public get sendEmailConfirmation() {
    const sendEmailConfirmation = this.$apiClient.$makeCaller(
      this.$metadata.methods.sendEmailConfirmation,
      (c) => c.sendEmailConfirmation(this.$primaryKey),
      () => ({}),
      (c, args) => c.sendEmailConfirmation(this.$primaryKey))
    
    Object.defineProperty(this, 'sendEmailConfirmation', {value: sendEmailConfirmation});
    return sendEmailConfirmation
  }
  
  public get setPassword() {
    const setPassword = this.$apiClient.$makeCaller(
      this.$metadata.methods.setPassword,
      (c, currentPassword: string | null, newPassword: string | null, confirmNewPassword: string | null) => c.setPassword(this.$primaryKey, currentPassword, newPassword, confirmNewPassword),
      () => ({currentPassword: null as string | null, newPassword: null as string | null, confirmNewPassword: null as string | null, }),
      (c, args) => c.setPassword(this.$primaryKey, args.currentPassword, args.newPassword, args.confirmNewPassword))
    
    Object.defineProperty(this, 'setPassword', {value: setPassword});
    return setPassword
  }
  
  constructor(initialData?: DeepPartial<$models.User> | null) {
    super($metadata.User, new $apiClients.UserApiClient(), initialData)
  }
}
defineProps(UserViewModel, $metadata.User)

export class UserListViewModel extends ListViewModel<$models.User, $apiClients.UserApiClient, UserViewModel> {
  static DataSources = $models.User.DataSources;
  
  public get inviteUser() {
    const inviteUser = this.$apiClient.$makeCaller(
      this.$metadata.methods.inviteUser,
      (c, email: string | null, role?: $models.Role | null) => c.inviteUser(email, role),
      () => ({email: null as string | null, role: null as $models.Role | null, }),
      (c, args) => c.inviteUser(args.email, args.role))
    
    Object.defineProperty(this, 'inviteUser', {value: inviteUser});
    return inviteUser
  }
  
  constructor() {
    super($metadata.User, new $apiClients.UserApiClient())
  }
}


export interface UserRoleViewModel extends $models.UserRole {
  id: string | null;
  get user(): UserViewModel | null;
  set user(value: UserViewModel | $models.User | null);
  get role(): RoleViewModel | null;
  set role(value: RoleViewModel | $models.Role | null);
  userId: string | null;
  roleId: string | null;
}
export class UserRoleViewModel extends ViewModel<$models.UserRole, $apiClients.UserRoleApiClient, string> implements $models.UserRole  {
  static DataSources = $models.UserRole.DataSources;
  
  constructor(initialData?: DeepPartial<$models.UserRole> | null) {
    super($metadata.UserRole, new $apiClients.UserRoleApiClient(), initialData)
  }
}
defineProps(UserRoleViewModel, $metadata.UserRole)

export class UserRoleListViewModel extends ListViewModel<$models.UserRole, $apiClients.UserRoleApiClient, UserRoleViewModel> {
  static DataSources = $models.UserRole.DataSources;
  
  constructor() {
    super($metadata.UserRole, new $apiClients.UserRoleApiClient())
  }
}


export class ImageServiceViewModel extends ServiceViewModel<typeof $metadata.ImageService, $apiClients.ImageServiceApiClient> {
  
  public get upload() {
    const upload = this.$apiClient.$makeCaller(
      this.$metadata.methods.upload,
      (c, content: string | Uint8Array | null) => c.upload(content),
      () => ({content: null as string | Uint8Array | null, }),
      (c, args) => c.upload(args.content))
    
    Object.defineProperty(this, 'upload', {value: upload});
    return upload
  }
  
  public get uploadFromUrl() {
    const uploadFromUrl = this.$apiClient.$makeCaller(
      this.$metadata.methods.uploadFromUrl,
      (c, url: string | null) => c.uploadFromUrl(url),
      () => ({url: null as string | null, }),
      (c, args) => c.uploadFromUrl(args.url))
    
    Object.defineProperty(this, 'uploadFromUrl', {value: uploadFromUrl});
    return uploadFromUrl
  }
  
  constructor() {
    super($metadata.ImageService, new $apiClients.ImageServiceApiClient())
  }
}


export class SecurityServiceViewModel extends ServiceViewModel<typeof $metadata.SecurityService, $apiClients.SecurityServiceApiClient> {
  
  public get whoAmI() {
    const whoAmI = this.$apiClient.$makeCaller(
      this.$metadata.methods.whoAmI,
      (c) => c.whoAmI(),
      () => ({}),
      (c, args) => c.whoAmI())
    
    Object.defineProperty(this, 'whoAmI', {value: whoAmI});
    return whoAmI
  }
  
  constructor() {
    super($metadata.SecurityService, new $apiClients.SecurityServiceApiClient())
  }
}


const viewModelTypeLookup = ViewModel.typeLookup = {
  AuditLog: AuditLogViewModel,
  AuditLogProperty: AuditLogPropertyViewModel,
  Comment: CommentViewModel,
  Event: EventViewModel,
  Group: GroupViewModel,
  GroupUser: GroupUserViewModel,
  Image: ImageViewModel,
  Post: PostViewModel,
  Role: RoleViewModel,
  Tenant: TenantViewModel,
  User: UserViewModel,
  UserRole: UserRoleViewModel,
}
const listViewModelTypeLookup = ListViewModel.typeLookup = {
  AuditLog: AuditLogListViewModel,
  AuditLogProperty: AuditLogPropertyListViewModel,
  Comment: CommentListViewModel,
  Event: EventListViewModel,
  Group: GroupListViewModel,
  GroupUser: GroupUserListViewModel,
  Image: ImageListViewModel,
  Post: PostListViewModel,
  Role: RoleListViewModel,
  Tenant: TenantListViewModel,
  User: UserListViewModel,
  UserRole: UserRoleListViewModel,
}
const serviceViewModelTypeLookup = ServiceViewModel.typeLookup = {
  ImageService: ImageServiceViewModel,
  SecurityService: SecurityServiceViewModel,
}

