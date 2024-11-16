<template>
  <v-btn
    width="100px"
    size="small"
    :color="isMember ? 'grey' : 'success'"
    variant="flat"
    @click="toggleMembership"
  >
    <v-icon class="mr-2">
      {{ isMember ? "fa-solid fa-check" : "fa-solid fa-plus" }}
    </v-icon>
    Join{{ isMember ? "ed" : "" }}
  </v-btn>
</template>

<script setup lang="ts">
const props = defineProps<{
  isMember: boolean;
}>();

const emits = defineEmits(["toggleMembership"]);

function toggleMembership() {
  emits("toggleMembership");
}
</script>
