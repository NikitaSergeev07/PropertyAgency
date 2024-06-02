<template>
    <div class="PromoBannerTabs">
        <ul :class="$style.wrapper">
            <li
                v-for="(tab, index) in tabs"
                :key="tab.id"
            >
                <UiButton
                    color="custom"
                    :class="[$style.button, { [$style['--is-active']]: index === actualTab }]"
                    :ripple="false"
                    @click="changeTab(index)"
                >
                    {{ tab.name }}
                </UiButton>
            </li>
        </ul>
    </div>
</template>

<script setup lang="ts">
const emit = defineEmits([
    'update:model-value',
]);

const { modelValue } = withDefaults(defineProps<{
    tabs: { id: string, name: string }[],
    modelValue: number;
}>(), {
    tabs: () => [],
    modelValue: 0,
});

const actualTab = toRef(modelValue);

const changeTab = (index: number) => {
    actualTab.value = index;

    emit('update:model-value', actualTab.value);
};
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        align-items: center;
    }

    .button {
        background-color: $gray-500;

        &.--is-active {
            background-color: $gray-100;

            span {
                color: $black;
            }
        }
    }
</style>
