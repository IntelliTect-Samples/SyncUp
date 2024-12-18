<template>
  <div>
    <v-file-input
      id="file"
      v-model="file"
      accept="image/*"
      style="display: none"
    />
    <v-hover v-slot="{ isHovering, props }">
      <div v-bind="props">
        <v-img
          :cover="cover"
          :src="imageUrl"
          :max-width="width"
          :max-height="height"
          lazy-src="/placeholder.svg"
        >
          <v-overlay
            v-if="!busy && !errorMessage"
            :model-value="!!isHovering"
            class="align-center justify-center"
            :scrim="image?.color || '#036358'"
            contained
          >
            <div v-if="!busy">
              <v-btn class="mr-2" @click="imageClick">Upload</v-btn>
              <v-btn @click="imageLink">Link</v-btn>
            </div>
          </v-overlay>
        </v-img>
      </div>
    </v-hover>
  </div>
</template>

<script setup lang="ts">
import { Image } from "@/models.g";
import { ImageServiceViewModel } from "@/viewmodels.g";

const image = defineModel<Image | null>({ default: null });

const props = defineProps<{
  cover?: boolean;
  height?: number;
  width?: number;
}>();

//console.log(props.height);

const emits = defineEmits(["changed"]);

const file = ref<File | null>(null);
const imageService = new ImageServiceViewModel();
const busy = ref(false);
const errorMessage = ref<string | null>(null);

watch(file, async (value) => {
  if (value) {
    busy.value = true;
    const bytes = await fileToByteArray(value);

    imageService
      .upload(bytes)
      .then((result) => {
        if (result) {
          busy.value = false;
          image.value = result;
          emits("changed");
        }
      })
      .catch(() => {
        busy.value = false;
        errorMessage.value = "Invalid image";
        setTimeout(() => {
          errorMessage.value = null;
        }, 5000);
      });
  }
});

const imageUrl = computed(() => {
  if (image.value?.imageUrl) {
    return image.value.imageUrl;
  }
  return "/placeholder.svg";
  //return URL.createObjectURL(file.value);
});

function imageClick() {
  const input = document.getElementById("file") as HTMLInputElement;
  input.click();
  input.onchange = (e) => {
    const target = e.target as HTMLInputElement;
    if (target.files) {
      file.value = target.files[0];
    }
  };
}

async function fileToByteArray(file: File): Promise<Uint8Array> {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = (event) => {
      const arrayBuffer = event.target?.result as ArrayBuffer;
      const byteArray = new Uint8Array(arrayBuffer);
      resolve(byteArray);
    };

    reader.onerror = (error) => {
      reject(error);
    };

    reader.readAsArrayBuffer(file);
  });
}

async function imageLink() {
  const url = prompt("Enter the URL of the image");
  if (url) {
    busy.value = true;
    imageService
      .uploadFromUrl(url)
      .then((result) => {
        busy.value = false;
        if (result) {
          image.value = result;
          emits("changed");
        }
      })
      .catch(() => {
        busy.value = false;
        errorMessage.value = "Invalid URL";
        setTimeout(() => {
          errorMessage.value = null;
        }, 5000);
      });
  }
}
</script>
