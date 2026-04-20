import { defineStore } from "pinia";
import api from "../api/axios";

export const useEcosistemaStore = defineStore('ecosistemas',{
  state: () => ({
    ecosistemaList: [],
    ecosistemaActual: null,
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
    },
    async fetchEcosistemaById(id) {
      this.cargando = true;
      this.error = null;

      try {
        const response = await api.get(`ecosistema/${id}`);
        this.ecosistemaActual = response.data;
      } catch (err) {
        this.error = ("Error al traer el ecosistema:", err);
      } finally {
        this.cargando = false;
      }
    },
    async deleteEcosistema(id){
      this.cargando = true;
      this.error = null;
      try {
        await api.delete(`ecosistema/${id}`);
        this.ecosistemaActual = null;
        this.ecosistemaList = this.ecosistemaList.filter(e => e.id !== id);
        return true;
      } catch(err) {
        this.error = "No se pudo eliminar el ecosistema";
        console.error(err);
        return false;
      } finally {
        this.cargando = false;
      }
    },
    async createEcosistema(nuevoEcosistema) {
      try {
        await api.post('ecosistema', nuevoEcosistema);
        return true;
      } catch (error) {
        console.error("Error al crear:", error);
        return false;
      }
    },

    async updateEcosistema(id, datosActualizados) {
      try {
        await api.put(`ecosistema/${id}`, datosActualizados);
        return true;
      } catch (error) {
        console.error("Error al actualizar:", error);
        return false;
      }
    }
  }
});