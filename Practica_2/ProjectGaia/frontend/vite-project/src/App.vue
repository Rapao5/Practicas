<script setup>
import { ref, onMounted, computed } from 'vue' 

const sensors = ref([]) 
const searchQuery = ref('')

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
  const search = searchQuery.value.toLowerCase();

  return sensors.value.filter(sensor =>{
      return sensor.zona.toLowerCase().includes(search);
  });
});
</script>

<template>
  <h1>Zonas y temperatura</h1>
    <input v-model="searchQuery" placeholder="Filtrar sensores..." />
  <button @click="fetchSensors">Buscar</button>
    <ul>
      <li v-for="sensor in searchSensor" :key="sensor.id">
        {{ sensor.zona }}: {{ sensor.estado }}: {{ sensor.temperatura }} ºC
      </li>
    </ul>
    
    <p v-if="searchSensor.length === 0">No se encontraron sensores.</p>
</template>

<style>

</style>