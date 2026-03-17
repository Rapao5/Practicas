<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useSensorStore } from '@/stores/sensorStore'
import FormularioComponentRegistrar from '@/components/formularioComponentRegistrar.vue'

const route = useRoute()
const router = useRouter()
const sensorStore = useSensorStore()
const sensorACargar = ref(null)

onMounted(async () => {
  sensorACargar.value = await sensorStore.getSensorById(route.params.id)
})

const manejarActualizar = async (datosEditados) => {
  let ok = await sensorStore.updateSensor(datosEditados)
  if (ok) router.push('/')
}
</script>

<template>
  <FormularioComponentRegistrar 
    v-if="sensorACargar"
    :modelo="sensorACargar" 
    :estados="sensorStore.estados"
    textoBoton="Actualizar Sensor"
    @enviar="manejarActualizar"
    @cancelar="router.push('/')"
  />
</template>
<style>

</style>