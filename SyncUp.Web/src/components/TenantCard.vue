<template>
  <v-card>
    <v-row align="center">
      <v-col>
        <v-card-title>
          {{ tenant.name }}
        </v-card-title>
      </v-col>
      <v-col align="right">
        <JoinButton
          class="ma-2"
          :is-member="false"
          @toggle-membership="toggleMembership"
        />
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

function toggleMembership() {
  console.log("toggleMembership");
  props.tenantsService.joinOrSwitchTenant(props.tenant.tenantId);

  router.push(`/tenant`);
}
</script>
