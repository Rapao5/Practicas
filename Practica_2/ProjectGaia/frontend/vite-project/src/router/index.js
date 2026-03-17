import FormularioCrear from "@/views/FormularioCrear.vue";
import FormularioEditar from "@/views/FormularioEditar.vue";
import Home from "@/views/Home.vue";
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/formularioCrear",
      name: "formularioCrear",
      component: FormularioCrear
    },
    {
      path: "/formularioEditar/:id",
      name: "formularioEditar",
      component: FormularioEditar
    }
  ]
})

export default router;