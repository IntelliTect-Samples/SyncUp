<template>
  <v-card height="100%" class="d-flex flex-column">
    <!-- Top content (v-row, first divider, and v-card-text) -->
    <div>
      <v-row
        no-gutters
        align="center"
        class="linked-title"
        @click="router.push(`/event/${event.eventId}`)"
      >
        <v-col>
          <v-card-title>
            <v-row no-gutters>
              {{ event.name }}

              <v-spacer />
              <v-chip
                v-if="isPast"
                size="small"
                class="pa-3 mr-n2"
                prepend-icon="fa fa-clock-rotate-left"
              >
                Ended
              </v-chip>
            </v-row>
          </v-card-title>

          <v-row no-gutters>
            <v-card-subtitle>
              <v-icon size="x-small"> fa fa-calendar </v-icon>
              {{ event.time?.toDateString() }}
              <br />
              <v-icon size="x-small"> fa fa-clock </v-icon>
              {{ event.time?.toLocaleTimeString() }}
            </v-card-subtitle>

            <v-spacer />

            <v-chip
              size="small"
              color="primary"
              class="my-auto mx-2 pa-3"
              prepend-icon="fa fa-location-dot"
            >
              {{ event.location }}
            </v-chip>
          </v-row>
        </v-col>
      </v-row>

      <v-divider class="my-2" />
      <v-card-text>{{ eventDescription }}</v-card-text>
    </div>

    <!-- Spacer to push content down -->
    <div class="flex-grow-1"></div>

    <!-- Bottom content (second divider and v-card-actions) -->
    <v-divider class="mt-2" />
    <v-card-actions class="d-flex justify-end">
      <join-button
        :is-member="false"
        :disabled="true"
        @toggle-membership="toggleMembership"
      />
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import router from "@/router";
import { EventViewModel } from "@/viewmodels.g";

const props = defineProps<{
  event: EventViewModel;
}>();

const isPast = computed(() =>
  props.event.time !== null ? props.event.time < new Date() : false,
);

const eventDescription = computed(() => {
  const characterLimit = 200;

  if ((props.event.description?.length ?? 0) > characterLimit) {
    return props.event.description?.slice(0, characterLimit) + "..."; // Truncate to 100 characters and append "..."
  }
  // Return the full description if it's 100 characters or less
  return props.event.description;
});

function toggleMembership() {
  //TODO: Toggle user's membership/attendance to an event
}
</script>
