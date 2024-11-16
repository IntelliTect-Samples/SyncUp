<template>
  <v-dialog v-model="modelValue" max-width="500">
    <v-card>
      <v-sheet color="primary">
        <v-card-title> {{ isNew ? "Add" : "Edit" }} Post </v-card-title>
      </v-sheet>
      <c-loader-status :loaders="[post.$save]" />
      <v-card-text>
        <c-input :model="post" for="title" />
        <c-input :model="post" for="body" textarea />
      </v-card-text>
      <v-divider />
      <v-card-actions>
        <v-btn color="primary" variant="tonal" @click="modelValue = false">
          Cancel
        </v-btn>
        <v-btn color="primary" variant="flat" @click="save"> Save </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { PostViewModel } from "@/viewmodels.g";

const props = defineProps<{
  postId?: number | null;
  groupId: number; // Required for new posts
}>();

const modelValue = defineModel<boolean>({ default: false });

const emits = defineEmits(["saved"]);

const isNew = computed(() => {
  return !props.postId; // Fine to use truthy/falsy here, as a 0 ID would not be valid
});

const post = new PostViewModel();
if (!isNew.value) {
  // Load post
  post.$load(props.postId!);
} else {
  post.groupId = props.groupId;
}

async function save() {
  await post.$save();
  modelValue.value = false;
  emits("saved");
}
</script>
