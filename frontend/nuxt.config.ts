// Common
import { defineNuxtConfig } from 'nuxt/config';

export default defineNuxtConfig({
    app: {
        pageTransition: {
            name: 'page',
            mode: 'out-in',
        },
    },

    typescript: {
        tsConfig: {
            include: [
                '../assets/types/plugins.d.ts',
            ],
        },
    },

    proxy: {
        '/uploads': {
            target: 'http://localhost:3001',
            pathRewrite: { '^/uploads': '' },
        },
    },

    ssr: false,

    devtools: { enabled: true },

    devServer: {
        port: Number(import.meta.env.VITE_PORT) || 3000,
    },

    modules: [
        '@pinia/nuxt',
        'nuxt-quasar-ui',
        ['@nuxtjs/google-fonts', {
            families: {
                'Golos Text': {
                    wght: [400, 500, 600, 700, 800, 900],
                },
            },
        }],
    ],

    quasar: {
        sassVariables: '../assets/scss/shared/_quasar_variables.scss',
        lang: 'ru',
        plugins: [
            'Dialog',
            'Notify',
        ],
        extras: {
            fontIcons: ['material-icons'],
        },
    },

    pinia: {
        storesDirs: ['./store/**'],
    },

    plugins: [
        '~/plugins/request',
        '~/plugins/routes',
    ],

    imports: {
        dirs: [
            'composables',
            'composables/**',
        ],
    },

    vite: {
        css: {
            preprocessorOptions: {
                scss: {
                    sourceMap: true,
                    additionalData: `
                        @import "~/assets/scss/shared/_variables.scss";
                        @import "~/assets/scss/shared/_functions.scss";
                        @import "~/assets/scss/shared/_mixins.scss";
                    `,
                },
            },
        },
    },

    css: ['~/assets/scss/common/_bundle.scss'],
});
