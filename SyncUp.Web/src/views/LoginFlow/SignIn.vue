<template>
  <login-flow>
    <template #title> Sign In </template>
    <c-loader-status class="pb-2" :loaders="signInService.signIn" />
    <v-col>
      <v-alert
        v-if="fromSignOut"
        color="success"
        text="Signed Out Successfully"
      />
      <v-row class="text-grey"> Username </v-row>
      <v-row>
        <v-text-field v-model="username" class="fill-width" />
      </v-row>
      <v-row class="text-grey"> Password </v-row>
      <v-row>
        <v-text-field v-model="password" type="password" class="fill-width" />
      </v-row>
    </v-col>
    <template #actionBar>
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        text="Sign in"
        @click="signIn"
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
        text="Forgot Password"
        variant="text"
        color="primary"
        to="/ForgotPassword"
      />
    </template>
  </login-flow>
</template>

<script setup lang="ts">
import { SignInServiceViewModel } from "@/viewmodels.g";
import LoginFlow from "@/components/LoginFlow.vue";
import router from "@/router";
import { refreshUserInfo } from "@/user-service";

defineProps<{ fromSignOut?: boolean }>();
const signInService = new SignInServiceViewModel();
const username = ref("");
const password = ref("");

async function signIn() {
  await signInService.signIn(username.value, password.value);
  await refreshUserInfo();
  if (signInService.signIn.wasSuccessful) {
    router.push("/");
  }
}
</script>
