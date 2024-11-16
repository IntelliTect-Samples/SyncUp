<template>
  <v-container>
    <c-loader-status :loaders="[tenantListViewModel.toggleMembership]" />
    <PageImageBanner
      :title="userInfo.tenantName"
      image-url="https://wallpapers.com/images/featured/widescreen-3ao0esn9qknhdudj.jpg"
      badge1-text="23 members"
      :badge2-text="numberOfGroups"
      description="This is a fake description that needs love"
      :is-member="isMember"
      @toggle-membership="toggleMembership"
    />

    <v-row class="ma-1">
      <v-col cols="12" md="6" lg="8">
        <AddGroupCard :group="newGroup" />
      </v-col>
      <v-col cols="12" md="6" lg="8">
        <v-row>
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
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { GroupListViewModel, TenantListViewModel, GroupViewModel } from "@/viewmodels.g";

useTitle("Home");
const newGroup = new GroupViewModel();
const groups = new GroupListViewModel();
groups.$useAutoLoad();
groups.$load();
groups.$count();
const { userInfo } = useUser();

const tenantListViewModel = new TenantListViewModel();

async function toggleMembership() {
  await tenantListViewModel.toggleMembership(userInfo.value.tenantId);
  await lookupMembership();
}

const isMember = ref(false);

async function lookupMembership() {
  const tenantListViewModel = new TenantListViewModel();
  await tenantListViewModel.isMemberOf(userInfo.value.tenantId);
  isMember.value = tenantListViewModel.isMemberOf.result ?? false;
}

onMounted(async () => {
  await lookupMembership();
});

const numberOfGroups = computed(() => {
  return groups.$count.result + " groups";
});
</script>
