<script setup>
import { useRoute, useRouter } from 'vue-router';
import { useInvestigadorStore } from '../stores/investigadorStore';
import { computed, onMounted } from 'vue';
import InvestigadorForm from '../components/investigadorComponente.vue';


const store = useInvestigadorStore();
const route = useRoute();
const router = useRouter();
const idUrl = route.params.id;

const investigador = computed(() => store.investigadorActual);

onMounted(async () => {
  await store.fetchInvestigadorById(idUrl);
});

const volver = async (datos) => {
  router.push(`/investigador`)
}

const handleActualizar = async (datos) => {
  const exito = await store.updateInvestigador(idUrl, datos);
  if(exito){
    router.push(`/investigador`);
  }
}
</script>
<template>
    <div class="p-10 bg-slate-50 min-h-screen">
      <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Editar Investigador</h1>
      <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
      <InvestigadorForm 
        v-if="investigador" 
        :initialData="investigador" 
        @enviar="handleActualizar" 
      />
      
      <div v-else class="text-center">
        <span class="loading loading-spinner loading-lg text-emerald-500"></span>
      </div>
    </div>
</template>