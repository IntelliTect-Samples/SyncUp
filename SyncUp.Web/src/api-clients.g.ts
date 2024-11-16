import * as $metadata from './metadata.g'
import * as $models from './models.g'
import { ModelApiClient, ServiceApiClient } from 'coalesce-vue/lib/api-client'
import type { AxiosPromise, AxiosRequestConfig, ItemResult, ListResult } from 'coalesce-vue/lib/api-client'

export class AuditLogApiClient extends ModelApiClient<$models.AuditLog> {
  constructor() { super($metadata.AuditLog) }
}


export class AuditLogPropertyApiClient extends ModelApiClient<$models.AuditLogProperty> {
  constructor() { super($metadata.AuditLogProperty) }
}


export class CommentApiClient extends ModelApiClient<$models.Comment> {
  constructor() { super($metadata.Comment) }
}


export class EventApiClient extends ModelApiClient<$models.Event> {
  constructor() { super($metadata.Event) }
}


export class GroupApiClient extends ModelApiClient<$models.Group> {
  constructor() { super($metadata.Group) }
  public checkMembership(id: number | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<boolean>> {
    const $method = this.$metadata.methods.checkMembership
    const $params =  {
      id,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public toggleMembership(id: number | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.toggleMembership
    const $params =  {
      id,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class GroupUserApiClient extends ModelApiClient<$models.GroupUser> {
  constructor() { super($metadata.GroupUser) }
}


export class PostApiClient extends ModelApiClient<$models.Post> {
  constructor() { super($metadata.Post) }
}


export class RoleApiClient extends ModelApiClient<$models.Role> {
  constructor() { super($metadata.Role) }
}


export class TenantApiClient extends ModelApiClient<$models.Tenant> {
  constructor() { super($metadata.Tenant) }
  public create(name: string | null, adminEmail: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.create
    const $params =  {
      name,
      adminEmail,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public isMemberOf(tenantId: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<boolean>> {
    const $method = this.$metadata.methods.isMemberOf
    const $params =  {
      tenantId,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public toggleMembership(tenantId: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.toggleMembership
    const $params =  {
      tenantId,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class UserApiClient extends ModelApiClient<$models.User> {
  constructor() { super($metadata.User) }
  public getPhoto(id: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<File>> {
    const $method = this.$metadata.methods.getPhoto
    const $params =  {
      id,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public evict(id: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.evict
    const $params =  {
      id,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public inviteUser(email: string | null, role?: $models.Role | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.inviteUser
    const $params =  {
      email,
      role,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public setEmail(id: string | null, newEmail: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.setEmail
    const $params =  {
      id,
      newEmail,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public sendEmailConfirmation(id: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.sendEmailConfirmation
    const $params =  {
      id,
    }
    return this.$invoke($method, $params, $config)
  }
  
  public setPassword(id: string | null, currentPassword: string | null, newPassword: string | null, confirmNewPassword: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.setPassword
    const $params =  {
      id,
      currentPassword,
      newPassword,
      confirmNewPassword,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class UserRoleApiClient extends ModelApiClient<$models.UserRole> {
  constructor() { super($metadata.UserRole) }
}


export class SecurityServiceApiClient extends ServiceApiClient<typeof $metadata.SecurityService> {
  constructor() { super($metadata.SecurityService) }
  public whoAmI($config?: AxiosRequestConfig): AxiosPromise<ItemResult<$models.UserInfo>> {
    const $method = this.$metadata.methods.whoAmI
    const $params =  {
    }
    return this.$invoke($method, $params, $config)
  }
  
}


