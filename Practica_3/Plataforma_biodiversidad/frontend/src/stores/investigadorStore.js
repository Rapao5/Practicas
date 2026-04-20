import {defineStore} from "pinia";
import api from "../api/axios";

export const useInvestigadorStore = defineStore('investigadores', {
  state: () => ({
      investigadorList: [],
      investigadorActual: null,
      cargando: false,
      error: null
  }),

  getters: {
    totalInvestigadores: (state) => state.investigadorList.length,
  },

  actions: {
    async fetchInvestigadores() {
        this.cargando = true;
        this.error = null;
        try {
          const response = await api.get('investigador');
          this.investigadorList = response.data;
        } catch (err) {
          this.error = "No se pudieron cargar los investigadores";
        } finally {
        this.cargando = false;
        }
    },
    async fetchInvestigadorById(id) {
        this.cargando = true;
        this.error = null;
        try {
          const response = await api.get(`investigador/${id}`);
          this.investigadorActual = response.data;
        } catch (err) {
          this.error = ("Error al traer el investigador:", err);
        } finally {
        this.cargando = false;
        }
    },
    async deleteInvestigador(id){
      this.cargando = true;
      this.error = null;
      try {
        await api.delete(`investigador/${id}`);
        this.investigadorActual = null;
        this.investigadorList = this.investigadorList.filter(e => e.id !== id);
        return true;
      } catch(err) {
        this.error = "No se pudo eliminar el investigador";
        console.error(err);
        return false;
      } finally {
        this.cargando = false;
      }
    },
    async createInvestigador(nuevoInvestigador) {
      try {
        await api.post('investigador', nuevoInvestigador);
        return true;
      } catch (error) {
        console.error("Error al crear:", error);
        return false;
      }
    },

    async updateInvestigador(id, datosActualizados) {
      try {
        await api.put(`investigador/${id}`, datosActualizados);
        return true;
      } catch (error) {
        console.error("Error al actualizar:", error);
        return false;
      }
    }
  }
});