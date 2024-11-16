<template>
  <v-container>
    <PageImageBanner
      :title="group.name!"
      :image-url="group.imageUrl!"
      :description="group.description!"
      badge1-text="23 members"
      badge2-text="2047 posts"
      @toggle-membership="console.log('membership toggled')"
    />

    <!-- Posts List -->
    <v-card
      v-for="post in posts.$items"
      :key="post.$stableId"
      class="mt-3"
      :to="`/post/${post.postId}`"
    >
      <v-card-title> {{ post.title }} </v-card-title>
      <!-- TODO: Truncate this text to a max character amount-->
      <v-card-text> {{ post.body }} </v-card-text>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { Post } from "@/models.g";
import { GroupViewModel, PostListViewModel } from "@/viewmodels.g";

useTitle("Group");

const props = defineProps<{
  groupId: number;
}>();

const group = new GroupViewModel();
group.$load(props.groupId);

const posts = new PostListViewModel();
const dataSource = new Post.DataSources.PostsForGroup();
dataSource.groupId = props.groupId;
posts.$dataSource = dataSource;
posts.$load();
</script>
