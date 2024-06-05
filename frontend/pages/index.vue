<template>
    <div class="IndexPage">
        <div :class="$style.wrapper">
            <PromoBanner/>

            <PropertyBlock
                v-if="propertyList?.length"
                :property-list="propertyList"
                :favorites="favoritesDict"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Favorites } from '~/assets/types/property';

import PromoBanner from '~/components/PromoBanner/PromoBanner.vue';
import PropertyBlock from '~/components/property/PropertyBlock.vue';


const { propertyList } = storeToRefs(usePropertyStore());
const { fetchPropertyList } = usePropertyStore();

useAsyncData('IndexPage', async () => {
    /**
     * Отправляем запрос за списком квартир
     */
    await fetchPropertyList();
});

const favoritesDict = computed(() => {
    if (!propertyList.value.length) {
        return {};
    }
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
    return propertyList.value?.reduce((acc: string[], property: any) => ({
        ...acc,
        ...property?.favorites?.length
            ? getFavoritesDictFromProperty(property.favorites)
            : [],
    }), {});
});
</script>

<style lang="scss" module>
    .wrapper {
        width: 100%;
    }
</style>
