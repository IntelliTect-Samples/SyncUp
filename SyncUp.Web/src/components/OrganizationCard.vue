<template>
  <v-card>
    <v-card-item>
      <div class="d-flex justify-space-between align-center">
        <div class="ms-4">
          <v-card-title>{{ userInfo.tenantName }}</v-card-title>
          <v-card-subtitle
            >This is a fake description that needs love</v-card-subtitle
          >
        </div>
        <v-btn color="primary" variant="flat">
          <v-icon icon="fas fa-check"></v-icon>
          Joined
        </v-btn>
      </div>
    </v-card-item>
    <v-card-text
      ><v-chip> <v-icon icon="fas fa-user" class="mr-2" />15 users</v-chip>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
const { userInfo } = useUser();
</script>
