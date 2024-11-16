<template>
  <v-container>
    <v-card>
      <v-card-title>
        {{ post.title }}
        <v-btn @click="showEditPostDialog = true"> Edit </v-btn>
        <EditOrAddPostDialog
          v-model="showEditPostDialog"
          :post-id="post.postId"
          @saved="post.$load"
        />
      </v-card-title>
      <v-card-text>
        {{ post.body }}
      </v-card-text>
      <v-card-actions>
        <v-btn rounded="xl" variant="elevated" @click="post.postLikeCount!++">
          <span class="me-1">{{ post.postLikeCount }}</span>
          <v-icon icon="fa-solid fa-thumbs-up"></v-icon>
        </v-btn>
        <span class="me-1">{{ post.comments?.length }}</span>
        <v-icon icon="fa-solid fa-comment"></v-icon>
      </v-card-actions>
    </v-card>
    <v-btn
      v-if="post.comments?.length"
      class="mt-3 me-3"
      variant="flat"
      @click="showComments = !showComments"
    >
      <v-icon
        :icon="
          showComments ? 'fa-solid fa-chevron-down' : 'fa-solid fa-chevron-up'
        "
      ></v-icon
    ></v-btn>
    <v-btn class="mt-3" rounded="xl" @click="showAddComment = !showAddComment">
      <v-icon icon="fa-solid fa-plus"></v-icon>
      <span>Add a comment</span>
    </v-btn>
    <div v-if="showAddComment" class="mt-3 ms-8">
      <v-card>
        <v-card-text>
          <c-input :model="newComment" for="body"></c-input>
          <div class="text-right">
            <v-btn
              class="me-2"
              variant="flat"
              @click="showAddComment = !showAddComment"
              >cancel</v-btn
            >
            <v-btn color="primary" variant="flat" @click="saveComment"
              >save</v-btn
            >
          </div>
        </v-card-text>
      </v-card>
    </div>
    <template v-for="comment in comments.$items" :key="comment.$stableId">
      <v-card v-if="showComments" class="mt-3 ms-8">
        <v-card-title>{{
          comment.createdBy?.userName ?? "unknown user"
        }}</v-card-title>
        <v-card-text>
          {{ comment.body }}
        </v-card-text>
        <v-card-actions>
          <v-btn
            size="small"
            rounded="xl"
            variant="elevated"
            @click="comment.commentLikeCount!++"
          >
            <span class="me-1">{{ comment.commentLikeCount }}</span>
            <v-icon icon="fa-solid fa-thumbs-up"></v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-container>
</template>
<script setup lang="ts">
import { Comment } from "@/models.g";
import { CommentListViewModel, PostViewModel } from "@/viewmodels.g";
import { CommentViewModel } from "@/viewmodels.g";

useTitle("Post");

const props = defineProps<{
  postId: number;
}>();

const showEditPostDialog = ref(false);

const post = new PostViewModel();
post.$load(props.postId);

post.$useAutoSave();

const comments = new CommentListViewModel();
const dataSource = new Comment.DataSources.CommentsForPost();
dataSource.postId = props.postId;
comments.$dataSource = dataSource;
comments.$load();
comments.$useAutoSave();

const newComment = new CommentViewModel();
async function saveComment() {
  newComment.postId = post.postId;
  await newComment.$save();
  showAddComment = ref(false);
  comments.$load();
}

let showAddComment = ref(false);
const showComments = ref(true);
</script>
