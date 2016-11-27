import Backbone from 'backbone';
import RNFS from 'react-native-fs';
import Rule from './Rule';

const knowledgeModule = Backbone.Model.extend({
    defaults: {
        description: '',
        knowledgeBase: [],
    },
    initialize: function(description){
        this.set('description', description);
    },
    constructor: function() {
        Backbone.Model.apply(this, arguments);
    },
    loadKnowledgeBase: function(file) {
        return RNFS.readFile(RNFS.ExternalDirectoryPath + '/' + file)
            .then(function(result) {
                return result
                    .replace(/<\/regla> /g, '</regla>\n')
                    .split('\n')
                    .filter(rule => rule.trim() !== '');
            });
    },
    getKnowledgeModule: () => {
        return {
            name: 'modulo conocimiento',
            knowledge: this.knowledgeBase,
        };
    },
    filterObjectives: function() {
        let objetives = [];

        this.get('knowledgeBase').filter(kb => {
            let rule = new Rule(kb);
            if ( rule.isObjective() ) {
                objetives.push(rule);
            }
        });

        return objetives;
    },
    unmark: function() {
        this.get('knowledgeBase').map(kb => {
            let rule = new Rule(kb);
            rule.set('mark', false);
        });
    },
    removeTriggers: function() {
        this.get('knowledgeBase').map(kb => {
            let rule = new Rule(kb);
            rule.set({ trigger: false });
        });
    },
    rulesTriggered: function() {
        return this.get('knowledgeBase').filter(kb => {
            let rule = new Rule(kb);
            if ( rule.trigger ) {
                return true;
            }
        });
    },
});

export default knowledgeModule;
