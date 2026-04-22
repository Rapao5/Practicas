<script setup>
import { ref, watch } from 'vue';
import { useInvestigadorStore } from '../stores/investigadorStore';


const props = defineProps({
  initialData: { 
    type: Object,
    default: () => ({
      nombre: '',
      email: '',
      experiencia: ''
    })
  }
});

const store = useInvestigadorStore();
const invForm = ref({...props.initialData});
const emit = defineEmits(['enviar']);
const errores = ref({
    nombre: '',
    email: '',
    experiencia: ''
});

watch(
  () => props.initialData,
  (newData) => {
    if (newData) {
      invForm.value = { ...newData };
    }
  },
  { deep: true }
);

const validar = () => {
  errores.value = {
    nombre: '',
    email: '',
    experiencia: ''
  };
  let valido = true;
  if(invForm.value.nombre.trim().length < 3){
      errores.value.nombre = "El nombre debe contener min 3 caracteres."
      valido = false
  }
  if(invForm.value.email.trim().length < 3){
      errores.value.email = "El email debe contener min 3 caracteres."
      valido = false
  }
  if(invForm.value.experiencia == null || invForm.value.experiencia == ''){
      errores.value.experiencia = "El campo debe estar relleno."
      valido = false
  }
  return valido;
};

const guardar = () => {
  if(validar()){
      emit('enviar', invForm.value);
  }
}
</script>
<template>
 <div class="card bg-white shadow-lg p-6 border-t-4 border-emerald-500">
    <div>
      <label class="label font-semibold text-slate-600">Nombre</label>
        <input v-model="invForm.nombre" type="text" placeholder="Ej: Jose el profe" 
                class="input input-bordered w-full focus:border-emerald-500" required />
        <span class="text-error text-xs" v-if="errores.nombre">{{ errores.nombre }}</span>
    </div>

    <div>
      <label class="label font-semibold text-slate-600">Email</label>
        <input v-model="invForm.email" type="email" placeholder="Ej: JoseElProfe@mail.com" 
                class="input input-bordered w-full focus:border-emerald-500" required />
        <span class="text-error text-xs" v-if="errores.email">{{ errores.email }}</span>
    </div>

    <div>
        <label class="label font-semibold text-slate-600">Estado</label>
        <br>
        <select v-model="invForm.experiencia" class="select select-bordered w-full">
          <option value="">Selecciona la experiencia</option>
          <option value="Junior">Junior</option>
          <option value="Semi_Senior">Semi-senior</option>
          <option value="Senior">Senior</option>
        </select>
        <span class="text-error text-xs" v-if="errores.experiencia">{{ errores.experiencia }}</span>
      </div>

    <div class="flex justify-end gap-2 mt-6">
        <button @click="guardar" class="btn bg-emerald-500 text-white border-none hover:bg-emerald-600">
          Guardar
        </button>
      </div>
 </div>
</template>