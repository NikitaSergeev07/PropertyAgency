<template>
    <div class="PromoBannerFilterFlat">
        <div :class="[$style.wrapper, classList]">
            <button :class="$style.button" @click="isOpen = true">
                <span>{{ selected }}</span>

                <QIcon
                    name="chevron_right"
                    :class="$style.chevron"
                />
            </button>

            <QMenu :class="$style.list" @before-hide="isOpen = false">
                <div
                    v-for="item in values"
                    :key="item.id"
                    :class="$style.parent"
                >
                    <QCheckbox
                        v-for="chItem in item.children"
                        :key="chItem.id"
                        :model-value="actualValue.includes(chItem.id)"
                        :label="chItem.name"
                        @update:model-value="onChangeSelected(chItem)"
                    />
                </div>
            </QMenu>
        </div>
    </div>
</template>

<script setup lang="ts">
interface OptionsValue {
    id: string;
    name: string;
    category: string;
}

const isOpen = ref(false);

const values = [
    {
        id: 'flats-group',
        children: [
            { id: 'new_building', name: 'Квартира в новостройке', category: 'flats-group' },
            { id: 'secondary_building', name: 'Квартира во вторичке', category: 'flats-group' },
        ],
    },
    {
        id: 'rooms-group',
        children: [
            { id: 'room', name: 'Комната', category: 'rooms-group' },
        ],
    },
    {
        id: 'house-group',
        children: [
            { id: 'house_cottage', name: 'Дом, дача', category: 'house-group' },
            { id: 'part_of_house', name: 'Часть дома', category: 'house-group' },
            { id: 'townhouse', name: 'Таунхаус', category: 'house-group' },
            { id: 'section', name: 'Участок', category: 'house-group' },
        ],
    },
];
const activeCategory = ref('flats-group');
const actualValue = ref(['new_building', 'secondary_building']);
const selectedValue = ref(['Квартира в новостройке', 'Квартира во вторичке']);

const classList = computed(() => [{
    '--is-opened': isOpen.value,
}]);

const selected = computed(() => selectedValue.value.join(', '));

const onChangeCategory = (val: OptionsValue) => {
    activeCategory.value = val.category;
    actualValue.value = [val.id];
    selectedValue.value = [val.name];
};

const delSelectedElement = (val: OptionsValue) => {
    if (actualValue.value.length < 2) {
        return false;
    }

    const index = actualValue.value.findIndex(v => v === val.id);

    actualValue.value.splice(index, 1);
    selectedValue.value.splice(index, 1);
};

const onChangeSelected = (val: OptionsValue) => {
    if (activeCategory.value !== val.category) {
        onChangeCategory(val);

        return false;
    }

    if (actualValue.value.includes(val.id)) {
        delSelectedElement(val);
    } else {
        actualValue.value.push(val.id);
        selectedValue.value.push(val.name);
    }
};
</script>

<style lang="scss" module>
    .wrapper {
        position: relative;
        width: mul($unit, 80);
        display: flex;

        &:global(.--is-opened) {
            .chevron {
                transform: translateY(-50%) rotate(-90deg);
            }

            .button {
                background-color: $primary-50;
            }
        }
    }

    .button {
        position: relative;
        overflow: hidden;
        width: 100%;
        padding: mul($unit, 5) mul($unit, 9.5) mul($unit, 5) mul($unit, 5);
        border-radius: $unit 0 0 $unit;
        border: none;
        background-color: $white;
        text-align: left;
        color: $black;
        text-overflow: ellipsis;
        white-space: nowrap;
        cursor: pointer;
        transition: background-color .3s ease;

        span {
            @include text-s;

            max-width: mul($unit, 62);
        }

        @include hover {
            background-color: $primary-50;
        }
    }

    .chevron {
        position: absolute;
        top: 50%;
        right: mul($unit, 4);
        z-index: 2;
        font-size: mul($unit, 5);
        transform: translateY(-50%) rotate(90deg);
        transition: all .3s ease;
    }

    .list {
        display: flex;
        flex-direction: column;
        width: mul($unit, 80);
        margin-top: mul($unit, 2) !important;
        padding: mul($unit, 4);
        row-gap: mul($unit, 3);
        border-radius: $unit;
        background: $white;
    }

    .parent {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 3);

        &:not(:first-child) {
            padding-top: mul($unit, 3);
            border-top: 1px solid $gray-200;
        }
    }
</style>
