import { combineReducers } from 'redux';
import InferenceMachine from './inferenceMachine';

const reducers = combineReducers({
    inferenceMachine: InferenceMachine,
});

export default reducers;
