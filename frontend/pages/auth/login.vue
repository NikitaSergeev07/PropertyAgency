<template>
    <div :class="$style.AuthPage">
        <div :class="$style.wrapper">
            <h2>Войти</h2>

            <form :class="$style.form" @submit.prevent="onSubmit">
                <UiInput
                    v-model="actualValue.email"
                    label="Email"
                    :error="hasErrors"
                    :rules="[isRequired, isEmailValid]"
                    required
                    :loading="isLoading"
                    :class="$style.input"
                    @update:model-value="onInput"
                >
                    <template #error>
                        {{ serverErrors.email }}
                    </template>
                </UiInput>

                <UiInput
                    v-model="actualValue.password"
                    label="Пароль"
                    :type="isPassword ? 'password' : 'text'"
                    :rules="[isRequired]"
                    :error="hasErrors"
                    required
                    :loading="isLoading"
                    :class="$style.input"
                    @update:model-value="onInput"
                >
                    <template #append>
                        <QIcon
                            v-if="!isLoading"
                            :name="isPassword ? 'visibility_off' : 'visibility'"
                            size="medium-s"
                            :class="$style.icon"
                            @click="isPassword = !isPassword"
                        />
                    </template>

                    <template #error>
                        {{ serverErrors.password }}
                    </template>
                </UiInput>

                <UiButton
                    type="submit"
                    size="md"
                    :class="$style.button"
                >
                    Войти
                </UiButton>
            </form>

            <p :class="$style.crossLink">
                У вас нет аккаунта?

                <UiButton
                    :to="{ name: $routes.REGISTRATION }"
                    size="x-small"
                    unelevated
                    flat
                    :class="$style.link"
                >
                    Зарегистрируйтесь
                </UiButton>
            </p>
        </div>
    </div>
</template>

<script setup lang="ts">
// Utils
import { isRequired, isEmailValid } from '~/assets/ts/utils/validation-utils';

// Meta
definePageMeta({
    layout: 'auth',
    name: 'login',
});

const { $router, $routes } = useNuxtApp();
const { authLogin, fetchUser } = useCommonStore();

const actualValue = ref({
    email: '',
    password: '',
});

const serverErrors = ref({
    email: '',
    password: '',
});

const hasErrors = ref(false);
const isLoading = ref(false);
const isPassword = ref(true);

const onInput = () => {
    if (!hasErrors.value) {
        return false;
    }

    hasErrors.value = false;
    serverErrors.value = {
        email: '',
        password: '',
    };
};

const onSubmit = async () => {
    isLoading.value = true;

    const { data, error } = await authLogin(actualValue.value);

    console.log(data);

    if (data?.message === 'success') {
        await fetchUser();

        return $router.push({ name: $routes.HOME });
    }

    if (error?.status === 400 && error?._data.message === 'Invalid credentials') {
        hasErrors.value = true;
        serverErrors.value = {
            email: 'Неверный логин или пароль',
            password: 'Неверный логин или пароль',
        };

        isLoading.value = false;
    }
};
</script>

<style lang="scss" module>
    .AuthPage {
        width: 100%;
    }

    .wrapper {
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
        row-gap: mul($unit, 8);
    }

    .logo {
        width: mul($unit, 33);
        height: auto;
    }

    .form {
        display: flex;
        flex-direction: column;
        width: 100%;
        row-gap: mul($unit, 5);

    }

    .formWrapper {
        display: flex;
        flex-direction: column;
        margin: auto 0;
        row-gap: mul($unit, 8);
    }

    .input {
        width: 100%;

        &:not(:first-child) {
            margin-top: mul($unit, 4);
        }
    }

    .icon {
        color: $gray-400;
        transition: color .3s ease;
        cursor: pointer;

        @include hover {
            color: $primary-500;
        }
    }

    .button {
        height: mul($unit, 12);
        margin-top: mul($unit, 3);
    }

    .crossLink {
        @include text-xs;

        display: flex;
        align-items: center;
        column-gap: 12px;
    }

    .link {
        padding: 8px;
    }
</style>
