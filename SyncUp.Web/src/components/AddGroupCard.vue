<template>
  <v-card title="Add New Group">
    <v-card-text>
      <c-input :model="group" for="name" />
    </v-card-text>
    <v-card-actions class="d-flex justify-end">
      <v-btn color="primary" variant="flat">
        <v-icon icon="fas fa-plus"></v-icon>
        Add Group
      </v-btn>
    </v-card-actions>
  </v-card>
</template>
<script setup lang="ts">

import { GroupViewModel } from "@/viewmodels.g";

const props = defineProps<{ group: GroupViewModel }>();
</script>
