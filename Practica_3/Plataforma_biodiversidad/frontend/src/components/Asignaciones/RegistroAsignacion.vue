<script setup>
import { useRoute, useRouter } from 'vue-router';
import AsginacionForm from './AsignacionesComponente.vue';
import { useAsignacionStore } from '../../stores/asignacionStore';

const store = useAsignacionStore();
const router = useRouter();
const route = useRoute();
const idProyecto = route.params.id;

const volver = async () => {
  router.push(`/asignacion/${idProyecto}`)
}

const handleCrear = async (datos) => {
  const exito = await store.createAsignacion(datos);
  if(exito){
    router.push(`/asignacion/${idProyecto}`)
  }
} 
</script>
<template>
  <div class="p-10 bg-slate-50 min-h-screen">
    <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Nueva asignación</h1>
    <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
    <AsginacionForm :proyectoId="idProyecto" @enviar="handleCrear" />
  </div>
</template>