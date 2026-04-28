import { setActivePinia, createPinia } from 'pinia'
import { describe, it, expect, beforeEach, vi } from 'vitest'
import { useProyectoStore } from '../../stores/proyectoStore'
import api from '../../api/axios' 

describe('Proyecto Store', () => {

  beforeEach(() => {
    setActivePinia(createPinia())
  })

  it('debe llenar proyectoList al llamar a fetchProyectos', async () => {
    const store = useProyectoStore()
  
    const proyectosMock = [
      { id: 1, nombre: 'Reforestación Sierra', estado: true },
      { id: 2, nombre: 'Estudio de Corales', estado: false }
    ]

    const spy = vi.spyOn(api, 'get').mockResolvedValue({
      data: proyectosMock
    })

    await store.fetchProyectos()

    expect(store.proyectoList).toEqual(proyectosMock)
    expect(store.proyectoList.length).toBe(2)
  })
})