<template>
    <div :class="$style.AuthPage">
        <div :class="$style.wrapper">
            <h2>Регистрация</h2>

            <form :class="$style.form" @submit.prevent="onSubmit">
                <UiInput
                    v-model="actualValue.userName"
                    label="Имя пользователя"
                    :error="hasErrors"
                    :rules="[isRequired]"
                    required
                    :loading="isLoading"
                    :class="$style.input"
                    @update:model-value="onInput"
                >
                    <template #error>
                        {{ serverErrors.userName }}
                    </template>
                </UiInput>

                <UiInput
                    v-model="actualValue.email"
                    label="Email"
                    :error="hasErrors"
                    :rules="[isRequired, isEmailValid]"
                    required
                    :loading="isLoading"
                    :class="$style.input"
                    @update:model-value="onInput"
                />

                <UiInput
                    v-model="actualValue.password"
                    label="Пароль"
                    :error="hasErrors || comparePassword"
                    :type="isPassword ? 'password' : 'text'"
                    :rules="[isRequired]"
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

                <UiInput
                    v-model="actualValue.confirmPassword"
                    label="Подтверждение пароля"
                    :error="hasErrors || comparePassword"
                    :type="isConfirmPassword ? 'password' : 'text'"
                    :rules="[isRequired]"
                    required
                    :loading="isLoading"
                    :class="$style.input"
                    @update:model-value="onInput"
                >
                    <template #append>
                        <QIcon
                            v-if="!isLoading"
                            :name="isConfirmPassword ? 'visibility_off' : 'visibility'"
                            size="medium-s"
                            :class="$style.icon"
                            @click="isConfirmPassword = !isConfirmPassword"
                        />
                    </template>
                    <template #error>
                        {{ serverErrors.confirmPassword }}
                    </template>
                </UiInput>

                <UiButton
                    type="submit"
                    size="md"
                    :class="$style.button"
                >
                    Зарегистрироваться
                </UiButton>
            </form>

            <p :class="$style.crossLink">
                У вас уже есть аккаунт?

                <UiButton
                    :to="{ name: $routes.LOGIN }"
                    size="x-small"
                    unelevated
                    flat
                    :class="$style.link"
                >
                    Авторизуйтесь
                </UiButton>
            </p>
        </div>
    </div>
</template>

<script setup lang="ts">
// Utils
import { isRequired, isEmailValid } from 'assets/ts/utils/validation-utils';

// Meta
definePageMeta({
    layout: 'auth',
    name: 'registration',
});

const { $router, $routes } = useNuxtApp();
const { authRegister, fetchUser } = useCommonStore();

const actualValue = ref({
    userName: '',
    email: '',
    password: '',
    confirmPassword: '',
});

const serverErrors = ref({
    userName: '',
    email: '',
    password: '',
    confirmPassword: '',
});

const hasErrors = ref(false);
const comparePassword = ref(false);
const isLoading = ref(false);
const isPassword = ref(true);
const isConfirmPassword = ref(true);

const onInput = () => {
    if (!hasErrors.value) {
        return false;
    }

    hasErrors.value = false;
    serverErrors.value = {
        userName: '',
        email: '',
        password: '',
        confirmPassword: '',
    };
};

const onSubmit = async () => {
    if (actualValue.value.password !== actualValue.value.confirmPassword) {
        serverErrors.value.password = 'Пароли не совпадают';
        serverErrors.value.confirmPassword = 'Пароли не совпадают';
        comparePassword.value = true;

        return false;
    }

    isLoading.value = true;

    const { data, error } = await authRegister(actualValue.value);

    if (data?.createdUser?.id) {
        await fetchUser();

        return $router.push({ name: $routes.HOME });
    }

    if (error) {
        hasErrors.value = true;
        serverErrors.value = {
            userName: 'Произошла ошибка при регистрации',
            email: 'Неверный логин или пароль',
            password: 'Неверный логин или пароль',
            confirmPassword: 'Неверный логин или пароль',
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
