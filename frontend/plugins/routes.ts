// Routes
import { routes } from '~/assets/routes/routes';

export default defineNuxtPlugin(() => ({
    provide: {
        routes,
    },
}));
