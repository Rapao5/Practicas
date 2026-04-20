<script setup>
import { useRouter } from 'vue-router';
import ProyectoForm from '../components/ProyectoComponente.vue';
import { useProyectoStore } from '../stores/proyectoStore';

const store = useProyectoStore();
const router = useRouter();

const volver = async (datos) => {
  router.push('/proyecto')
}

const handleCrear = async (datos) => {
  const exito = await store.createProyecto(datos);
  if(exito){
    router.push('/proyecto');
  }
};

</script>
<template>
  <div class="p-10 bg-slate-50 min-h-screen">
    <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Nuevo Proyecto</h1>
    <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
    <ProyectoForm @enviar="handleCrear" />
  </div>
</template>