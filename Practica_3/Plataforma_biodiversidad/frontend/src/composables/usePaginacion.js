import { ref, onUnmounted } from "vue";

export function usePaginacion(fetchFn){
  const skip = ref(0);
  const take = 6; 
  const cargando = ref(false);
  const hayMas = ref(true);
  const centinela = ref(null);

const cargarMas = async () => {
    if (cargando.value || !hayMas.value) return;

    cargando.value = true;
    try {
      const cantidad = await fetchFn(skip.value, take);
      if (cantidad < take) {
        hayMas.value = false;
      }
      skip.value += take;
    } finally {
      cargando.value = false;
    }
  };

const observer = new IntersectionObserver((entries) => {
  if (entries[0].isIntersecting) {
    cargarMas();
    }
  }, { threshold: 0.1 });

const iniciarObservador = (elemento) => {
    if (elemento) {
      centinela.value = elemento;
      observer.observe(elemento);
    }
  };

onUnmounted(() =>{
   if(observer) observer.disconnect()
});
  
  return {
    skip,
    cargando,
    hayMas,
    iniciarObservador,
    cargarMas
  };
}