<template>
  <v-container>
    <c-loader-status no-initial-content :loaders="[groupService.group.$load]">
      <PageImageBanner
        :title="groupService.group.name!"
        :image-url="groupService.group.bannerImage?.imageUrl!"
        :description="groupService.group.description!"
        :badge1-text="groupService.numberOfPosts.value"
        :badge2-text="groupService.numberOfEvents.value"
        :badge3-text="groupService.numberOfUsers.value"
        :is-member="groupService.isMember.value"
        @toggle-membership="groupService.toggleMembership()"
      />
    </c-loader-status>

    <!-- Posts List -->
    <c-loader-status :loaders="[groupService.posts.$load]">
      <v-row dense>
        <v-col>
          <v-text-field
            v-model="groupService.posts.$params.search"
            label="Search Posts"
            prepend-inner-icon="fa fa-search"
            hide-details
            clearable
          />
        </v-col>
        <v-col cols="auto" align="right">
          <v-btn color="primary" @click="showAddPostDialog = true">
            <v-icon class="mr-2"> fas fa-plus </v-icon> Add Post
          </v-btn>

          <EditOrAddPostDialog
            v-model="showAddPostDialog"
            :group-id="groupId"
            @saved="groupService.posts.$load"
          />
        </v-col>
      </v-row>

      <v-card
        v-for="post in groupService.posts.$items"
        :key="post.$stableId"
        class="mt-3"
        :to="`/post/${post.postId}`"
      >
        <v-card-title> {{ post.title }} </v-card-title>
        <v-card-text> {{ truncateText(post.body) }} </v-card-text>
      </v-card>
    </c-loader-status>

    <v-divider class="mt-5 mb-2" />
    <EventList :group-id="groupId" />
  </v-container>
</template>

<script setup lang="ts">
import GroupService from "@/services/GroupService";
import { GroupViewModel } from "@/viewmodels.g";

useTitle("Group");

const props = defineProps<{
  groupId: number;
}>();

const showAddPostDialog = ref(false);

// Load group
const groupService = new GroupService(new GroupViewModel());

groupService.group.$load(props.groupId).then(async () => {
  groupService.lookupMembership();

  groupService.posts.$load();
  groupService.posts.$count();

  groupService.events.$count();

  groupService.groupUser.$count();
});

groupService.posts.$useAutoLoad({ wait: 100 });

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
