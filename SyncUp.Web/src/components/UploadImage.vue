<template>
  <v-file-input
    id="file"
    v-model="file"
    accept="image/*"
    style="display: none"
  />
  <v-img :width="300" cover :src="imageUrl" @click="imageClick"> </v-img>
</template>

<script setup lang="ts">
import { Image } from "@/models.g";
import { ImageServiceViewModel } from "@/viewmodels.g";

const image = defineModel<Image | null>({ default: null });

const file = ref<File | null>(null);
const imageService = new ImageServiceViewModel();

watch(file, async (value) => {
  if (value) {
    const bytes = await fileToByteArray(value);

    imageService.upload(bytes).then((result) => {
      if (result) {
        image.value = result;
      }
    });
  }
});

const imageUrl = computed(() => {
  if (image.value?.imageUrl) {
    return image.value.imageUrl;
  }
  return "https://www.pngkey.com/png/detail/233-2332677_image-500580-placeholder-transparent.png";
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
</script>
