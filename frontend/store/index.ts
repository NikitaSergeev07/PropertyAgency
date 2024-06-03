// Types
interface User {
    id: string;
    userName: string;
    email: string;
    password:string;
    role: number;
    properties: any[],
    favorites: any[]
}

export const useCommonStore = defineStore('common', () => {
    const { $request } = useNuxtApp();

    /**
     * State
     */
    const user: Ref<Partial<User> | null> = ref(null);
    const showAuthInformer = ref(true);

    /**
     * Actions
     */
    const authLogin = async (payload: { email: string, password: string }) => await $request.$post('/login', payload);
    const authRegister = async (payload: { email: string, password: string }) => await $request.$post('/register', payload);
    const userLogout = async () => {
        const { data } = await $request.$post('/logout');

        return data?.message === 'success';
    };

    const changeAuthInformer = (val: boolean) => {
        showAuthInformer.value = val;
    };

    const fetchUser = async () => {
        const { data, error } = await $request.$get('/user');

        if (data?.user) {
            user.value = data?.user;
            changeAuthInformer(false);

            return true;
        } else if (error && error.statusCode === 401) {
            return false;
        }
    };

    return {
        user,
        showAuthInformer,
        authLogin,
        authRegister,
        fetchUser,
        userLogout,
        changeAuthInformer,
    };
});
