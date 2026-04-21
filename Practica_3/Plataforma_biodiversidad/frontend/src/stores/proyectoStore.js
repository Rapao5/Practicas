import { defineStore } from "pinia";
import api from "../api/axios";

export const useProyectoStore = defineStore('proyectos', {
  state: () => ({
      proyectoList: [],
      proyectoActual: null,
      cargando: false,
      error: null
  }),

  getters: {
    totalProyectos: (state) => state.proyectoList.length,
  },

  actions: {
    async fetchProyectosPaginados(skip, take) {
    try {
      const response = await api.get(`/proyecto/paginados?skip=${skip}&take=${take}`);
      const nuevosProyectos = response.data;

      if (skip === 0) {
        this.proyectoList = nuevosProyectos;
      } else {
        this.proyectoList.push(...nuevosProyectos);
      }
      
      return nuevosProyectos.length;
    } catch (error) {
      console.error("Error al cargar proyectos paginados", error);
      return 0;
    }
  },
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
    },
 
    async fetchProyectoById(id) {
        this.cargando = true;
        this.error = null;
        try {
          const response = await api.get(`proyecto/${id}`);
          this.proyectoActual = response.data;
          return response.data
        } catch (err) {
          this.error = ("Error al traer el proyecto:", err); 
        } finally {
        this.cargando = false;
        }
    },
    async deleteProyecto(id){
      this.cargando = true;
      this.error = null;
      try {
        await api.delete(`proyecto/${id}`);
        this.proyectoActual = null;
        this.proyectoList = this.proyectoList.filter(e => e.id !== id);
        return true;
      } catch(err) {
        this.error = "No se pudo eliminar el proyecto";
        console.error(err);
        return false;
      } finally {
        this.cargando = false;
      }
    },
    async createProyecto(nuevoProyecto) {
      try {
        await api.post('proyecto', nuevoProyecto);
        return true;
      } catch (error) {
        console.error("Error al crear:", error);
        return false;
      }
    },

    async updateProyecto(id, datosActualizados) {
      try {
        await api.put(`proyecto/${id}`, datosActualizados);
        return true;
      } catch (error) {
        console.error("Error al actualizar:", error);
        return false;
      }
    }
  }
});