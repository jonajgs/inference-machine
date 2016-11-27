/*
 * Consultor
 * 
 * Clase auxiliar responsable de consultar al usuario del
 * entorno gen�rico de sistema experto acerca del 
 * cumplimiento de diferentes hechos que podr�an darse en
 * el caso de la realidad que se verifica.
 * 
 * De acuerdo al funcionamiento convencional de un sistema
 * experto, se deben tomar hechos de la realidad y luego 
 * confrontarlos con el conocimiento almacenado en la ontolog�a
 * o base de conocimientos; el resultado es informaci�n �til,
 * normalmente conocida como la meta del sistema experto.
 * 
 * Datos ---> Sistema Experto---> Informaci�n
 *                  ^
 *                  |
 *             Conocimiento
 * 
 * Desarrollador: Luis Alberto Casillas Santill�n
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
