<script setup>
import ComponenteFormulario from '@/components/ComponenteFormulario.vue';
import { useGamesStore } from '@/stores/games';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const id = route.params.id;
const juegoAEditar = ref(null);
const store = useGamesStore();

onMounted(async() => {
  juegoAEditar.value = await store.getGameById(id);
});

const guardarGame = async (gameData) => {
  await store.updateGame(gameData);
  router.push('/lista');
}
</script>
<template>
<h1>Editar Game</h1>
  <ComponenteFormulario v-if="juegoAEditar"
  :juegoInicial="juegoAEditar"
  @enviar="guardarGame"
   />
</template>
<style>
</style>