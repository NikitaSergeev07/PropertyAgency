<template>
    <component
        :is="props.tag"
        class="UiIcon"
        :class="classList"
    >
        <svg :viewBox="props.viewBox">
            <slot name="default" />

            <g
                v-if="!$slots.default && svg"
                v-html="svg"
            />
        </svg>
    </component>
</template>

<script setup lang="ts">
// Icons
import { iconList } from './icon-list';

// Types
import type { IconName, IconSize } from '~/assets/types/ui/UiIcon.types';

// Props
const props = withDefaults(defineProps<{
    tag?: string
    viewBox?: string
    name: IconName
    size?: IconSize
}>(), {
    tag: 'div',
    viewBox: '0 0 32 32',
    name: 'user',
    size: 'medium',
});

// Computed
const classList = computed(() => [{
    [`--${props.size}-size`]: props.size,
}]);

const svg = computed(() => {
    const icon = iconList.get(props.name);

    return icon ? icon.innerHTML : null;
});
</script>

<style lang="scss">
@import "UiIcon";
</style>
