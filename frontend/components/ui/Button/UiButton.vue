<template>
    <QBtn
        :class="[$style.button, classList]"
        v-bind="attrs"
        :size="size"
        :color="color"
    >
        <template #default>
            <slot name="default" />
        </template>
        <template #loading>
            <slot name="loading">
                <QSpinner
                    :color="loaderColor"
                    :size="loaderSize"
                />
            </slot>
        </template>
    </QBtn>
</template>

<script setup lang="ts">
// Props
const props = withDefaults(defineProps<{
    size?: 'xx-small' | 'x-small' | 'small' | 'medium' | 'large' | 'x-large'
    color?: string
    loaderColor?: string;
    loaderSize?: string;
}>(), {
    size: 'medium',
    color: 'primary',
    loaderColor: 'white',
    loaderSize: 'md',
});

// Attrs
const attrs = useAttrs();

// Computed
const classList = computed(() => [
    {
        [`--${props.size}`]: props.size,
    },
]);
</script>

<style lang="scss" module>
.button {
    &:global(.q-btn--actionable.q-btn--standard:before) {
        content: none;
    }

    &:global {
        &.--xx-small {
            line-height: 120%;
            font-size: mul($unit, 2.5) !important;
        }

        &.--x-small {
            line-height: 140%;
            font-size: mul($unit, 3) !important;
        }

        &.--small {
            line-height: 128%;
            font-size: mul($unit, 3.5) !important;
        }

        &.--medium {
            line-height: 150%;
            font-size: mul($unit, 4) !important;
        }

        &.--large {
            line-height: 160%;
            font-size: mul($unit, 5) !important;
        }

        &.--x-large {
            line-height: 133%;
            font-size: mul($unit, 6) !important;
        }
    }
}
</style>
