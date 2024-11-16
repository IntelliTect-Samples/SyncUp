<template>
  <login-flow>
    <template #title> Email Confirmed </template>
    <v-col>
      <v-row> Thank you for confirming your email </v-row>
    </v-col>
    <template #actionBar>
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        text="Continue"
        @click="redirect"
      />
    </template>
  </login-flow>
</template>

<script setup lang="ts">
import LoginFlow from "@/components/LoginFlow.vue";

defineProps<{ fromSignOut?: boolean }>();
const redirect = useReturnUrls();
</script>
