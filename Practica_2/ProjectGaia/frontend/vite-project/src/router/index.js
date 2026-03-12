import Formulario from "@/views/formulario.vue";
import Home from "@/views/home.vue";
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
      path: "/formulario/:id?",
      name: "formulario",
      component: Formulario
    }
  ]
})

export default router;