/*
 * Consultor
 * 
 * Clase auxiliar responsable de consultar al usuario del
 * entorno genérico de sistema experto acerca del 
 * cumplimiento de diferentes hechos que podrían darse en
 * el caso de la realidad que se verifica.
 * 
 * De acuerdo al funcionamiento convencional de un sistema
 * experto, se deben tomar hechos de la realidad y luego 
 * confrontarlos con el conocimiento almacenado en la ontología
 * o base de conocimientos; el resultado es información útil,
 * normalmente conocida como la meta del sistema experto.
 * 
 * Datos ---> Sistema Experto---> Información
 *                  ^
 *                  |
 *             Conocimiento
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 29/10/2006
 * Hora: 06:42 a.m.
 * 
 */

using System;

namespace Experto{
	public class Consultar{
		internal static bool porAtomo(Atomo aa,Regla ra){
			string resp;
			do{
				Console.Write("Se cumple "+aa.Desc+"? (S/N/P): ");
				resp=Console.ReadLine();
				resp=resp.ToUpper();
				if (resp.Equals("P")){
					Console.WriteLine("Se intenta probar que: "+ra);
				}
			}while(!resp.Equals("S")&&!resp.Equals("N"));
			return resp.Equals("S");
		}
	}
}
