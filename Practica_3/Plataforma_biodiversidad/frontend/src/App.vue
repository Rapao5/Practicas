<script setup>
import { RouterView } from 'vue-router';
import { useErrorStore } from './stores/errorStore';
const errorStore = useErrorStore();
</script>

<template>
<header class="bg-emerald-600 shadow-lg h-15 pt-5 text-center pr-3">
<RouterLink to="/" class="ml-5">Inicio</RouterLink>
<RouterLink to="/ecosistema" class="ml-5">Ecosistemas</RouterLink>
<RouterLink to="/proyecto" class="ml-5">Proyectos</RouterLink>
<RouterLink to="/investigador" class="ml-5">Investigadores</RouterLink>
</header>
  <div class="bg-white">
    <div v-if="errorStore.mensaje" role="alert" class="alert alert-error">
  <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 shrink-0 stroke-current" fill="none" viewBox="0 0 24 24">
    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" />
  </svg>
  <span>{{ errorStore.mensaje }}</span>
  <button @click="errorStore.mensaje = null" class="btn btn-error"> [X] Cerrar</button>
</div>
  </div>
<RouterView />
</template>
