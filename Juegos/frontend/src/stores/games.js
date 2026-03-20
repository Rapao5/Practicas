import { defineStore } from "pinia";
import { ref } from "vue";
import axios from "axios";

export const useGamesStore = defineStore("games", () => {
  const games = ref([])
  const API_URL = 'https://localhost:7020/api/Games'
  const nuevoGame = {
    titulo: "",
    genero: "",
    descripcion: "",
    jugado: null,
    tiempo: null,
    puntuacion: null
  };

  const fetchGames = async (filtros = {}) => {
    const response = await axios.get(API_URL, {
      params: filtros
    });
    games.value=response.data;
  }

  const saveGame = async (nuevoGame) => {
    await axios.post(API_URL,  nuevoGame);
    await fetchGames();
  };

  const deleteGame = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
    await fetchGames();
  }

  const updateGame = async (nuevoGame) => {
    await axios.put(API_URL, nuevoGame);
    await fetchGames();
  }

  const getGameById = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  };

  return{
    games,
    fetchGames,
    saveGame,
    deleteGame,
    updateGame,
    getGameById
  }
});