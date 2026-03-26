<script setup>
import ComponenteFormularioPlayer from '@/components/ComponenteFormularioPlayer.vue';
import { useGamesStore } from '@/stores/games';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const id = route.params.id;
const playerAEditar = ref(null);
const store = useGamesStore();

onMounted(async() => {
  playerAEditar.value = await store.getPlayerById(id);
});

const guardarPlayer = async (gamePlayer) => {
  await store.updatePlayer(gamePlayer);
  router.push({
    path: '/listaPlayers',
    query: route.query
  });
}
</script>
<template>
<h1 class="text-center text-3xl">Editar player</h1>
<div class="mt-5 flex items-center justify-center w-full">
  <ComponenteFormularioPlayer v-if="playerAEditar"
    :playerInicial="playerAEditar"
    @enviar="guardarPlayer"
    />
</div>
  
</template>
<style>
</style>