<script setup>
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useProyectoStore } from '../stores/proyectoStore';

const route = useRoute();
const store = useProyectoStore();
const router = useRouter();
const idUrl = route.params.id;
const proyecto = computed(() => store.proyectoActual);
onMounted(async () => {
  await store.fetchProyectoById(idUrl);
});

const volver = async (datos) => {
  router.push(`/proyecto`);
}

const handleEliminar = async () => {
  const confirmado = confirm("¿Estás seguro de que quieres eliminar este proyecto? Esta acción no se puede deshacer.");
  
  if (confirmado) {
    const exito = await store.deleteProyecto(idUrl);
    if (exito) {
      router.push('/proyecto'); 
    } else {
      alert("Hubo un error al borrar.");
    }
  }
};
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-50 min-h-screen text-center">  
  <div v-if="proyecto" class="card bg-white w-200  shadow-sm m-auto duration-300 border-l-8 border-emerald-500">
  <div class="card-body">
     <h2 class="text-2xl font-bold text-slate-800 mb-2">
            {{ proyecto.nombre }}
      </h2>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Presupuesto
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        {{ proyecto.presupuesto }} €
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Fechas
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
          Fecha de inicio: {{ proyecto.fechaInicio }}
        </span>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
          Fecha de final: {{ proyecto.fechaFinal }}
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Estado
        </h3>
        <span v-if="proyecto.estado === true" class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
          Activo
        </span>
        <span v-else class="badge badge-outline badge-md py-3 px-4 text-red-700 border-red-200 bg-red-50 m-2">
          Terminado
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Ecosistema
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
          {{ proyecto.ecosistema }}
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Especie foco
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
         {{ proyecto.especieFoco }}
        </span>
      </div>
      <div class="p-3">
          <h2 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">Investigadores:</h2>
          <div v-for="(investigador, iIndex) in proyecto.investigadores" :key="iIndex">
              <span class="badge badge-outline badge-md py-3 mt-1 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                {{ investigador }}: {{ proyecto.investigadoresRol[iIndex] }}
              </span>
          </div>
            <span class="mt-4 flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg" v-if="proyecto.investigadores.length == 0">
              No hay investigadores asignados a este proyecto
            </span>
          </div>
      </div>
      <div>
        <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
      </div>
      <br>
      <div class="p-3"> 
          <RouterLink :to="`/editarProyecto/${proyecto.id}`">
            <button class="btn bg-emerald-500">Editar</button>
          </RouterLink>
          <RouterLink :to="`/registroAsignacion/${proyecto.id}`">
            <button class="btn bg-emerald-500 m-2">Añadir asignación</button>
          </RouterLink>
           <RouterLink :to="`/asignacion/${proyecto.id}`">
            <button class="btn bg-emerald-500">Gestionar asignaciones</button>
          </RouterLink>
          <button @click="handleEliminar" class="btn bg-red-500 m-2">
              Eliminar proyecto
          </button>
      </div>
  </div>
</div>
</template>