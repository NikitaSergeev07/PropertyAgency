<template>
    <div class="AdminPage">
        <div class="container" :class="$style.wrapper">

            <header :class="$style.header">
                <h3>Админ панель</h3>

                <UiButton
                    :class="$style.button"
                    @click="openCreateModal"
                >
                    Добавить квартиру
                </UiButton>
            </header>

            <ul v-if="propertyList.length" :class="$style.list">
                <li
                    v-for="property in propertyList"
                    :key="property.id"
                    :class="$style.item"
                >
                    <PropertyCard
                        :room-count="property.roomCount"
                        :title="property.title"
                        :price="property.price"
                        :status="property.status"
                        :address="property.address"
                        :description="property.description"
                        :has-favorite="false"
                        :class="$style.card"
                    />

                    <div :class="$style.actions">
                        <UiLink
                            @click="openUpdateModal(property)"
                        >
                            Редактировать
                        </UiLink>

                        <UiLink
                            color="error"
                            @click="openDeleteModal(property)"
                        >
                            Удалить
                        </UiLink>
                    </div>
                </li>
            </ul>

        </div>
    </div>
</template>

<script setup lang="ts">
import type { Property } from '~/assets/types/property';

definePageMeta({
    layout: 'admin',
    middleware: ['check-role'],
});

const { propertyList } = storeToRefs(usePropertyStore());
const { fetchPropertyList, fetchAddressList, deleteProperty } = usePropertyStore();

useAsyncData('IndexPage', async () => {
    await Promise.all([
        fetchPropertyList(),
        fetchAddressList(),
    ]);
});

const { dialog } = useQuasar();

const openUpdateModal = (property: Property) => {
    dialog({
        component: defineAsyncComponent(() => import('~/components/ui/Modal/UiModalCurtain.vue')),
        componentProps: {
            component: defineAsyncComponent(() => import('~/components/property/PropertyUpdateModal.vue')),
            componentProps: {
                property,
            },
        },
    });
};

const openCreateModal = () => {
    dialog({
        component: defineAsyncComponent(() => import('~/components/ui/Modal/UiModalCurtain.vue')),
        componentProps: {
            component: defineAsyncComponent(() => import('~/components/property/PropertyCreateModal.vue')),
        },
    });
};

const openDeleteModal = (property: Property) => {
    dialog({
        component: defineAsyncComponent(() => import('~/components/ui/Modal/UiModalConfirm.vue')),
        componentProps: {
            title: '<b>Удалить квартиру</b>',
            text: 'Вы действительно хотите удалить квартиру?',
        },
    })
        .onOk(async () => await deleteProperty(property.id));
};
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        flex-direction: column;
        padding: mul($unit, 16) 0;
        row-gap: mul($unit, 8);
    }

    .header {
        display: flex;
        align-items: center;
        justify-content: space-between;
    }

    .button {
        height: mul($unit, 12);
    }

    .list {
        display: flex;
        flex-wrap: wrap;
        padding-top: mul($unit, 8);
        column-gap: mul($unit, 6);
        row-gap: mul($unit, 6);
        border-top: 1px solid $gray-200;
    }

    .item {
        display: flex;
        flex-direction: column;
        width: mul($unit, 80);
        row-gap: mul($unit, 4);
    }

    .card {
        width: 100%;
    }

    .actions {
        display: flex;
        align-items: center;
        justify-content: space-between;
    }
</style>
