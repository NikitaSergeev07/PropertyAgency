<template>
    <footer class="TheFooter">
        <div :class="$style.wrapper">
            <div :class="$style.container" class="container">
                <img
                    src="https://storage.yandexcloud.net/daily-flex/lian/logo_clean.svg"
                    alt=""
                    loading="lazy"
                    :class="$style.image"
                />

                <ul :class="$style.list">
                    <li
                        v-for="social in socialList"
                        :key="social.id"
                    >
                        <UiLink
                            tag="a"
                            href="https://www.youtube.com/watch?v=dQw4w9WgXcQ"
                            target="_blank"
                            :class="$style.linkIcon"
                        >
                            <UiIcon :name="social.icon" />
                        </UiLink>
                    </li>
                </ul>
            </div>
        </div>
    </footer>
</template>

<script setup lang="ts">
const socialList = [
    { id: 'vk', icon: 'vk' },
    { id: 'telegram', icon: 'telegram' },
    { id: 'youtube', icon: 'youtube' },
];
</script>

<style lang="scss" module>
    .wrapper {
        padding: mul($unit, 11) 0;
        background-color: $gray-50;
    }

    .container {
        display: flex;
        align-items: center;
    }

    .image {
        width: 48px;
    }

    .list {
        display: flex;
        margin-left: auto;
        column-gap: $unit;
    }

    .linkIcon {
        flex-shrink: 0;
        display: flex;
        justify-content: center;
        width: 48px;
        height: 48px;
        border-radius: 50%;
        transition: all .3s ease;

        @include hover {
            background-color: $primary-100;
        }
    }
</style>
