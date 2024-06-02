// Types
import type { RoutesType } from 'assets/routes/routes'

declare module '#app' {
    interface NuxtApp {
        $routes: RoutesType
    }
}

declare module 'vue' {
    interface ComponentCustomProperties {
        $routes: RoutesType
    }
}

export {}
