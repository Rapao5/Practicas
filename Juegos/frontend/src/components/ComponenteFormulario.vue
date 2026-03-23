<script setup>
import { ref } from 'vue';

const props = defineProps({
  juegoInicial: {
    type: Object,
    default: () => ({
      titulo: "",
      genero: "",
      descripcion: "",
      jugado: false,
      tiempo: 0,
      puntuacion: 0,
      imagen: ""
    })
  }
});

const gameForm = ref({...props.juegoInicial });
const emit = defineEmits(['enviar']);
const errores = ref({
  titulo: "",
  genero: "",
  descripcion: "",
  tiempo: 0,
  puntuacion: 0,
  imagen: ""
});

const validar = () => {
  errores.value = { 
    titulo: "", 
    genero: "", 
    descripcion: "", 
    tiempo: "", 
    puntuacion: ""
  };
  let valido = true;
  if(gameForm.value.titulo.trim().length < 3){
    errores.value.titulo = "El título debe contener min 3 caracteres"
    valido = false
  }
  if(gameForm.value.genero.trim().length < 3){
    errores.value.genero = "El género debe contener min 3 caracteres"
    valido = false
  }
  if(gameForm.value.descripcion.trim().length < 3){
    errores.value.descripcion = "La descripción debe contener min 3 caracteres"
    valido = false
  }
  if(gameForm.value.tiempo < 0){
    errores.value.tiempo = "El tiempo de juego debe ser igual o superior a 0"
    gameForm.value.tiempo = 0
    valido = false
  }
  if(gameForm.value.puntuacion < 0 || gameForm.value.puntuacion > 10){
    errores.value.puntuacion = "La puntuación debe estar entre 0 y 10"
    gameForm.value.puntuacion = 0
    valido=false
  }
  return valido;
}

const guardar = () => {
  if(validar()){
    emit('enviar', gameForm.value);
  }
}
</script>
<template>
  <div class="bg-gray-200 grid p-5 max-w-md text-center min-h-60 min-w-96">
    <label>Imagen</label>
    <input type="text" v-model="gameForm.imagen" placeholder="Url de la imagen">
    <label>Título</label>
    <input type="text" v-model="gameForm.titulo" placeholder="Ej: Crash Bandicoot">
    <span class="text-red-400" v-if="errores.titulo">{{ errores.titulo }}</span>
    <label>Género</label>
    <input type="text" v-model="gameForm.genero" placeholder="Ej: Plataformas">
    <span class="text-red-400" v-if="errores.genero">{{ errores.genero }}</span>
    <label>Descripción</label>
    <textarea v-model="gameForm.descripcion" placeholder="Ej: Crash Bandicoot es un videojuego de plataformas desarrollado por Naughty Dog y publicado por Sony Computer Entertainment para la PlayStation en 1996."></textarea>
    <span class="text-red-400" v-if="errores.descripcion">{{ errores.descripcion }}</span>
    <label>¿Jugado?</label>
    <select v-model="gameForm.jugado">
      <option :value="false">No</option>
      <option :value="true">Sí</option>
    </select>
    <label>Tiempo de juego</label>
    <input type="number" v-model.number="gameForm.tiempo">
    <span class="text-red-400" v-if="errores.tiempo">{{ errores.tiempo }}</span>
    <label>Puntuación</label>
    <input type="number" v-model.number="gameForm.puntuacion">
    <span class="text-red-400" v-if="errores.puntuacion">{{ errores.puntuacion }}</span>
    <button class="btn bg-blue-200 mt-3" type="button" @click="guardar">Guardar juego</button>
  </div>
</template>
<style></style>