import axios from 'axios';
import { useErrorStore } from '../stores/errorStore';

const api = axios.create({
  baseURL: 'http://localhost:5100/api',
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    const errorStore = useErrorStore();

    if (error.response) {
      const status = error.response.status;
      const data = error.response.data;

      let mensajeFinal = 'Ocurrió un error inesperado';

      if (typeof data === 'string') {
        mensajeFinal = data;
      } else if (data && data.message) {
        mensajeFinal = data.message;
      } else if (data && data.errors) {
        mensajeFinal = Object.values(data.errors).flat().join(', ');
      }

      errorStore.mostrar(mensajeFinal); 
    } else {
      errorStore.mostrar('No se pudo conectar con el servidor.');
    }
    
    return Promise.reject(error);
  }
);

export default api;