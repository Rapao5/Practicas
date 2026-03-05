class Sensor{
    
    #lecturas=[];

    constructor(id){
        this.id=id;
        this.#lecturas=[];
    }
    addReading(value){
        this.#lecturas.push(value);
    }  
    getAverageReading(){
        if(this.#lecturas.length !== 0){
            let total = 0;
            for(let i=0;i < this.#lecturas.length; i++){
                total +=this.#lecturas[i];
            }
            return total/this.#lecturas.length;
        }else{
            return 0;
        }
    }
}

const sensor = new Sensor('Caldera');
const sensor2 = new Sensor('Cocina');
sensor2.addReading(18);
sensor2.addReading(15);
sensor2.addReading(12);
sensor.addReading(20);
sensor.addReading(22);
sensor.addReading(30);
console.log("La lectura media de " + sensor.id + " es: "+sensor.getAverageReading() + "°C");
console.log("La lectura media de " + sensor2.id + " es: "+sensor2.getAverageReading() + "°C");
