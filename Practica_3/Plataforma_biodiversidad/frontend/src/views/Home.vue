<script setup> 
import { onMounted, computed } from 'vue';
import { useEcosistemaStore } from '../stores/ecosistemaStore';
import { useProyectoStore } from '../stores/proyectoStore';
import { useInvestigadorStore } from '../stores/investigadorStore';

const store = useEcosistemaStore();
const storeProyeto = useProyectoStore();
const storeInvestidor = useInvestigadorStore();


const proyectosActivos = computed(() => {
  return storeProyeto.proyectoList.filter(proyecto => proyecto.estado === true);
});

onMounted(async () => {
  await store.fetchEcosistemas();
  await storeProyeto.fetchProyectos();
  await storeInvestidor.fetchInvestigadores();
});
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-50 min-h-screen text-center"> 
    <h1 class="text-4xl font-extrabold text-emerald-800 tracking-tight">
        Plataforma de Biodiversidad 🌿
    </h1> 
  <div class="relative w-full mt-10">
    <img
    src="https://actuamosporelclima.org/wp-content/uploads/2021/09/Ecosistemas-terrestres-header.jpg"
      class="w-full" />
  </div>
 <div class="mt-10 grid grid-cols-1 md:grid-cols-3 gap-8 px-4">
  
  <div class="card bg-white shadow-md hover:shadow-2xl hover:-translate-y-2 transition-all duration-300 border-b-4 border-emerald-500">
    <div class="card-body items-center text-center">
      <div class="bg-emerald-100 p-3 rounded-full mb-2">
        <span class="text-2xl">🌍</span> 
      </div>
      <h2 class="card-title text-slate-400 uppercase text-xs tracking-widest">Ecosistemas</h2>
      <p class="text-5xl font-black text-slate-800 my-2">{{ store.totalEcosistemas }}</p>
      <p class="text-slate-500 text-sm">Hábitats bajo seguimiento</p>
      <div class="card-actions mt-6 w-full">
        <RouterLink :to="`/ecosistema`" class="w-full">
          <button class="btn bg-emerald-500 hover:bg-emerald-600 text-white border-none w-full shadow-lg shadow-emerald-200">
            Ver Listado
          </button>
        </RouterLink>
      </div>
    </div>
  </div>

  <div class="card bg-white shadow-md hover:shadow-2xl hover:-translate-y-2 transition-all duration-300 border-b-4 border-sky-500">
    <div class="card-body items-center text-center">
      <div class="bg-sky-100 p-3 rounded-full mb-2">
        <span class="text-2xl">🔬</span>
      </div>
      <h2 class="card-title text-slate-400 uppercase text-xs tracking-widest">Proyectos</h2>
      <p class="text-5xl font-black text-slate-800 my-2">{{ proyectosActivos.length }}</p>
      <p class="text-slate-500 text-sm">Investigaciones en curso</p>
      <div class="card-actions mt-6 w-full">
        <RouterLink :to="`/proyecto`" class="w-full">
          <button class="btn bg-sky-500 hover:bg-sky-600 text-white border-none w-full shadow-lg shadow-sky-200">
            Explorar Proyectos
          </button>
        </RouterLink>
      </div>
    </div>
  </div>

  <div class="card bg-white shadow-md hover:shadow-2xl hover:-translate-y-2 transition-all duration-300 border-b-4 border-amber-500">
    <div class="card-body items-center text-center">
      <div class="bg-amber-100 p-3 rounded-full mb-2">
        <span class="text-2xl">👥</span>
      </div>
      <h2 class="card-title text-slate-400 uppercase text-xs tracking-widest">Investigadores</h2>
      <p class="text-5xl font-black text-slate-800 my-2">{{ storeInvestidor.totalInvestigadores }}</p>
      <p class="text-slate-500 text-sm">Expertos asignados</p>
      <div class="card-actions mt-6 w-full">
        <RouterLink :to="`/investigador`" class="w-full">
          <button class="btn bg-amber-500 hover:bg-amber-600 text-white border-none w-full shadow-lg shadow-amber-200">
            Gestionar Equipo
          </button>
        </RouterLink>
      </div>
    </div>
  </div>

</div>
</div>
</template>
<style>
</style>