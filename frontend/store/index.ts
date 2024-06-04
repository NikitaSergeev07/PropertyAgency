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

interface VerificationInformer {
    show: boolean;
    title?: string;
    text?: string;
    buttonText?: string;
    redirectUrlName?: string;
}

export const useCommonStore = defineStore('common', () => {
    const { $request } = useNuxtApp();

    /**
     * State
     */
    const user: Ref<Partial<User> | null> = ref(null);
    const verificationInformer = ref({
        show: false,
        title: '',
        text: '',
        buttonText: '',
        redirectUrlName: '',
    });

    /**
     * Actions
     */
    const authLogin = async (payload: { email: string, password: string }) => await $request.$post('/login', payload);
    const authRegister = async (payload: { email: string, password: string }) => await $request.$post('/register', payload);
    const userLogout = async () => {
        const { data } = await $request.$post('/logout');

        return data?.message === 'success';
    };

    const changeVerificationInformer = ({ show, title, text, buttonText, redirectUrlName }: VerificationInformer) => {
        verificationInformer.value.show = show ?? verificationInformer.value.show;
        verificationInformer.value.title = title ?? verificationInformer.value.title;
        verificationInformer.value.text = text ?? verificationInformer.value.text;
        verificationInformer.value.buttonText = buttonText ?? verificationInformer.value.buttonText;
        verificationInformer.value.redirectUrlName = redirectUrlName ?? verificationInformer.value.redirectUrlName;
    };

    const fetchUser = async () => {
        const { data, error } = await $request.$get('/user');

        if (data?.user) {
            user.value = data?.user;
            changeVerificationInformer({ show: false });

            return true;
        } else if (error && error.statusCode === 401) {
            return false;
        }
    };

    return {
        user,
        verificationInformer,
        authLogin,
        authRegister,
        fetchUser,
        userLogout,
        changeVerificationInformer,
    };
});
