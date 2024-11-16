import router from "@/router";
import { bindToQueryString } from "coalesce-vue";

const returnUrl = ref<string | undefined>();

function pushReturn() {
  const returnUrlChain = returnUrl.value?.split("?") ?? [];

  const routeName = returnUrlChain.shift()?.replace("/", "");
  const chainedUrl = returnUrlChain[0]
    ? decodeURIComponent(returnUrlChain[0]!.replace("ReturnUrl=", ""))
    : undefined;

  router.push({
    name: routeName,
    query: {
      ReturnUrl: chainedUrl,
    },
  });
}

export default function useReturnUrls() {
  const instance = getCurrentInstance()?.proxy;
  returnUrl.value = router.currentRoute.value.query["ReturnUrl"]?.toString();
  bindToQueryString(instance!, returnUrl, "value");

  return pushReturn;
}
