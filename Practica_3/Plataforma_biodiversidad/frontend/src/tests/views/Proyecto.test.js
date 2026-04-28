import { mount } from '@vue/test-utils'
import { describe, it, expect, vi } from 'vitest'
import { createTestingPinia } from '@pinia/testing'
import Proyecto from '../../views/Proyecto.vue'

global.IntersectionObserver = class IntersectionObserver {
  constructor() {}
  observe() { return null; }
  unobserve() { return null; }
  disconnect() { return null; }
};

describe('Vista Proyecto.vue', () => {
  it('debe renderizar las tarjetas de los proyectos con sus nombres y estados correctos', async () => {
    const wrapper = mount(Proyecto, {
      global: {
        plugins: [
          createTestingPinia({
            createSpy: vi.fn,
            initialState: {
              proyectos: { 
                proyectoList: [
                  { 
                    id: 1, 
                    nombre: 'Reforestación Sierra', 
                    estado: true, 
                    investigadores: [], 
                    investigadoresRol: [] 
                  },
                  { 
                    id: 2, 
                    nombre: 'Estudio de Corales', 
                    estado: false, 
                    investigadores: ['Dra. Ana'], 
                    investigadoresRol: ['Líder'] 
                  }
                ]
              }
            }
          })
        ],
        stubs: ['RouterLink']
      }
    })

    await wrapper.vm.$nextTick();

    const tarjetas = wrapper.findAll('.card')
    
    expect(tarjetas.length).toBe(2)

    expect(tarjetas[0].text()).toContain('Reforestación Sierra')
    expect(tarjetas[0].text()).toContain('Activo')

    expect(tarjetas[1].text()).toContain('Estudio de Corales')
    expect(tarjetas[1].text()).toContain('Terminado')
  })
})