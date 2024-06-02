<template>
    <div
        class="UiPasswordInput"
        :class="classList"
    >
        <UiInput
            :id="props.id"
            v-model="actualValue"
            v-bind="$attrs"
            :type="inputType"
            :size="props.size"
            :color="props.color"
            :disabled="props.disabled"
            :error="props.error"
            :outline="props.outline"
            class="UiPasswordInput__input"
            @input="onInput"
            @error-cleared="emit('error-cleared')"
        />

        <QIcon
            name="thumb_up"
            size="s"
            class="UiPasswordInput__icon"
            @click="isVisible = !isVisible"
        />
    </div>
</template>

<script setup lang="ts">
// Props
const props = withDefaults(defineProps<{
    modelValue: string | number | null
    id: string
    size: 'small' | 'medium' | 'large' | 'custom'
    color: 'grey' | 'custom'
    disabled: boolean
    error: string | number | null
    outline: boolean
}>(), {
    modelValue: null,
    id: 'medium',
    size: 'medium',
    color: 'grey',
    disabled: false,
    error: null,
    outline: false,
});

// Emits
const emit = defineEmits([
    'update:error',
    'input',
    'error-cleared',
]);

// Data
const actualValue: Ref = ref(null);
const isVisible: Ref = ref(false);

// Computed
const classList = computed(() => [
    {
        '--is-disabled': props.disabled,
        '--is-error': props.error,
        '--is-visible': isVisible.value,
        '--is-outline': props.outline,
        [`--is-${props.size}`]: props.size,
        [`--is-${props.color}`]: props.color,
    },
]);

const inputType = computed(() => isVisible.value ? 'text' : 'password');

const icon = computed(() => isVisible.value ? 'eye-open' : 'eye-close');

// Watch
watch(() => props.modelValue, (val: string | number | null) => {
    actualValue.value = val;
}, {
    immediate: true,
});

// Methods
function onInput(e: Event) {
    emit('input', e);
    emit('update:error', null);
}
</script>

<style lang="scss">
@import "UiPasswordInput";
</style>
