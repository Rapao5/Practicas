<script setup>
import { onMounted } from 'vue';
import { useEcosistemaStore } from '../stores/ecosistemaStore';

const store = useEcosistemaStore();

onMounted(async () => {
  await store.fetchEcosistemas();
});
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-50 min-h-screen">  
    <header class="mb-10 text-center">
      <h2 class="text-black mt-10 text-3xl">
        Ecosistemas
      </h2>
      <p class="text-slate-500 mt-2 text-lg">
        Total de ecosistemas gestionados: 
        <span class="badge badge-lg badge-primary font-bold">{{ store.totalEcosistemas }}</span>
      </p>
      <RouterLink :to="`/registroEcosistema`">
        <button class="btn bg-emerald-500">Añadir ecosistema</button>
      </RouterLink>
    </header>
    <div class="grid grid-cols-1 gap-6">
      <div v-for="(ecosistema, index) in store.ecosistemaList" 
        :key="ecosistema.id || index" 
        class="card bg-white shadow-xl hover:shadow-2xl transition-shadow duration-300 border-l-8 border-emerald-500">
        <div class="card-body grid grid-cols-4">
          <h2 class="card-title text-2xl text-slate-800 mb-2">
            {{ ecosistema.descripcion }}
          </h2>

          <div v-if="ecosistema.proyectos && ecosistema.proyectos.length > 0" class="mt-4">
            <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Proyectos Asociados
            </h3>
            <div class="flex flex-wrap gap-2">
              <span 
                v-for="(proyecto, pIndex) in ecosistema.proyectos" 
                :key="pIndex"
                class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                {{ proyecto }}
              </span>
            </div>
          </div>
          <div v-else class="mt-4 flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg">
            <span>No hay proyectos activos asignados a este ecosistema.</span>
          </div>
          <div class="col-start-4">
            <RouterLink :to="{
              path: `/mostrarEcosistema/${ecosistema.id}`
            }">
              <button class="btn m-2 bg-emerald-500">
                  Ver Ecosistema
              </button>
            </RouterLink>
            <br>
            <RouterLink :to="`/editarEcosistema/${ecosistema.id}`">
              <button class="btn bg-emerald-500">Editar Ecosistema</button>
            </RouterLink>
          </div>
        </div>
      </div>
    </div>
  </div>  
</template>