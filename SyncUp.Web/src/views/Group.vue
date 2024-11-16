<template>
  <v-container>
    <c-loader-status no-initial-content :loaders="[groupService.group.$load]">
      <PageImageBanner
        :title="groupService.group.name!"
        :image-url="groupService.group.imageUrl!"
        :description="groupService.group.description!"
        :badge1-text="groupService.numberOfPosts.value"
        :badge2-text="groupService.numberOfUsers.value"
        :is-member="groupService.isMember.value"
        @toggle-membership="groupService.toggleMembership()"
      />

      <!-- Posts List -->
      <v-card
        v-for="post in groupService.posts.$items"
        :key="post.$stableId"
        class="mt-3"
        :to="`/post/${post.postId}`"
      >
        <v-card-title> {{ post.title }} </v-card-title>
        <!-- TODO: Truncate this text to a max character amount-->
        <v-card-text> {{ truncateText(post.body) }} </v-card-text>
      </v-card>
    </c-loader-status>
  </v-container>
</template>

<script setup lang="ts">
import GroupService from "@/services/GroupService";
import { GroupViewModel } from "@/viewmodels.g";

useTitle("Group");

const props = defineProps<{
  groupId: number;
}>();

// Load group
const groupService = new GroupService(new GroupViewModel());

groupService.group
  .$load(props.groupId)
  .then(async () => groupService.lookupMembership());

groupService.posts.$load();
groupService.posts.$count();

groupService.groupUser.$count();

watch(groupService.isMember, () => {
  groupService.groupUser.$count();
});

// UI Functions
function truncateText(text: string | null) {
  const characterLimit = 500;

  if ((text?.length ?? 0) > characterLimit) {
    return text?.slice(0, characterLimit) + "..."; // Truncate to 100 characters and append "..."
  }
  // Return the full text if it's 100 characters or less
  return text;
}
</script>
