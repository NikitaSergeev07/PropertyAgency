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
                        :in-favorite="Boolean(favoriteDict[property.id])"
                        :room-count="property.roomCount"
                        @add-favorite="addToFavorite(property.id)"
                        @remove-favorite="removeFromFavorite(property.id)"
                    />
                </li>
            </ul>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Property } from '~/assets/types/property';

import PropertyCard from '~/components/Properties/PropertyCard.vue';

const { propertyList } = withDefaults(defineProps<{
    propertyList: any[];
}>(), {
    propertyList: () => [],
});

const { user } = useCommonStore();
const { addPropertyToFavorite, removePropertyFromFavorite } = usePropertyStore();
const favoriteDict = ref(propertyList?.reduce((acc: string[], item: Property) => ({
    ...acc,
    ...item?.favorites?.length
        ? item.favorites.reduce((fAcc, fItem) => ({
            ...fAcc,
            [fItem.propertyId]: fItem.id,
        }), {})
        : {},
}), {}));

const addToFavorite = async (propertyId: string) => {
    const res = await addPropertyToFavorite({
        propertyId,
        userId: user?.id || '',
    });

    if (!res) {
        return false;
    }

    // TODO: Поправить на запись значения Favorite ID
    favoriteDict.value[propertyId] = true;
};

const removeFromFavorite = async (propertyId: string) => {
    const res = await removePropertyFromFavorite(favoriteDict.value[propertyId]);

    if (!res) {
        return false;
    }

    delete favoriteDict.value[propertyId];
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
        width: 320px;
    }

    .card {
        width: 100%;
    }
</style>
