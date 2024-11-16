<template>
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
      <c-input :model="eventsDataSource" for="showPastEvents" />
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
</template>

<script setup lang="ts">
import { EventListViewModel } from "@/viewmodels.g";
import { Event } from "@/models.g";

const eventsDataSource = new Event.DataSources.EventsByDate();
const events = new EventListViewModel();
events.$dataSource = eventsDataSource;
events.$useAutoLoad();
events.$load();
</script>
