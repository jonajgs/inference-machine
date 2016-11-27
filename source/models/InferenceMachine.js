import Backbone from 'backbone';
import KnowledgeModule from './KnowledgeModule';
import Denial from './Denial';
import Rule from './Rule';
import Atom from './Atom';

const InferenceMachine = Backbone.Model.extend({
    defaults: {
        backward: false,
    },
    initialize: function() {
        // stuff
    },
    constructor: function() {
        Backbone.Model.apply(this, arguments);
    },
    chainForward: function(knowledgeModule, workingMemory) {
        let atomRule;
        let atom;
        let consultResult = false;
        let conditionResult = false;

        knowledgeModule.get('knowledgeBase').map(element => {
            atomRule = new Rule(element);
            element.conditionParts.map(condition => {
                if ( condition instanceof Atom ) {
                    atom = new Atom(atom);
                    if ( !workingMemory.present(atom) ) {
                        consultResult = consultPerAtom(atom, atomRule);
                        atom.set('state', consultResult);

                        //try {
                            workingMemory.saveAtom(atom);
                        //} catch ( DuplicatedAtom duplicatedAtom ) {
                        //    console.log('Se duplico del atomo ', duplicatedAtom);
                        //}
                    }
                }
            });
            conditionResult = atomRule.conditionTest(workingMemory);

            if ( conditionResult ) {
                console.log('se disparo ', atomRule);
                atomRule.trigger(workingMemory);

                conditionResult = false;

                if ( atomRule.isObjective() ) {
                    return atomRule.get('conclutionParts');
                }
            } else {
                if ( backward ) {
                    return null;
                }

                console.log('No se disparo ...');
            }
        });

        return null;
    },

    concat: function(destination, source) {
        source.map(atom => {
            if ( atom instanceof Atom ) {
                destination.push(atom);
            }

            if ( atom instanceof Denial ) {
                atom.set('state', !atom.get('state'));
            }
        });
    },

    isEligible: function(rule, toSatisfy) {
        let conclutionAtoms = [];
        let atomTemp;

        rule.get('conclutionParts').map(atom => {

            if ( atom instanceof Atom ) {
                conclutionAtoms.push(atom);
            }

            if ( atom instanceof Denial ) {
                atom.set('state', !atom.get('state'));
            }
        });

        conclutionAtoms.map(atom => {
            if ( toSatisfy.find(a => a == atom) ) {
                return true;
            }
        });

        return false;
    },

    chainBack: function(knowledgeModule, workingMemory) {
        let objetiveRules = knowledgeModule.filterObjectives();
        let toSatisfy = [];
        let knowledgeBasePremium = [];
        let result;
        let knowledgeBasePremiumName;
        let used = new Array(objetiveRules.length);
        let exit = false;
        let position = -1;
        let times = 0;
        let total = objetiveRules.length;
        this.backward = true;
        let knowledgeModuleTemp;

        do {
            position = Math.floor((Math.random() * total));

            if ( !used[position] ) {
                times++;
                used[position] = true;
                toSatisfy = [];
                knowledgeBasePremium = [];
                knowledgeModule.unmark();
                knowledgeModule.removeTriggers();
                objetiveRules[position].get('conclutionParts').map(conclution => {
                    if ( conclution instanceof Atom ) {
                        if ( conclution.get('objective') ) {
                            knowledgeBasePremiumName = conclution.get('description').toUpperCase();
                        }
                    }
                });

                knowledgeModuleTemp = new KnowledgeModule(knowledgeBasePremiumName);
                this.concat(toSatisfy, objetiveRules[position].get('conclutionParts'));
                do {
                    exit = true;
                    knowledgeModule.get('knowledgeBase').map(atomRule => {
                        let a = new Rule(atomRule);
                        if ( !a.get('mark') && this.isEligible(a, toSatisfy) ) {
                            exit = false;
                            a.set('mark', true);
                            this.concat(toSatisfy, a.get('conditionParts'));
                            knowledgeBasePremium.unshift(a);
                        }
                    });
                } while ( !exit );
                knowledgeModuleTemp.set('knowledgeBase', knowledgeBasePremium);
                console.log('Intentando con ', knowledgeModuleTemp);

                let result = this.chainForward(knowledgeModuleTemp, workingMemory);
                if ( result ) {
                    this.backward = false;
                    return result;
                }
            }
        } while ( times < total );
        this.backward = false;
        return null;
    },
});

export default InferenceMachine;
