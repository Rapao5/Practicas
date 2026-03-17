<script setup>
import { useRoute, useRouter } from 'vue-router'
import FormularioComponent from '@/components/formularioComponent.vue'

const route = useRoute()
const router = useRouter()

const terminar = () => {
  router.push('/')
}
</script>

<template>
  <main>
    <FormularioComponent 
      :id="route.params.id" 
      @guardado="terminar" 
      @cancelar="terminar" 
    />
  </main>
</template>
<style>

</style>