<template>
  <v-card>
    <v-card-item>
      <v-card-title>
        {{ post.title }}
      </v-card-title>
      <div>
        <v-chip density="comfortable" class="mr-1">{{
          post.group?.name
        }}</v-chip>
        <c-display
          :model="post"
          for="createdOn"
          format="M/d/yyyy"
          class="text-body-2"
        />
      </div>
    </v-card-item>
    <v-card-text>
      <div class="mb-4 mt-2">
        {{ shortBody }}
      </div>
      <v-chip density="comfortable">
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
    body = props.post.body?.slice(0, 300);
    if (props.post.body.length > 300) {
      body = body.concat("...");
    }
  }
  return body;
});
</script>
