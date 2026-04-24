<script setup>
import { useRoute, useRouter } from 'vue-router';
import { useProyectoStore } from '../../stores/proyectoStore';
import { computed, onMounted } from 'vue';
import ProyectoForm from './ProyectoComponente.vue';

const route = useRoute();
const router = useRouter();
const store = useProyectoStore();
const idUrl = route.params.id;

const proyecto = computed(() => store.proyectoActual);

onMounted(async () => {
  await store.fetchProyectoById(idUrl);
});

const volver = async (datos) => {
  router.push(`/proyecto`)
}

const handleActualizar = async (datos) => {
  const proyectoData = { ...datos, id: Number(idUrl) };
  const exito = await store.updateProyecto(idUrl, datos);
  if(exito){
    router.push(`/proyecto`);
  }
}
</script>
<template>
    <div class="p-10 bg-slate-50 min-h-screen">
      <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Editar Proyecto</h1>
      <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
      <ProyectoForm 
        v-if="proyecto" 
        :initialData="proyecto" 
        @enviar="handleActualizar" 
      />
      
      <div v-else class="text-center">
        <span class="loading loading-spinner loading-lg text-emerald-500"></span>
      </div>
    </div>
</template>