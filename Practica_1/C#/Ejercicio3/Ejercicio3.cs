using System;

namespace Ejercicio1
{
    public interface IReportable
    {
        string GenerateReport();
    }
	public class Investigador : IReportable
	{
		public int id;
		public string nombre{get; set;}
		public string especialidad{get; set;}
        public List<string> habilidades;

		public Investigador(int id, string nombre, string especialidad)
		{
			this.id=id;
			this.nombre=nombre;
			this.especialidad=especialidad;
            this.habilidades = new List<string>();
		}
        public void RegistrarHabilidad(string habilidad)
        {
            habilidades.Add(habilidad);
        }

        public string GenerateReport()
        {
            string listaHabilidades = string.Join(", ", this.habilidades);
            return "Información de investigador: -Id: " + this.id + " -Nombre: " + this.nombre + " -Especialidad: " + this.especialidad + "-Habilidades: " + listaHabilidades + ".";
        }
	}
	public class Especie : IReportable
	{
		public int id;
		public string nombreCientifico{ get; set; }

		public string habitat{ get; set; }

		public Especie(int id, string nombreCientifico, string habitat)
		{
			this.id=id;
			this.nombreCientifico=nombreCientifico;
			this.habitat=habitat;
		}
        public string GenerateReport()
        {
            return "Información de la especie: -Id: " + this.id +" -Nombre Científico: " + this.nombreCientifico + " -Habitat: " + this.habitat + ".";
        }
	}
	class Program
	{
		static void Main(String [] args)
		{
			Investigador persona = new Investigador(1, "Alex God", "Jefe de programación");
            Investigador persona2 = new Investigador(2, "Jose Profe", "Enseñanza avanzada");
            Investigador persona3 = new Investigador(3, "Raúl Tower", "Defensa en CSS");
            persona.RegistrarHabilidad("Compilación por fe");
            persona.RegistrarHabilidad("Google Fu Nivel Maestro");
            persona2.RegistrarHabilidad("Telepatía de punto y coma");
            persona2.RegistrarHabilidad("Resistencia al café frío");
            persona3.RegistrarHabilidad("Domador de Divs");
            persona3.RegistrarHabilidad("Inmunidad al Z-Index");

			Especie animal = new Especie(101, "Capius Pastum", "Stack Overflow");

			Console.WriteLine("El investigador " + persona.nombre + " trabaja con " + animal.nombreCientifico + ".");
            Console.WriteLine(animal.GenerateReport());

            List<Investigador> investigadores = [persona, persona2, persona3];

            foreach (var investigador in investigadores)
            {
                Console.WriteLine(investigador.GenerateReport());
            }

		}
	}
}