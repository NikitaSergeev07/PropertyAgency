<template>
    <div class="DefaultLayout">
        <div :class="$style.wrapper">
            <AuthInformer
                v-if="verificationInformer.show"
                :title="verificationInformer.title"
                :text="verificationInformer.text"
            />

            <template v-else>
                <TheHeader />

                <main :class="$style.content">
                    <slot />
                </main>

                <TheFooter />
            </template>
        </div>
    </div>
</template>

<script lang="ts" setup>
// Components
import TheHeader from '~/components/common/TheHeader.vue';
import TheFooter from '~/components/common/TheFooter.vue';
import AuthInformer from '~/components/common/AuthInformer.vue';

const { verificationInformer } = useCommonStore();
</script>

<style lang="scss" module>
    .wrapper {
        position: relative;
        display: flex;
        flex-direction: column;
        width: 100%;
        min-height: 100vh;
        background-color: $white;
    }

    .menu {
        position: sticky;
        top: 0;
        flex-shrink: 0;
        width: mul($unit, 58);
        height: 100vh;
    }

    .content {
        flex-grow: 1;
    }
</style>
