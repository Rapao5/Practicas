<script setup>
import { ref, watch, onMounted } from 'vue';
import { useProyectoStore } from '../stores/proyectoStore';
import { useEcosistemaStore } from '../stores/ecosistemaStore';

const props = defineProps({
  initialData: {
    type: Object,
    default: () => ({
      nombre: '',
      presupuesto: 0,
      fechaInicio: '',
      fechaFinal: '',
      estado: null,
      especieFoco: '',
      ecosistemaId: 0
         })
  }
});
const ecoStore = useEcosistemaStore();
const proStore = useProyectoStore();
const proForm = ref({...props.initialData});
const emit = defineEmits(['enviar']);
const errores = ref({
    nombre: '',
    presupuesto: '',
    fechaInicio: '',
    fechaFinal: '',
    estado: '',
    especieFoco: '',
    ecosistemaId: ''
});

onMounted(async () => {
  if (ecoStore.ecosistemaList.length === 0) {
    await ecoStore.fetchEcosistemas();
  }
});

watch(
  () => props.initialData,
  (newData) => {
    if (newData) {
      proForm.value = { ...newData };
    }
  },
  { deep: true }
);

const validar = () => {
  errores.value = {
    nombre: '',
    presupuesto: '',
    fechaInicio: '',
    fechaFinal: '',
    estado: '',
    especieFoco: '',
    ecosistemaId: ''
  };
  let valido = true;
  if(proForm.value.nombre.trim().length < 3){
      errores.value.nombre = "El nombre del proyecto debe contener min 3 caracteres."
      valido = false
  }
  if(proForm.value.presupuesto < 0){
      errores.value.presupuesto = "El presupuesto no puede ser negativo"
      valido = false
  }
  if(proForm.value.fechaInicio == ''){
      errores.value.fechaInicio = "El campo fecha no puede estar vacío."
      valido = false
  }
  if(proForm.value.fechaFinal == ''){
      errores.value.fechaFinal = "El campo fecha no puede estar vacío."
      valido = false
  }
  if(proForm.value.fechaInicio > proForm.value.fechaFinal){
      errores.value.fechaInicio = "La fecha inical no puede ser posterior a la fecha final"
      valido = false
  }
  if(proForm.value.estado == null){
      errores.value.estado = "El campo debe estar relleno"
      valido = false
  }
  if(proForm.value.especieFoco.trim().length < 3){
      errores.value.especieFoco = "La especie debe contener min 3 caracteres."
      valido = false
  }
  if(proForm.value.ecosistemaId <= 0){
      errores.value.ecosistemaId = "El proyecto debe tener un Id válido"
      valido = false
  }
  return valido;
}

const guardar = () => {
  if(validar()){
      emit('enviar', proForm.value);
  }
}
</script>

<template>
  <div class="card bg-white shadow-lg p-6 border-t-4 border-emerald-500">
      <div>
        <label class="label font-semibold text-slate-600">Nombre del Proyecto</label>
        <input v-model="proForm.nombre" type="text" placeholder="Ej: Selva Amazónica" 
               class="input input-bordered w-full focus:border-emerald-500" required />
        <span class="text-error text-xs" v-if="errores.nombre">{{ errores.nombre }}</span>
      </div>

      <div>
        <label class="label font-semibold text-slate-600">Presupuesto</label>
        <input v-model="proForm.presupuesto" type="number" placeholder="Ej: 30.4" 
               class="input input-bordered w-full" required />
        <span class="text-error text-xs" v-if="errores.presupuesto">{{ errores.presupuesto }}</span>
      </div>

       <div>
        <label class="label font-semibold text-slate-600">Fecha de inicio</label>
        <input v-model="proForm.fechaInicio" type="date" placeholder="Ej: 56.4" 
               class="input input-bordered w-full" required />
        <span class="text-error text-xs" v-if="errores.fechaInicio">{{ errores.fechaInicio }}</span>
      </div>

      <div>
        <label class="label font-semibold text-slate-600">Fecha final</label>
        <input v-model="proForm.fechaFinal" type="date" placeholder="Ej: 56.4" 
               class="input input-bordered w-full" required />
        <span class="text-error text-xs" v-if="errores.fechaFinal">{{ errores.fechaFinal }}</span>
      </div>

      <div>
        <label class="label font-semibold text-slate-600">Estado</label>
        <select v-model="proForm.estado" class="select select-bordered w-full">
          <option :value="null">Selecciona un estado</option>
          <option :value="true">Activo</option>
          <option :value="false">Terminado</option>
        </select>
        <span class="text-error text-xs" v-if="errores.estado">{{ errores.estado }}</span>
      </div>

      <div>
        <label class="label font-semibold text-slate-600">Especie foco</label>
        <input v-model="proForm.especieFoco" type="text" placeholder="Ej: Tigre" 
               class="input input-bordered w-full focus:border-emerald-500" required />
        <span class="text-error text-xs" v-if="errores.especieFoco">{{ errores.especieFoco }}</span>
      </div>

      <div>
        <label class="label font-bold text-slate-600">Ecosistema</label>
        <select v-model="proForm.ecosistemaId" class="select select-bordered w-full" :class="{'select-error': errores.ecosistemaId}">
          <option :value="0" disabled>Selecciona un ecosistema</option>
          <option v-for="eco in ecoStore.ecosistemaList" :key="eco.id" :value="eco.id">
            {{ eco.descripcion }}
          </option>
        </select>
        <span class="text-error text-xs" v-if="errores.ecosistemaId">{{ errores.ecosistemaId }}</span>
      </div>

      <div class="flex justify-end gap-2 mt-6">
        <button @click="guardar" class="btn bg-emerald-500 text-white border-none hover:bg-emerald-600">
          Guardar
        </button>
      </div>
  </div>
</template>