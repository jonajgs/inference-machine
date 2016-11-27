/*
 * Regla
 * 
 * La clase regla es esencial para este entorno genérico de 
 * sistemas expertos. Representa una expresión del conocimiento
 * en una codificación SI-ENTONCES por medio del lenguaje
 * LM-Regla. 
 * 
 * Responsable de codificar y descodificar el conocimiento 
 * que debe gestionar, puede evaluar y disparar a partir del
 * conocimiento en la memoria de trabajo. Del mismo modo, al
 * disparar ingresa a la memoria de trabajo los elementos 
 * de la conclusión múltiple que debe gestionar, y en el 
 * caso de haber alcanzado un objetivo definitivo del sistema
 * experto lo notifica adecuadamente.
 * 
 * Desarrollador: Luis Alberto Casillas Santillán
 * Fecha: 26/10/2006
 * Hora: 07:49 a.m.
 * 
 */

using System;
using System.Collections;

namespace Experto{	
	public class Regla{
		internal ArrayList partesCond=new ArrayList();		
		internal ArrayList partesConc=new ArrayList();
		internal bool marca=false;
		internal bool disparo=false;
		internal bool objetivo=false;
		public Regla(string reglaCad){
			analiza(reglaCad);
		}
		public override string ToString(){
			string retorno="SI ";
			//for(ParteRegla elemCond : partesCond)
			foreach(ParteRegla elemCond in partesCond){
				retorno+=(elemCond+" ");
			}
			retorno+="ENTONCES ";
			foreach(ParteRegla elemConc in partesConc){
				retorno+=(elemConc+" ");
			}
			return retorno;
		}
		internal bool probarCondicion(MemoriaTrabajo mt){
			PilaBooleana pb=new PilaBooleana();
			bool verdad1=false,verdad2=false;
			Atomo aTmp=null,aMT=null;
			Negacion nTmp=null;
			Binario bTmp=null;
			foreach(ParteRegla elemCond in partesCond){				
				if (elemCond is Atomo){
					aTmp=(Atomo)elemCond;
					aMT=mt.recupera(aTmp);
					//Console.WriteLine(aTmp);
					verdad1=aTmp.verVerdad(aMT);
					pb.push(verdad1);
				} else
				if (elemCond is Negacion){
					nTmp=(Negacion)elemCond;
					verdad1=pb.pop();
					verdad1=!verdad1;
					pb.push(verdad1);
				} else
				if (elemCond is Binario){
					bTmp=(Binario)elemCond;
					verdad1=pb.pop();
					verdad2=pb.pop();
					pb.push(bTmp.conj?verdad1&&verdad2:verdad1||verdad2);
				}
			}
			return pb.pop();
		}
		internal bool dispara(MemoriaTrabajo mt){
			Atomo aTmp=null;
			bool llegoObj=false;
			disparo=true;
			ArrayList atomos=new ArrayList();
			foreach(ParteRegla elemConc in partesConc){
				// El nivel de certidumbre que se reciba
				// se asigna a los átomos conclusión
				// que se ingresarán a la MT.
				if (elemConc is Atomo){
					aTmp=new Atomo((Atomo)elemConc);		
					atomos.Add(aTmp);
					if (aTmp.Objetivo) llegoObj=true;
				} else
				if (elemConc is Negacion){
					aTmp.Estado=!aTmp.Estado;					
				}				
			}
			foreach(Atomo aa in atomos){
				try{
					mt.guardaAtomo(aa);
				}catch(AtomoDuplicado ad){
					Console.WriteLine("Se duplico el atomo: {0}",aa);
					// Hacer nada...
				}
			}
			return llegoObj;
		}
		internal bool esObjetivo(){
			return objetivo;
			/*Atomo aTmp=null;
			foreach(ParteRegla elemento in partesConc){
				if (elemento is Atomo){
					aTmp=(Atomo)elemento;
					if (aTmp.Objetivo) return true;
				}
			}
			return false;*/
		}
		void analiza(string r){
			string [] partes=r.Split();
			bool regla,cond,conc,atomo,obj;
			ParteRegla pr=null;
			regla=cond=conc=atomo=obj=false;
			//Console.WriteLine(r);
			foreach(string parte in partes){
				//Console.WriteLine(parte);
				switch(parte){
					// Etiquetas Dobles //
					case "<atomo>":      atomo=true;
								         obj=false;
								         break;
					// ---------------------------------			         
					case "</atomo>":     atomo=false;
					                     obj=false;
									     break;
					// ---------------------------------				     
					case "<atomoObj>":   atomo=true;
					                     obj=true;
					                     objetivo=true;
								         break;
					// ---------------------------------			         
					case "</atomoObj>":  atomo=false;
					                     obj=false;
									     break;
					// ---------------------------------				     
					case "<condicion>":  cond=true;
								         break;
					// ---------------------------------			         
					case "</condicion>": cond=false;
										 break;
					// ---------------------------------			         
					case "<conclusion>": conc=true;
										 break;
					// ---------------------------------			         
					case "</conclusion>":conc=false;
										 break;
					// Etiquetas Sencillas //
					case "<negacion/>":  pr=new Negacion();
										 if (cond&&!conc) partesCond.Add(pr);
										 if (conc&&!cond) partesConc.Add(pr);
										 break;
					// ---------------------------------				     
					case "<conjuncion/>":pr=new Binario(true);
									     if (cond&&!conc) partesCond.Add(pr);
									     if (conc&&!cond) partesConc.Add(pr);
									     break;
					// ---------------------------------				     
					case "<disyuncion/>":pr=new Binario(false);
									     if (cond&&!conc) partesCond.Add(pr);
									     //if (conc&&!cond) partesConc.Add(pr);
									     break;
					// ---------------------------------				     
					default:if (atomo){
							  pr=new Atomo(parte,true,obj);
							  if (cond&&!conc&&!obj) partesCond.Add(pr);
							  if (conc&&!cond) partesConc.Add(pr);
							}
							break;
				}
			}
		}
	}
}
