<template>
    <div class="FavoritesPage">
        <div :class="$style.wrapper">
            <FavoritesEmpty
                v-if="!data?.propertyList?.length"
                :class="$style.empty"
            />

            <PropertyBlock
                v-else
                :property-list="data.propertyList"
                :favorites="data.favorites as any"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import FavoritesEmpty from '~/components/favorites/FavoritesEmpty.vue';
import PropertyBlock from '~/components/property/PropertyBlock.vue';

const { fetchFavoriteList } = usePropertyStore();

const { data } = useAsyncData('FavoritePage', async () => {
    /**
     * Отправляем запрос за списком квартир которые в избранном
     */
    const favorites = await fetchFavoriteList();

    if (!favorites && !favorites.length) {
        return {
            propertyList: [],
            favorites: {},
        };
    }

    let list = [];
    let favoritesDict = {};

    /**
     * Проверяем список избранных на наличие
     * Если список не пустой, то перебираем массив и формируем объект
     * Где ключ - это ID квартиры, а значение это ID Favorite
     * А так же в одном переборе формируем список квартир для отображения на странице
     */
    if (favorites && favorites.length) {
        list = favorites.reduce((acc: any, item: any) => {
            favoritesDict = {
                ...favoritesDict,
                ...item?.propertyId && {
                    [item.propertyId]: item.id,
                },
            };

            acc.push(item.property);

            return acc;
        }, []);
    }

    return {
        propertyList: list,
        favorites: favoritesDict,
    };
});
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        flex-direction: column;
        justify-content: center;
        width: 100%;
        height: 100%;
    }

    .empty {
        margin-top: mul($unit, 30);
    }
</style>
