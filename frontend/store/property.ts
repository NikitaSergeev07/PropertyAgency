import type { Property } from '~/assets/types/property';

interface AddToFavorite {
    userId: string;
    propertyId: string;
}

interface FavoriteItemResponse {
    property: Property;
}

export const usePropertyStore = defineStore('property', () => {
    const { $request } = useNuxtApp();
    const propertyList = ref<Property[]>([]);
    const favoriteList = ref([]);

    const fetchPropertyList = async () => {
        const { data } = await $request.$get('/Properties');

        if (data) {
            propertyList.value = data;
        }
    };

    const addPropertyToFavorite = async ({ userId, propertyId }: AddToFavorite) => {
        try {
            await $request.$post('/Favorites', { userId, propertyId });

            return true;
        } catch (e) {
            console.log('PROPERTY_STORE:ADD_PROPERTY_TO_FAVORITE:', e);

            return false;
        }
    };

    const removePropertyFromFavorite = async (favoriteId: string) => {
        try {
            await $request.$delete(`/Favorites/${favoriteId}`);

            return true;
        } catch (e) {
            console.log('PROPERTY_STORE:REMOVE_PROPERTY_FROM_FAVORITE:', e);

            return false;
        }
    };

    const fetchFavoriteList = async () => {
        const { data } = await $request.$get('/Favorites');

        if (data && data.length) {
            favoriteList.value = data.reduce((acc: any[], item: FavoriteItemResponse) => {
                if (!acc.find(p => p.id === item.property.id)) {
                    acc.push(item.property);
                }

                return acc;
            }, []);
        }
    };

    return {
        propertyList,
        fetchPropertyList,
        addPropertyToFavorite,
        removePropertyFromFavorite,
        // Favorite
        favoriteList,
        fetchFavoriteList,
    };
});
