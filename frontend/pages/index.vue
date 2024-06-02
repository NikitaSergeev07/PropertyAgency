<template>
    <div class="IndexPage">
        <PromoBanner/>

    </div>
</template>

<script setup lang="ts">
import PromoBanner from '~/components/PromoBanner/PromoBanner.vue';

const { $request } = useNuxtApp();
const { data } = useAsyncData('IndexPage', async () => {
    try {
        const { data } = await $request.$get('/Properties');

        if (data) {
            return {
                propertyList: data,
            };
        }
    } catch (e) {
        console.log('INDEX_PAGE:USE_ASYNC_DATA:', e);
    }
});

const propertyList = ref(data.value?.propertyList || []);
</script>

<style lang="scss" module>

</style>
