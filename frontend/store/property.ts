import type { Property } from '~/assets/types/property';

interface AddToFavorite {
    userId: string;
    propertyId: string;
}

export const usePropertyStore = defineStore('property', () => {
    const { $request } = useNuxtApp();
    const propertyList = ref<Property[]>([]);

    const fetchPropertyList = async () => {
        try {
            const { data } = await $request.$get('/Properties');

            if (data) {
                propertyList.value = data;
            }
        } catch (e) {
            console.log('PROPERTY_STORE:FETCH_PROPERTY_LIST:', e);
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

    return {
        propertyList,
        fetchPropertyList,
        addPropertyToFavorite,
        removePropertyFromFavorite,
    };
});
