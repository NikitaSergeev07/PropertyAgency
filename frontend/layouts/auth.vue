<template>
    <div class="AuthLayout">
        <div :class="$style.wrapper">
            <div :class="$style.container">
                <main :class="$style.content">
                    <slot />
                </main>

                <aside :class="$style.aside">
                    <img
                        src="https://storage.yandexcloud.net/daily-flex/illustrations/ill-login.svg"
                        loading="lazy"
                        :class="$style.image"
                        alt=""
                    />

                    <h3 :class="$style.title">Вход в систему</h3>

                    <div :class="$style.text">
                        Для того чтобы воспользоваться нашим сервисом Вам надо авторизоваться
                    </div>
                </aside>
            </div>
        </div>
    </div>
</template>

<style lang="scss" module>
    $background: #f8fbff;

    .wrapper {
        position: relative;
        display: flex;
        width: 100%;
        min-height: 100vh;
    }

    .container {
        position: relative;
        display: flex;
        align-items: center;
        width: 100%;
        background-color: $background;
    }

    .content {
        display: flex;
        flex-direction: column;
        justify-content: center;
        width: mul($unit, 140);
        height: 100%;
        padding: mul($unit, 10) mul($unit, 13);
        background-color: $white;
    }

    .aside {
        display: flex;
        flex-direction: column;
        width: mul($unit, 100);
        margin: auto;
        row-gap: mul($unit, 6);
        text-align: center;
    }

    .image {
        width: 100%;
        height: mul($unit, 100);
    }

    .title {
        font-weight: 600;
    }

    .text {
        @include text-m;

        color: $gray-500;
    }
</style>
