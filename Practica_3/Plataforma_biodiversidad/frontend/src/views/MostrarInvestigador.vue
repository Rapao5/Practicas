<script setup>
import { computed, onMounted } from 'vue';
import { useInvestigadorStore } from '../stores/investigadorStore';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const store = useInvestigadorStore();
const idUrl = route.params.id;
const investigador = computed(() => store.investigadorActual);
onMounted(async () => {
  await store.fetchInvestigadorById(idUrl);

});

const volver = async (datos) => {
  router.push(`/investigador`);
}

const handleEliminar = async () => {
  const confirmado = confirm("¿Estás seguro de que quieres eliminar este investigador? Esta acción no se puede deshacer.");
  
  if (confirmado) {
    const exito = await store.deleteInvestigador(idUrl);
    if (exito) {
      router.push('/investigador'); 
    } else {
      alert("Hubo un error al borrar.");
    }
  }
};
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-100 min-h-screen text-center">  
  <div v-if="investigador" class="card bg-white w-200  shadow-sm m-auto duration-300 border-l-8 border-emerald-500">
  <div class="card-body">
     <h2 class="text-2xl font-bold text-slate-800 mb-2">
          Investigador con ID:  {{ investigador.id }}
      </h2>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Nombre
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        {{ investigador.nombre }}
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Email
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        {{ investigador.email }}
        </span>
      </div>
      <div class="p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Experiencia
        </h3>
        <span class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
        {{ investigador.experiencia }}
        </span>
      </div>
      <div v-if="investigador.proyectos && investigador.proyectos.length > 0" class="m-3">
            <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
              Proyectos Asociados
            </h3>
              <span 
                v-for="(proyecto, pIndex) in investigador.proyectos" 
                :key="pIndex"
                class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
                {{ proyecto }}: {{ investigador.asignaciones[pIndex]}}
              </span>
          </div>
          <div v-else class="mt-4 flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg">
            <span>No hay proyectos activos asignados a este investigador.</span>
          </div>
      </div>
      <div>
        <button class="btn m-2 bg-emerald-500" @click="volver">Volver</button>
      </div>
      <br>
      <div class="p-3"> 
          <RouterLink :to="`/editarInvestigador/${investigador.id}`">
              <button class="btn bg-emerald-500">Editar investigador</button>
          </RouterLink>
          <button @click="handleEliminar" class="btn m-2 bg-red-500">
              Eliminar investigador
          </button>
      </div>
  </div>
</div>
</template>