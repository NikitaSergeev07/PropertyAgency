<script setup lang="ts">
// {
//     "id": "e6984037-e4fd-4fd0-b957-27cfe74ba7ae",
//     "title": "Квартира",
//     "description": "мега",
//     "price": 12500000,
//     "typeProperty": 0,
//     "status": 0,
//     "userId": "1e9bebfe-6f39-44d7-81e5-4581abc2bd62",
//     "user": {
//       "id": "1e9bebfe-6f39-44d7-81e5-4581abc2bd62",
//       "userName": "Nikitas",
//       "email": "sergeevnicolas20@gmail.com",
//       "password": "$2a$11$kOljYRawOa/IzmPeJKU3/.JOJ9P7GRPxY/kEKY.BQZwO5LFPiNeuu",
//       "role": 0,
//       "properties": [],
//       "favorites": []
//     },
//     "address": {
//       "id": "ee6037fa-3809-4413-909e-704dfae6b6c3",
//       "street": "Улица клыкова",
//       "city": "Курск",
//       "state": "Курская обл",
//       "country": "РФ",
//       "zipCode": "305005",
//       "propertyId": "e6984037-e4fd-4fd0-b957-27cfe74ba7ae"
//     },
//     "favorites": [
//       {
//         "id": "4c76ffda-fa26-4306-baa8-fb76aa99d071",
//         "userId": "1e9bebfe-6f39-44d7-81e5-4581abc2bd62",
//         "user": null,
//         "propertyId": "e6984037-e4fd-4fd0-b957-27cfe74ba7ae"
//       }
//     ]
interface PropertyAddress {
    id: string;
    street: string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
    propertyId: string;
}

withDefaults(defineProps<{
    title: string | null;
    description: string | null;
    price: number;
    address?: Partial<PropertyAddress>,
    typeProperty: number;
    favorites: {
        id: string;
        userId: string;
        user: string;
        propertyId: string;
    }[],
}>(), {
    title: null,
    description: null,
    price: 0,
    typeProperty: 0,
    address: () => ({}),
    favorites: () => [],
});
</script>

<template>
    <div class="PropertyCard">
        <div :class="$style.wrapper">
            <img
                src="https://images.cdn-cian.ru/images/posutochnyj-dom-neptun-centralnaya-ulica-2078218062-4.jpg"
                alt=""
                loading="lazy"
                :class="$style.image"
            />


        </div>
    </div>
</template>

<style lang="scss" module>

</style>
