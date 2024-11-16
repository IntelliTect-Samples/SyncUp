<template>
  <v-card>
    <v-card-item>
      <div class="d-flex">
        <v-avatar icon="fas fa-user" size="60" color="primary"></v-avatar>
        <div class="ms-4">
          <v-card-title>{{ group.name }}</v-card-title>
          <v-card-subtitle>{{ group.subTitle }}</v-card-subtitle>
        </div>
      </div>
    </v-card-item>
    <v-card-actions class="d-flex justify-end">
      <v-chip>
        {{ group.posts?.length ?? 0 }}
      </v-chip>
      <v-btn color="primary" variant="flat">
        <v-icon icon="fas fa-check"></v-icon>
        Join
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import { GroupViewModel } from "@/viewmodels.g";

const props = defineProps<{
  group: GroupViewModel;
}>();
</script>
