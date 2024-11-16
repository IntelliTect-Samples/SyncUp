<template>
  <v-container>
    <c-loader-status :loaders="[tenantListViewModel.toggleMembership]" />
    <PageImageBanner
      :title="userInfo.tenantName"
      image-url="https://wallpapers.com/images/featured/widescreen-3ao0esn9qknhdudj.jpg"
      badge1-text="23 members"
      badge2-text="2047 posts"
      description="This is a fake description that needs love"
      :refresh-flag="refreshFlag"
      @toggle-membership="toggleMembership"
    />
    <v-row class="mt-1">
      <v-col
        v-for="group in groups.$items"
        :key="group.groupId!"
        cols="12"
        md="6"
        lg="4"
      >
        <GroupCard :group="group" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { GroupListViewModel, TenantListViewModel } from "@/viewmodels.g";

useTitle("Home");
const groups = new GroupListViewModel();
groups.$useAutoLoad();
groups.$load();
const { userInfo } = useUser();
const refreshFlag = ref(0);

const tenantListViewModel = new TenantListViewModel();

async function toggleMembership() {
  await tenantListViewModel.toggleMembership(userInfo.value.tenantId);
  refreshFlag.value += 1;
}
</script>
