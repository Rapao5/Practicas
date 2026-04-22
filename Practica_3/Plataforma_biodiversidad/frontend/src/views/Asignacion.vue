<script setup>
import { onMounted, ref } from 'vue';
import { useAsignacionStore } from '../stores/asignacionStore';
import { useProyectoStore } from '../stores/proyectoStore';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const store = useAsignacionStore();
const proStore = useProyectoStore();
const idUrl = route.params.id;
const proyecto = ref({ nombre: 'Cargando...' });

const volver = async () => {
  router.push(`/proyecto`)
}

onMounted(async () => {
  await store.fetchAsignacionesByProyecto(idUrl);
  try {
    const data = await proStore.fetchProyectoById(idUrl);
    if (data) {
      proyecto.value = data;
    }
  } catch (error) {
    console.error("Error al obtener el proyecto:", error);
  }
});
const handleEliminar = async (asignacionId) => {
  const confirmado = confirm("¿Estás seguro de que quieres eliminar esta asignación? Esta acción no se puede deshacer.");
  
  if (confirmado) {
    const exito = await store.deleteAsignacion(asignacionId);
    if (exito) {
      
    } else {
      alert("Hubo un error al borrar.");
    }
  }
};
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-50 min-h-screen">
    <header class="mb-10 text-center">
      <h2 class="text-black mt-10 text-3xl">
        Asignaciones en {{ proyecto.nombre }}
      </h2>
        <p class="text-slate-500 mt-2 text-lg">
        Total de asignaciones: 
        <span class="badge badge-lg badge-primary font-bold">{{ store.totalAsignacionesProyecto}}</span>
      </p>
        <RouterLink :to="`/registroAsignacion/${proyecto.id}`">
            <button class="btn bg-emerald-500">Añadir asignación</button>
        </RouterLink>
    </header>
    <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
    <div class="grid grid-cols-1 gap-6">
  <div v-for="(asignacion, index) in store.asignacionListProyecto" 
    :key="asignacion.id || index" 
    class="card bg-white shadow-xl hover:shadow-2xl transition-shadow duration-300 border-l-8 border-emerald-500">
    
    <div class="card-body grid grid-cols-1 md:grid-cols-4 gap-4 items-center">
      
      <div class="mt-2">
        <h2 class="card-titl text-slate-800 mb-2 md:text-base uppercase text-xs font-bold tracking-wider">
          Nombre: 
        </h2>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 w-full md:w-auto">
          {{ asignacion.investigador }}
        </span>
      </div>

      <div class="mt-2">
        <h3 class="card-title text-slate-800 mb-2 md:text-base uppercase text-xs font-bold tracking-wider">
          Rol: 
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 w-full md:w-auto">
          {{ asignacion.rol }}
        </span>
      </div>

      <div class="mt-2">
        <h3 class="card-title text-slate-800 mb-2 md:text-base uppercase text-xs font-bold tracking-wider">
          Fecha de entrada:
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 w-full md:w-auto">
          {{ asignacion.fechaEntrada }}
        </span>
      </div>

      <div class="flex flex-col gap-2 mt-4 md:mt-0 md:items-end">
        <RouterLink :to="{ path: `/editarAsignacion/${asignacion.id}` }" class="w-full md:w-auto">
          <button class="btn bg-emerald-500 text-white w-full shadow-md border-none">
            Editar asignación
          </button>
        </RouterLink>
        
        <button @click="handleEliminar(asignacion.id)" class="btn bg-red-500 text-white w-full md:w-auto shadow-md border-none">
          Eliminar asignación
        </button>
      </div>

    </div>
  </div>
</div>
  </div>
</template>