<template>
    <div class="PromoBanner">
        <div :class="$style.wrapper">
            <img
                src="https://storage.yandexcloud.net/daily-flex/lian/index-rk-05-28.png"
                alt=""
                loading="lazy"
                :class="$style.image"
            />

            <main :class="$style.content">
                <PromoBannerTabs
                    v-model="activeTab"
                    :tabs="tabs"
                />

                <div :class="$style.form">
                    <PromoBannerFilterFlat/>
                    <PromoBannerFilterRooms />
                </div>
            </main>
        </div>
    </div>
</template>

<script setup lang="ts">
import PromoBannerTabs from '~/components/PromoBanner/PromoBannerTabs.vue';

const tabs = [
    {
        id: 'sale',
        name: 'Купить',
    },
];

const activeTab = ref(0);

</script>

<style lang="scss" module>
    .wrapper {
        position: relative;
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 500px;
    }

    .content {
        position: relative;
        z-index: 2;
    }

    .form {
        display: flex;
        align-items: center;
    }

    .image {
        position: absolute;
        top: 0;
        right: 0;
        bottom: 0;
        left: 0;
        width: 100%;
        height: 100%;
        object-fit: cover;
        pointer-events: none;
    }
</style>
