<script setup>
import EcosistemaForm from '../components/ecosistemaComponente.vue';
import { useEcosistemaStore } from '../stores/ecosistemaStore';
import { useRouter } from 'vue-router';

const store = useEcosistemaStore();
const router = useRouter();

const volver = async (datos) => {
  router.push('/ecosistema')
}

const handleCrear = async (datos) => {
  const exito = await store.createEcosistema(datos);
  if (exito) {
    router.push('/ecosistema'); 
  }
};
</script>

<template>
  <div class="p-10 bg-slate-50 min-h-screen">
    <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Nuevo Ecosistema</h1>
    <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
    <EcosistemaForm @enviar="handleCrear" />
  </div>
</template>