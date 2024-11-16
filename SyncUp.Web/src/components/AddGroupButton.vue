<template>
  <v-btn
    width="100px"
    size="small"
    color="success"
    variant="flat"
    @click="toggleShow"
  >
    Add Group
  </v-btn>

  <v-dialog v-model="show" max-width="500">
    <v-card>
      <v-sheet color="primary">
        <v-card-title> Add A New Group</v-card-title>
      </v-sheet>
      <c-loader-status
        :loaders="{
          '': [group.$save],
        }"
      >
        <v-card-text>
          <c-input :model="group" for="name" />
          <c-input :model="group" for="description" />
          <c-input :model="group" for="imageUrl" />
        </v-card-text>
        <v-divider class="mt-2"></v-divider>

        <v-card-actions class="my-2 d-flex justify-end">
          <v-btn
            class="text-none"
            rounded="xl"
            text="Cancel"
            @click="show = false"
          ></v-btn>

          <v-btn
            class="text-none"
            color="primary"
            rounded="xl"
            text="Save"
            variant="flat"
            :loading="group.$save.isLoading"
            @click="saveGroup"
          ></v-btn>
        </v-card-actions>
      </c-loader-status>
    </v-card>
  </v-dialog>
</template>
<script setup lang="ts">
import { GroupViewModel } from "@/viewmodels.g";
import { ref } from "vue";

const props = defineProps<{ group: GroupViewModel }>();

const show = ref<boolean>(false);

const emits = defineEmits(["newGroupCreated"]);

async function saveGroup() {
  await props.group.$save().then((g) => {
    emits("newGroupCreated");
    show.value = false;
  });
}

function toggleShow() {
  show.value = !show.value;
}
</script>
