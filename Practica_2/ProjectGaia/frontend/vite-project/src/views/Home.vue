<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useSensorStore } from '@/stores/sensorStore'

const router = useRouter()
const sensorStore = useSensorStore()

const searchZona = ref('')
const searchEstado = ref('')
const searchTemperaturaAlta = ref('')
const searchTemperaturaBaja = ref('')

const buscar = () => {
  sensorStore.fetchSensors({
    zona: searchZona.value,
    estado: searchEstado.value,
    tempMin: searchTemperaturaAlta.value,
    tempMax: searchTemperaturaBaja.value
  });
}

const irACrear = () => router.push('/formulario')
const irAEditar = (id) => router.push(`/formulario/${id}`)

onMounted(() => {
  buscar();
})
</script>

<template>
  <div class="principal">
    <h1>Zonas y temperatura</h1>
    
    <label>Zona</label>
    <input v-model="searchZona" placeholder="Filtrar zonas..." />
    <br>
    <label>Estado</label>
    <select v-model="searchEstado">
      <option value="">Todos los estados</option>
      <option v-for="(val, key) in sensorStore.estados" :key="key" :value="val">{{ val }}</option>
    </select>
    <br>
    <label>Temperatura mayor a</label>
      <input v-model="searchTemperaturaAlta" placeholder="Ej. 20 ºC" type="number">
      <br>
      <label>Temperatura menor a</label>
      <input v-model="searchTemperaturaBaja" placeholder="Ej. 0 ºC" type="number">
    <button @click="buscar">Buscar</button>

    <ul>
      <li v-for="sensor in sensorStore.sensors" :key="sensor.id">
        {{ sensor.zona }}: {{ sensor.estado }}: {{ sensor.temperatura }} ºC
        <button @click="irAEditar(sensor.id)">Editar</button>
        <button @click="sensorStore.deleteSensor(sensor.id)">Borrar</button>
      </li>
    </ul>

    <p v-if="sensorStore.sensors.length === 0">No se encontraron sensores.</p>
    <button class="boton_crear" @click="irACrear">Crear un sensor</button>
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