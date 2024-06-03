<template>
    <div class="AuthInformer">
        <div :class="$style.wrapper">
            <img
                :src="imagePath"
                loading="lazy"
                :class="$style.image"
                alt=""
            />

            <aside :class="$style.aside">
                <h1 :class="$style.title">
                    {{ title }}
                </h1>

                <div :class="$style.description">
                    {{ text }}
                </div>

                <UiButton
                    :to="{ name: redirectUrlName }"
                    size="large"
                    :class="$style.button"
                >
                    {{ buttonText }}
                </UiButton>
            </aside>
        </div>
    </div>
</template>

<script setup lang="ts">
withDefaults(defineProps<{
    title?: string;
    text?: string;
    buttonText?: string | null;
    redirectUrlName?: string | null;
}>(), {
    title: 'Вы не авторизованы',
    text: 'Для продолжения работы в системе, Вам необходимо войти в систему',
    buttonText: 'Авторизоваться',
    redirectUrlName: 'login',
});

const randomKeyValue = Math.floor(Math.random() * 3) + 1;
const imagePath = `https://storage.yandexcloud.net/daily-flex/illustrations/404-${randomKeyValue}.svg`;
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100vh;
        column-gap: mul($unit, 15);
    }

    .image {
        width: mul($unit, 130);
        height: mul($unit, 110);
    }

    .aside {
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        width: mul($unit, 100);
        row-gap: mul($unit, 2);
    }

    .title {
        font-size: mul($unit, 14);
        font-weight: 500;
        line-height: 1;
    }

    .description {
        font-size: mul($unit, 4.5);
        font-weight: 500;
        color: $gray-500;
    }

    .button {
        min-width: mul($unit, 40);
        margin-top: mul($unit, 6);

        span {
            @include text-m;

            line-height: 1;
            font-weight: 500;
        }
    }
</style>
