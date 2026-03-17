<script setup>
import { useRouter } from 'vue-router'
import { useSensorStore } from '@/stores/sensorStore'
import FormularioComponentRegistrar from '@/components/formularioComponentRegistrar.vue'

const router = useRouter()
const sensorStore = useSensorStore()

const nuevoSensor = { zona: '', estado: 'Activo', temperatura: 0 }

const manejarCrear = async (datos) => {
  let ok = await sensorStore.postSensor(datos)
  if (ok) router.push('/')
}
</script>

<template>
  <FormularioComponentRegistrar 
    :modelo="nuevoSensor" 
    :estados="sensorStore.estados"
    textoBoton="Crear Sensor"
    @enviar="manejarCrear"
    @cancelar="router.push('/')"
  />
</template>
<style>

</style>