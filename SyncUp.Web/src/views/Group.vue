<template>
  <v-container>
    <c-loader-status no-initial-content :loaders="[group.$load]">
      <PageImageBanner
        :title="group.name!"
        :image-url="group.imageUrl!"
        :description="group.description!"
        :badge1-text="numberOfPosts"
        :badge2-text="numberOfUsers"
        :is-member="isMember"
        @toggle-membership="toggleMembership(group)"
      />
      <!-- TODO: Implement is-member -->

      <!-- Posts List -->
      <v-card
        v-for="post in posts.$items"
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
import { GroupUser, Post } from "@/models.g";
import {
  GroupUserListViewModel,
  GroupViewModel,
  PostListViewModel,
} from "@/viewmodels.g";

useTitle("Group");

const props = defineProps<{
  groupId: number;
}>();

// Load group
const group = new GroupViewModel();
group.$load(props.groupId);

// Load posts
const posts = new PostListViewModel();
const postsDataSource = new Post.DataSources.PostsForGroup();
postsDataSource.groupId = props.groupId;
posts.$dataSource = postsDataSource;
posts.$load();
posts.$count();

// Count members
const groupUser = new GroupUserListViewModel();
const groupUserDataSource = new GroupUser.DataSources.UsersForGroup();
groupUserDataSource.groupId = props.groupId;
groupUser.$dataSource = groupUserDataSource;
groupUser.$count();

// Import membership functions
const { isMember, toggleMembership, lookupMembership } = useGroup();

watch(isMember, () => {
  groupUser.$count();
});

group.$load.onFulfilled(async () => lookupMembership(group));

// UI Functions
function truncateText(text: string | null) {
  const characterLimit = 500;

  if ((text?.length ?? 0) > characterLimit) {
    return text?.slice(0, characterLimit) + "..."; // Truncate to 100 characters and append "..."
  }
  // Return the full text if it's 100 characters or less
  return text;
}

const numberOfPosts = computed(() => {
  return (posts.$count.result ?? 0) + " posts";
});

const numberOfUsers = computed(() => {
  return (groupUser.$count.result ?? 0) + " users";
});
</script>
