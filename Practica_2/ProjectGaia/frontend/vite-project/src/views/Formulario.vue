<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()
const estados = {
  ACTIVO: 'Activo',
  INACTIVO: 'Inactivo',
  MANTENIMIENTO: 'Mantenimiento'
}
const nuevoSensor = ref({
  zona: '',
  estado: 'Activo',
  temperatura: 0
})

onMounted(async () => {
  const id = route.params.id 
  if (id) {
    const response = await fetch(`https://localhost:7016/api/sensors/${id}`);
    if (response.ok) {
      const data = await response.json();
      nuevoSensor.value = data;
    } else {
      console.error("Sensor no encontrado");
      router.push('/');
    }
  }
})

const postSensor = async () => {
  const response = await fetch('https://localhost:7016/api/sensors', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(nuevoSensor.value)
  })
  if (response.ok) {
    router.push('/') 
  }
}

const updateSensor = async () => {
  const response = await fetch('https://localhost:7016/api/sensors', {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(nuevoSensor.value)
  })
  if (response.ok) {
    router.push('/')
  }
}
</script>
<template>
  <h3>{{nuevoSensor.id ? 'Editar Sensor' : 'Nuevo sensor'}}</h3>
    <div>
      <label>Zona:</label>
      <input v-model="nuevoSensor.zona" placeholder="Nombre de la zona..." />

      <label>Estado:</label>
      <select v-model="nuevoSensor.estado">
        <option v-for="(val, key) in estados" :key="key" :value="val">{{ val }}</option>
      </select>

      <label>Temperatura (ºC):</label>
      <input v-model.number="nuevoSensor.temperatura" type="number"/>

      <button v-if="!nuevoSensor.id" @click="postSensor">Guardar</button>
      <button v-else @click="updateSensor">Actualizar</button>
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