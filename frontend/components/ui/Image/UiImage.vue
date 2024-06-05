<template>
    <div class="UiImage">
        <div :class="$style.wrapper">
            <img
                v-if="loadImage"
                :src="loadImage"
                loading="lazy"
                :class="$style.image"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
const props = withDefaults(defineProps<{
    image?: string,
}>(), {
    image: '',
});

const loadImage = ref(null);

onMounted(async () => {
    const formatImageName = props.image.replace('./', '');

    loadImage.value = await $fetch(`http://localhost:3001/${formatImageName}`);
});
</script>

<style lang="scss" module>
    .wrapper {
        width: 100%;
        height: 100%;
    }

    .image {
        width: 100%;
        height: 100%;
        object-fit: contain;
    }
</style>
