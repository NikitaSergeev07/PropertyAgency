<template>
    <div
        class="UiFormCell"
        :class="classList"
    >
        <label
            v-if="slots.label"
            :for="props.name"
            class="UiFormCell__label"
        >
            <slot name="label" />
        </label>

        <div class="UiFormCell__body">
            <slot />
        </div>

        <div
            v-if="slots.info"
            class="UiFormCell__info"
        >
            <slot name="info" />
        </div>
    </div>
</template>

<script setup lang="ts">
// Props
const props = withDefaults(defineProps<{
    name?: string
    size?: 'small' | 'medium' | 'large' | 'custom'
    success?: string | number | null
}>(), {
    name: '',
    size: 'custom',
    success: null,
});

// Slots
const slots = useSlots();

// Computed
const classList = computed(() => [
    `--is-${props.size}-size`,
]);
</script>

<style lang="scss">
@import "UiFormCell";
</style>
