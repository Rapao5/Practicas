let sensorReading = {deviceld: 'T-101', value: 25.4, unit: 'C'}
let sensorReading2 = {deviceld: 'T-102', value: 21.4, unit: 'C'};

function formatear(objeto){
    return lectura = "Lectura del sensor " + objeto.deviceld + ": " + objeto.value + "°" + objeto.unit;
}

console.log(formatear(sensorReading));
console.log(formatear(sensorReading2));