<template>
  <v-container>
    <v-row v-if="edit" class="justify-center">
      <v-col cols="12" md="6" lg="4" xl="3">
        <EditProfile :user="user" @back="edit = false" />
      </v-col>
    </v-row>
    <template v-else>
      <v-row class="justify-center">
        <v-col cols="12" md="6" lg="4" xl="3">
          <v-card>
            <v-card-item>
              <div class="d-flex">
                <UserAvatar :user="user" color="primary" class="mr-3 mt-2" />
                <div>
                  <v-card-title>
                    {{ user.userName }}
                  </v-card-title>
                  <v-card-subtitle>
                    {{ user.email }}
                    <div v-if="user.fullName">
                      {{ user.fullName }}
                    </div>
                  </v-card-subtitle>
                </div>
                <v-btn
                  class="ml-auto"
                  color="primary"
                  size="small"
                  variant="tonal"
                  icon="fas fa-pencil"
                  @click="edit = true"
                />
              </div>
            </v-card-item>
            <v-card-text>
              <v-chip class="mr-2">
                <v-icon icon="fas fa-thumbtack" class="mr-2" />
                12 Posts
              </v-chip>
              <v-chip class="mr-2">
                <v-icon icon="fas fa-comment" class="mr-2" />
                3 Comments
              </v-chip>
              <v-chip class="mr-2">
                <v-icon icon="fas fa-calendar" class="mr-2" />
                1 Event
              </v-chip>
              <v-chip class="mr-2">
                <v-icon icon="fas fa-user-group" class="mr-2" />
                1 Group
              </v-chip>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
      <v-row class="justify-center">
        <v-col cols="12" md="6" lg="4" xl="3">
          <v-tabs v-model="tab" align-tabs="center" color="primary" grow>
            <v-tab :value="1">Posts</v-tab>
            <v-tab :value="2">Comments</v-tab>
            <v-tab :value="3">Events</v-tab>
            <v-tab :value="4">Groups</v-tab>
          </v-tabs>
        </v-col>
      </v-row>

      <v-tabs-window v-model="tab">
        <v-tabs-window-item :value="1">
          <v-row class="justify-center mt-3">
            <v-col cols="12" md="6" lg="4" xl="3">
              <template v-for="post in userPosts.$items" :key="post.postId!">
                <PostCard
                  :to="`/post/${post.postId}`"
                  class="mb-4"
                  :post="post"
                />
              </template>
            </v-col>
          </v-row>
        </v-tabs-window-item>
        <v-tabs-window-item :value="3">
          <v-row class="justify-center mt-3">
            <v-col cols="12" md="6" lg="4" xl="3">
              <template v-for="event in userEvents.$items" :key="event.id!">
                <EventCard class="mb-4" :event="event" />
              </template>
            </v-col>
          </v-row>
        </v-tabs-window-item>
      </v-tabs-window>
    </template>
  </v-container>
</template>

<script setup lang="ts">
import {
  EventListViewModel,
  PostListViewModel,
  UserViewModel,
} from "@/viewmodels.g";

const { userInfo } = useUser();

const user = ref<UserViewModel>(new UserViewModel());
user.value.$load(userInfo.value.id!);
const edit = ref(false);

const tab = ref(1);
const userPosts = new PostListViewModel();
const userEvents = new EventListViewModel();
userPosts.$useAutoLoad();
userPosts.$load();
userEvents.$useAutoLoad();
userEvents.$load();
</script>
