<script setup>
import { ref, onMounted} from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
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
  const params = new URLSearchParams();

  if (searchZona.value) params.append('zona', searchZona.value);
  if (searchEstado.value) params.append('estado', searchEstado.value);
  if (searchTemperaturaAlta.value !=='') params.append('tempMin', searchTemperaturaAlta.value);
  if (searchTemperaturaBaja.value !=='') params.append('tempMax', searchTemperaturaBaja.value);

  const finalUrl = `${API_URL}?${params.toString()}`;
  const response = await fetch(finalUrl);
  const data = await response.json();
  
  sensors.value = data;
}
const deleteSensor = async(id) =>{
  const API_URL = `https://localhost:7016/api/sensors/${id}`;

  const response = await fetch(API_URL, {
    method: 'DELETE'
  });

  if(response.ok){
    await fetchSensors();
  }
}
const irACrear = () => {
  router.push('/formulario');
}

const irAEditar = (id) => {
  router.push(`/formulario/${id}`);
}
onMounted(()=>{
  fetchSensors();
})

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
        <li v-for="sensor in sensors" :key="sensor.id">
          {{ sensor.zona }}: {{ sensor.estado }}: {{ sensor.temperatura }} ºC
          <button @click="irAEditar(sensor.id)">Editar</button>
          <button @click="deleteSensor(sensor.id)">Borrar</button>
        </li>
      </ul>
      <p v-if="sensors.length === 0">No se encontraron sensores.</p>
      <button @click="irACrear">Crear un sensor</button>
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