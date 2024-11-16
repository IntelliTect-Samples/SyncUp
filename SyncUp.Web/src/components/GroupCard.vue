<template>
  <v-card :to="`/group/${group.groupId}`">
    <v-card-item>
      <v-row dense align="center">
        <v-col cols="auto">
          <v-avatar icon="fas fa-user" size="60" color="primary"></v-avatar>
        </v-col>
        <v-col>
          <v-card-title>
            {{ group.name }}
          </v-card-title>
        </v-col>
      </v-row>
      <v-card-subtitle>{{ group.description }}</v-card-subtitle>
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
