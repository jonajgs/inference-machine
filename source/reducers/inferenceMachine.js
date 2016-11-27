import {
    START,
} from '../actions/inferenceMachine';
import {
    startRepository,
} from '../repositories/inferenceMachine';

const defaultState = {
    filename: 'filezoo.bc',
    workingMemory: null,
    knoledgeModule: null,
    inferenceMachine: null,
};

export default function inferenceMachineReducer(state = defaultState, action) {
    switch ( action.type ) {
        case START:
            return startRepository(state);
        default:
            return defaultState;
    }
}
