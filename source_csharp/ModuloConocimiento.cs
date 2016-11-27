/*
 * ModuloConocimiento
 * 
 * Clase esencial de este entorno genérico para sistemas
 * expertos. Es la responsable de contener y gestionar
 * todo el conocimiento que administra el sistema experto.
 * 
 * Específicamente contiene la ontología que el experto
 * artificial debe procesar, por medio de una base de
 * conocimiento que contiene todas las reglas expresadas
 * originalmente en LM-Regla y luego en una codificación
 * interna congruente al resto de este modelo.
 * 
 * Responsable de cargar la base de conocimiento, apoyándose
 * en el comportamiento implementado en la clase Regla. 
 * Filtra las reglas que concluyen objetivos, elimina las 
 * marcas de disparo y las marcas para encadenamiento hacia
 * atrás hechas durante este tipo de inferencia.
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 29/10/2006
 * Hora: 06:10 a.m.
 * 
 */

using System;
using System.Collections;

namespace Experto{	
	public class ModuloConocimiento{
		internal string desc;
		internal ArrayList bc;
		internal ModuloConocimiento(string desc){
			this.desc=desc;
		}
		internal ModuloConocimiento(string desc,string archBC){
			this.desc=desc;
			bc=new ArrayList();
			cargarBC(archBC);
		}
		void cargarBC(string nomArch){
			LectorArch la=new LectorArch(nomArch);
			string reglaCad=null;
			while((reglaCad=la.leeCad())!=null){
				bc.Add(new Regla(reglaCad));
			}
			la.cierra();
		}
		public override string ToString(){
			string retorno="Modulo de Conocimiento: "+desc+"\n";
			foreach(Regla elemento in bc){
				retorno+=(elemento+"\n");
			}
			return retorno;
		}
		internal ArrayList filtrarObjs(){
			ArrayList objs=new ArrayList();
			Regla r=null;
			foreach(object elemento in bc){
				r=(Regla)elemento;
				if (r.esObjetivo()) objs.Add(r);
			}
			return objs;
		}
		internal void desmarcar(){
			Regla r=null;
			foreach(object elemento in bc){
				r=(Regla)elemento;
				r.marca=false;
			}
		}
		internal void quitarDisparos(){
			Regla r=null;
			foreach(object elemento in bc){
				r=(Regla)elemento;
				r.disparo=false;
			}
		}
		internal String muestraReglasDisparadas(){
			String retorno = "";
			foreach(Regla rActual in bc){
				if (rActual.disparo) retorno+=(rActual+"\n"); 
			}
			return retorno;
		}
	}
}
