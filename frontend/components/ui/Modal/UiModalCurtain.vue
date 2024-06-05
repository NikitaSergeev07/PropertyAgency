<template>
    <QDialog
        ref="dialogRef"
        class="UiModalCurtain"
        :class="classList"
        transition-show="slide-right"
        transition-hide="slide-left"
    >
        <div class="UiModalCurtain__wrapper">
            <div class="UiModalCurtain__header">
                <UiButton
                    size="sm"
                    round
                    flat
                    class="UiModalCurtain__close"
                    @click="onDialogCancel"
                >
                    <QIcon
                        name="close"
                        size="medium"
                        class="UiModalCurtain__icon"
                    />
                </UiButton>
            </div>

            <main class="UiModalCurtain__content">
                <component
                    :is="props.component"
                    v-if="props.component"
                    v-bind="componentProps"
                    :on-close="onDialogCancel"
                    class="UiModalCurtain__component"
                />
            </main>
        </div>
    </QDialog>
</template>

<script setup lang="ts">
// Types
import type { Component } from 'vue';

// Data
const { dialogRef, onDialogCancel } = useDialogPluginComponent();

// Props
const props = withDefaults(defineProps<{
    component: Component | undefined;
    componentProps?: any;
    withoutOverlay?: boolean;
    size?: 'small' | 'medium' | 'large',
}>(), {
    component: undefined,
    componentProps: () => ({}),
    withoutOverlay: false,
    size: 'small',
});

// Computed
const classList = computed(() => [{
    '--without-overlay': props.withoutOverlay,
    [`--${props.size}-size`]: props.size,
}]);
</script>

<style lang="scss">
    .UiModalCurtain {
        width: 100%;
        height: 100%;

        $modal: &;

        &.--without-overlay {
            .q-dialog__backdrop {
                background-color: transparent;
            }
        }

        &.--small-size {
            #{$modal} {
                &__wrapper {
                    width: 560px;
                }
            }
        }

        &.--medium-size {
            #{$modal} {
                &__wrapper {
                    width: 700px;
                }
            }
        }

        &.--large-size {
            #{$modal} {
                &__wrapper {
                    width: 800px;
                }
            }
        }

        &__wrapper {
            position: relative;
            display: flex;
            flex-direction: column;
            height: 100%;
            row-gap: mul($unit, 2);
            background-color: $white;
            pointer-events: auto;
        }

        &__header {
            position: relative;
            z-index: 2;
            padding: mul($unit, 3) mul($unit, 3) 0;
            display: flex;
            justify-content: flex-end;
            width: 100%;
            background: $white;
        }

        &__close {
            @include hover {
                #{$modal} {
                    &__icon {
                        transform: rotate(90deg);
                    }
                }
            }
        }

        &__icon {
            transition: transform .3s ease;
        }

        &__content {
            overflow: auto;
        }

        &__content,
        &__component {
            height: 100%;
        }

        .q-dialog__backdrop {
            backdrop-filter: blur($unit);
        }

        .q-dialog__inner {
            justify-content: flex-start;
            padding: 0;

            &--minimized > div {
                overflow: visible;
                max-width: initial;
                max-height: initial;
            }
        }
    }
</style>
