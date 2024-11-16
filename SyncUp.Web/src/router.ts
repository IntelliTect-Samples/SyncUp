import { createRouter, createWebHistory } from "vue-router";
import {
  CAdminEditorPage,
  CAdminTablePage,
  CAdminAuditLogPage,
} from "coalesce-vue-vuetify3";
import { Permission } from "./models.g";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      component: () => import("./views/Home.vue"),
    },
    {
      path: "/users",
      component: () => import("./views/EditUsers.vue"),
    },
    {
      path: "/admin",
      component: () => import("./views/Admin.vue"),
    },
    {
      path: "/group/:groupId",
      component: () => import("./views/Group.vue"),
      props: (r) => ({
        groupId: +r.params.groupId,
      }),
    },
    {
      path: "/post/:postId",
      component: () => import("./views/Post.vue"),
      props: (r) => ({
        postId: +r.params.postId,
      }),
    },
    {
      path: "/openapi",
      props: true,
      component: () => import("./views/OpenAPI.vue"),
    },

    // Coalesce admin routes
    {
      path: "/admin/:type",
      name: "coalesce-admin-list",
      component: titledAdminPage(CAdminTablePage),
      props: true,
    },
    {
      path: "/admin/:type/edit/:id?",
      name: "coalesce-admin-item",
      component: titledAdminPage(CAdminEditorPage),
      props: true,
    },
    {
      path: "/admin/audit",
      component: titledAdminPage(CAdminAuditLogPage),
      meta: { permissions: [Permission.ViewAuditLogs] },
      props: { type: "AuditLog" },
    },
    {
      name: "error-404",
      path: "/:pathMatch(.*)*",
      component: () => import("@/views/errors/NotFound.vue"),
    },
  ],
});

/** Creates a wrapper component that will pull page title from the
 *  coalesce admin page component and pass it to `useTitle`.
 */
function titledAdminPage<
  T extends
    | typeof CAdminTablePage
    | typeof CAdminEditorPage
    | typeof CAdminAuditLogPage,
>(component: T) {
  return defineComponent({
    setup() {
      const pageRef = ref<InstanceType<T>>();
      useTitle(() => pageRef.value?.pageTitle);
      return { pageRef };
    },
    render() {
      return h(component as any, {
        color: "primary",
        ref: "pageRef",
        ...this.$attrs,
      });
    },
  });
}

// Azure Monitor Application Insights configuration
let flushPageView: (() => void) | undefined;
router.beforeEach((to, from) => {
  if (to.path != from.path) {
    // If there's a previous page view still unsent,
    // flush it now before the new page takes over and changes
    // the window.location and document.title.
    // This only happens when a user is clicking through pages very fast.
    flushPageView?.();
  }
});
router.afterEach((to, from) => {
  if (to.path != from.path) {
    const time = new Date();
    let hasSent = false;

    flushPageView = () => {
      if (hasSent) return;
      hasSent = true;
      //@ts-expect-error appInsights from backend JavaScriptSnippet; no types available.
      window.appInsights?.trackPageView({
        startTime: time,
        properties: { duration: 0 },
      });
    };

    // Wait a moment before sending the page view
    // so that the page has a chance to update `document.title`,
    // which makes the timeline easier to read in app insights.
    setTimeout(flushPageView, 2000);
  }
});

export default router;
