<template>
  <v-app id="vue-app">
    <v-app-bar color="primary" density="compact">
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title>
        <router-link to="/" style="color: inherit">
          IntelliTect.SyncUp &mdash; {{ $userInfo.tenantName }}
        </router-link>
      </v-toolbar-title>

      <v-menu bottom offset-y>
        <template #activator="{ props }">
          <v-list-item v-bind="props">
            <UserAvatar :user="$userInfo" />
          </v-list-item>
        </template>
        <v-list min-width="300px">
          <v-list-item
            :title="$userInfo.fullName!"
            :subtitle="$userInfo.userName!"
            :to="`/user/${$userInfo.id}`"
          >
            <template #prepend>
              <UserAvatar :user="$userInfo" class="mr-2 ml-n1" />
            </template>
          </v-list-item>
          <v-divider class="mt-1" />

          <v-list-item prepend-icon="fa fa-moon">
            <v-switch
              v-model="theme"
              label="Dark Mode"
              true-value="dark"
              false-value="light"
              hide-details
              class="ml-2"
              density="compact"
            />
          </v-list-item>

          <v-divider />
          <v-list-item
            href="/SelectTenant"
            prepend-icon="fa fa-building"
            title="Switch Organization"
          />
          <v-list-item
            href="/SignOut"
            prepend-icon="fa fa-sign-out"
            title="Log Out"
          />
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer">
      <v-list>
        <v-list-item to="/" prepend-icon="fa fa-home" title="Home" />
        <v-divider></v-divider>
        <v-list-item
          v-if="$can(Permission.UserAdmin)"
          to="/admin/User"
          prepend-icon="fa fa-users"
          title="Users"
        />
        <v-list-item
          v-if="$can(Permission.UserAdmin)"
          to="/admin/Role"
          prepend-icon="fa fa-id-card"
          title="Roles"
        />
        <v-list-item
          v-if="$can(Permission.Admin)"
          to="/admin"
          prepend-icon="fa fa-cogs"
          title="Admin"
        />

        <v-divider></v-divider>
        <v-list-item
          v-if="$userInfo.roles?.includes('GlobalAdmin')"
          to="/admin/Tenant?dataSource=GlobalAdminSource"
          prepend-icon="fa fa-building"
          title="All Tenants"
        />
      </v-list>

      <div
        class="position-absolute left-0 bottom-0 px-1 text-caption text-grey text-center"
      >
        {{ buildDate }}
      </div>
    </v-navigation-drawer>

    <v-main>
      <!-- https://stackoverflow.com/questions/52847979/what-is-router-view-key-route-fullpath -->
      <router-view v-slot="{ Component, route }">
        <transition name="router-transition" mode="out-in" appear>
          <Forbidden
            v-if="isForbidden"
            key="$forbidden"
            :permissions="routeMeta?.permissions"
          />
          <component :is="Component" v-else :key="route.path" />
        </transition>
      </router-view>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { Permission } from "./models.g";
import Forbidden from "./views/errors/Forbidden.vue";
import { useLocalStorage, usePreferredDark } from "@vueuse/core";
import { useTheme } from "vuetify";
import { format } from "date-fns-tz";

const drawer = ref<boolean | null>(null);

const router = useRouter();
const { userInfo } = useUser();
const vuetifyTheme = useTheme();

const theme = useLocalStorage(
  "theme",
  usePreferredDark().value ? "dark" : "light",
);
watch(theme, (v) => (vuetifyTheme.global.name.value = v), { immediate: true });

const routeMeta = computed(() => {
  const route = router.currentRoute.value;
  return route?.meta as
    | {
        permissions?: Permission[];
      }
    | null
    | undefined;
});

const isForbidden = computed(() => {
  if (
    routeMeta.value?.permissions &&
    !userInfo.value?.permissions?.some(
      // @ts-expect-error indexing enum with arbitrary string
      (r) => routeMeta.value?.permissions?.includes(Permission[r as any]),
    )
  ) {
    return true;
  }

  return false;
});

const buildDate = computed(() => {
  if (!BUILD_DATE) return "";
  return "build " + format(BUILD_DATE, "yyyy-MM-dd hh:mm a z");
});
</script>

<style lang="scss">
.router-transition-enter-active,
.router-transition-leave-active {
  transition: 0.1s ease-out;
}

.router-transition-enter-from,
.router-transition-leave-to {
  opacity: 0;
}
</style>
