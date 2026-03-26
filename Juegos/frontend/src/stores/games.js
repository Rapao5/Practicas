import { defineStore } from "pinia";
import { ref } from "vue";
import axios from "axios";

export const useGamesStore = defineStore("games", () => {
  const games = ref([])
  const players = ref([])
  const API_URL = 'https://localhost:7020/api/Games'
  const nuevoGame = {
    titulo: "",
    genero: "",
    descripcion: "",
    jugado: null,
    tiempo: null,
    puntuacion: null,
    imagen: ""
  };
  const nuevoPlayer = {
    nombre: "",
    espicialidad: "",
    idGames: null
  }

  const scrollGames = async (filtros = {}, pagina = 1) => {
      const response = await axios.get(`${API_URL}/paginado`, {
      params: {
        ...filtros, 
        pagina: pagina,
        cantidad: 50
      } 
    });
    if(pagina===1){
      games.value=response.data.juegos;
    }else{
      games.value = [ ...games.value, ...response.data.juegos];
    }

    return response.data.juegos.length;
  } 

  const fetchGames = async (filtros = {}, pagina = 1) => {
    const response = await axios.get(`${API_URL}/paginado`, {
      params: {
        ...filtros, 
        pagina: pagina,
        cantidad: 50
      } 
    });
    games.value=response.data.juegos;
  }

  const fetchPlayer = async (filtros = {}, pagina = 1) => {
    const response = await axios.get(`${API_URL}/paginado/player`, {
      params: {
        ...filtros,
        pagina: pagina,
        cantidad: 50
      }
    });
    players.value=response.data.player;
  } 

  const saveGame = async (nuevoGame) => {
    await axios.post(API_URL,  nuevoGame);
    await fetchGames();
  };

    const savePlayer = async (nuevoPlayer) => {
    await axios.post(`${API_URL}/crearPlayer`, nuevoPlayer);
    await fetchPlayer();
  };

  const deleteGame = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
    await fetchGames();
  }

  const deletePlayer = async (id) => {
    await axios.delete(`${API_URL}/deletePlayer/${id}`);
    await fetchPlayer();
  }

  const updateGame = async (nuevoGame) => {
    await axios.put(API_URL, nuevoGame);
    await fetchGames();
  }

    const updatePlayer = async (nuevoPlayer) => {
    await axios.put(`${API_URL}/updatePlayer`, nuevoPlayer);
    await fetchGames();
  }

  const getGameById = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  };

  const getPlayerById = async (id) => {
    const response = await axios.get(`${API_URL}/player/${id}`);
    return response.data;
  };

  return{
    games,
    players,
    fetchGames,
    saveGame,
    deleteGame,
    updateGame,
    getGameById,
    scrollGames,
    fetchPlayer,
    savePlayer,
    deletePlayer,
    updatePlayer,
    getPlayerById
  }
});