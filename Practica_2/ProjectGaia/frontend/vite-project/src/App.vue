<script setup>
import { ref, onMounted, computed } from 'vue' 

const sensors = ref([]) 
const searchZona = ref('')
const searchEstado = ref('')
const searchTemperaturaAlta = ref('')
const searchTemperaturaBaja = ref('')
const estados={
  ACTIVO: 'Activo',
  INACTIVO: 'Inactivo',
  MANTENIMIENTO: 'Mantenimiento'
}

const fetchSensors = async() =>{
  const API_URL = 'https://localhost:7016/api/sensors';
  const response = await fetch(API_URL);
  const data = await response.json();
  sensors.value = data;
}
onMounted(()=>{
  fetchSensors();
})
const searchSensor = computed(()=>{
  const search_zona = searchZona.value.toLowerCase();
  const search_estado = searchEstado.value;
  const search_temperaturaAlta = searchTemperaturaAlta.value;
  const search_temperaturaBaja = searchTemperaturaBaja.value;

  return sensors.value.filter(sensor =>{
      const zona = sensor.zona.toLowerCase().includes(search_zona);
      const estado = search_estado === "" || sensor.estado === search_estado;
      const temperaturaBaja = search_temperaturaBaja === "" || sensor.temperatura <= search_temperaturaBaja;
      const temperaturaAlta = search_temperaturaAlta === "" ||  sensor.temperatura >= search_temperaturaAlta;
      return zona && estado && temperaturaAlta && temperaturaBaja;
  });
});
</script>

<template>
  <div>
    <h1>Zonas y temperatura</h1>
    <label>Zona</label>
      <input v-model="searchZona" placeholder="Filtrar zonas..." />
      <br>
      <label>Estado</label>
      <select v-model="searchEstado">
        <option value="">Todos los estados</option>
        <option v-for="(val, key) in estados" :key="key" :value="val">{{ val }}</option>
      </select>
      <br>
      <label>Temperatura mayor a</label>
      <input v-model="searchTemperaturaAlta" placeholder="Ej. 20 ºC" type="number">
      <br>
      <label>Temperatura menor a</label>
      <input v-model="searchTemperaturaBaja" placeholder="Ej. 0 ºC" type="number">
    <button @click="fetchSensors">Buscar</button>
      <ul>
        <li v-for="sensor in searchSensor" :key="sensor.id">
          {{ sensor.zona }}: {{ sensor.estado }}: {{ sensor.temperatura }} ºC
        </li>
      </ul>
      <p v-if="searchSensor.length === 0">No se encontraron sensores.</p>
    </div>
</template>

<style>
button{
    margin-top: 20px;
  }
div{
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>