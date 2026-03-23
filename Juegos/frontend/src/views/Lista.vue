<script setup>
import { onMounted, ref } from 'vue';
import { useGamesStore } from '@/stores/games';
import { RouterLink } from 'vue-router';

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

onMounted(() => {
  store.fetchGames();
});

const aplicarFiltro = () => {
  store.fetchGames(filtros.value);
};

const resetearFiltros = () => {
  filtros.value.titulo = '';
  filtros.value.genero = '';
  filtros.value.tiempo = null;
  filtros.value.tiempoMayorA = null
  filtros.value.tiempoMenorA = null
  filtros.value.puntuacionMayorA = null
  filtros.value.puntuacionMenorA = null
  filtros.value.puntuacion = null
  filtros.value.jugado = null
  
  store.fetchGames(filtros.value);
};

const eliminar = async (id) => {
  await store.deleteGame(id);
}
</script>
<template>
  <div class="text-center p-5">
    <h1 class="m-5">Games</h1>
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
      <button class="btn bg-green-200 p-3 ml-3" @click="aplicarFiltro">Enviar</button>
      <button class="btn bg-blue-200 p-3 ml-3" @click="resetearFiltros">Limpiar filtros</button>
    </div>
    <table class="table w-full">
      <thead>
        <tr class="bg-gray-400">
          <th>Título</th>
          <th>Género</th>
          <th>Tiempo</th>
          <th>Descripción</th>
          <th>Puntuación</th>
          <th>¿Jugado?</th>
          <th></th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr class="bg-gray-100" v-for="game in store.games" :key="game.id">
          <td>{{ game.titulo }}</td>
          <td>{{ game.genero }}</td>
          <td>{{ game.tiempo }} Horas</td>
          <td>{{ game.descripcion }}</td>
          <td>{{ game.puntuacion }} /10</td>
          <td>{{ game.jugado ? 'Sí' : 'No'}}</td>
          <td> <RouterLink :to="`/formularioUpdate/${game.id}`">
            <button class="btn bg-yellow-200 p-3">Editar</button>
            </RouterLink>
          </td>
          <td><button class="btn bg-red-200 p-3" @click="eliminar(game.id)">Eliminar</button></td>
        </tr>
      </tbody>
    </table>
    <p v-if="store.games.length === 0"> No hay juegos disponibles</p>
  </div>
</template>
<style></style>