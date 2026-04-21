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
        <div class="card-body grid grid-cols-3">
          <div class="mt-2">
          <h2 class="card-title text-xl text-slate-800 mb-2">
            Nombre: 
          </h2>
          <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                {{ asignacion.investigador }}
              </span>
          </div>
          <div class="mt-2">
              <h3 class="card-title text-xl text-slate-800 mb-2">
                Rol: 
              </h3>
              <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                {{ asignacion.rol }}
              </span>
          </div>
          <div class="mt-2">
              <h3 class="card-title text-xl text-slate-800 mb-2">
                Fecha de entrada:
              </h3>
              <span  class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                  {{ asignacion.fechaEntrada }}
              </span>
          </div>
          <div class="col-start-4">
            <RouterLink :to="{
              path: `/editarAsignacion/${asignacion.id}`
            }">
              <button class="btn m-2 bg-emerald-500">
                  Editar asignación
              </button>
            </RouterLink>
            <br>
              <button @click="handleEliminar(asignacion.id)" class="btn bg-red-500">Eliminar asignación</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>