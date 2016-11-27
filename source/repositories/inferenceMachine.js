import WorkingMemory from '../models/WorkingMemory';
import KnowledgeModule from '../models/KnowledgeModule';
import InferenceMachine from '../models/InferenceMachine';

export function startRepository(state) {
    let workingMemory = new WorkingMemory();
    let knowledgeModule = new KnowledgeModule("Animales", state.filename);
    let inferenceMachine = new InferenceMachine();

    knowledgeModule.loadKnowledgeBase(state.filename).then(function(response) {
        knowledgeModule.set('knowledgeBase', response);
        let result = inferenceMachine.chainBack(knowledgeModule, workingMemory);
        console.log(result);
    });

    return {
        ...state,
    };
};
