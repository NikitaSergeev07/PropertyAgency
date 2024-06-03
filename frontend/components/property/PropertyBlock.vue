<template>
    <div class="PropertyBlock">
        <div :class="[$style.wrapper, 'container']">
            <ul :class="$style.list">
                <li
                    v-for="property in propertyList"
                    :key="property.id"
                >
                    <PropertyCard
                        :title="property.title"
                        :description="property.description"
                        :address="property.address"
                        :status="property.status"
                        :price="property.price"
                        :in-favorite="Boolean(favoritesDict[property.id])"
                        :room-count="property.roomCount"
                        @click="onNavigateToDetail(property.id)"
                        @add-favorite="addToFavorite(property.id)"
                        @remove-favorite="removeFromFavorite(property.id)"
                    />
                </li>
            </ul>
        </div>
    </div>
</template>

<script setup lang="ts">
import PropertyCard from '~/components/property/PropertyCard.vue';

const { propertyList, favorites } = withDefaults(defineProps<{
    propertyList: any[];
    favorites: Record<string, string>,
}>(), {
    propertyList: () => [],
    favorites: () => ({}),
});

const { $router } = useNuxtApp();
const { user } = useCommonStore();
const { addPropertyToFavorite, removePropertyFromFavorite } = usePropertyStore();
const favoritesDict = ref(favorites);

const onNavigateToDetail = async (propertyId: string) => {
    await $router.push({ path: `/property/${propertyId}` });
};

const addToFavorite = async (propertyId: string) => {
    const res = await addPropertyToFavorite({
        propertyId,
        userId: user?.id || '',
    });

    if (!res) {
        return false;
    }

    favoritesDict.value[propertyId] = res;
};

const removeFromFavorite = async (propertyId: string) => {
    const res = await removePropertyFromFavorite(favoritesDict.value[propertyId]);

    if (!res) {
        return false;
    }

    delete favoritesDict.value[propertyId];
};
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        flex-direction: column;
        padding: mul($unit, 8) 0;
        row-gap: mul($unit, 8);
    }

    .list {
        display: flex;
        flex-wrap: wrap;
        row-gap: mul($unit, 6);
        column-gap: mul($unit, 6);
    }

    .item {
        width: mul($unit, 80);
    }

    .card {
        width: 100%;
    }
</style>
