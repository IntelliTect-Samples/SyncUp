<template>
  <login-flow>
    <template #title> Forgot Password </template>
    <c-loader-status class="pb-2" :loaders="signInService.forgotPassword" />
    <v-col>
      <v-alert
        v-if="signInService.forgotPassword.wasSuccessful"
        color="success"
        text="If an account matching your input was found, a message with password reset instructions will be sent to the account's email address."
      />
      <v-row class="text-grey"> Username </v-row>
      <v-row>
        <v-text-field v-model="username" class="fill-width" />
      </v-row>
    </v-col>
    <template #actionBar>
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        :disabled="!username.length"
        text="Reset Password"
        @click="resetPassword"
      />
    </template>
    <template #actionSubBar>
      <v-btn
        class="text-body-2"
        text="Sign up"
        variant="text"
        color="primary"
        to="/SignUp"
      />
      &#8226;
      <v-btn
        class="text-body-2"
        text="Sign In"
        variant="text"
        color="primary"
        to="/SignIn"
      />
    </template>
  </login-flow>
</template>

<script setup lang="ts">
import { SignInServiceViewModel } from "@/viewmodels.g";
import LoginFlow from "@/components/LoginFlow.vue";

defineProps<{ fromSignOut?: boolean }>();
const signInService = new SignInServiceViewModel();
const username = ref("");

async function resetPassword() {
  await signInService.forgotPassword(username.value);
}
</script>
