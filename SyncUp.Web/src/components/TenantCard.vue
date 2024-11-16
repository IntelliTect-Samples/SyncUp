<template>
  <v-card class="mt-3">
    <v-row align="center">
      <v-col>
        <v-card-title class="linked-title" @click="switchTenant">
          {{ tenant.name }}
        </v-card-title>
        <v-card-subtitle>
          {{ tenant.tenantId }}
        </v-card-subtitle>
      </v-col>
    </v-row>
    <v-card-text>
      {{ tenant.description }}
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { Tenant } from "@/models.g";
import router from "@/router";
import { TenantsServiceViewModel } from "@/viewmodels.g";

const props = defineProps<{
  tenant: Tenant;
  tenantsService: TenantsServiceViewModel;
}>();

async function switchTenant() {
  await props.tenantsService.switchTenant(props.tenant.tenantId);
  goToTenant();
}

function goToTenant() {
  window.location.replace("/tenant");
}
</script>
