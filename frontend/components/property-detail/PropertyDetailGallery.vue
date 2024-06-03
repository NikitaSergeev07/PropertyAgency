<template>
    <div class="PropertyDetailGallery">
        <div :class="$style.wrapper">
            <div
                ref="swiper"
                class="swiper"
                :class="$style.slider"
            >
                <div class="swiper-wrapper">
                    <div
                        v-for="(slide, index) in gallery"
                        :key="`slide__${index}`"
                        :class="$style.slide"
                        class="swiper-slide"
                    >
                        <img
                            :src="slide.src"
                            alt=""
                            loading="lazy"
                            :class="$style.image"
                        />
                    </div>
                </div>
            </div>

            <UiButton
                rounded
                size="custom"
                :class="$style.arrowLeft"
                class="swiper-arrow-left"
            >
                <QIcon
                    name="chevron_left"
                />
            </UiButton>

            <UiButton
                rounded
                size="custom"
                class="swiper-arrow-right"
                :class="$style.arrowRight"
            >
                <QIcon
                    name="chevron_right"
                />
            </UiButton>
        </div>
    </div>
</template>

<script setup lang="ts">
import { Swiper } from 'swiper';
import { Navigation } from 'swiper/modules';

const gallery = [
    { src: 'https://images.cdn-cian.ru/images/2141709541-1.jpg' },
    { src: 'https://images.cdn-cian.ru/images/2141709561-1.jpg' },
    { src: 'https://images.cdn-cian.ru/images/2141709573-1.jpg' },
    { src: 'https://images.cdn-cian.ru/images/2141709584-1.jpg' },
];

const slider = ref<any>(null);
const swiper = ref<any>(null);

const initSwiper = () => {
    slider.value = new Swiper(swiper.value, {
        modules: [
            Navigation,
        ],

        navigation: {
            prevEl: '.swiper-arrow-left',
            nextEl: '.swiper-arrow-right',
        },

        speed: 550,
    });
};

onMounted(() => initSwiper());
</script>

<style lang="scss" module>
    .wrapper {
        position: relative;
        width: 100%;

        @include hover {
            .arrowLeft,
            .arrowRight {
                opacity: 1;
            }
        }
    }

    .arrowLeft,
    .arrowRight {
        position: absolute;
        top: 50%;
        z-index: 2;
        width: 40px;
        height: 40px;
        padding: 0;
        transform: translateY(-50%);
    }

    .arrowLeft {
        left: 0;
    }

    .arrowRight {
        right: 0;
    }

    .slider {
        width: 100%;
    }

    .slide {
        width: 100%;
    }

    .image {
        width: 100%;
        height: mul($unit, 120);
        object-fit: contain;
    }
</style>
