<script setup>
import { ref } from 'vue';

const props = defineProps({
  playerInicial: {
    type: Object,
    default: () => ({
      nombre: "",
      especialidad: "",
      idGames: []
    })
  }
});

const playerForm = ref({...props.playerInicial });
const emit = defineEmits(['enviar']);
const errores = ref({
  nombre: "",
  especialidad: ""
});

const validar = () => {
  errores.value = { 
    nombre: "",
    especialidad: ""
  };
  let valido = true;
  if(playerForm.value.nombre.trim().length < 3){
    errores.value.nombre = "El nombre debe contener min 3 caracteres"
    valido = false
  }
  if(playerForm.value.especialidad.trim().length < 3){
    errores.value.especialidad = "La especialidad debe contener min 3 caracteres"
    valido = false
  }
  return valido;
}

const guardar = () => {
  if(validar()){
    const ids = playerForm.value.idGames; 
    
    if (typeof ids === 'string') {
      playerForm.value.idGames = ids .split(',')
          .map(id => parseInt(id.trim()))
          .filter(id => !isNaN(id));
    }

    emit('enviar', playerForm.value);
  }
}
</script>
<template>
  <div class="bg-gray-200 grid p-5 max-w-md text-center min-h-60 min-w-96">
    <label>Nombre</label>
    <input type="text" v-model="playerForm.nombre" placeholder="Ej: Jose el profe">
    <span class="text-red-400" v-if="errores.nombre">{{ errores.nombre }}</span>
    <label>Especialidad</label>
    <input type="text" v-model="playerForm.especialidad" placeholder="Ej: Código">
    <span class="text-red-400" v-if="errores.especialidad">{{ errores.especialidad }}</span>
    <label>Id games</label>
    <input type="text" v-model="playerForm.idGames" placeholder="Ej: 1,2">
    <button class="btn bg-blue-200 mt-3" type="button" @click="guardar">Guardar juego</button>
  </div>
</template>
<style></style>