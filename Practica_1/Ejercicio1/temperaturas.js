const temperaturas = [-10, 20, -2, 18, 22.5, 32, 25];

let temperaturaBaja = null;
let temperaturaAlta = null;
let total = 0;
let contador = 0;

temperaturaAlta = Math.max(...temperaturas);
temperaturaBaja = Math.min(...temperaturas);
total = temperaturas.reduce((acumulador, num) => acumulador + num, 0);

let media = total/temperaturas.length;

console.log("Temperatura media: " + media + " Grados", " Temperatura más baja: " + temperaturaBaja + " Grados", " Temperatura más alta: " + temperaturaAlta + " Grados.");