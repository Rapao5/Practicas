<script setup>
import { useRouter } from 'vue-router';
import { useInvestigadorStore } from '../stores/investigadorStore';
import InvestigadorForm from '../components/investigadorComponente.vue';


const store = useInvestigadorStore();
const router = useRouter();

const volver = async (datos) => {
  router.push('/investigador')
}

const handleCrear = async (datos) => {
  const exito = await store.createInvestigador(datos);
  if(exito){
      router.push('/investigador');
  }
};
</script>
<template>
    <div class="p-10 bg-slate-50 min-h-screen">
      <h1 class="text-3xl font-bold text-slate-800 mb-8 text-center">Nuevo investigador</h1>
      <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
      <InvestigadorForm @enviar="handleCrear" />
    </div>
</template>