export default defineNuxtRouteMiddleware(async to => {
    const { fetchUser, user, changeAuthInformer } = useCommonStore();

    /**
     * Проверяем если страница login или registration или есть user ID
     * то прекращаем дальнейший код
     */
    if (to.path.includes('/auth/login') || to.path.includes('/auth/registration') || user?.id) {
        changeAuthInformer({ show: false });

        return;
    }

    /**
     * Если первая проверка не прошла, мы не на странице авторизации и у нас нет user ID
     * То отправляем запрос за пользователем, чтобы получить ответ сервера
     * Если ответом будут данные пользователя, то мы их запишем в store
     * Если пользователь не авторизован, то ответа тут не будет и будет показано уведомление о том, что пользователю
     * Необходимо авторизоваться
     */
    const res = await fetchUser();

    if (!res) {
        changeAuthInformer({ show: true });
    }
});
