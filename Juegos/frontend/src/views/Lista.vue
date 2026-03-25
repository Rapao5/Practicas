<script setup>
import { ref, watch } from 'vue';
import { useGamesStore } from '@/stores/games';
import { RouterLink, useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const store = useGamesStore();
const filtros = ref({
  titulo: '',
  genero: '',
  tiempo: null,
  tiempoMayorA: null,
  tiempoMenorA: null,
  puntuacionMayorA: null,
  puntuacionMenorA: null,
  puntuacion: null,
  jugado: null
});
const paginaActual = ref(1);

watch(
  () => route.query,
  async (nuevaQuery) => {
    console.log(nuevaQuery)
    paginaActual.value = nuevaQuery.pagina ? parseInt(nuevaQuery.pagina) : 1;

    filtros.value.titulo = nuevaQuery.titulo || '';
    filtros.value.genero = nuevaQuery.genero || '';
    filtros.value.tiempo = parseInt(nuevaQuery.tiempo) || null;
    filtros.value.tiempoMayorA = parseInt(nuevaQuery.tiempoMayorA) || null;
    filtros.value.tiempoMenorA = parseInt(nuevaQuery.tiempoMenorA) || null;
    filtros.value.puntuacion = parseInt(nuevaQuery.puntuacion) || null;
    filtros.value.puntuacionMayorA = parseInt(nuevaQuery.puntuacionMayorA) || null;
    filtros.value.puntuacionMenorA = parseInt(nuevaQuery.puntuacionMenorA) || null;
    filtros.value.jugado = nuevaQuery.jugado === 'true' ? true : (nuevaQuery.jugado === 'false' ? false : null);
    
    await store.fetchGames(filtros.value, paginaActual.value);
  },
  { immediate: true }
);

const cambiarPagina = (nuevaPagina) =>{
  router.push({
    path: '/lista',
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
    path: '/lista',
    query: {
      ...filtros.value,
      pagina: 1
    }
  });
};

const resetearFiltros = () => {
  router.push({
    path: '/lista',
    query: { pagina: 1} 
  });
};

const eliminar = async (id) => {
  await store.deleteGame(id);
  await store.fetchGames(filtros.value, paginaActual.value);
}
</script>
<template>
  <div class="text-center">
    <h1 class="m-5 text-3xl">Games</h1>
    <div class="shadow-lg p-4 mb-8 bg-gray-50">
      <label class="input ml-3 mt-3">
        <span class="label">Título</span>
        <input class="text" type="text" v-model="filtros.titulo" placeholder="Ej: Resindet Evil 2">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Género</span>
        <input class="text" type="text" v-model="filtros.genero" placeholder="Ej: Terror">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Tiempo</span>
        <input class="text" type="number" v-model="filtros.tiempo" placeholder="Ej: 50">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Tiempo mayor a:</span>
        <input class="text" type="number" v-model="filtros.tiempoMayorA" placeholder="Ej: 20">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Tiempo menor a:</span>
        <input class="text" type="number" v-model="filtros.tiempoMenorA" placeholder="Ej: 100">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Puntuación</span>
        <input class="text" type="number" v-model="filtros.puntuacion" placeholder="Ej: 8">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Puntuación mayor a</span>
        <input class="text" type="number" v-model="filtros.puntuacionMayorA" placeholder="Ej: 3">
      </label>
      <label class="input ml-3 mt-3">
        <span class="label">Puntuación menor a</span>
        <input class="text" type="number" v-model="filtros.puntuacionMenorA" placeholder="Ej: 9">
      </label>
      <select class="select ml-3 pl-5 mt-3" v-model="filtros.jugado">
        <option :value="null">¿Jugado?</option>
        <option :value="true">Si</option>
        <option :value="false">No</option>
      </select>
      <button class="btn bg-green-200 p-3 ml-3 mt-3" @click="aplicarFiltro">Enviar</button>
      <button class="btn bg-blue-200 p-3 ml-3 mt-3" @click="resetearFiltros">Limpiar filtros</button>
    </div>
    <table class="table w-full">
      <thead>
        <tr class="bg-gray-400">
          <th>Portada</th>
          <th>Título</th>
          <th>Género</th>
          <th>Tiempo</th>
          <th>Descripción</th>
          <th>Puntuación</th>
          <th>¿Jugado?</th>
          <th>Acciones</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr class="bg-gray-100" v-for="game in store.games" :key="game.id">
          <td><img class="w-32 h-32" :src="game.imagen ? game.imagen : 'https://i.redd.it/czk30lrobkxa1.jpeg'"></td>
          <td class="text-xl font-bold">{{ game.titulo }}</td>
          <td>{{ game.genero }}</td>
          <td>{{ game.tiempo }} Horas</td>
          <td class="max-w-xs break-words px-4">{{ game.descripcion }}</td>
          <td v-if="game.puntuacion < 4" class="text-red-500 text-xl">{{ game.puntuacion }} /10</td>
          <td v-else-if="game.puntuacion >= 4 && game.puntuacion < 7" class="text-yellow-500 text-xl">{{ game.puntuacion }} /10</td>
          <td v-else class="text-green-500 text-xl">{{ game.puntuacion }} /10</td>
          <td>{{ game.jugado ? 'Sí' : 'No'}}</td>
          <td> <RouterLink :to="{
                  path :`/formularioUpdate/${game.id}`,
                  query: {...route.query}
                  }">
            <button class="btn bg-yellow-200 p-3">Editar</button>
            </RouterLink>
          </td>
          <td><button class="btn bg-red-200 p-3" @click="eliminar(game.id)">Eliminar</button></td>
        </tr>
      </tbody>
    </table>
    <p class="mt-5" v-if="store.games.length === 0"> No hay juegos disponibles</p>
  </div>
  <div v-if="store.games.length > 0" class="flex justify-center items-center gap-4 my-6">
      <button class="btn bg-blue-300 p-3"
      @click="volverPagina"
      :disabled="paginaActual <= 1">Anterior
      </button>
      <span class="font-bold">Página: {{ paginaActual }}</span>
      <button class="btn bg-blue-300 p-3"
      @click="pasarPagina"
      :disabled="store.games.length < 50">Siguiente</button>
    </div>
</template>
<style></style>