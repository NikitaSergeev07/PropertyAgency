<template>
    <q-input
        v-bind="$attrs"
        :model-value="props.modelValue"
        outlined
        stack-label
        dense
        :error="error"
        :label="label"
        :rules="rules"
        input-class="UiInput__inputField"
        class="UiInput"
        :class="classList"
        @update:model-value="emit('update:modelValue', $event)"
    >
        <template
            v-for="(_, slot) in ($slots as Readonly<QInputSlots>)"
            #[slot]="scope"
        >
            <slot
                :name="slot"
                v-bind="scope || {}"
            />
        </template>
    </q-input>
</template>

<script setup lang="ts">
// Types
import type { QInputSlots, ValidationRule } from 'quasar';

// Props
const props = withDefaults(defineProps<{
    modelValue?: string | number | null
    label?: string
    required?: boolean
    rules?: ValidationRule[] | undefined
    error?: boolean;
    size?: 'xs-small' | 'medium';
}>(), {
    modelValue: null,
    label: '',
    required: false,
    ValidationRule: undefined,
    rules: undefined,
    error: false,
    size: 'medium',
});

// Emits
const emit = defineEmits([
    'update:modelValue',
]);

// Computed
const classList = computed(() => [
    {
        _isRequired: props.required,
        [`--${props.size}-size`]: props.size,
    },
]);
</script>

<style lang="scss">
@import "UiInput";
</style>
