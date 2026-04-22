<script setup>
import { ref, watch } from 'vue';

const props = defineProps({
  initialData: {
    type: Object,
    default: () => ({
      descripcion: '',
      areaLatitud: 0,
      areaLongitud: 0,
      conservacion: ''
         })
  }
});

const ecoForm = ref({...props.initialData});
const emit = defineEmits(['enviar']);
const errores = ref({
  descripcion: '',
  areaLatitud: 0,
  areaLongitud: 0,
  aonservacion: ''
});

watch(
  () => props.initialData,
  (newData) => {
    if (newData) {
      ecoForm.value = { ...newData };
    }
  },
  { deep: true }
);

const validar = () => {
  errores.value = {
    descripcion: '',
    areaLatitud: '',
    areaLongitud: '',
    conservacion: ''
  };
  let valido = true;
  if(ecoForm.value.descripcion.trim().length < 3){
      errores.value.descripcion = "La descripción del ecosistema debe contener min 3 caracteres."
      valido = false
  }
  if(ecoForm.value.areaLatitud > 90 || ecoForm.value.areaLatitud < -90){
      errores.value.areaLatitud = "Las coordenadas de latitud deben estar entre 90º y -90º"
      valido = false
  }
  if(ecoForm.value.areaLongitud > 180 || ecoForm.value.areaLongitud < -180){
      errores.value.areaLongitud = "Las coordenadas de longitud deben estar entre 180º y -180º"
      valido = false
  }
  if(ecoForm.value.conservacion == null || ecoForm.value.conservacion == ''){
      errores.value.conservacion = "El campo debe estar relleno"
      valido = false
  }
  return valido;
}

const guardar = () => {
  if(validar()){
      emit('enviar', ecoForm.value);
  }
}
</script>

<template>
  <div class="card bg-white shadow-lg p-6 border-t-4 border-emerald-500">
      <div>
        <label class="label font-semibold text-slate-600">Descripción del Ecosistema</label>
        <input v-model="ecoForm.descripcion" type="text" placeholder="Ej: Selva Amazónica" 
               class="input input-bordered w-full" required />
        <span class="text-error text-xs" v-if="errores.descripcion">{{ errores.Descripcion }}</span>
      </div>

      <div>
        <label class="label font-semibold text-slate-600">Área latitud</label>
        <input v-model="ecoForm.areaLatitud" type="number" placeholder="Ej: 30.4" 
               class="input input-bordered w-full" required />
        <span class="text-error text-xs" v-if="errores.areaLatitud">{{ errores.areaLatitud }}</span>
      </div>

       <div>
        <label class="label font-semibold text-slate-600">Área longitud</label>
        <input v-model="ecoForm.areaLongitud" type="number" placeholder="Ej: 56.4" 
               class="input input-bordered w-full" required />
        <span class="text-error text-xs" v-if="errores.areaLongitud">{{ errores.areaLongitud }}</span>
      </div>

      <div>
        <label class="label font-semibold text-slate-600">Estado</label>
        <br>
        <select v-model="ecoForm.conservacion" class="select select-bordered w-full">
          <option value="">Selecciona un estado</option>
          <option value="Intacto">Intacto</option>
          <option value="Vulnerable">Vulnerable</option>
          <option value="EnPeligro">En peligro</option>
          <option value="EnRestauracion">En restauracion</option>
          <option value="Protegido">Protegido</option>
        </select>
        <span class="text-error text-xs" v-if="errores.conservacion">{{ errores.conservacion }}</span>
      </div>

      <div class="flex justify-end gap-2 mt-6">
        <button @click="guardar" class="btn bg-emerald-500 text-white border-none hover:bg-emerald-600">
          Guardar
        </button>
      </div>
  </div>
</template>