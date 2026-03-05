using System;

namespace Ejercicio1
{
    public interface IReportable
    {
        string GenerateReport();
    }
	public class Investigador
	{
		public int id;
		public string nombre{get; set;}
		public string especialidad{get; set;}

		public Investigador(int id, string nombre, string especialidad)
		{
			this.id=id;
			this.nombre=nombre;
			this.especialidad=especialidad;
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
			Especie animal = new Especie(101, "Capius Pastum", "Stack Overflow");

			Console.WriteLine("El investigador " + persona.nombre + " trabaja con " + animal.nombreCientifico + ".");
            Console.WriteLine(animal.GenerateReport());
		}
	}
}