<template>
  <v-container>
    <PageImageBanner
      :title="userInfo.tenantName"
      image-url="https://wallpapers.com/images/featured/widescreen-3ao0esn9qknhdudj.jpg"
      badge1-text="23 members"
      badge2-text="2047 posts"
      description="This is a fake description that needs love"
      @toggle-membership="console.log('membership toggled')"
    />
    <v-row class="ma-1">
      <v-col cols="12" md="6" lg="8">
        <v-row>
          <v-col
            v-for="group in groups.$items"
            :key="group.groupId!"
            cols="12"
            md="6"
            lg="4"
          >
            <GroupCard :group="group" />
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { GroupListViewModel } from "@/viewmodels.g";

useTitle("Home");
const groups = new GroupListViewModel();
groups.$useAutoLoad();
groups.$load();
const { userInfo } = useUser();
</script>
