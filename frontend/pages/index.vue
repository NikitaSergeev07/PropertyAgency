<template>
    <div class="IndexPage">
        <div :class="$style.wrapper">
            <PromoBanner/>

            <PropertyBlock
                v-if="data?.propertyList?.length"
                v-bind="data"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Favorites } from '~/assets/types/property';

import PromoBanner from '~/components/PromoBanner/PromoBanner.vue';
import PropertyBlock from '~/components/property/PropertyBlock.vue';

const { fetchPropertyList } = usePropertyStore();

const { data } = useAsyncData('IndexPage', async () => {
    /**
     * Отправляем запрос за списком квартир
     */
    const propertyList = await fetchPropertyList();

    /**
     * Вспомогательная функуция для обработки массива favorites у каждой сущности Property
     */
    const getFavoritesDictFromProperty = (favorites: Favorites[]) => {
        if (!favorites?.length) {
            return {};
        }

        return favorites.reduce((acc, item) => ({
            ...acc,
            [item.propertyId]: item.id,
        }), {});
    };

    /**
     * Формируем объект квартир, которые были добавлены в избранное
     * Где ключ - это ID квартиры, а значение это ID Favorite
     */
    const favoritesDict = propertyList?.reduce((acc: string[], property: any) => ({
        ...acc,
        ...property?.favorites?.length
            ? getFavoritesDictFromProperty(property.favorites)
            : [],
    }), {});

    return {
        propertyList,
        favorites: favoritesDict,
    };
});
</script>

<style lang="scss" module>
    .wrapper {
        width: 100%;
    }
</style>
