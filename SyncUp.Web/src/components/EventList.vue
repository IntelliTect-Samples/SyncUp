<template>
  <c-loader-status :loaders="[events.$load]">
    <v-row dense>
      <v-col>
        <v-text-field
          v-model="events.$params.search"
          label="Search Events"
          prepend-inner-icon="fa fa-search"
          hide-details
          clearable
        />
      </v-col>
      <v-col cols="auto" align="right">
        <c-input
          :model="eventsDataSource"
          for="showPastEvents"
          hide-details
          density="compact"
          class="ml-2"
        />
      </v-col>
    </v-row>
    <v-row>
      <v-col
        v-for="event in events.$items"
        :key="event.eventId!"
        cols="12"
        md="6"
        lg="4"
      >
        <EventCard :event="event" />
      </v-col>
    </v-row>
  </c-loader-status>
</template>

<script setup lang="ts">
import { EventListViewModel } from "@/viewmodels.g";
import { Event } from "@/models.g";

const props = defineProps<{
  groupId?: number;
}>();

const { userInfo } = useUser();

const eventsDataSource = new Event.DataSources.EventsByDate();
const events = new EventListViewModel();

if (props.groupId) {
  events.$params.filter = { groupId: props.groupId };
} else {
  eventsDataSource.userId = userInfo.value.id;
}

events.$dataSource = eventsDataSource;
events.$useAutoLoad();
events.$load();
</script>
