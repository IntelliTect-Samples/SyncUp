<template>
  <v-container>
    <v-row class="justify-center">
      <v-col cols="12" md="6" lg="4" xl="3">
        <v-card>
          <v-card-item>
            <div class="d-flex align-center">
              <UserAvatar :user="user" color="primary" class="mr-3" />
              <div>
                <v-card-title>
                  {{ user.userName }}
                </v-card-title>
                <v-card-subtitle>
                  {{ user.email }}
                </v-card-subtitle>
              </div>
            </div>
          </v-card-item>
        </v-card>
      </v-col>
    </v-row>
    <v-row class="justify-center">
      <v-col cols="12" md="6" lg="4" xl="3">
        <v-tabs v-model="tab" align-tabs="center" color="primary" grow>
          <v-tab :value="1">Posts</v-tab>
          <v-tab :value="2">Comments</v-tab>
          <v-tab :value="3">Events</v-tab>
        </v-tabs>
      </v-col>
    </v-row>

    <v-tabs-window v-model="tab">
      <v-tabs-window-item :value="1">
        <v-row class="justify-center">
          <v-col cols="12" md="6" lg="4" xl="3">
            <template v-for="post in userPosts.$items" :key="post.id!">
              <PostCard class="mb-4" :post="post" />
            </template>
          </v-col>
        </v-row>
      </v-tabs-window-item>
    </v-tabs-window>
  </v-container>
</template>

<script setup lang="ts">
import { PostListViewModel, UserViewModel } from "@/viewmodels.g";

const { userInfo } = useUser();

const user = ref<UserViewModel>(new UserViewModel());
user.value.$load(userInfo.value.id!);

const tab = ref(1);
const userPosts = new PostListViewModel();
userPosts.$useAutoLoad();
userPosts.$load();
</script>
