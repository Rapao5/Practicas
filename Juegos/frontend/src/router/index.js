import FormularioCrear from "@/views/FormularioCrear.vue";
import FormularioUpdate from "@/views/FormularioUpdate.vue";
import Home from "@/views/Home.vue"
import Lista from "@/views/Lista.vue"
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
    }
  ]
})

export default router;