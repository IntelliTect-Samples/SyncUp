<template>
  <v-card>
    <c-loader-status
      :loaders="{
        '': [user.$bulkSave],
      }"
    ></c-loader-status>
    <v-card-item>
      <div class="d-flex align-center">
        <v-btn
          color="primary"
          icon="fas fa-chevron-left"
          size="small"
          variant="tonal"
          class="mr-2"
          @click="goBack()"
        />
        <v-card-title> Edit Profile </v-card-title>
      </div>
    </v-card-item>
    <v-card-text>
      <v-form ref="form">
        <v-row dense>
          <v-col cols="12">
            <c-input :model="user" for="userName"></c-input>
          </v-col>
          <v-col cols="12">
            <c-input :model="user" for="fullName"></c-input>
          </v-col>
          <v-col cols="12">
            <c-input
              :model="user"
              for="email"
              readonly
              :hint="user.emailConfirmed ? '' : 'Email not verified'"
              persistent-hint
              class="mb-3"
            >
              <template #append-inner>
                <v-icon v-if="user.emailConfirmed" color="success">
                  fa fa-check-circle
                </v-icon>
                <v-icon v-else color="warning">
                  fa fa-exclamation-circle
                </v-icon>
              </template>
            </c-input>
          </v-col>
          <v-col cols="12">
            <v-btn
              color="primary"
              variant="text"
              @click="editPassword = !editPassword"
            >
              Edit Password
            </v-btn>
          </v-col>
          <template v-if="editPassword">
            <v-col cols="12">
              <c-loader-status :loaders="user.setPassword" />
            </v-col>
            <v-col cols="12">
              <c-input
                :model="user.setPassword"
                for="currentPassword"
              ></c-input>
            </v-col>
            <v-col cols="12">
              <c-input :model="user.setPassword" for="newPassword"></c-input>
            </v-col>
            <v-col cols="12">
              <c-input
                :model="user.setPassword"
                for="confirmNewPassword"
              ></c-input>
            </v-col>
          </template>
        </v-row>
        <div class="d-flex justify-end">
          <v-btn color="primary" @click="saveUser()">Save</v-btn>
        </div>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { UserViewModel } from "@/viewmodels.g";

const props = defineProps<{
  user: UserViewModel;
}>();

const emit = defineEmits(["back"]);
const editPassword = ref(false);
const form = useForm(false);

function goBack() {
  emit("back");
}

async function saveUser() {
  const valid = await form.value?.validate();
  const isValid = !editPassword || valid?.valid;
  if (isValid && props.user.$bulkSavePreview().isDirty) {
    await props.user.$bulkSave();
    emit("back");
  }
}
</script>
