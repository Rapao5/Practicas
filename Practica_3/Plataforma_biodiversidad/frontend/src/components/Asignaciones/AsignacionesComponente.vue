<script setup>
import { ref, watch, onMounted } from 'vue';
import { useInvestigadorStore } from '../../stores/investigadorStore';
import { useProyectoStore } from '../../stores/proyectoStore';

const props = defineProps({
  proyectoId: {
    type: [Number, String],
    required: true
  },
  initialData: {
    type: Object,
    default: () => ({
      rol: '',
      fechaEntrada: '',
      investigadorId: 0
    })
  }
});

const invStore = useInvestigadorStore();
const proStore = useProyectoStore();
const emit = defineEmits(['enviar']);

const asigForm = ref({
  ...props.initialData,
  proyectoId: props.proyectoId 
});

const errores = ref({
  rol: '',
  fechaEntrada: '',
  proyectoId: '',
  investigadorId: ''
});

onMounted(async () => {
  if (invStore.investigadorList.length === 0) await invStore.fetchInvestigadores();
  if (proStore.proyectoList.length === 0) await proStore.fetchProyectos();
});


const nombreProyecto = ref('Cargando...');
watch(() => proStore.proyectoList, (list) => {
  const pro = list.find(p => p.id == props.proyectoId);
  if (pro) nombreProyecto.value = pro.nombre;
}, { immediate: true, deep: true });

const validar = () => {
  errores.value = { rol: '', fechaEntrada: '', proyectoId: '', investigadorId: '' };
  let valido = true;

  if (!asigForm.value.rol) {
    errores.value.rol = "El campo rol es obligatorio";
    valido = false;
  }
  if (!asigForm.value.fechaEntrada) {
    errores.value.fechaEntrada = "La fecha es obligatoria";
    valido = false;
  }
  if (asigForm.value.investigadorId <= 0) {
    errores.value.investigadorId = "Selecciona un investigador";
    valido = false;
  }
  return valido;
};

const guardar = () => {
  if (validar()) {
    emit('enviar', asigForm.value);
  }
};
</script>
<template>
<div class="card bg-white shadow-lg p-6 border-t-4 border-emerald-500">
  <div class="bg-slate-50 p-4 rounded-lg border border-slate-200">
        <label class="text-xs font-bold text-slate-400 uppercase tracking-widest">Proyecto Destino</label>
        <p class="text-lg font-bold text-emerald-700">{{ nombreProyecto }}</p>
        <span class="text-xs text-slate-400 font-mono">ID: {{ proyectoId }}</span>
      </div>
  <div>
      <label class="label font-semibold text-slate-600">Rol</label>
      <br>
      <select v-model="asigForm.rol" class="select select-bordered w-full">
        <option value="">Selecciona un rol</option>
        <option value="LiderCampo">Lider de campo</option>
        <option value="InvestigadorPrincipal">Investigador principal</option>
        <option value="TecnicoLaboratorio">Técnico de laboratorio</option>
        <option value="GestorDatos">Gestor de datos</option>
      </select>
      <span class="text-error text-xs" v-if="errores.rol">{{ errores.rol }}</span>
    </div>
    <div>
      <label class="label font-semibold text-slate-600">Fecha de entrada</label>
      <input v-model="asigForm.fechaEntrada" type="date"
              class="input input-bordered w-full" required />
      <span class="text-error text-xs" v-if="errores.fechaEntrada">{{ errores.fechaEntrada }}</span>
    </div>
    <div>
      <label class="label font-bold text-slate-600">Investigador</label>
      <select v-model="asigForm.investigadorId" class="select select-bordered w-full" :class="{'select-error': errores.investigadorId}">
        <option :value="0" disabled>Selecciona un investigador</option>
        <option v-for="inv in invStore.investigadorList" :key="inv.id" :value="inv.id">
            {{ inv.nombre }}
        </option>
      </select>
      <span class="text-error text-xs" v-if="errores.investigadorId">{{ errores.investigadorId }}</span>
    </div>
    <div class="flex justify-end gap-2 mt-6">
      <button @click="guardar" class="btn bg-emerald-500 text-white border-none hover:bg-emerald-600">
        Guardar
      </button>
    </div>
</div>
</template>