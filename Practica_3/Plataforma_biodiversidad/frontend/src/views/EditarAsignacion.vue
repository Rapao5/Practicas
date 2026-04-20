<script setup>
import { useRoute, useRouter } from 'vue-router';
import { useAsignacionStore } from '../stores/asignacionStore';
import { onMounted, computed } from 'vue';
import AsignacionForm from '../components/AsignacionesComponente.vue';


const route = useRoute();
const router = useRouter();
const store = useAsignacionStore();
const idUrl = route.params.id;

const asignacion = computed(() => store.asignacionActual);

onMounted(async () => {
  await store.fetchAsignacionById(idUrl);
});

const volver = async (datos) => {
  router.push(`/asignacion/${datos.proyectoId}`)
}

const handleActualizar = async (datos) => {
  const exito = await store.updateAsignacion(idUrl, datos);
  if(exito) {
    router.push(`/asignacion/${datos.proyectoId}`)
  }
};
</script>
<template>
  <div class="p-10 bg-slate-50 min-h-screen">
    <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Editar asignación</h1>
    <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
    <AsignacionForm 
      v-if="asignacion" 
      :initialData="asignacion" 
      :proyectoId="asignacion.proyectoId"
      @enviar="handleActualizar" 
    />
    
    <div v-else class="text-center">
      <span class="loading loading-spinner loading-lg text-emerald-500"></span>
    </div>
  </div>
</template>