<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <OrganizationCard />
      </v-col>
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
</script>
