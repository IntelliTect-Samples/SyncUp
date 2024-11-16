<template>
  <div>
    <v-card
      class="d-flex align-end"
      :style="{
        backgroundImage: `url('${imageUrl}')`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
      }"
      :min-height="minHeight"
    >
      <div class="ma-5">
        <v-chip color="primary" variant="flat" size="x-large" class="pa-7">
          <h1>{{ title }}</h1>
        </v-chip>
      </div>
    </v-card>

    <v-card class="mt-3">
      <v-row class="ma-1">
        <v-col cols="auto">
          <v-chip v-if="badge1Text" class="mr-1" size="small" color="primary">
            {{ badge1Text }}
          </v-chip>
          <v-chip v-if="badge2Text" size="small" color="primary">
            {{ badge2Text }}
          </v-chip>
        </v-col>
        <v-col align="right">
          <JoinButton
            class="mr-2"
            :is-member="isMember"
            @toggle-membership="$emit('toggleMembership')"
          />
          <AddGroupButton
            :group="newGroup"
            @new-group-created="$emit('refreshGroups')"
          />
        </v-col>
      </v-row>
      <v-card-text> {{ description }} </v-card-text>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { GroupViewModel } from "@/viewmodels.g";

const props = withDefaults(
  defineProps<{
    title: string | null;
    imageUrl: string | null;
    description: string | null;
    isMember: boolean;
    badge1Text?: string | null;
    badge2Text?: string | null;
    minHeight?: string;
  }>(),
  {
    badge1Text: null,
    badge2Text: null,
    minHeight: "300",
  },
);

defineEmits(["toggleMembership", "refreshGroups"]);

const newGroup = new GroupViewModel();
newGroup.$stopAutoSave();
</script>
