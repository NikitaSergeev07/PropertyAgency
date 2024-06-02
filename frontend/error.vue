<template>
    <div class="ErrorPage">
        <div :class="$style.wrapper">
            <img
                :src="getErrorValue.imagePath"
                loading="lazy"
                :class="$style.image"
                alt=""
            />

            <aside :class="$style.aside">
                <h1 :class="$style.title">
                    {{ getErrorValue.title }}
                </h1>

                <div :class="$style.description">
                    {{ getErrorDescription }}
                </div>

                <UiButton
                    size="large"
                    :class="$style.button"
                    @click="handleError"
                >
                    Вернуться
                </UiButton>
            </aside>
        </div>
    </div>
</template>

<script setup lang="ts">
// Types
import type { NuxtError } from '#app';

// Props
const { error } = withDefaults(defineProps<{
    error: NuxtError
}>(), {
    error: () => ({}) as NuxtError,
});

interface ErrorDescription {
    1: string;
    2: string;
    3: string;
}

interface ErrorValue {
    404: {
        title: string,
        description: ErrorDescription,
        imagePath: string;
    }
}

// Data
const randomKeyValue = Math.floor(Math.random() * 3) + 1;
const errorValues: ErrorValue = {
    404: {
        title: '404',
        description: {
            1: 'Страница не найдена. Возможно ее просто никогда и не существовало 👾',
            2: 'Прилетело НЛО и украло эту страницу 👽',
            3: 'Кто-то украл эту страницу и поэтому мы не можем отобразить ее содержимое 🥷',
        },
        imagePath: `https://storage.yandexcloud.net/daily-flex/illustrations/404-${randomKeyValue}.svg`,
    },
};

// Computed
const getErrorValue = computed(() => errorValues[error.statusCode as keyof ErrorValue]);
const getErrorDescription = computed(() => getErrorValue.value.description[randomKeyValue as keyof ErrorDescription]);

// Methods
const handleError = () => clearError({ redirect: '/' });
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
        font-size: mul($unit, 40);
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
