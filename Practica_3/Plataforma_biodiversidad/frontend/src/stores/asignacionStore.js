import { defineStore } from "pinia";
import api from "../api/axios";

export const useAsignacionStore = defineStore('asignacion',{
  state: () => ({
    asignacionList: [],
    asignacionListProyecto: [],
    asignacionActual: null,
    cargando: false,
    error: null
  }),

  getters: {
    totalAsignaciones: (state) => state.asignacionList.length,
    totalAsignacionesProyecto: (state) => state.asignacionListProyecto.length
  },

  actions: {
    async fetchAsignaciones(){
      this.cargando = true;
      this.error = null;

      try{
        const response = await api.get('asignaciones');
        this.asignacionList = response.data;
      } catch (err) {
        this.error = "No se pudieron cargar las asignaciones";
      } finally {
        this.cargando = false;
      }
    },
    async fetchAsignacionesByProyecto(idProyecto) {
      this.cargando = true;
      this.error = null;

        try{
          const response = await api.get(`asignaciones/proyecto/${idProyecto}`)
          this.asignacionListProyecto = response.data;
        } catch (err) {
          this.error = ("Error al traer las asignaciones: ", err)
        } finally {
          this.cargando = false;
        }
    },
    async fetchAsignacionById(id) {
      this.cargando = true;
      this.error = null;
      this.asignacionActual = null;
      try {
        const response = await api.get(`asignaciones/${id}`);
        this.asignacionActual = response.data;
      } catch (err) {
        this.error = ("Error al traer el asignación:", err);
      } finally {
        this.cargando = false;  
      }
    },
    async deleteAsignacion(id) {
      this.cargando = true;
      this.error = null;
      try {
        await api.delete(`asignaciones/${id}`);
        
        this.asignacionActual = null;
        this.asignacionList = this.asignacionList.filter(e => e.id !== id);
        this.asignacionListProyecto = this.asignacionListProyecto.filter(e => e.id !== id);
        
        return true;
      } catch(err) {
        this.error = "No se pudo eliminar la asignación";
        console.error(err);
        return false;
      } finally {
        this.cargando = false;
      }
    },
    async createAsignacion(nuevaAsignacion) {
      try {
        await api.post('asignaciones', nuevaAsignacion);
        return true;
      } catch (error) {
        console.error("Error al crear:", error);
        return false;
      }
    },

    async updateAsignacion(id, datosActualizados) {
      try {
        await api.put(`asignaciones/${id}`, datosActualizados);
        return true;
      } catch (error) {
        console.error("Error al actualizar:", error);
        return false;
      }
    }
  }
});