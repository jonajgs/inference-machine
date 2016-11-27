/*
 * PilaBooleana
 * 
 * Clase auxiliar, responsable de administrar una pila
 * con una representación de datos específica para el 
 * enfoque de este entorno genérico para sistemas expertos.
 * 
 * En particular, es utilizada para realizar la evaluación
 * lógica de las condiciones compuestas, contenidas en las
 * representaciones LM-Regla; que se encuentran expresadas
 * en notación postfija libre de complejidad en paréntesis
 * y presedencias, y directamente procesable por la maquinaria
 * computacional.
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
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
