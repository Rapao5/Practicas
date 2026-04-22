<script setup> 
import { onMounted, ref} from 'vue';
import { useProyectoStore } from '../stores/proyectoStore';
import { usePaginacion } from '../composables/usePaginacion';
import { useModal } from '../composables/useModal';

const store = useProyectoStore();
const centinela = ref(null);
const { cargando, hayMas, iniciarObservador, cargarMas } = usePaginacion(store.fetchProyectosPaginados);
const { modalRef, seleccionado, abrirModal, cerrarModal } = useModal();

const handleEliminar = async (pro) => {
  const confirmado = confirm("¿Estás seguro de que quieres eliminar este proyecto? Esta acción no se puede deshacer.");
  
  if (confirmado) {
    const exito = await store.deleteProyecto(pro);
    if (exito) {
      cerrarModal(); 
    } else {
      alert("Hubo un error al borrar.");
    }
  }
};

onMounted(async () => {
  await cargarMas()
  iniciarObservador(centinela.value);
});
</script>
<template>
  <div class="p-6 md:p-10 bg-slate-50 min-h-screen"> 
    <header class="mb-10 text-center">
      <h2 class="text-black mt-10 text-3xl">Proyectos</h2>
      <RouterLink :to="`/registroProyecto`">
        <button class="btn bg-emerald-500 mt-5">Añadir proyecto</button>
      </RouterLink>
    </header>

      <div class="grid grid-cols-1 gap-6">
        <div v-for="(proyecto, index) in store.proyectoList"
          :key="proyecto.id || index"
          class="card bg-white shadow-xl hover:shadow-2xl transition-shadow duration-300 border-l-8 border-emerald-500">
          
          <div class="card-body grid grid-cols-1 md:grid-cols-4 gap-6 items-center">
            
            <h2 class="card-title text-2xl text-slate-800">{{ proyecto.nombre }}</h2>
            
            <div>
              <h2 class="card-title text-xl text-slate-600 mb-2 md:text-lg">Estado:</h2>
              <span v-if="proyecto.estado == true" class="badge badge-outline badge-md py-3 mt-1 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">Activo</span>
              <span v-else class="badge badge-outline badge-md py-3 mt-1 px-4 text-red-700 border-red-200 bg-red-50">Terminado</span>
            </div>

            <div>
              <h2 class="card-title text-xl text-slate-600 mb-2 md:text-lg">Investigadores:</h2>
              <div class="flex flex-wrap gap-2">
                <div v-for="(investigador, iIndex) in proyecto.investigadores" :key="iIndex">
                  <span class="badge badge-outline badge-md py-6 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                    {{ investigador }}: {{ proyecto.investigadoresRol[iIndex] }}
                  </span>
                </div>
              </div>
              <span class="mt-4 flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg text-sm" v-if="proyecto.investigadores.length == 0">
                No hay investigadores asignados
              </span>
            </div>

            <div class="flex flex-col gap-3">
              <button class="btn bg-sky-500 text-white w-full" @click="abrirModal(proyecto)">
                Ver Detalles
              </button>
              
              <RouterLink :to="`/editarProyecto/${proyecto.id}`" class="w-full">
                <button class="btn bg-emerald-500 text-white w-full">
                  Editar proyecto
                </button>
              </RouterLink>
            </div>

          </div>
        </div>
      </div>

    <div ref="centinela" class="h-20 flex items-center justify-center mt-10">
      <div v-if="cargando" class="flex flex-col items-center gap-2">
        <span class="loading loading-spinner loading-lg text-emerald-500"></span>
        <p class="text-slate-400 text-sm italic">Cargando más proyectos...</p>
      </div>
      
      <p v-if="!hayMas && store.proyectoList.length > 0" class="text-slate-400 italic">
        Has llegado al final de la lista
      </p>
    </div>
  </div>

  <dialog ref="modalRef" class="modal">
    <div v-if="seleccionado" class="modal-box max-w-3xl border-l-8 border-emerald-500 bg-white">
      <h2 class="text-3xl font-bold text-slate-800 mb-4">{{ seleccionado.nombre }}</h2>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div class="p-3 bg-slate-50 rounded-lg">
          <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest">Presupuesto</h3>
          <p class="text-xl font-semibold text-emerald-700">{{ seleccionado.presupuesto }} €</p>
        </div>
        <div class="p-3 bg-slate-50 rounded-lg">
          <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest">Ecosistema / Especie</h3>
          <p class="text-slate-700 font-medium">{{ seleccionado.ecosistema }}</p>
          <p class="text-slate-500 text-sm">Foco: {{ seleccionado.especieFoco }}</p>
        </div>
      </div>

      <div class="mt-4 p-3 bg-slate-50 rounded-lg">
        <h3 class="text-xs font-bold text-slate-400 uppercase mb-2">Cronograma</h3>
        <div class="flex gap-4">
          <span class="badge badge-outline badge-md py-7 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">Inicio: {{ seleccionado.fechaInicio }}</span>
          <span class="badge badge-outline badge-md py-7 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">Fin: {{ seleccionado.fechaFinal }}</span>
        </div>
      </div>

      <div class="mt-4 p-3">
        <h3 class="text-xs font-bold text-slate-400 uppercase mb-2">Equipo Asignado</h3>
        <div v-if="seleccionado.investigadores?.length" class="flex flex-wrap gap-2">
          <span v-for="(investigador, iIndex) in seleccionado.investigadores" :key="iIndex"
            class="badge badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
            {{ investigador }}: <span class="font-bold ml-1">{{ seleccionado.investigadoresRol[iIndex] }}</span>
          </span>
        </div>
        <p v-else class="text-slate-400 italic">No hay investigadores asignados.</p>
      </div>

      <div class="modal-action flex flex-wrap justify-between border-t pt-4 mt-6">
        <div class="flex gap-2">
          <RouterLink :to="`/editarProyecto/${seleccionado.id}`" class="btn btn-warning btn-sm">Editar</RouterLink>
          <RouterLink :to="`/asignacion/${seleccionado.id}`" class="btn btn-info btn-sm text-white">Gestionar Equipo</RouterLink>
          <button @click="handleEliminar(seleccionado.id)" class="btn btn-error btn-sm">Eliminar</button>
        </div>
        <button class="btn btn-sm" @click="cerrarModal">Cerrar</button>
      </div>
    </div>
    <form method="dialog" class="modal-backdrop">
      <button @click="cerrarModal">close</button>
    </form>
  </dialog>
</template>