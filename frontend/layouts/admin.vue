<template>
    <div class="AdminLayout">
        <div :class="$style.wrapper">
            <AuthInformer
                v-if="verificationInformer.show"
                :title="verificationInformer.title"
                :text="verificationInformer.text"
                :button-text="verificationInformer.buttonText"
                :redirect-type="verificationInformer.redirectType"
            />

            <main v-else :class="$style.content">
                <slot />
            </main>
        </div>
    </div>
</template>

<script setup lang="ts">
import AuthInformer from '~/components/common/AuthInformer.vue';

const { verificationInformer } = useCommonStore();
</script>

<style lang="scss" module>
    .wrapper {
        width: 100%;
    }
</style>
