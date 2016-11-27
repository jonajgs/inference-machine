/*
 * MainClass
 * 
 * Esta clase es el orquestador principal del desempe�o
 * de este sistema experto.
 * 
 * Desde este punto se crean las instancias concretas
 * de los elementos principales del sistema experto: 
 * Motor de Inferencia, M�dulo de Conocimiento y
 * Memoria de Trabajo.
 * 
 * Desarrollador: Luis Alberto Casillas Santill�n
 * Fecha: 29/10/2006
 * Hora: 06:34 a.m.
 * 
 */
 
using System;
using System.Collections;

namespace Experto{
	class MainClass{		
		MemoriaTrabajo mt=new MemoriaTrabajo();
		// Al construir el m�dulo de conocimiento se
		// indica por medio de sus par�metros cu�l
		// es el nombre de la ontolog�a con que se
		// trabajar� y el nombre del archivo donde se
		// encuentra almacenada la base de conocimiento.
		// En este caso el nombre es Animales, y se encuentra
		// en el archivo Zoo.bc.
		ModuloConocimiento mc=new ModuloConocimiento("Animales","zoo.bc");
		MotorInferencia mi=new MotorInferencia();
		public static void Main(string[] args){
			ArrayList res=null;
			MainClass se=new MainClass();
			Console.WriteLine("Sistema Experto C#");
			Console.WriteLine(se.mc);
			// Aqu� podr�an inyectarse �tomos
			// a mt, antes de hacer la inferencia...
			res=se.mi.encadenarAtras(se.mc,se.mt);
			if (res!=null){
				Console.Write("Exito: ");
				foreach(Atomo a in res){
					Console.Write(a+" ");
				}
				Console.WriteLine(se.mt);
			}
			else Console.WriteLine("Fracaso...");
			Console.WriteLine(se.mc.muestraReglasDisparadas());
			//Console.ReadLine();
		}
	}
}
