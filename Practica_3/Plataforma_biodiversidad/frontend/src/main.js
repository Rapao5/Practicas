import { createApp } from 'vue'
import './style.css'
import router from './router'
import { createPinia } from 'pinia'
import App from './App.vue'
import './style.css'

const pinia = createPinia();

createApp(App).use(router).use(pinia).mount('#app')
