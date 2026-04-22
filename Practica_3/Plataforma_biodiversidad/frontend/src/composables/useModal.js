import { ref } from 'vue';

export function useModal(fechtFn){
  const modalRef = ref(null);
  const seleccionado = ref(null);

  const abrirModal = (inv) => {
    seleccionado.value = inv; 
    modalRef.value.showModal();
  };

const cerrarModal = () => {
    modalRef.value.close();
    seleccionado.value = null;
  };

  return{
    modalRef,
    seleccionado,
    abrirModal,
    cerrarModal
  }
}