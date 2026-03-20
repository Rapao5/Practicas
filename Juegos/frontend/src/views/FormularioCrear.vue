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
  <h1>Añadir game</h1>
  <ComponenteFormulario @enviar="guardarGame"/>
</template>
<style>
</style>