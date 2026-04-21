<script setup>
import { onMounted, onUnmounted, ref } from 'vue';
import { useEcosistemaStore } from '../stores/ecosistemaStore';

const store = useEcosistemaStore();
const skip = ref(0);
const take = 6;
const cargando = ref(false);
const hayMas = ref(true);

const centinela = ref(null);

const cargarSiguientePagina = async () => {
  if (cargando.value || !hayMas.value) return;

  cargando.value = true;
  try {
    const cantidadRecibida = await store.fetchEcosistemasPaginados(skip.value, take);
    
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

const abrirModal = (eco) => {
  seleccionado.value = eco;
  modalRef.value.showModal();
};

const cerrarModal = () => {
  modalRef.value.close();
  seleccionado.value = null;
};

const handleEliminar = async (eco) => {
  const confirmado = confirm("¿Estás seguro de que quieres eliminar este ecosistema? Esta acción no se puede deshacer.");
  
  if (confirmado) {
    const exito = await store.deleteEcosistema(eco);
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
        Ecosistemas
      </h2>
      <RouterLink :to="`/registroEcosistema`">
        <button class="btn bg-emerald-500 mt-5">Añadir ecosistema</button>
      </RouterLink>
    </header>
    <div class="grid grid-cols-1 gap-6">
      <div v-for="(ecosistema, index) in store.ecosistemaList" 
          :key="ecosistema.id || index" 
          class="card bg-white shadow-xl hover:shadow-2xl transition-shadow duration-300 border-l-8 border-emerald-500">
          
          <div class="card-body grid grid-cols-1 md:grid-cols-4 gap-6 items-center">
            
            <h2 class="card-title text-2xl text-slate-800">
              {{ ecosistema.descripcion }}
            </h2>

            <div class="md:col-span-2">
              <div v-if="ecosistema.proyectos && ecosistema.proyectos.length > 0">
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
              <div v-else class="flex items-center text-slate-400 italic bg-slate-100 p-3 rounded-lg text-sm">
                <span>No hay proyectos activos asignados.</span>
              </div>
            </div>

            <div class="flex flex-col gap-3 md:col-start-4">
              <button class="btn bg-sky-500 text-white w-full" @click="abrirModal(ecosistema)">
                Ver Detalles
              </button>
              <RouterLink :to="`/editarEcosistema/${ecosistema.id}`" class="w-full">
                <button class="btn bg-emerald-500 text-white w-full">
                  Editar Ecosistema
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
      <p v-if="!hayMas && store.ecosistemaList.length > 0" class="text-slate-400 italic">
        Has llegado al final de la lista
      </p>
    </div>
  </div>

  
  
  <dialog ref="modalRef" class="modal">
  <div class="modal-box w-11/12 max-w-2xl border-l-8 border-emerald-500 bg-white">
    
    <div v-if="seleccionado">
      <header class="flex justify-between items-start mb-6">
        <h2 class="text-3xl font-bold text-slate-800">
          {{ seleccionado.descripcion }}
        </h2>
        <form method="dialog">
          <button class="btn btn-sm btn-circle btn-ghost">✕</button>
        </form>
      </header>

      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div class="p-4 bg-slate-50 rounded-lg">
          <h3 class="text-xs font-bold text-slate-400 uppercase mb-2">Coordenadas / Área</h3>
          <p class="text-emerald-700 font-medium">Long: {{ seleccionado.areaLongitud }}</p>
          <p class="text-emerald-700 font-medium">Lat: {{ seleccionado.areaLatitud }}</p>
        </div>

        <div class="p-4 bg-slate-50 rounded-lg">
          <h3 class="text-xs font-bold text-slate-400 uppercase mb-2">Estado</h3>
          <span class="badge badge-primary">{{ seleccionado.conservacion }}</span>
        </div>
      </div>

      <div class="mt-6">
        <h3 class="text-xs font-bold text-slate-400 uppercase mb-3">Proyectos Asociados</h3>
        <div v-if="seleccionado.proyectos?.length" class="flex flex-wrap">
          <span v-for="pro in seleccionado.proyectos" :key="pro" class="badge badge-outline badge-md py-3 px-4 text-emerald-700 border-emerald-200 bg-emerald-50 m-2">
            {{ pro }}
          </span>
        </div>
        <p v-else class="text-slate-400 italic">Sin proyectos asociados.</p>
      </div>

      <div class="modal-action flex justify-between border-t pt-4 mt-6">
        <div class="flex gap-2">
          <RouterLink :to="`/editarEcosistema/${seleccionado.id}`" class="btn btn-warning btn-sm">
            Editar
          </RouterLink>
          <button class="btn btn-error btn-sm" @click="handleEliminar(seleccionado.id)">
            Eliminar
          </button>
        </div>
        <form method="dialog">
          <button class="btn btn-sm">Cerrar</button>
        </form>
      </div>
    </div>
  </div>

  <form method="dialog" class="modal-backdrop">
    <button>close</button>
  </form>
</dialog>
</template>