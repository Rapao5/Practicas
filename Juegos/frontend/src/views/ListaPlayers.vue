<script setup>
import { ref, watch } from 'vue';
import { useGamesStore } from '@/stores/games';
import { RouterLink, useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const store = useGamesStore();
const filtros = ref({
  nombre: '',
  especialidad: '',
  game: ''
});
const paginaActual = ref(1);

watch(
  () => route.query,
  async (nuevaQuery) => {
    console.log(nuevaQuery)
    paginaActual.value = nuevaQuery.pagina ? parseInt(nuevaQuery.pagina) : 1;

    filtros.value.nombre = nuevaQuery.nombre || '';
    filtros.value.especialidad = nuevaQuery.especialidad || '';
    filtros.value.game = parseInt(nuevaQuery.game) || null;

    await store.fetchPlayer(filtros.value, paginaActual.value);
    console.log("JSON de los jugadores:", JSON.parse(JSON.stringify(store.players)));
  },
  { immediate: true }
);

const cambiarPagina = (nuevaPagina) =>{
  router.push({
    path: '/listaPlayers',
    query: {...route.query, pagina: nuevaPagina}
  });
}

const pasarPagina = () => {
  cambiarPagina(paginaActual.value + 1);
}

const volverPagina = () => {
  if(paginaActual.value > 1) cambiarPagina(paginaActual.value - 1);
}

const aplicarFiltro = () => {
  router.push({
    path: '/listaPlayers',
    query: {
      ...filtros.value,
      pagina: 1
    }
  });
};

const resetearFiltros = () => {
  router.push({
    path: '/listaPlayers',
    query: { pagina: 1} 
  });
};

const eliminar = async (id) => {
  await store.deletePlayer(id);
  await store.fetchPlayer(filtros.value, paginaActual.value);
}

</script>
<template>
  <div class="text-center">
    <h1 class="m-5 text-3xl">Players</h1>
    <div class="shadow-lg p-4 mb-8 bg-gray-50">
      <label class="input ml-3 mt-3">
        <span class="label">Nombre</span>
        <input class="text" type="text" v-model="filtros.nombre" placeholder="Ej: Alex God">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Especialidad</span>
        <input class="text" type="text" v-model="filtros.especialidad" placeholder="Ej: Terror">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Juego</span>
        <input class="text" type="number" v-model="filtros.game" placeholder="Añade el Id del juego">
      </label>
      <button class="btn bg-green-200 p-3 ml-3 mt-3" @click="aplicarFiltro">Enviar</button>
      <button class="btn bg-blue-200 p-3 ml-3 mt-3" @click="resetearFiltros">Limpiar filtros</button>
    </div>
    <table class="table w-full">
      <thead>
        <tr class="bg-gray-400">
          <th>Nombre</th>
          <th>Especialidad</th>
          <th>Juegos</th>
          <th>Acciones</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr class="bg-gray-100" v-for="player in store.players" :key="player.id">
          <td class="text-xl font-bold">{{ player.nombre }}</td>
          <td>{{ player.especialidad }}</td>
          <td>
            <div v-for="game in player.game">
              {{ game.titulo }}
            </div>
          </td>
          <td> <RouterLink :to="{
                  path :`/formularioUpdatePlayer/${player.id}`,
                  query: {...route.query}
                  }">
            <button class="btn bg-yellow-200 p-3">Editar</button>
            </RouterLink>
          </td>
          <td><button class="btn bg-red-200 p-3" @click="eliminar(player.id)">Eliminar</button></td>
        </tr>
      </tbody>
    </table>
    <p class="mt-5" v-if="store.players.length === 0"> No hay jugadores disponibles</p>
  </div>
  <div v-if="store.players.length > 0" class="flex justify-center items-center gap-4 my-6">
      <button class="btn bg-blue-300 p-3"
      @click="volverPagina"
      :disabled="paginaActual <= 1">Anterior
      </button>
      <span class="font-bold">Página: {{ paginaActual }}</span>
      <button class="btn bg-blue-300 p-3"
      @click="pasarPagina"
      :disabled="store.players.length < 50">Siguiente</button>
    </div>
</template>
<style>
</style>