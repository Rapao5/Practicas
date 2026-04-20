<script setup>
import { onMounted, computed } from 'vue';
import { useEcosistemaStore } from '../stores/ecosistemaStore';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const store = useEcosistemaStore();
const idUrl = route.params.id;
const ecosistema = computed(() => store.ecosistemaActual);
onMounted(async () => {
  await store.fetchEcosistemaById(idUrl);
});

const volver = async (datos) => {
  router.push(`/ecosistema`);
}

const handleEliminar = async () => {
  const confirmado = confirm("¿Estás seguro de que quieres eliminar este ecosistema? Esta acción no se puede deshacer.");
  
  if (confirmado) {
    const exito = await store.deleteEcosistema(idUrl);
    if (exito) {
      router.push('/ecosistema'); 
    } else {
      alert("Hubo un error al borrar.");
    }
  }
};
</script>
<template>
<div class="p-6 md:p-10 bg-slate-50 min-h-screen text-center">  
  <div v-if="ecosistema" class="card bg-white w-200  shadow-sm m-auto duration-300 border-l-8 border-emerald-500">
  <div class="card-body">
     <h2 class="text-2xl font-bold text-slate-800 mb-2">
            {{ ecosistema.descripcion }}
      </h2>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Area
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        Area Longitud: {{ ecosistema.areaLongitud }}
        </span>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        Area Latitud: {{ ecosistema.areaLatitud }}
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Estado de conservación
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        {{ ecosistema.conservacion }}
        </span>
      </div>
      <div v-if="ecosistema.proyectos && ecosistema.proyectos.length > 0" class="m-3">
            <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Proyectos Asociados
            </h3>
              <span 
                v-for="(proyecto, pIndex) in ecosistema.proyectos" 
                :key="pIndex"
                class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
                {{ proyecto }}
              </span>
          </div>
          <div v-else class="mt-4 flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg">
            <span>No hay proyectos activos asignados a este ecosistema.</span>
          </div>
      </div>
      <div>
        <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
      </div>
      <br>
      <div class="p-3"> 
          <RouterLink :to="`/editarEcosistema/${ecosistema.id}`">
            <button class="btn bg-emerald-500">Editar</button>
          </RouterLink>
          <button @click="handleEliminar" class="btn m-2 bg-red-500">
              Eliminar ecosistema
          </button>
      </div>
  </div>
</div>
</template>