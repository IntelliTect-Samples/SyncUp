<template>
<v-container>
    <v-card>
        <div>
            <h5>testing testing</h5>
            {{ post.title }}
        </div>
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
</script>
