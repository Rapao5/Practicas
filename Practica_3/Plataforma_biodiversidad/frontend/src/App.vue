<script setup>
import { RouterView } from 'vue-router';
</script>

<template>
<header class="bg-emerald-600 shadow-lg h-15 pt-5 text-center">
<RouterLink to="/" class="ml-5">Inicio</RouterLink>
<RouterLink to="/ecosistema" class="ml-5">Ecosistemas</RouterLink>
<RouterLink to="/proyecto" class="ml-5">Proyectos</RouterLink>
<RouterLink to="/investigador" class="ml-5">Investigadores</RouterLink>
</header>
<RouterView />
</template>
