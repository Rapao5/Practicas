import { defineStore } from "pinia";
import api from "../api/axios";

export const useProyectoStore = defineStore('proyectos', {
  state: () => ({
      proyectoList: [],
      cargando: false,
      error: null
  }),

  getters: {
    totalProyectos: (state) => state.proyectoList.length,
  },

  actions: {
    async fetchProyectos() {
        this.cargando = true;
        this.error = null;
        try {
          const response = await api.get('proyecto');
          this.proyectoList = response.data;
        } catch (err) {
          this.error = "No se pudieron cargar los proyectos";
        } finally {
        this.cargando = false;
        }
    }
  }
});