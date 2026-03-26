<script setup></script>

<template>
  <header class="bg-blue-400 shadow-lg p-4 mb-8 text-center">
    <RouterLink to="/" class="ml-5">Inicio</RouterLink>
    <router-link to="/lista" class="ml-5">Lista de games</router-link> 
    <router-link to="/formularioCrear" class="ml-5">Añadir Nuevo game</router-link>
    <router-link to="/listaPlayers" class="ml-5">Lista de players</router-link>
    <router-link to="/formularioCrearPlayer" class="ml-5">Añadir nuevo player</router-link>
  </header>
    <router-view/>
</template>

<style>
</style>
