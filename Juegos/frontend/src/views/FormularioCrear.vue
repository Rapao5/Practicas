<script setup>
import ComponenteFormulario from '@/components/ComponenteFormulario.vue';
import { useGamesStore } from '@/stores/games';
import { useRouter } from 'vue-router';

const store = useGamesStore();
const router = useRouter();

const guardarGame = async (gameData) => {
  await store.saveGame(gameData);
  router.push('/lista');
}

</script>
<template>
  <h1 class="text-center text-3xl">Añadir game</h1>
  <div class="mt-5 flex items-center justify-center w-full">
    <ComponenteFormulario @enviar="guardarGame"/>
  </div>
</template>
<style>
</style>