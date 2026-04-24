import {createRouter, createWebHashHistory} from "vue-router"
import Home from "../views/Home.vue";
import Ecosistema from "../views/Ecosistema.vue";
import Proyecto from "../views/Proyecto.vue";
import Investigador from "../views/Investigador.vue";
import RegistroEcosistema from "../components/Ecosistema/RegistroEcosistema.vue";
import EditarEcosistema from "../components/Ecosistema/EditarEcosistema.vue";
import RegistroProyecto from "../components/Proyecto/EditarProyecto.vue";
import EditarProyecto from "../components/Proyecto/RegistroProyecto.vue";
import RegistroInvestigador from "../components/Investigador/RegistroInvestigador.vue";
import EditarInvestigador from "../components/Investigador/EditarInvestigador.vue";
import RegistroAsignacion from "../components/Asignaciones/RegistroAsignacion.vue";
import Asignacion from "../views/Asignacion.vue";
import EditarAsignacion from "../components/Asignaciones/EditarAsignacion.vue";

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/ecosistema",
      name: "ecosistema",
      component: Ecosistema
    },
    {
      path: "/registroEcosistema",
      name: "regsitroEcosistema",
      component: RegistroEcosistema
    },
    {
      path: "/proyecto",
      name: "proyecto",
      component: Proyecto
    },
    {
      path: "/investigador",
      name: "investigador",
      component: Investigador
    },
    {
      path: "/editarEcosistema/:id",
      name: "editarEcosistema",
      component: EditarEcosistema
    },
    {
      path: "/registroProyecto",
      name: "registroProyecto",
      component: RegistroProyecto
    },
    {
      path: "/editarProyecto/:id",
      name: "editarProyecto",
      component: EditarProyecto
    },
    {
      path: "/registroInvestigador",
      name: "registroInvestigador",
      component: RegistroInvestigador
    },
    {
      path: "/editarInvestigador/:id",
      name: "editarInvestigador",
      component: EditarInvestigador
    },
    {
      path: "/registroAsignacion/:id",
      name: "registroAsignacion",
      component: RegistroAsignacion
    },
    {
      path: "/editarAsignacion/:id",
      name: "editarAsignacion",
      component: EditarAsignacion
    },
    {
      path: "/asignacion/:id",
      name: "asignacion",
      component: Asignacion
    }
  ]
})

export default router;