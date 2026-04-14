import { defineStore } from "pinia";
import api from "../api/axios";

export const useEcosistemaStore = defineStore('ecosistemas',{
  state: () => ({
    ecosistemaList: [],
    cargando: false,
    error: null
  }),

  getters: {
    totalEcosistemas: (state) => state.ecosistemaList.length,
  },

  actions: {
    async fetchEcosistemas() {
      this.cargando = true;
      this.error = null;

      try {
        const response = await api.get('ecosistema');
        this.ecosistemaList = response.data;
      } catch (err) {
        this.error = "No se pudieron cargar los ecosistemas";
      } finally {
        this.cargando = false;
      }
    }
  }
});