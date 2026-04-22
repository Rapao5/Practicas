<script setup>
import { onMounted, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useEcosistemaStore } from '../stores/ecosistemaStore';
import EcosistemaForm from '../components/ecosistemaComponente.vue';

const route = useRoute();
const router = useRouter();
const store = useEcosistemaStore();
const idUrl = route.params.id;

const ecosistema = computed(() => store.ecosistemaActual);

const volver = async (datos) => {
  router.push(`/ecosistema`)
}

onMounted(async () => {
  await store.fetchEcosistemaById(idUrl);
});

const handleActualizar = async (datos) => {
  const exito = await store.updateEcosistema(idUrl, datos);
  if (exito) {
    router.push(`/ecosistema`);
  }
};
</script>

<template>
  <div class="p-10 bg-slate-50 min-h-screen">
    <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Editar Ecosistema</h1>
    <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
    <EcosistemaForm 
      v-if="ecosistema" 
      :initialData="ecosistema" 
      @enviar="handleActualizar" 
    />
    
    <div v-else class="text-center">
      <span class="loading loading-spinner loading-lg text-emerald-500"></span>
    </div>
  </div>
</template>