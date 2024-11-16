import { GroupUser, Post } from "@/models.g";
import {
  GroupUserListViewModel,
  GroupViewModel,
  PostListViewModel,
} from "@/viewmodels.g";
import { ref } from "vue";

export default class GroupService {
  public group: GroupViewModel;
  public isMember = ref(false);

  public posts = new PostListViewModel();
  private postsDataSource = new Post.DataSources.PostsForGroup();

  public groupUser = new GroupUserListViewModel();
  private groupUserDataSource = new GroupUser.DataSources.UsersForGroup();

  constructor(group: GroupViewModel) {
    this.group = group;

    // Add watch for group.groupId for when the group loads
    watch(
      () => this.group.groupId,
      () => {
        this.postsDataSource.groupId = this.group.groupId;
        this.posts.$dataSource = this.postsDataSource;

        this.groupUserDataSource.groupId = this.group.groupId;
        this.groupUser.$dataSource = this.groupUserDataSource;
      },
    );
  }

  public numberOfPosts = computed(() => {
    let postfix = "-- ";
    if (!this.posts.$count.isLoading) {
      postfix = (this.posts.$count.result ?? 0).toString();
    }

    return postfix + " posts";
  });

  public numberOfUsers = computed(() => {
    let postfix = "-- ";
    if (!this.groupUser.$count.isLoading) {
      postfix = (this.groupUser.$count.result ?? 0).toString();
    }

    return postfix + " members";
  });

  public async toggleMembership() {
    await this.group.toggleMembership();
    this.isMember.value = !this.isMember.value;
  }

  public async lookupMembership() {
    this.isMember.value = (await this.group.checkMembership()) ?? false;
  }
}
