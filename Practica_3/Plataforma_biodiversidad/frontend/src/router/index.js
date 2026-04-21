import {createRouter, createWebHashHistory} from "vue-router"
import Home from "../views/Home.vue";
import Ecosistema from "../views/Ecosistema.vue";
import Proyecto from "../views/Proyecto.vue";
import Investigador from "../views/Investigador.vue";
import MostrarEcosistema from "../views/MostrarEcosistema.vue";
import MostrarProyecto from "../views/MostrarProyecto.vue";
import MostrarInvestigador from "../views/MostrarInvestigador.vue";
import RegistroEcosistema from "../views/RegistroEcosistema.vue";
import EditarEcosistema from "../views/EditarEcosistema.vue";
import RegistroProyecto from "../views/RegistroProyecto.vue";
import EditarProyecto from "../views/EditarProyecto.vue";
import RegistroInvestigador from "../views/RegistroInvestigador.vue";
import EditarInvestigador from "../views/EditarInvestigador.vue";
import RegistroAsignacion from "../views/RegistroAsignacion.vue";
import Asignacion from "../views/Asignacion.vue";
import EditarAsignacion from "../views/EditarAsignacion.vue";

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
      path: "/mostrarEcosistema/:id",
      name: "mostrarEcosistema",
      component: MostrarEcosistema
    },
    {
      path: "/mostrarProyecto/:id",
      name: "mostrarProyecto",
      component: MostrarProyecto
    },
    {
      path: "/mostrarInvestigador/:id",
      name: "mostrarInvestigador",
      component: MostrarInvestigador
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