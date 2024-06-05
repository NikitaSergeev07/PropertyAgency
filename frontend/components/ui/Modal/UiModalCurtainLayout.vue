<template>
    <div class="UiModalCurtainLayout">
        <header
            v-if="slots.header"
            class="UiModalCurtainLayout__header"
        >
            <slot name="header" />
        </header>

        <main
            v-if="slots.content"
            class="UiModalCurtainLayout__content"
        >
            <slot name="content" />
        </main>

        <footer
            v-if="slots.footer"
            class="UiModalCurtainLayout__footer"
        >
            <slot name="footer" />
        </footer>
    </div>
</template>

<script setup lang="ts">
// Slots
const slots = useSlots();
</script>

<style lang="scss">
    .UiModalCurtainLayout {
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
        padding: 0 mul($unit, 3) mul($unit, 6) mul($unit, 6);
        row-gap: mul($unit, 6);

        $layout: &;

        &__header {
            position: relative;
            z-index: 2;
            flex-shrink: 0;
        }

        &__content {
            @include scrollbar;

            position: relative;
            z-index: 1;
            overflow: auto;
            flex-grow: 1;
            flex-shrink: 1;
            padding-right: mul($unit, 3);
        }

        &__footer {
            position: relative;
            z-index: 2;
            flex-shrink: 0;
            display: flex;
            flex-direction: column;
            padding: mul($unit, 6) mul($unit, 3) 0 0;
            row-gap: mul($unit, 4);
            border-top: 1px solid $gray-200;
        }
    }
</style>
