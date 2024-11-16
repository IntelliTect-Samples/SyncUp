<template>
  <v-container>
    <c-loader-status
      :loaders="{
        'no-initial-content no-error-content': [users.$load],
      }"
    >
      <v-row>
        <v-col cols="12" md="6" lg="4" class="mt-4">
          <v-card title="All Users">
            <v-card-text>
              <v-list lines="two">
                <v-list-item
                  v-for="user in users.$items"
                  :key="user.id!"
                  color="primary"
                  append-icon="fas fa-chevron-right"
                  @click="selectedUser = user"
                >
                  <template #prepend>
                    <UserAvatar :user="user" color="primary" />
                  </template>

                  <v-list-item-title>
                    {{ user.userName }}
                  </v-list-item-title>
                  <v-list-item-subtitle>
                    {{ user.email }}
                  </v-list-item-subtitle>
                </v-list-item>
              </v-list>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="12" md="6" lg="8">
          <UserProfile v-if="selectedUser" :user="selectedUser" />
        </v-col>
      </v-row>
    </c-loader-status>
  </v-container>
</template>

<script setup lang="ts">
import { UserListViewModel, UserViewModel } from "@/viewmodels.g";

const users = new UserListViewModel();
users.$useAutoLoad();
users.$load();
const selectedUser = ref<UserViewModel | null>(null);
</script>
