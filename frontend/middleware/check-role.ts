export default defineNuxtRouteMiddleware(to => {
    const { $routes } = useNuxtApp();
    const { user, changeVerificationInformer } = useCommonStore();

    if (!user?.role) {
        changeVerificationInformer({
            title: 'У Вас недостаточно прав',
            text: 'Для того чтобы попасть на эту страницу у Вас должна быть роль администратора',
            show: true,
            buttonText: 'Вернуться назад',
            redirectUrlName: $routes.HOME,
        });
    }
});
