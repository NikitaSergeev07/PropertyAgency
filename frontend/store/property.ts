import type { Property } from '~/assets/types/property';

interface AddToFavorite {
    userId: string;
    propertyId: string;
}

export const usePropertyStore = defineStore('property', () => {
    const { $request } = useNuxtApp();
    const propertyList = ref<Property[]>([]);
    const propertyDetail = ref<Partial<Property>>({});
    const favoriteList = ref([]);

    const addressList = ref([]);

    const fetchPropertyList = async () => {
        const { data } = await $request.$get('/Properties');

        if (data) {
            propertyList.value = data;

            return data;
        }

        return [];
    };

    const fetchPropertyDetail = async (propertyId: string) => {
        const { data } = await $request.$get(`/Properties/${propertyId}`);

        if (data) {
            propertyDetail.value = data;

            return data;
        }

        return {};
    };

    const addPropertyToFavorite = async ({ userId, propertyId }: AddToFavorite) => {
        try {
            const { data } = await $request.$post('/Favorites', { userId, propertyId });

            return data;
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
            favoriteList.value = data;

            return data;
        }

        return [];
    };

    const createProperty = async (payload: any) => {
        try {
            const { data } = await $request.$post('/Properties', {
                title: payload.title || '',
                description: payload.description || '',
                price: payload.price || '',
                roomCount: payload.roomCount || '',
                status: payload.status || '',
                addressId: payload.addressId || '',
            });

            propertyList.value.push({
                ...data,
                address: data?.address || payload.address,
            });
        } catch (e) {
            console.log('PROPERTY_STORE:CREATE_PROPERTY: ', e);
        }
    };

    const updateProperty = async (propertyId: string, payload: any) => {
        try {
            const { data } = await $request.$patch(`/Properties/${propertyId}`, {
                title: payload.title || '',
                description: payload.description || '',
                price: payload.price || '',
                roomCount: payload.roomCount || '',
                status: payload.status || '',
                addressId: payload.addressId || '',
            });

            const index = propertyList.value.findIndex(p => p.id === propertyId);

            propertyList.value[index] = {
                ...data,

                address: data?.address || payload.address,
            };
        } catch (e) {
            console.log('PROPERTY_STORE:UPDATE_PROPERTY: ', e);
        }
    };

    const deleteProperty = async (propertyId: string) => {
        try {
            const { data } = await $request.$delete(`/Properties/${propertyId}`);

            if (data) {
                const index = propertyList.value.findIndex(p => p.id === propertyId);

                propertyList.value.splice(index, 1);
            }
        } catch (e) {
            console.log('PROPERTY_STORE:UPDATE_PROPERTY: ', e);
        }
    };

    const fetchAddressList = async () => {
        try {
            const { data } = await $request.$get('/Addresses');

            if (data) {
                addressList.value = data;
            }
        } catch (e) {
            console.log('PROPERTY_STORE:FETCH_ADDRESS_LIST: ', e);
        }
    };

    const createAddress = async (payload: any) => {
        try {
            const { data } = await $request.$post('/Addresses', {
                street: payload.street || '',
                city: payload.city || '',
                state: payload.state || '',
                country: payload.country || '',
                zipCode: payload.zipCode || '',
            });

            addressList.value.push(data);

            return data;
        } catch (e) {
            console.log('[PropertyUpdateModal/createNewAddress]: ', e);
            return {};
        }
    };

    const updateImagesProperty = async (propertyId: string, payload: any[]) => {
        if (!payload?.length) {
            return false;
        }

        try {
            const uploadFile = async (file: any) => {
                const formData = new FormData();

                formData.append('file', file);
                formData.append('propertyId', propertyId);

                return await $fetch('http://localhost:5248/Images', {
                    method: 'POST',
                    body: formData,
                });
            };

            await Promise.all(payload.map(file => uploadFile(file)));
        } catch (e) {
            console.log('[PropertyUpdateModal/updateImagesProperty]: ', e);
            return {};
        }
    };

    return {
        propertyList,
        fetchPropertyList,
        propertyDetail,
        fetchPropertyDetail,
        addPropertyToFavorite,
        removePropertyFromFavorite,
        createProperty,
        updateProperty,
        deleteProperty,
        // Favorite
        favoriteList,
        fetchFavoriteList,
        // Address
        addressList,
        fetchAddressList,
        createAddress,
        // Images
        updateImagesProperty,
    };
});
