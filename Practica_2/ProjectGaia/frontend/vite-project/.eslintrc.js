module.exports = {
  root: true,
  env: {
    node: true,
    'vue/setup-compiler-macros': true
  },
  extends: [
    'plugin:vue/vue3-essential',  // Menos reglas, bueno para principiantes
    'plugin:vue/vue3-recommended', // Más estricto, recomendado para desarrollo profesional
    'eslint:recommended',
    '@vue/eslint-config-prettier'
  ],
  parserOptions: {
    ecmaVersion: 2022
  },
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'vue/multi-word-component-names': 'off', // Para principiantes
    'vue/require-default-prop': 'off', // Para principiantes
    'vue/script-setup-uses-vars': 'error',
    'vue/no-unused-vars': 'error'
  }
}