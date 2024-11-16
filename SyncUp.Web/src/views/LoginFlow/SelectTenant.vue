<template>
  <!--  Tenant Selection/Create Prompt  -->
  <login-flow v-if="!creatingTenant">
    <template #title> Choose Organization </template>
    <c-loader-status
      class="pb-2"
      no-initial-content
      :loaders="signInService.loadTenants"
    >
      <c-loader-status class="pb-2" :loaders="signInService.setTenant" />
      <template v-if="!hasTenants">
        <v-col>
          <v-row>
            <b>{{ userInfo.email }}</b> is a not a member of any organization.
            If you received an invitation, please open the invitation link.
          </v-row>
          <v-divider />
        </v-col>
      </template>
      <template v-else>
        <div
          v-for="tenant in availableTenants"
          :key="`${tenant.name}-${tenant.tenantId}`"
        >
          <v-btn
            variant="elevated"
            color="primary"
            block
            rounded
            :text="tenant.name ?? undefined"
            @click="tenantId = tenant.tenantId"
          >
          </v-btn>
        </div>
      </template>
    </c-loader-status>
    <v-divider class="my-2" />
    <template
      v-if="signInService.loadTenants.wasSuccessful && !hasTenants"
      #actionBar
    >
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        text="Create Organization"
        @click="creatingTenant = true"
      />
    </template>
    <template v-else #actionBar>
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        text="Create Organization"
        @click="selectTenant"
      />
    </template>
    <template #actionSubBar>
      <v-btn
        variant="text"
        color="primary"
        block
        rounded
        text="Sign Out"
        @click="signOut"
      />
    </template>
  </login-flow>
  <!--  Tenant Creation  -->
  <login-flow v-else>
    <template #title> New Organization </template>
    <c-loader-status class="pb-2" :loaders="signInService.setTenant" />
    <v-row class="text-grey"> Organization Name </v-row>
    <v-row>
      <v-text-field v-model="tenantName" class="fill-width" />
    </v-row>
    <template #actionBar>
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        text="Create"
        @click="selectTenant"
      />
    </template>
    <template #actionSubBar>
      <v-btn
        variant="text"
        color="primary"
        block
        rounded
        text="Sign Out"
        @click="signOut"
      />
    </template>
  </login-flow>
</template>

<script setup lang="ts">
import { SignInServiceViewModel } from "@/viewmodels.g";
import LoginFlow from "@/components/LoginFlow.vue";
import { userInfo } from "@/user-service";
import router from "@/router";
import { Tenant } from "@/models.g";

defineProps<{ fromSignOut?: boolean }>();
const signInService = new SignInServiceViewModel();

const hasTenants = ref(false);
const creatingTenant = ref(false);
const availableTenants = ref<Tenant[]>([]);

const tenantName = ref<string | null>(null);
const tenantId = ref<string | null>(null);

signInService.loadTenants().then((res) => {
  hasTenants.value = !!res.length;
  availableTenants.value = res;
});

async function selectTenant() {
  await signInService.setTenant(tenantName.value, tenantId.value);
  if (signInService.setTenant.wasSuccessful) {
    router.push("/");
  }
}

async function signOut() {
  await signInService.signOut();
  router.push("/SignIn");
}
onBeforeRouteLeave(() => {
  document.getElementById("vue-appbar")!.hidden = false;
  document.getElementById("vue-navDrawer")!.hidden = false;
  document
    .getElementsByClassName("v-main")[0]
    .classList.remove("login-flow-background");
});
document.getElementById("vue-appbar")!.hidden = true;
document.getElementById("vue-navDrawer")!.hidden = true;
document
  .getElementsByClassName("v-main")[0]
  .classList.add("login-flow-background");
</script>
