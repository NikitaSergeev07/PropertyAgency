<template>
    <header class="TheHeader">
        <div :class="$style.wrapper">
            <div :class="$style.top">
                <img
                    src="https://storage.yandexcloud.net/daily-flex/lian/logo.png"
                    alt=""
                    loading="lazy"
                    :class="$style.image"
                />

                <div :class="$style.list">
                    <UiLink
                        v-for="item in topNav"
                        :key="item.id"
                        bold
                        color="gray-500"
                    >
                        <QIcon :name="item.iconName"/>

                        <QTooltip
                            anchor="bottom middle"
                            self="top middle"
                            :class="$style.tooltipText"
                        >
                            {{ item.tooltipText }}
                        </QTooltip>
                    </UiLink>

                    <UiButton
                        v-if="!user?.id"
                        size="x-small"
                        color="custom"
                        :class="$style.button"
                    >
                        <QIcon name="account_circle"/>

                        Войти
                    </UiButton>

                    <QAvatar
                        v-else
                        color="primary"
                        text-color="white"
                        size="40px"
                        font-size="16px"
                        :class="$style.avatar"
                    >
                        {{ user?.userName[0] }}

                        <QMenu :class="$style.avatarDropdown">
                            <UiButton
                                color="blue-5"
                                size="x-small"
                                flat
                                :class="$style.logout"
                                @click="onLogout"
                            >
                                Выйти
                            </UiButton>
                        </QMenu>
                    </QAvatar>
                </div>
            </div>

            <div :class="$style.bottom">
                <UiLink
                    v-for="item in bottomNav"
                    :key="item.id"
                    bold
                    size="small"
                    color="black"
                >
                    {{ item.name }}
                </UiLink>
            </div>
        </div>
    </header>
</template>

<script setup lang="ts">
const { $router, $routes } = useNuxtApp();
const { user, userLogout } = useCommonStore();

const topNav = computed(() => [
    {
        id: 'favorite',
        iconName: 'favorite',
        tooltipText: 'Избранное',
    },
]);

const bottomNav = computed(() => [
    {
        id: 'rent',
        name: 'Аренда',
    },
    {
        id: 'sale',
        name: 'Продажа',
    },
]);

const onLogout = async () => {
    const res = await userLogout();

    if (res) {
        $router.push({ name: $routes.LOGIN });
    }
};
</script>

<style lang="scss" module>
    .wrapper {
        display: flex;
        flex-direction: column;
        width: mul($unit, 344);
        margin: 0 auto;
        padding: mul($unit, 4) 0;
        row-gap: mul($unit, 6);
        background-color: $white;
    }

    .top {
        display: flex;
        align-items: center;
    }

    .image {
        width: mul($unit, 28);
    }

    .list {
        display: flex;
        align-items: center;
        column-gap: mul($unit, 4);
        margin-left: auto;
    }

    .button {
        padding: 8px 16px;
        background-color: $gray-500;

        span {
            display: flex;
            align-items: center;
            column-gap: 12px;
        }
    }

    .avatar {
        cursor: pointer;
    }

    .tooltipText {
        @include text-xs;
    }

    .bottom {
        display: flex;
        column-gap: 24px;
        padding-top: 16px;
        border-top: 1px solid $gray-200;
    }
</style>
