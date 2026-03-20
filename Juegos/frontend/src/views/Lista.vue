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
  <h1>Games</h1>
  <label>Titulo</label>
  <input type="text" v-model="filtros.titulo">
  <label>Género</label>
  <input type="text" v-model="filtros.genero">
  <label>Tiempo</label>
  <input type="number" v-model="filtros.tiempo">
  <label>Tiempo mayor a:</label>
  <input type="number" v-model="filtros.tiempoMayorA">
  <label>Tiempo menor a:</label>
  <input type="number" v-model="filtros.tiempoMenorA">
  <label>Puntuación</label>
  <input type="number" v-model="filtros.puntuacion">
  <label>Puntuación mayor a</label>
  <input type="number" v-model="filtros.puntuacionMayorA">
  <label>Puntuación menor a</label>
  <input type="number" v-model="filtros.puntuacionMenorA">
  <select v-model="filtros.jugado">
    <option :value="null">¿Jugado?</option>
    <option :value="true">Si</option>
    <option :value="false">No</option>
  </select>
  <button @click="aplicarFiltro">Enviar</button>
  <button @click="resetearFiltros">Limpiar filtros</button>
  <table>
    <thead>
      <tr>
        <th>Título</th>
        <th>Género</th>
        <th>Tiempo</th>
        <th>Descripción</th>
        <th>Puntuación</th>
        <th>¿Jugado?</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="game in store.games" :key="game.id">
        <td>{{ game.titulo }}</td>
        <td>{{ game.genero }}</td>
        <td>{{ game.tiempo }} Horas</td>
        <td>{{ game.descripcion }}</td>
        <td>{{ game.puntuacion }} /10</td>
        <td>{{ game.jugado ? 'Sí' : 'No'}}</td>
        <td> <RouterLink :to="`/formularioUpdate/${game.id}`">
          <button>Editar</button>
          </RouterLink>
        </td>
        <td><button @click="eliminar(game.id)">Eliminar</button></td>
      </tr>
    </tbody>
  </table>
  <p v-if="store.games.length === 0"> No hay juegos disponibles</p>
</template>
<style></style>