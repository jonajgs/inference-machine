/*
 * Operador
 * 
 * Clase auxiliar que representa a cualquiera de los operadores
 * l�gicos que pueden utilizarse en las expresiones l�gicas
 * utilizadas tanto en las condiciones compuestas como en las
 * conclusiones m�ltiples soportadas por la representaci�n
 * de conocimiento LM-Regla.
 * 
 * Espec�ficamente representa en un primer acercamiento una
 * negaci�n (operador unario) y un operador binario, que a su 
 * vez puede ser de dos naturalezas: conjunci�n o disyunci�n.
 * 
 * Es utilizado en las reglas y es congruente con un enfoque
 * polimorfo, por tal motivo esta sola clase representa diferentes
 * tipos de operadores.
 * 
 * Desarrollador: Luis Alberto Casillas Santill�n
 * Fecha: 26/10/2006
 * Hora: 07:53 a.m.
 * 
 */

using System;

namespace Experto{	
	public class Operador:ParteRegla{
		public Operador(){
		}
	}
	public class Negacion:Operador{
		public Negacion(){			
		}
		public override string ToString(){
			return "!";
		}
	}
	public class Binario:Operador{
		internal bool conj;
		// Las instancias en creadas con "true" representan una
		// conjunci�n, con "false" representan una disyunci�n.
		public Binario(bool conj){
			this.conj=conj;
		}
		public override string ToString(){
			return conj?"&":"|";
		}
	}
}
