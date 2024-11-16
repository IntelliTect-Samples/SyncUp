<template>
  <login-flow>
    <template #title> Sign In </template>
    <c-loader-status class="pb-2" :loaders="signInService.register" />
    <v-alert
      v-if="
        signInService.register.wasSuccessful && signInService.register.message
      "
      color="success"
      :text="signInService.register.message"
    />
    <v-col>
      <v-row class="text-grey"> Username </v-row>
      <v-row>
        <v-text-field v-model="username" class="fill-width" />
      </v-row>
      <v-form v-model="valid">
        <v-row class="text-grey"> Password </v-row>
        <v-row>
          <v-text-field
            v-model="password"
            :rules="[passwordLengthRule]"
            type="password"
            class="fill-width password-with-hint"
          >
            <template #details>
              <span class="text-grey">
                Passwords must be at least 7 characters.
              </span>
            </template>
          </v-text-field>
        </v-row>
        <v-row class="text-grey"> Confirm Password </v-row>
        <v-row>
          <v-text-field
            v-model="confirmPassword"
            :rules="[passwordMatchRule]"
            type="password"
            class="fill-width"
          />
        </v-row>
      </v-form>
    </v-col>
    <template #actionBar>
      <v-btn
        variant="flat"
        color="primary"
        block
        rounded
        text="Register"
        @click="signUp"
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
const confirmPassword = ref("");

const passwordLengthRule = (v: string) =>
  v.length >= 7 || "Password must be at least 7 characters.";
const passwordMatchRule = (_: string) =>
  password.value == confirmPassword.value;

const valid = ref(false);

async function signUp() {
  await signInService.register(username.value, password.value);
  await refreshUserInfo();
  if (signInService.register.wasSuccessful && !signInService.register.message) {
    router.push("/");
  }
}
</script>

<style lang="scss">
.password-with-hint .v-messages {
  max-width: 0;
}
.password-with-hint .v-input__details {
  justify-content: start;
}
</style>
