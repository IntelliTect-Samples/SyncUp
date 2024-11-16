import { GroupViewModel } from "@/viewmodels.g";

const isMember = ref(false);

async function toggleMembership(group: GroupViewModel) {
  await group.toggleMembership();
  isMember.value = !isMember.value;
}

async function lookupMembership(group: GroupViewModel) {
  isMember.value = (await group.checkMembership()) ?? false;
}

export function useGroup() {
  return { isMember, toggleMembership, lookupMembership };
}
