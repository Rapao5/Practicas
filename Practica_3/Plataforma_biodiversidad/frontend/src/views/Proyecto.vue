<script setup> 
import { onMounted} from 'vue';
import { useProyectoStore } from '../stores/proyectoStore';

const store = useProyectoStore();


onMounted(async () => {
  await store.fetchProyectos();
});
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-50 min-h-screen"> 
    <header class="mb-10 text-center">
        <h2 class="text-black mt-10 text-3xl">
          Proyectos
        </h2>
          <p class="text-slate-500 mt-5 text-lg">
          Total de proyectos: 
          <span class="badge badge-lg badge-primary font-bold">{{ store.proyectoList.length }}</span>
          </p>
          <RouterLink :to="`/registroProyecto`">
            <button class="btn bg-emerald-500">Añadir proyecto</button>
          </RouterLink>
    </header>
      <div class="grid grid-cols-1 gap-6">
        <div v-for="(proyecto, index) in store.proyectoList"
        :key="proyecto.id || index"
        class="card bg-white shadow-xl hover:shadow-2xl transition-shadow duration-300 border-l-8 border-emerald-500">
          <div class="card-body grid grid-cols-4">
              <h2 class="card-title text-2xl text-slate-800 mb-2">
                  {{ proyecto.nombre}}
              </h2>
              <div>
                <h2 class="card-title text-xl text-slate-600 mb-2">Estado:</h2>
                <span v-if="proyecto.estado == true" class="badge badge-outline badge-md py-3 mt-1 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">Activo</span>
                <span v-else class="badge badge-outline badge-md py-3 mt-1 px-4 text-red-700 border-red-200 bg-red-50">Terminado</span>
              </div>
              <div>
                <h2 class="card-title text-xl text-slate-600 mb-2">Investigadores:</h2>
                <div v-for="(investigador, iIndex) in proyecto.investigadores" :key="iIndex">
                    <span class="badge badge-outline badge-md py-3 mt-1 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                      {{ investigador }}: {{ proyecto.investigadoresRol[iIndex] }}
                    </span>
                </div>
                <span class="mt-4 flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg" v-if="proyecto.investigadores.length == 0">No hay investigadores asignados a este proyecto</span>
              </div>
              <div>
                <RouterLink :to="{
                  path: `/mostrarProyecto/${proyecto.id}`
                }">
                <button class="btn m-2 bg-emerald-500">
                    Ver proyecto
                </button>
                </RouterLink>
                <br>
                <RouterLink :to="`/editarProyecto/${proyecto.id}`">
                  <button class="btn bg-emerald-500">Editar proyecto</button>
                </RouterLink>
              </div>
          </div>
        </div>
      </div>
  </div>
</template>