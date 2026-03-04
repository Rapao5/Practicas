const array = [];

const obj = {deviceld: 'T-101', value: 25.4, unit: 'C'};
const obj2 = {deviceld: 'T-102', value: 12, unit: 'C'};
const obj3 = {deviceld: 'T-103', value: null, unit: 'C'};
const obj4 = {deviceld: 'T-104', value: 20.9, unit: 'C'};
const obj5 = {deviceld: 'T-105', value: null, unit: 'C'};

array.push(obj);
array.push(obj2);
array.push(obj3);
array.push(obj4);
array.push(obj5);

const arrayBueno = array.filter(valor=>valor.value !=null); 
const arrayMapeado = arrayBueno.map(grados => grados.value *9/5 + 32);
const suma = arrayMapeado.reduce((acumulador, num) => acumulador + num, 0);
const media = suma / arrayMapeado.length;

console.log(array);
console.log(arrayBueno);
console.log(arrayMapeado);
console.log(media);