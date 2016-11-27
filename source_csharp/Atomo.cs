/*
 * Atomo
 * 
 * La clase �tomo es esencial para este entorno gen�rico 
 * para sistemas expertos. Representa elementos concretos
 * de la realidad que busca ser procesada a trav�s de un
 * experto artificial modelado al interior del presente
 * entorno.
 * 
 * Esta clase incluye diversos atributos y operadores, a 
 * partir de los cuales logra el tratamiento adecuado de la
 * porci�n m�nima de realidad a tratar, un �tomo. Elemento
 * simb�lico de la programaci�n declarativa, �til en la
 * confecci�n de una ontolog�a.
 * 
 * Desarrollador: Luis Alberto Casillas Santill�n
 * Fecha: 26/10/2006
 * Hora: 07:14 a.m.
 * 
 */

using System;

namespace Experto{
	public class Atomo:ParteRegla{
		string desc;
		bool estado;
		bool objetivo;
		public Atomo(string desc,bool estado,bool objetivo){
			this.desc=desc.ToLower();
			this.estado=estado&&true;
			this.objetivo=objetivo&&true;
		}
		public Atomo(Atomo otro){
			desc=new string(otro.Desc.ToCharArray());
			estado=otro.Estado&&true;
			objetivo=otro.Objetivo&&true;
		}
		internal string Desc{
			get{
				return desc;
			}
			set{
				desc=value;
			}
		}
		internal bool Estado{
			get{
				return estado;
			}
			set{
				estado=value;
			}
		}
		internal bool Objetivo{
			get{
				return objetivo;
			}
			set{
				objetivo=value;
			}
		}
		public override string ToString(){
			return (estado?"":"!")+(objetivo?"*":"")+desc;
		}
		public override int GetHashCode(){
			return desc.GetHashCode()^(estado?1:0)^(objetivo?1:0);
		}
		public override bool Equals(object obj){
			Atomo aTmp=(Atomo)obj;
			return desc.Equals(aTmp.Desc)&&(estado==aTmp.Estado);			
		}
		internal bool verIgualdad(Atomo aTmp){
			return desc.Equals(aTmp.Desc)/*&&(estado==aTmp.Estado)*/;
		}
		internal bool verVerdad(Atomo aTmp){
			if (aTmp==null) return false;
			return estado&&aTmp.Estado;
		}
	}
}
