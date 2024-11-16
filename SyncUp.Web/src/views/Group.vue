<template>
  <PageImageBanner
    title="Summit Seekers"
    image-url="https://wallpapers.com/images/featured/widescreen-3ao0esn9qknhdudj.jpg"
    badge1-text="23 members"
    badge2-text="2047 posts"
    description="The Summit Seekers Mountaineering Club is a vibrant community of outdoor enthusiasts united by their love for adventure, nature, and the thrill of conquering peaks. Whether you're an experienced mountaineer or just beginning your journey into the mountains, our group welcomes adventurers of all skill levels.

With a focus on exploration, education, and camaraderie, we organize guided expeditions, skill-building workshops, and social events throughout the year. From serene hikes through alpine meadows to technical climbs on rugged terrain, we offer something for everyone seeking to connect with the great outdoors.

Safety and sustainability are at the core of everything we do. Our members are committed to Leave No Trace principles, preserving the beauty of the wilderness for generations to come. Join the Summit Seekers to push your limits, share unforgettable experiences, and build lasting friendships on the trail to adventure."
    @toggle-membership="console.log('membership toggled')"
  />
</template>

<script setup lang="ts">
useTitle("Group");
</script>
