import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useSensorStore = defineStore('sensor', () => {
  const sensors = ref([])
  const API_URL = 'https://localhost:7016/api/sensors'
  
  const estados = {
    ACTIVO: 'Activo',
    INACTIVO: 'Inactivo',
    MANTENIMIENTO: 'Mantenimiento'
  }

  const fetchSensors = async (filtros = {}) => {
    const params = new URLSearchParams()
    if (filtros.zona) params.append('zona', filtros.zona)
    if (filtros.estado) params.append('estado', filtros.estado)
    if (filtros.tempMin) params.append('tempMin', filtros.tempMin)
    if (filtros.tempMax) params.append('tempMax', filtros.tempMax)

    const response = await fetch(`${API_URL}?${params.toString()}`)
    sensors.value = await response.json()
  }

  const getSensorById = async (id) => {
    const response = await fetch(`${API_URL}/${id}`)
    if (response.ok) return await response.json()
    throw new Error("Sensor no encontrado")
  }

  const postSensor = async (nuevoSensor) => {
    const response = await fetch(API_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(nuevoSensor)
    })
    return response.ok
  }
  
  const updateSensor = async (sensorEditado) => {
    const response = await fetch(API_URL, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(sensorEditado)
    })
    return response.ok
  }

  const deleteSensor = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, { method: 'DELETE' })
    if (response.ok) await fetchSensors()
  }

  return {
    sensors,
    estados,
    fetchSensors,
    getSensorById,
    postSensor,
    updateSensor,
    deleteSensor
  }
})