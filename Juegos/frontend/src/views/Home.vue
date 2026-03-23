<script setup>
import { useGamesStore } from '@/stores/games';
import { onMounted } from 'vue';
import { RouterLink } from 'vue-router';

const store = useGamesStore();
onMounted(() => {
  store.fetchGames();
});
</script>
<template>
<div class="text-center" v-if="store.games.length">
  <h1 class="text-2xl">¡Tus juegos favoritos hechos cartas!</h1>
  <table class="table w-full">
    <tr class="flex flex-wrap justify-center gap-10" >
      <td class="mt-5" v-for="game in store.games" :key="game.id">
        <div class="hover-3d">
        <figure class="w-60 rounded-2xl">
          <img class="w-60 h-80" :src="game.imagen ? game.imagen : 'https://i.redd.it/czk30lrobkxa1.jpeg'" alt="Tailwind CSS 3D card" />
        </figure>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        <div></div>
        </div>
        <p class="text-xl text-center">{{ game.titulo }}</p>
      </td>
    </tr>
  </table>
</div>
<div class="text-center" v-if="!store.games.length">
  <h1 class="text-2xl">Añade tus juegos favoritos</h1>
  <button class="btn bg-green-300 pr-3 mt-10"><router-link to="/formularioCrear" class="ml-5">Añadir Nuevo game</router-link></button>
</div>
</template>
<style></style>