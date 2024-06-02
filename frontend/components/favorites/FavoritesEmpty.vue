<template>
    <div class="FavoritesEmpty">
        <div :class="$style.wrapper">
            <img
                src="https://storage.yandexcloud.net/daily-flex/illustrations/empty.svg"
                alt=""
                loading="lazy"
                :class="$style.image"
            />

            <main :class="$style.content">
                <h1 :class="$style.title">А тут ничего нет...</h1>

                <div :class="$style.description">
                    Выберите идеальную квартиру
                </div>

                <UiButton
                    to="/"
                    :class="$style.button"
                >
                    Выбрать квартиру
                </UiButton>
            </main>
        </div>
    </div>
</template>

<script setup lang="ts">

</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;
        row-gap: mul($unit, 10);
    }

    .image {
        width: mul($unit, 100);
    }

    .content {
        display: flex;
        flex-direction: column;
        align-items: center;
        row-gap: mul($unit, 4);
        text-align: center;
    }

    .title {
        font-size: mul($unit, 9);
        line-height: mul($unit, 10);
        font-weight: 600;
    }

    .description {
        @include text-m;

        color: $gray-500;
    }

    .button {
        min-width: mul($unit, 30);
        height: mul($unit, 12);
    }
</style>
