<template>
    <div class="PromoBannerFilterRooms">
        <div :class="[$style.wrapper, classList]">

            <button :class="$style.button" @click="isOpen = true">
                <span>{{ selected }}</span>

                <QIcon
                    name="chevron_right"
                    :class="$style.chevron"
                />
            </button>

            <QMenu :class="$style.dropdown" @before-hide="isOpen = false">
                <ul :class="$style.list">
                    <li
                        v-for="item in values"
                        :key="item.id"
                        :class="$style.parent"
                    >
                        <UiButton
                            :outline="!selectedValue.includes(item.id)"
                            :color="selectedValue.includes(item.id) ? 'blue-5' : 'grey-5'"
                            size="x-small"
                            :class="$style.roomButton"
                            @click="onChangeSelected(item.id)"
                        >
                            {{ item.name }}
                        </UiButton>
                    </li>
                </ul>

                <QCheckbox
                    :model-value="selectedValue.includes(0)"
                    label="Студия"
                    @update:model-value="onChangeSelected(0)"
                />
            </QMenu>
        </div>
    </div>
</template>

<script setup lang="ts">
const isOpen = ref(false);

const values = [
    { id: 1, name: '1' },
    { id: 2, name: '2' },
    { id: 3, name: '3' },
    { id: 4, name: '4' },
    { id: 5, name: '5' },
    { id: 6, name: '6+' },
];

const selectedValue = ref([1]);

const classList = computed(() => [{
    '--is-opened': isOpen.value,
    '--is-empty': !selectedValue.value.length,
}]);

const selected = computed(() => {
    if (!selectedValue.value.length) {
        return 'Комнат';
    }

    // let mainLabel = '';
    // const additionalLabel = '';
    //
    // if (mainSelected.value.length < 2) {
    //     mainLabel = `${mainSelected.value[0]}${mainSelected.value[0] !== 6 ? '-комнатную' : ' комн.'}`;
    // } else {
    //     mainLabel = `${mainSelected.value.join(', ')} комн.`;
    // }
    //
    // if (additionalSelected.value.length < 2) {
    //     mainLabel = `${mainSelected.value[0]}${mainSelected.value[0] !== 6 ? '-комнатную' : ' комн.'}`;
    // }
    //
    // return `${selectedValue.value.map.join(', ')} комн.`;
});

const onChangeSelected = (val: number) => {
    // if (selectedValue.value.includes(val)) {
    //     const index = selectedValue.value.findIndex(v => v === val);
    //
    //     selectedValue.value.splice(index, 1);
    // } else {
    //     selectedValue.value.push(val);
    // }
};
</script>

<style lang="scss" module>
    .wrapper {
        position: relative;
        width: mul($unit, 40);
        display: flex;

        &:global(.--is-opened) {
            .chevron {
                transform: translateY(-50%) rotate(-90deg);
            }

            .button {
                background-color: $primary-50;
            }
        }

        &:global(.--is-empty) {
            .button {
                color: $gray-400;
            }
        }
    }

    .button {
        position: relative;
        overflow: hidden;
        width: 100%;
        height: 61px;
        padding: mul($unit, 5) mul($unit, 9.5) mul($unit, 5) mul($unit, 5);
        border-top: none;
        border-right: 1px solid $gray-300;
        border-bottom: none;
        border-left: 1px solid $gray-300;
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

    .dropdown {
        display: flex;
        flex-direction: column;
        width: mul($unit, 80);
        margin-top: mul($unit, 2) !important;
        padding: mul($unit, 4);
        row-gap: mul($unit, 3);
        border-radius: $unit;
        background: $white;
    }

    .list {
        display: flex;
        align-items: center;
        column-gap: mul($unit, 2);
    }

    .parent {
        display: flex;
        flex-direction: column;
        row-gap: mul($unit, 3);
    }

    .roomButton {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 40px;
        height: 40px;
        padding: 8px;

        span {
            flex: none;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 12px;
            line-height: 16px;
        }

        &.--is-active {
            background-color: red !important;
        }
    }
</style>
