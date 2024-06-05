<template>
    <div class="PropertyUpdateModal">
        <div :class="$style.wrapper">

            <h4 :class="$style.title">
                Редактирование квартиры

                <template v-if="property.title">
                    – {{ property.title }}
                </template>
            </h4>

            <QForm
                id="PropertyUpdateModal"
                ref="formRef"
                :class="$style.form"
                @submit="onSubmit"
            >
                <UiInput
                    v-model="actualValue.title"
                    label="Заголовок"
                    :rules="[isRequired]"
                    required
                    :class="$style.input"
                />

                <UiInput
                    v-model="actualValue.description"
                    label="Описание"
                    :rules="[isRequired]"
                    required
                    :class="$style.input"
                />

                <UiInput
                    v-model="actualValue.price"
                    label="Стоимость"
                    :rules="[isRequired, onlyNumber]"
                    required
                    :class="$style.input"
                />

                <UiInput
                    v-model="actualValue.roomCount"
                    label="Комнатность"
                    :rules="[isRequired]"
                    required
                    :class="$style.input"
                />

                <QSelect
                    v-model="actualValue.status"
                    :options="statusOptions"
                    label="Статус"
                    outlined
                >
                    <template #selected-item="props">
                        {{ props.opt }}
                    </template>
                    <template #option="props">
                        <QItem v-bind="props.itemProps">
                            {{ props.opt }}
                        </QItem>
                    </template>
                </QSelect>
            </QForm>

            <div :class="$style.cell">
                <h5 :class="$style.title">Адрес квартиры</h5>

                <template v-if="!addNewAddress">
                    <QVirtualScroll
                        v-if="addressList.length"
                        v-slot="{ item, index }"
                        style="max-height: 300px;"
                        :items="addressList"
                        separator
                        :class="$style.addressList"
                    >
                        <QItem
                            :key="index"
                            dense
                            clickable
                            :active="item.id === actualValue.addressId"
                            :class="$style.addressWrapper"
                            @click="onChangeAddress(item)"
                        >
                            <QItemSection>
                                <div :class="$style.addressContainer">
                                    <span>{{ index }}.</span>

                                    <QItemLabel :class="$style.addressItem">
                                        <div v-if="item.zipCode">{{ item.zipCode }}</div>
                                        <div v-if="item.country">{{ item.country }}</div>
                                        <div v-if="item.state">{{ item.state }}</div>
                                        <div v-if="item.city">{{ item.city }}</div>
                                        <div v-if="item.street">{{ item.street }}</div>
                                    </QItemLabel>
                                </div>
                            </QItemSection>
                        </QItem>
                    </QVirtualScroll>
                </template>

                <div v-else :class="$style.addressForm">
                    <UiInput
                        v-model="actualValue.newAddress.zipCode"
                        label="Почтовый индекс"
                        :rules="[isRequired, onlyNumber]"
                        type="text"
                        required
                        :class="$style.input"
                    />

                    <UiInput
                        v-model="actualValue.newAddress.country"
                        label="Страна"
                        :rules="[isRequired]"
                        required
                        :class="$style.input"
                    />

                    <UiInput
                        v-model="actualValue.newAddress.state"
                        label="Регион"
                        :rules="[isRequired]"
                        required
                        :class="$style.input"
                    />

                    <UiInput
                        v-model="actualValue.newAddress.city"
                        label="Город"
                        :rules="[isRequired]"
                        required
                        :class="$style.input"
                    />

                    <UiInput
                        v-model="actualValue.newAddress.street"
                        label="Улица"
                        :rules="[isRequired]"
                        required
                        :class="$style.input"
                    />
                </div>

                <UiButton
                    v-if="addNewAddress"
                    outline
                    :disabled="!newAddressValueValid"
                    :class="$style.button"
                    @click="createNewAddress"
                >
                    Создать новый адрес
                </UiButton>

                <UiButton
                    outline
                    :class="$style.button"
                    @click="addNewAddress = !addNewAddress"
                >
                    {{ addNewAddress ? 'Выбрать адрес из списка' : 'Добавить новый адрес' }}
                </UiButton>
            </div>

            <div :class="$style.buttons">
                <UiButton
                    :class="$style.button"
                    @click="onSubmit"
                >
                    Редактировать
                </UiButton>

                <UiButton
                    v-close-popup
                    outline
                    color="grey-5"
                    :class="$style.button"
                >
                    Отмена
                </UiButton>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { QForm } from 'quasar';
import type { Property } from '~/assets/types/property';

import { isRequired, onlyNumber } from '~/assets/ts/utils/validation-utils';

const { property, onClose } = withDefaults(defineProps<{
    property: Partial<Property>;
    onClose?: any;
}>(), {
    property: () => ({}),
    onClose: () => ({}),
});

const statusOptions = [
    'Продано',
    'Забронировано',
    'Свободная продажа',
];

const { $request } = useNuxtApp();
const { addressList } = storeToRefs(usePropertyStore());
const { updateProperty, createAddress } = usePropertyStore();

const formRef = ref<QForm>(null as any);
const addNewAddress = ref(false);

const actualValue = ref({
    title: property.title || '',
    description: property.description || '',
    price: property.price || 0,
    roomCount: property.roomCount || 0,
    status: property.status || '',
    addressId: property.address?.id || '',
    address: {
        street: '',
        city: '',
        state: '',
        country: '',
        zipCode: '',
    },
    newAddress: {
        street: '',
        city: '',
        state: '',
        country: '',
        zipCode: '',
    },
});

const newAddressValueValid = computed(() => {
    let isValid = true;

    for (const key in actualValue.value.newAddress) {
        if (!actualValue.value.newAddress[key]) {
            isValid = false;
        }
    }

    return isValid;
});

const onChangeAddress = (item: any) => {
    actualValue.value.addressId = item.id;
    actualValue.value.address.city = item.city;
    actualValue.value.address.country = item.country;
    actualValue.value.address.state = item.state;
    actualValue.value.address.street = item.street;
    actualValue.value.address.zipCode = item.zipCode;
};

const createNewAddress = async () => {
    if (!newAddressValueValid.value) {
        return false;
    }

    try {
        const res = await createAddress(actualValue.value.newAddress);

        onChangeAddress(res);

        addNewAddress.value = false;
    } catch (e) {
        console.log('[PropertyUpdateModal/createNewAddress]: ', e);
        return {};
    }
};

const onSubmit = async () => {
    const res = await formRef.value.validate();

    if (!res || !actualValue.value.addressId) {
        return false;
    }

    try {
        await updateProperty(property?.id || '', actualValue.value);

        onClose();
    } catch (e) {
        console.log('[PropertyUpdateModal/onSubmit]: ', e);
    }
};
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        flex-direction: column;
        height: 100%;
        padding: 0 32px 32px;
        row-gap: 32px;
    }

    .title {
        font-weight: 500;
    }

    .form {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 8);
    }

    .input {
        width: 100%;
    }

    .cell {
        position: relative;
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 7);
    }

    .addressList {
        border-radius: mul($unit, 2);
        border: 1px solid $gray-300;
    }

    .addressWrapper {
        min-height: 48px;
        background-color: transparent;
        transition: background-color .3s ease;
        cursor: pointer;

        @include hover {
            background-color: $primary-50;
        }
    }

    .addressContainer {
        display: flex;
        align-items: baseline;
        flex-direction: row;
        justify-content: flex-start;
        flex-wrap: nowrap;
        column-gap: mul($unit, 2);
    }

    .addressItem {
        display: flex;
        flex-wrap: wrap;
        column-gap: $unit;
    }

    .addressForm {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 9);
    }

    .buttons {
        display: flex;
        flex-direction: column;
        margin-top: auto;
        padding: mul($unit, 6) 0 mul($unit, 8) 0;
        row-gap: mul($unit, 3);
        border-top: 1px solid $gray-300;
    }

    .button {
        height: mul($unit, 12);
    }
</style>
