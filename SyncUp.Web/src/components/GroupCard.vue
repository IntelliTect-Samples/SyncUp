<template>
  <v-card height="100%" class="d-flex flex-column">
    <!-- Top content (v-row, first divider, and v-card-text) -->
    <div>
      <v-row
        no-gutters
        align="center"
        class="linked-title"
        @click="router.push(`/group/${group.groupId}`)"
      >
        <v-col cols="auto">
          <v-avatar size="60" color="primary" class="mt-1 ml-1" />
        </v-col>
        <v-col cols="auto">
          <v-card-title>
            {{ group.name }}
          </v-card-title>
          <v-chip size="x-small" color="primary" class="ml-3 mt-n4">
            {{ groupService.numberOfPosts }}
          </v-chip>
          <v-chip size="x-small" color="primary" class="ml-1 mt-n4">
            {{ groupService.numberOfEvents }}
          </v-chip>
          <v-chip size="x-small" color="primary" class="ml-1 mt-n4">
            {{ groupService.numberOfUsers }}
          </v-chip>
        </v-col>
      </v-row>

      <v-divider class="my-2" />
      <v-card-text>{{ groupDescription }}</v-card-text>
    </div>

    <!-- Spacer to push content down -->
    <div class="flex-grow-1"></div>

    <!-- Bottom content (second divider and v-card-actions) -->
    <v-divider class="mt-2" />
    <v-card-actions class="d-flex justify-end">
      <join-button
        :is-member="groupService.isMember.value"
        :disabled="groupService.group.checkMembership.isLoading"
        @toggle-membership="toggleMembership"
      />
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import router from "@/router";
import GroupService from "@/services/GroupService";
import { GroupViewModel } from "@/viewmodels.g";

const props = defineProps<{
  group: GroupViewModel;
}>();

const groupService = new GroupService(props.group);
groupService.lookupMembership();

groupService.events.$count();
groupService.posts.$count();
groupService.groupUser.$count();

async function toggleMembership() {
  await groupService.toggleMembership();
  groupService.groupUser.$count();
}

const groupDescription = computed(() => {
  const characterLimit = 200;

  if ((props.group.description?.length ?? 0) > characterLimit) {
    return props.group.description?.slice(0, characterLimit) + "..."; // Truncate to 100 characters and append "..."
  }
  // Return the full description if it's 100 characters or less
  return props.group.description;
});
</script>
