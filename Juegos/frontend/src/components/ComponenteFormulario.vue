<script setup>
import { ref } from 'vue';

const props = defineProps({
  juegoInicial: {
    type: Object,
    default: () => ({
      id: 0,
      titulo: "",
      genero: "",
      descripcion: "",
      jugado: false,
      tiempo: 0,
      puntuacion: 0
    })
  }
});
const gameForm = ref({...props.juegoInicial });
const emit = defineEmits(['enviar']);
const guardar = () => {
  emit('enviar', gameForm.value);
}
</script>
<template>
  <label>Título</label>
  <input type="text" v-model="gameForm.titulo">
  <label>Género</label>
  <input type="text" v-model="gameForm.genero">
  <label>Descripción</label>
  <textarea v-model="gameForm.descripcion"></textarea>
  <select v-model="gameForm.jugado">
    <option :value="false">No</option>
    <option :value="true">Sí</option>
  </select>
  <label>Tiempo de juego</label>
  <input type="number" v-model.number="gameForm.tiempo">
  <label>Puntuación</label>
  <input type="number" v-model.number="gameForm.puntuacion">
  <button type="button" @click="guardar">Guardar juego</button>
</template>
<style></style>