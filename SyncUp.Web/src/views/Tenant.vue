<template>
  <v-container>
    <c-loader-status :loaders="[tenantListViewModel.toggleMembership]" />
    <PageImageBanner
      :title="tenant.name"
      :image-url="tenant.bannerImage?.imageUrl!"
      badge1-text="23 members"
      :badge2-text="numberOfGroups"
      description="This is a fake description that needs love"
      :is-member="isMember"
      @toggle-membership="toggleMembership"
    />

    <c-loader-status :loaders="[groups.$load]">
      <v-row align="center">
        <v-col>
          <v-text-field
            v-model="groups.$params.search"
            label="Search groups"
            prepend-inner-icon="fa fa-search"
            hide-details
            clearable
          />
        </v-col>
        <v-col cols="auto" align="right">
          <AddGroupButton :group="newGroup" @new-group-created="groups.$load" />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
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
      <v-row>
        <v-col>
          <UploadImage v-model="tenant.bannerImage" />
        </v-col>
      </v-row>
    </c-loader-status>
  </v-container>
</template>

<script setup lang="ts">
import {
  GroupListViewModel,
  GroupViewModel,
  TenantListViewModel,
  TenantViewModel,
} from "@/viewmodels.g";

useTitle("Home");

const groups = new GroupListViewModel();
groups.$useAutoLoad();
groups.$load();
groups.$count();

const newGroup = new GroupViewModel();

const { userInfo } = useUser();

const tenantListViewModel = new TenantListViewModel();
const tenant = ref<TenantViewModel>(new TenantViewModel());

async function toggleMembership() {
  await tenantListViewModel.toggleMembership(userInfo.value.tenantId);
  await lookupMembership();
}

const isMember = ref(false);

async function lookupMembership() {
  const tenantListViewModel = new TenantListViewModel();
  await tenantListViewModel.isMemberOf(userInfo.value.tenantId);
  isMember.value = (tenantListViewModel.isMemberOf.result as boolean) ?? false;
}

onMounted(async () => {
  await lookupMembership();
  await tenant.value.$load(userInfo.value.tenantId!);
});

const numberOfGroups = computed(() => {
  return groups.$count.result + " groups";
});
</script>
