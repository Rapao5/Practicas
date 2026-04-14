import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5100/api',
});

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response) {
      const status = error.response.status;
      const data = error.response.data;

      switch (status) {
        case 400:
        case 404:
          let mensajeError = 'Error en la petición';
          
          if (typeof data === 'string') {
            mensajeError = data;
          } else if (data && data.message) {
            mensajeError = data.message;
          }

          console.error(`Toast [${status}]: ${mensajeError}`);
          break;

        case 401:
          console.error('Toast: Tu sesión ha expirado. Por favor, inicia sesión de nuevo.');
          break;

        case 403:
          console.error('Toast: No tienes permisos para realizar esta acción.');
          break;

        case 500:
          console.error('Toast: Error interno del servidor.');
          break;
          
        default:
          console.error(`Toast: Ocurrió un error inesperado (Código ${status}).`);
      }
    } else {
      console.error('Toast: No se pudo conectar con el servidor.');
    }
    
    return Promise.reject(error);
  }
);

export default api;