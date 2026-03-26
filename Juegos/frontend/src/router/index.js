import FormularioCrear from "@/views/FormularioCrear.vue";
import FormularioCrearPlayer from "@/views/FormularioCrearPlayer.vue";
import FormularioUpdate from "@/views/FormularioUpdate.vue";
import FormularioUpdatePlayer from "@/views/FormularioUpdatePlayer.vue";
import Home from "@/views/Home.vue"
import Lista from "@/views/Lista.vue"
import ListaPlayers from "@/views/ListaPlayers.vue";
import { createRouter, createWebHashHistory } from "vue-router"

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path:"/lista",
      name: "lista",
      component: Lista
    },
    {
      path:"/formularioCrear",
      name: "formularioCrear",
      component: FormularioCrear
    },
    {
      path:"/formularioUpdate/:id",
      name: "formularioUpdate",
      component: FormularioUpdate
    },
    {
      path:"/listaPlayers",
      name: "listaPlayers",
      component: ListaPlayers
    },
    {
      path:"/formularioCrearPlayer",
      name: "formularioCrearPlayer",
      component: FormularioCrearPlayer
    },,
    {
      path:"/formularioUpdatePlayer/:id",
      name: "formularioUpdatePlayer",
      component: FormularioUpdatePlayer
    }
  ]
})

export default router;