const temperaturas = [10, 20, 30, 18, 22.5, 32, 25];

let temperaturaBaja = 100;
let temperaturaAlta = 0;
let total = 0;
let contador = 0;

for(let i=0; i<temperaturas.length; i++){
    total += temperaturas[i];
    contador++;
    if(temperaturas[i]<temperaturaBaja){
        temperaturaBaja=temperaturas[i];
    }
    if(temperaturas[i]>temperaturaAlta){
        temperaturaAlta=temperaturas[i];
    }
}

let media = total/contador;

console.log("Temperatura media: " + media + " Grados", " Temperatura más baja: " + temperaturaBaja + " Grados", " Temperatura más alta: " + temperaturaAlta + " Grados.");