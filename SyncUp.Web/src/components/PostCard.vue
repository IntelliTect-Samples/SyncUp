<template>
  <v-card>
    <v-card-item>
      <v-card-title>
        {{ post.title }}
      </v-card-title>
      <v-card-subtitle>
        <v-chip>{{ post.group?.name }}</v-chip>
        <c-display :model="post" for="createdOn" format="M/d/yyyy" />
      </v-card-subtitle>
    </v-card-item>
    <v-card-text>
      <div class="mb-3">
        {{ shortBody }}
      </div>
      <v-chip>
        <v-icon icon="fas fa-comment" color="primary" class="mr-2" />
        {{ post.comments.length }}
      </v-chip>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { PostViewModel } from "@/viewmodels.g";

const props = defineProps<{
  post: PostViewModel;
}>();

const shortBody = computed(() => {
  let body = "";
  if (props.post.body) {
    body = props.post.body?.slice(0, 100);
    if (props.post.body.length > 100) {
      body = body.concat("...");
    }
  }
  return body;
});
</script>
