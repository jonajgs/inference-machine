/*
 * PilaBooleana
 * 
 * Clase auxiliar, responsable de administrar una pila
 * con una representaci�n de datos espec�fica para el 
 * enfoque de este entorno gen�rico para sistemas expertos.
 * 
 * En particular, es utilizada para realizar la evaluaci�n
 * l�gica de las condiciones compuestas, contenidas en las
 * representaciones LM-Regla; que se encuentran expresadas
 * en notaci�n postfija libre de complejidad en par�ntesis
 * y presedencias, y directamente procesable por la maquinaria
 * computacional.
 * 
 * Desarrollador: Luis Alberto Casillas Santill�n
 * Fecha: 28/10/2006
 * Hora: 06:04 p.m.
 * 
 */

using System;
using System.Collections;

namespace Experto{
	public class PilaBooleana{
		ArrayList datos;
		internal PilaBooleana(){
			datos=new ArrayList();
		}
		internal void anula(){
			datos.Clear();
		}
		internal bool vacia(){
			return datos.Count==0;
		}
		internal bool top(){
			if (!vacia()){
				return (bool)datos[datos.Count-1];
			}
			return false;
		}
		internal bool pop(){
			bool tmp;
			if (!vacia()){
				tmp=(bool)datos[datos.Count-1];
				datos.RemoveAt(datos.Count-1);
				return tmp;
			}
			return false;
		}
		internal void push(bool valor){
			datos.Add(valor);
		}
	}
}
