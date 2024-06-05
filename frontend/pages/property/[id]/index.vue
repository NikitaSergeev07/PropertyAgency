<template>
    <div class="PropertyDetailPage">
        <div :class="[$style.wrapper, classList]">

            <div :class="$style.container" class="container">
                <div :class="$style.left">
                    <header :class="$style.header">
                        <h3 v-if="propertyDetail.title" :class="$style.title">
                            {{ propertyDetail.title }}
                        </h3>

                        <div v-if="propertyDetail.address" :class="$style.addressWrapper">
                            <div :class="$style.address">
                                <span v-if="propertyDetail.address.state">{{ propertyDetail.address.state }}, </span>
                                <span v-if="propertyDetail.address.city">г. {{ propertyDetail.address.city }}, </span>
                                <span v-if="propertyDetail.address.street">ул. {{ propertyDetail.address.street }}</span>
                            </div>

                            <UiLink
                                size="small-s"
                                :class="$style.mapLink"
                            >
                                на карте
                            </UiLink>
                        </div>
                    </header>

                    <main :class="$style.content">
                        <PropertyDetailGallery
                            :images="propertyDetail.images"
                            :class="$style.gallery"
                        />

                        <div v-if="propertyDetail.description" :class="$style.description">
                            {{ propertyDetail.description }}
                        </div>
                    </main>
                </div>

                <aside :class="$style.aside">
                    <div :class="$style.top">
                        <h4 v-if="propertyDetail.roomCount !== null" :class="$style.rooms">
                            {{ !propertyDetail.roomCount ? 'Студия' : `${propertyDetail.roomCount}-комнатная` }}
                        </h4>

                        <h4 v-if="propertyDetail.price" :class="$style.price">
                            {{ splitThousands(propertyDetail.price) }} ₽
                        </h4>
                    </div>

                    <div :class="$style.bottom">
                        <UiButton
                            v-if="PROPERTY_STATUS_DISPLAY[propertyDetail.status]"
                            color="grey-4"
                            :class="$style.button"
                        >
                            {{ PROPERTY_STATUS_DISPLAY[propertyDetail.status] }}
                        </UiButton>

                        <UiButton
                            size="custom"
                            color="custom"
                            :class="$style.favorite"
                            @click="onClickFavorite"
                        >
                            <QIcon
                                :name="favoriteId ? 'favorite' : 'favorite_border'"
                                size="16px"
                                :class="$style.icon"
                            />

                            <QTooltip
                                anchor="top middle"
                                self="bottom middle"
                                :offset="[10, 10]"
                                :class="$style.tooltipText"
                            >
                                {{ favoriteId ? 'Удалить' : 'Добавить' }}
                            </QTooltip>
                        </UiButton>
                    </div>
                </aside>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { splitThousands } from '~/assets/ts/utils/numbers';

const { user } = useCommonStore();
const { propertyDetail } = storeToRefs(usePropertyStore());
const { fetchPropertyDetail, fetchFavoriteList, addPropertyToFavorite, removePropertyFromFavorite } = usePropertyStore();

const route = useRoute();
const favoriteId = ref(null);

const PROPERTY_STATUS_TYPES = {
    BOOKING: 'booking',
    SOLD: 'sold',
};
const PROPERTY_STATUS_DISPLAY = {
    [PROPERTY_STATUS_TYPES.BOOKING]: 'Забронирована',
    [PROPERTY_STATUS_TYPES.SOLD]: 'Продана',
};

const { data } = useAsyncData('PropertyDetailPage', async () => {
    const { id: propertyId } = route.params;

    const res = await fetchPropertyDetail(propertyId as string);
    const favorites = await fetchFavoriteList();

    const favorite = favorites?.find((f: any) => f.propertyId === res.id) || {};

    return {
        favoriteId: favorite.id || null,
    };
});

const classList = computed(() => [{
    '--in-favorite': favoriteId.value,
}]);

watch(() => data.value?.favoriteId, (val: string | null) => {
    if (val) {
        favoriteId.value = data.value?.favoriteId;
    }
}, { immediate: true });

const onClickFavorite = async () => {
    if (favoriteId.value) {
        await removePropertyFromFavorite(favoriteId.value).then(() => {
            favoriteId.value = null;
        });
    } else {
        favoriteId.value = await addPropertyToFavorite({
            userId: user?.id || '',
            propertyId: propertyDetail.value.id || '',
        });
    }
};
</script>

<style lang="scss" module>
    .wrapper {
        width: 100%;
        padding-top: mul($unit, 12);

        &:global(.--in-favorite) {
            .favorite {
                background-color: $error-50;
            }
        }
    }

    .container {
        display: flex;
        align-items: flex-start;
        column-gap: mul($unit, 10);
    }

    .left {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 10);
    }

    .header {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 4);
    }

    .title {
        font-size: 48px;
        font-weight: 500;
    }

    .addressWrapper {
        display: flex;
        align-items: center;
        column-gap: mul($unit, 2);
    }

    .address {
        @include text-s;

        color: $gray-500;
    }

    .mapLink {
        padding: 0;
        line-height: 128%;
    }

    .content {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 12);
    }

    .gallery {
        width: mul($unit, 195);
    }

    .description {
        @include text-m;
    }

    .aside {
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        width: mul($unit, 90);
        margin-left: auto;
        padding: mul($unit, 6);
        row-gap: mul($unit, 6);
        border-radius: mul($unit, 2);
        background-color: $white;
        box-shadow: 0 4px 16px rgba(0, 0, 0, .08);
    }

    .top {
        display: flex;
        align-items: center;
        width: 100%;
        column-gap: mul($unit, 3);
    }

    .rooms,
    .price {
        font-weight: 600;
    }

    .price {
        margin-left: auto;
    }

    .bottom {
        display: flex;
        align-items: center;
        width: 100%;
        padding-top: mul($unit, 6);
        column-gap: mul($unit, 2);
        border-top: 1px solid $gray-200;
    }

    .button {
        flex-grow: 1;
        height: mul($unit, 11);
    }

    .favorite {
        width: mul($unit, 11);
        height: mul($unit, 11);
        margin-left: auto;
        padding: mul($unit, 2);
        border-radius: mul($unit, 2);
        border: 1px solid $error-50;
        background-color: transparent;
        transition: background-color .3s ease;

        @include hover {
            background-color: $error-50;
        }

        &:before {
            content: none;
        }
    }

    .icon {
        color: $error-600;
    }

    .tooltipText {
        @include text-xs;
    }
</style>
