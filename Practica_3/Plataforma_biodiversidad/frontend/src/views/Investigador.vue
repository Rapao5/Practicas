<script setup>
import { onMounted, onUnmounted, ref } from 'vue';
import { useInvestigadorStore } from '../stores/investigadorStore';

const store = useInvestigadorStore();
const skip = ref(0);
const take = 6;
const cargando = ref(false);
const hayMas = ref(true);

const centinela = ref(null);

const cargarSiguientePagina = async () => {
  if (cargando.value || !hayMas.value) return;

  cargando.value=true;
  try {
    const cantidadRecibida = await store.fetchInvestigadoresPaginados(skip.value, take);

    if (cantidadRecibida === 0 || cantidadRecibida < take) {
      hayMas.value = false;
    }

    skip.value += take;
  } catch (error) {
    console.error("Fallo al cargar", error);
    hayMas.value = false;
  } finally {
    cargando.value = false;
  }
}

const observer = new IntersectionObserver((entries) => {
  if (entries[0].isIntersecting) {
    cargarSiguientePagina();
  }
}, {threshold: 0.1});

const modalRef = ref(null);
const seleccionado = ref(null);

const abrirModal = (inv) => {
  seleccionado.value = inv; 
  modalRef.value.showModal();
};

const cerrarModal = () => {
  modalRef.value.close();
  seleccionado.value = null;
};

const handleEliminar = async () => {
  if (!seleccionado.value) return;
  
  const confirmado = confirm(`¿Estás seguro de que quieres eliminar a ${seleccionado.value.nombre}?`);
  
  if (confirmado) {
    const exito = await store.deleteInvestigador(seleccionado.value.id);
    if (exito) {
      cerrarModal();
    } else {
      alert("Hubo un error al borrar.");
    }
  }
};

onMounted(async () => {
  if (centinela.value) {
    observer.observe(centinela.value);
  }
});
onUnmounted(() => {
  observer.disconnect();
});
</script>
<template>
<div class="p-6 md:p-10 bg-slate-50 min-h-screen">
  <header class="mb-10 text-center">
      <h2 class="text-black mt-10 text-3xl">
        Investigadores
      </h2>
      <RouterLink :to="`/registroInvestigador`">
        <button class="btn bg-emerald-500 mt-5">Añadir investigador</button>
      </RouterLink>
    </header>
    <div class="grid grid-cols-1 gap-6">
        <div v-for="(investigador, index) in store.investigadorList" 
            :key="investigador.id || index" 
            class="card bg-white shadow-xl hover:shadow-2xl transition-shadow duration-300 border-l-8 border-emerald-500">
            
            <div class="card-body grid grid-cols-1 md:grid-cols-4 gap-6 items-center">
              
              <h2 class="card-title text-2xl text-slate-800">
                {{ investigador.nombre }}
              </h2>

              <div class="md:col-span-2">
                <div v-if="investigador.proyectos && investigador.proyectos.length > 0">
                  <h3 class="text-xs font-bold text-slate-400 uppercase tracking-widest mb-3">
                    Proyectos Asociados
                  </h3>
                  <div class="flex flex-wrap gap-2">
                    <span 
                      v-for="(proyecto, pIndex) in investigador.proyectos" 
                      :key="pIndex"
                      class="badge badge-outline badge-md py-6 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
                      {{ proyecto }}: <span class="font-bold ml-1">{{ investigador.asignaciones[pIndex] }}</span>
                    </span>
                  </div>
                </div>
                <div v-else class="flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg text-sm">
                  <span>No hay proyectos activos asignados a este investigador.</span>
                </div>
              </div>

              <div class="flex flex-col gap-3 md:col-start-4">
                <button class="btn bg-sky-500 text-white w-full" @click="abrirModal(investigador)">
                  Ver Detalles
                </button>
                
                <RouterLink :to="`/editarInvestigador/${investigador.id}`" class="w-full">
                  <button class="btn bg-emerald-500 text-white w-full">
                    Editar investigador
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
      <p v-if="!hayMas && store.investigadorList.length > 0" class="text-slate-400 italic">
        Has llegado al final de la lista
      </p>
    </div>
</div>
<dialog ref="modalRef" class="modal">
  <div class="modal-box max-w-2xl border-l-8 border-emerald-500 bg-white" v-if="seleccionado">
    
    <h2 class="text-2xl font-bold text-slate-800 mb-4">
      Ficha del Investigador #{{ seleccionado.id }}
    </h2>

    <div class="grid grid-cols-2 gap-4">
      <div class="p-3 bg-slate-50 rounded-lg">
        <h3 class="text-xs font-bold text-slate-400 uppercase">Nombre</h3>
        <p class="text-slate-800 font-semibold">{{ seleccionado.nombre }}</p>
      </div>
      <div class="p-3 bg-slate-50 rounded-lg">
        <h3 class="text-xs font-bold text-slate-400 uppercase">Email</h3>
        <p class="text-slate-800 font-semibold">{{ seleccionado.email }}</p>
      </div>
      <div class="p-3 bg-slate-50 rounded-lg">
        <h3 class="text-xs font-bold text-slate-400 uppercase">Experiencia</h3>
        <p class="text-slate-800 font-semibold">{{ seleccionado.experiencia }}</p>
      </div>
    </div>

    <div class="mt-4 p-3 bg-slate-50 rounded-lg">
      <h3 class="text-xs font-bold text-slate-400 uppercase mb-2">Proyectos y Roles</h3>
      <div v-if="seleccionado.proyectos?.length" class="flex flex-wrap gap-2">
        <span v-for="(proyecto, pIndex) in seleccionado.proyectos" :key="pIndex"
          class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50">
          {{ proyecto }}: <span class="font-bold ml-1">{{ seleccionado.asignaciones[pIndex] }}</span>
        </span>
      </div>
      <p v-else class="text-slate-400 italic">Sin proyectos asignados.</p>
    </div>

    <div class="modal-action flex justify-between mt-8 border-t pt-4">
      <div class="flex gap-2">
        <button @click="handleEliminar" class="btn btn-error btn-sm">Eliminar</button>
        <RouterLink :to="`/editarInvestigador/${seleccionado.id}`" class="btn btn-warning btn-sm">
          Editar
        </RouterLink>
      </div>
      <button class="btn btn-sm" @click="cerrarModal">Cerrar</button>
    </div>
  </div>

  <form method="dialog" class="modal-backdrop">
    <button @click="seleccionado = null">close</button>
  </form>
</dialog>
</template>