<template>
    <div class="PropertyCard">
        <div :class="[$style.wrapper, classList]">
            <img
                src="https://images.cdn-cian.ru/images/kvartira-mytishci-astrahova-prospekt-2177685667-4.jpg"
                alt=""
                loading="lazy"
                :class="$style.image"
            />

            <QBtn
                round
                outline
                :class="$style.favorite"
                @click="emit(inFavorite ? 'remove-favorite' : 'add-favorite');"
            >
                <QIcon
                    :name="inFavorite ? 'favorite' : 'favorite_border'"
                    size="16px"
                    :class="$style.icon"
                />
            </QBtn>

            <main :class="$style.content">
                <h4 v-if="title" :class="$style.title">
                    {{ title }}
                </h4>

                <h6 :class="$style.flat">
                    <span>{{ roomCount === 0 ? 'Студия' : 'Квартира' }}</span>
                    <span></span>
                </h6>

                <div v-if="address" :class="$style.address">
                    <span v-if="address.state">{{ address.state }}, </span>
                    <span v-if="address.city">{{ address.city }}, </span>
                    <span v-if="address.street">{{ address.street }}</span>
                </div>
            </main>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { PropertyAddress } from 'assets/types/property';

const emit = defineEmits([
    'add-favorite',
    'remove-favorite',
]);

const props = withDefaults(defineProps<{
    title: string | null;
    description: string | null;
    price: number;
    address?: Partial<PropertyAddress>,
    roomCount: number;
    status: string | null;
    inFavorite: boolean,
}>(), {
    title: null,
    description: null,
    price: 0,
    roomCount: 0,
    status: null,
    address: () => ({}),
    inFavorite: false,
});

const classList = computed(() => [
    {
        '--in-favorite': props.inFavorite,
    },
]);
</script>

<style lang="scss" module>
    .wrapper {
        position: relative;
        overflow: hidden;
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
        row-gap: mul($unit, 3);
        border-radius: 12px;

        &:global(.--in-favorite) {
            .icon {
                color: $error-500;
            }
        }
    }

    .image {
        width: 320px;
        height: 280px;
    }

    .favorite {
        position: absolute;
        top: mul($unit, 3);
        right: mul($unit, 3);
        z-index: 3;

        &:before {
            border: none;
            background-color: rgba($black, .4);
        }
    }

    .icon {
        color: $white;
        transition: color .3s ease;
    }

    .content {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 2);
    }

    .title {
        @include text-xl;

        font-weight: 500;
    }

    .flat {
        @include text-m;

        font-weight: 500;
    }

    .address {
        @include text-s;

        color: $gray-400;
    }
</style>
