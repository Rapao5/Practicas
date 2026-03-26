<script setup>
import { useGamesStore } from '@/stores/games';
import { onMounted, computed, ref } from 'vue';
import { RouterLink } from 'vue-router';
import { useVirtualList } from '@vueuse/core'; 
import { useIntersectionObserver } from '@vueuse/core';

const paginaActual = ref(1);
const centinela = ref(null);
const store = useGamesStore();

onMounted(async () => {
  await store.scrollGames();
});

const juegos = computed(() => store.games);

const { list, containerProps, wrapperProps } = useVirtualList(
  juegos,
  {
    itemHeight: 100,
  },
);

useIntersectionObserver (
  centinela,
  async (entries) => {
    const entry = entries[0];
    if(entry.isIntersecting){
      paginaActual.value ++;
      await store.scrollGames({}, paginaActual.value);
    }
    
  }
);
</script>

<template>
  <div class="text-center" v-if="store.games.length">
    <h1 class="text-2xl mb-4">¡Tus juegos favoritos hechos cartas!</h1>
    
    <div v-bind="containerProps" style="height: 500px; overflow-y: auto; border: 1px solid #eee;">
      <div v-bind="wrapperProps">
        <div v-for="game in list" :key="game.index" style="height: 100px" class="flex items-center justify-center border-b">
          <img 
            class="w-20 h-20 object-cover" 
            :src="game.data.imagen ? game.data.imagen : 'https://i.redd.it/czk30lrobkxa1.jpeg'"
          >
          <span class="ml-4 font-bold">{{ game.data.titulo }}</span>
        </div>
        <div ref="centinela"></div>
        <p>Cargando</p>
      </div>
    </div>
  </div>

  <div class="text-center" v-else>
    <h1 class="text-2xl">Añade tus juegos favoritos</h1>
    <RouterLink to="/formularioCrear">
      <button class="btn bg-green-300 p-3 mt-10">Añadir Nuevo game</button>
    </RouterLink>
  </div>
</template>