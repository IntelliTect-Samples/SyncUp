<template>
  <v-container>
    <h1>Tenants</h1>
    <c-loader-status no-initial-content :loaders="[tenantsService.loadTenants]">
      <TenantCard
        v-for="tenant in tenants"
        :key="tenant.tenantId!"
        :tenant="tenant"
        :tenants-service="tenantsService"
      />
    </c-loader-status>
  </v-container>
</template>

<script setup lang="ts">
import { TenantsServiceViewModel } from "@/viewmodels.g";

const tenantsService = new TenantsServiceViewModel();
tenantsService.loadTenants();

const tenants = computed(() => tenantsService.loadTenants.result);
</script>
