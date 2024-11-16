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
  </v-container>
</template>

<script setup lang="ts">
import { GroupViewModel } from "@/viewmodels.g";

useTitle("Group");

const props = defineProps<{
  groupId: number;
}>();

const group = new GroupViewModel();
group.$load(props.groupId);
</script>
