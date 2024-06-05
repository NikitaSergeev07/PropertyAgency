<template>
    <QDialog ref="dialogRef" class="UiModalConfirm">
        <div class="UiModalConfirm__wrapper">

            <header class="UiModalConfirm__header">
                <div class="UiModalConfirm__iconWrapper">
                    <QIcon
                        name="warning"
                        size="custom"
                        class="UiModalConfirm__icon"
                    />
                </div>
            </header>

            <main v-if="title || text" class="UiModalConfirm__content">
                <h3
                    v-if="title"
                    class="UiModalConfirm__title"
                    v-html="title"
                >
                </h3>

                <div
                    v-if="text"
                    class="UiModalConfirm__text"
                    v-html="text"
                >
                </div>
            </main>

            <footer class="UiModalConfirm__footer">
                <UiButton
                    color="grey-2"
                    size="medium"
                    class="UiModalConfirm__button"
                    @click="onDialogCancel"
                >
                    Отмена
                </UiButton>

                <UiButton
                    class="UiModalConfirm__button"
                    @click="onDialogOK"
                >
                    Подтвердить
                </UiButton>
            </footer>
        </div>
    </QDialog>
</template>

<script setup lang="ts">
// Emit
defineEmits([
    ...useDialogPluginComponent.emits,
]);

// Data
const { dialogRef, onDialogCancel, onDialogOK } = useDialogPluginComponent();

// Props
withDefaults(defineProps<{
    title: string | null;
    text: string | null;
}>(), {
    title: null,
    text: null,
});
</script>

<style lang="scss">
    .UiModalConfirm {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 100%;

        $modal: &;

        &__wrapper {
            position: relative;
            overflow: hidden;
            display: flex;
            flex-direction: column;
            width: mul($unit, 125);
            padding: mul($unit, 6);
            row-gap: mul($unit, 6);
            border-radius: $border-radius;
            background-color: $white;
        }

        &__header {
            position: relative;
            align-self: center;
            width: 64px;
            height: 64px;

            &:after {
                content: '';
                position: absolute;
                top: 50%;
                left: 50%;
                z-index: 2;
                width: calc(100% + 12px);
                height: calc(100% + 12px);
                border-radius: 50%;
                background-color: $warning-50;
                transform: translate(-50%, -50%);
            }
        }

        &__iconWrapper {
            position: relative;
            z-index: 3;
            display: flex;
            align-items: center;
            justify-content: center;
            width: 100%;
            height: 100%;
            border-radius: 50%;
            background: $warning-100;
            color: $warning-500;
        }

        &__icon {
            font-size: mul($unit, 9);
            margin-top: mul($unit, -1);
        }

        &__content {
            display: flex;
            flex-direction: column;
            row-gap: mul($unit, 3);
            text-align: center;
        }

        &__title {
            font-weight: 500;
            color: $black;
        }

        &__text {
            @include text-s;

            color: $gray-500;
        }

        &__footer {
            position: relative;
            display: flex;
            align-items: center;
            padding-top: 24px;
            column-gap: mul($unit, 6);

            &:before {
                content: '';
                position: absolute;
                top: 0;
                left: 50%;
                width: calc(100% + 48px);
                height: 1px;
                background: $gray-200;
                transform: translateX(-50%);
            }
        }

        &__button {
            width: 50%;
            height: mul($unit, 12);

            span {
                font-weight: 500;
            }

            &:nth-child(1) {
                span {
                    color: $gray-500;
                }
            }
        }

        .q-dialog__backdrop {
            backdrop-filter: blur($unit);
        }
    }
</style>
