<template>
    <div class="PropertyCard">
        <div :class="[$style.wrapper, classList]" @click="emit('click')">
            <img
                src="./uploads/6658c732-4008-4e26-b0f8-e180d0028751.jpeg"
                alt=""
                loading="lazy"
                :class="$style.image"
            />

            <QBtn
                v-if="hasFavorite"
                round
                outline
                :class="$style.favorite"
                @click.stop="emit(inFavorite ? 'remove-favorite' : 'add-favorite');"
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

                <h6 v-if="price" :class="$style.price">
                    {{ splitThousands(price) }} ₽
                </h6>

                <h6 :class="$style.flat">
                    <span>{{ roomCount === 0 ? 'Студия' : 'Квартира' }}</span>
                    <span v-if="roomCount">{{ roomCount }}-комнатная</span>
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
import { splitThousands } from '../../assets/ts/utils/numbers';
import type { PropertyAddress } from 'assets/types/property';

const emit = defineEmits([
    'click',
    'add-favorite',
    'remove-favorite',
]);

const props = withDefaults(defineProps<{
    title: string | null;
    description: string | null;
    price: number;
    address?: Partial<PropertyAddress>,
    roomCount: number;
    status?: string | null;
    inFavorite?: boolean,
    hasFavorite?: boolean,
}>(), {
    title: null,
    description: null,
    price: 0,
    roomCount: 0,
    status: null,
    address: () => ({}),
    inFavorite: false,
    hasFavorite: true,
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
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
        cursor: pointer;

        &:global(.--in-favorite) {
            .icon {
                color: $error-500;
            }
        }

        @include hover {
            .title {
                color: $primary-500;
            }
        }
    }

    .image {
        width: 320px;
        height: 280px;
        border-radius: 12px 12px 0 0;
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
        padding: mul($unit, 3);
        row-gap: mul($unit, 2);
        border-radius: 0 0 mul($unit, 3) mul($unit, 3);
        border: 1px solid $gray-200;
        border-top: none;
    }

    .title {
        @include text-xl;

        padding-bottom: mul($unit, 3);
        border-bottom: 1px solid $gray-200;
        font-weight: 500;
        color: $black;
        transition: color .3s ease;
    }

    .price,
    .flat {
        @include text-m;

        font-weight: 500;
    }

    .flat {
        display: flex;
        align-items: center;
        column-gap: mul($unit, 2);

        span {
            &:last-of-type {
                order: 3;
            }
        }

        &:after {
            content: '';
            order: 2;
            width: 4px;
            height: 4px;
            border-radius: 50%;
            background-color: $gray-400;
        }
    }

    .address {
        @include text-s;

        color: $gray-400;
    }
</style>
