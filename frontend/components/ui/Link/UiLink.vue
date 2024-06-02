<template>
    <component
        :is="componentTag"
        v-bind="attrs"
        :disabled="props.disabled"
        class="UiLink"
        :class="classList"
        @click="emit('click', $event)"
    >
        <slot />
    </component>
</template>

<script setup lang="ts">
// Props
const props = withDefaults(defineProps<{
    tag?: string
    size?: 'small-s' | 'small' | 'medium' | 'large' | 'custom'
    color?: 'primary' | 'primary-400' | 'gray' | 'gray-500' | 'white' | 'black' | 'error'
    active?: boolean
    bold?: boolean
    disabled?: boolean
}>(), {
    tag: '',
    size: 'medium',
    color: 'primary',
    active: false,
    bold: false,
    disabled: false,
});

// Emits
const emit = defineEmits([
    'click',
]);

// Attrs
const attrs = useAttrs();

// Computed
const componentTag = computed(() => {
    if (props.tag) {
        return props.tag;
    } else if (attrs.to) {
        return resolveComponent('NuxtLink');
    } else if (attrs.href) {
        return 'a';
    } else {
        return 'button';
    }
});

const classList = computed(() => [
    `--${props.size}-size`,
    `--${props.color}-color`,
    {
        '--is-bold': props.bold,
        '--is-disabled': props.disabled,
        '--is-active': props.active,
        '--is-interactive': ['a', 'button'].includes(componentTag.value),
    },
]);
</script>

<style lang="scss">
@import "UiLink";
</style>
