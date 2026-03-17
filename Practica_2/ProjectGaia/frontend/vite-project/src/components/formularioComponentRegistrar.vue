<script setup>
import { ref } from 'vue'

const props = defineProps(['modelo', 'textoBoton', 'estados'])
const emit = defineEmits(['enviar', 'cancelar'])

const sensorData = ref({ ...props.modelo })

const guardar = () => {
  emit('enviar', sensorData.value)
}
</script>

<template>
  <div class="principal">
    <h3>{{ textoBoton }}</h3>
    <label>Zona:</label>
    <input v-model="sensorData.zona" placeholder="Nombre de la zona..." />

    <label>Estado:</label>
    <select v-model="sensorData.estado">
      <option v-for="estado in estados" :key="estado" :value="estado">
        {{ estado }}
      </option>
    </select>

    <label>Temperatura (ºC):</label>
    <input v-model.number="sensorData.temperatura" type="number"/>

    <div>
      <button class="boton_crear" @click="guardar">{{ textoBoton }}</button>
      <button @click="emit('cancelar')">Cancelar</button>
    </div>
  </div>
</template>

<style>
.boton_crear{
  margin-top: 20px;
}
.principal{
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
