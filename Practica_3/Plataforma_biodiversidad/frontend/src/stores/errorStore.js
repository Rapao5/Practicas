import { defineStore } from 'pinia';

export const useErrorStore = defineStore('error', {
  state: () => ({
    mensaje: null,
    visible: false
  }),
  actions: {
    mostrar(texto) {
      this.mensaje = texto;
      this.visible = true;
      setTimeout(() => { this.visible = false; }, 5000);
    }
  }
});