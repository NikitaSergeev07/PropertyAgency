<template>
    <div class="IndexPage">
        <div :class="$style.wrapper">
            <PromoBanner/>

            <PropertyBlock
                v-if="data?.propertyList?.length"
                :property-list="data.propertyList"
                :favorites="data.favorites"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import PromoBanner from '~/components/PromoBanner/PromoBanner.vue';
import PropertyBlock from '~/components/Properties/PropertyBlock.vue';

const { fetchPropertyList, fetchFavoriteList } = usePropertyStore();

const { data } = useAsyncData('IndexPage', async () => {
    /**
     * Отправляем запрос за списком квартир и списком квартир которые в избранном
     */
    const [
        propertyList,
        favorites,
    ] = await Promise.all([
        fetchPropertyList(),
        fetchFavoriteList(),
    ]);

    let favoritesDict = {};

    /**
     * Проверяем список избранных на наличие
     * Если список не пустой, то перебираем массив и формируем объект
     * Где ключ - это ID квартиры, а значение это ID Favorite
     */
    if (favorites && favorites.length) {
        favoritesDict = favorites?.reduce((acc: string[], item: any) => ({
            ...acc,
            ...item?.propertyId && {
                [item.propertyId]: item.id,
            },
        }), {});
    }

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
