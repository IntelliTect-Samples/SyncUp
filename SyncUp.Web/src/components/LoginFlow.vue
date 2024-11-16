<template>
  <v-card class="fill-height login-flow-background">
    <v-row class="justify-center align-center fill-height">
      <v-card color="white" width="350" style="top: -10%" class="pa-4 px-8">
        <v-row class="justify-center pb-4">
          <v-card-title class="text-h4">
            <slot name="title"> Welcome! </slot>
          </v-card-title>
        </v-row>

        <slot />
        <v-card-actions>
          <v-col>
            <v-row>
              <slot name="actionBar" />
            </v-row>
            <v-row class="align-baseline justify-center">
              <slot name="actionSubBar" />
            </v-row>
          </v-col>
        </v-card-actions>
      </v-card>
    </v-row>
  </v-card>
</template>

<script setup lang="ts"></script>

<style scoped lang="scss">
.card-padding {
  padding: 1.5rem !important;
}
</style>
<style>
.login-flow-background {
  background: rgb(33, 33, 33);
}
</style>
