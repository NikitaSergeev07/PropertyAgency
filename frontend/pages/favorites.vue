<template>
    <div class="FavoritesPage">
        <div :class="$style.wrapper">
            <FavoritesEmpty
                v-if="!favoriteList.length"
                :class="$style.empty"
            />

            <PropertyBlock
                v-else
                :property-list="favoriteList"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import FavoritesEmpty from '~/components/favorites/FavoritesEmpty.vue';
import PropertyBlock from '~/components/Properties/PropertyBlock.vue';

const { favoriteList } = storeToRefs(usePropertyStore());
const { fetchFavoriteList } = usePropertyStore();

useAsyncData('FavoritePage', async () => await fetchFavoriteList());
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
