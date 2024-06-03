<template>
    <div class="AdminPage">
        <h2>AdminPage</h2>
    </div>
</template>

<script setup lang="ts">
definePageMeta({
    layout: 'admin',
    middleware: ['check-role'],
});
</script>

<style lang="scss" module>

</style>
