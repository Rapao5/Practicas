<script setup>

</script>

<template>
<div class="principal">
  <router-link to="/">Lista de Sensores</router-link> 
  <router-link to="/formularioCrear">Añadir Nuevo</router-link>
  <router-view/>
</div>
  
</template>

<style>
.principal{
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>