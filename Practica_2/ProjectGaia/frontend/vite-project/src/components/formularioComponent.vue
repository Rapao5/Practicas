<script setup>
import { ref, onMounted } from 'vue'
import { useSensorStore } from '@/stores/sensorStore'

const props = defineProps({
  id: {
    type: String,
    default: null
  }
})

const emit = defineEmits(['guardado', 'cancelar'])

const sensorStore = useSensorStore()
const sensorData = ref({ zona: '', estado: 'Activo', temperatura: 0 })

onMounted(async () => {
  if (props.id) {
    try {
      sensorData.value = await sensorStore.getSensorById(props.id)
    } catch (error) {
      console.error(error)
      emit('cancelar')
    }
  }
})

const manejarGuardar = async () => {
  let ok = props.id 
    ? await sensorStore.updateSensor(sensorData.value)
    : await sensorStore.postSensor(sensorData.value)

  if (ok) emit('guardado')
}
</script>

<template>
  <div class="principal">
    <h3>{{ id ? 'Editar Sensor' : 'Nuevo sensor' }}</h3>
      <label>Zona:</label>
      <input v-model="sensorData.zona" placeholder="Nombre de la zona..." />

      <label>Estado:</label>
      <select v-model="sensorData.estado">
        <option v-for="(val, key) in sensorStore.estados" :key="key" :value="val">
          {{ val }}
        </option>
      </select>

      <label>Temperatura (ºC):</label>
      <input v-model.number="sensorData.temperatura" type="number"/>

      <div>
        <button class="boton_crear" @click="manejarGuardar">
          {{ id ? 'Actualizar' : 'Guardar' }}
        </button>
        <button @click="$emit('cancelar')">Cancelar</button>
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
