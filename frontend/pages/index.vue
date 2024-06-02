<template>
    <div class="IndexPage">
        <div :class="$style.wrapper">
            <PromoBanner/>

            <PropertyBlock
                v-if="propertyList.length"
                :property-list="propertyList"
            />
        </div>
    </div>
</template>

<script setup lang="ts">
import PromoBanner from '~/components/PromoBanner/PromoBanner.vue';
import PropertyBlock from '~/components/Properties/PropertyBlock.vue';

const { propertyList } = storeToRefs(usePropertyStore());
const { fetchPropertyList } = usePropertyStore();

useAsyncData('IndexPage', async () => await fetchPropertyList());
</script>

<style lang="scss" module>
    .wrapper {
        width: 100%;
    }
</style>
