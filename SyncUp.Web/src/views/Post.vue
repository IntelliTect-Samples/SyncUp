<template>
  <v-container>
    <v-card>
      <v-card-title>
        {{ post.title }}
      </v-card-title>
      <v-card-text>
        {{post.body}}
      </v-card-text>
      <v-card-actions>
        <v-btn v-on:click="likeCount++" rounded="xl" variant="elevated">
          <span class="me-1">{{ likeCount }}</span>
          <v-icon icon="fa-solid fa-thumbs-up"></v-icon>
        </v-btn>
          <span class="me-1">{{post.comments?.length}}</span>
          <v-icon icon="fa-solid fa-comment"></v-icon>
      </v-card-actions>
    </v-card>
    <v-btn v-if="post.comments?.length" v-on:click="showComments = !showComments" class="mt-3 me-3" variant="flat">
        <v-icon icon="fa-solid fa-chevron-down"></v-icon></v-btn>
    <v-btn class="mt-3" rounded="xl">
      <v-icon icon="fa-solid fa-plus"></v-icon>
      <span>Add a comment</span>
    </v-btn>
    <v-card v-if="showComments"
            v-for="comment in post.comments"
            class="mt-3 ms-8">
      <v-card-title>{{ comment.createdBy ?? 'unknown user'}}</v-card-title>
      <v-card-text>
        {{ comment.body }}
      </v-card-text>
    </v-card>
  </v-container>
</template>
<script setup lang="ts">
  import { PostViewModel } from "@/viewmodels.g";

  useTitle("Post");

  const props = defineProps<{
    postId: number;
  }>();

  const post = new PostViewModel();
  post.$load(props.postId);
  var showComments = true;
  var likeCount = 0;

</script>
