import {EventEmitter} from 'events'
import {AppDispatcher} from '../../core/AppDispatcher';
import {LOAD_TOPICS_SUCCESS, CREATE_TOPIC_SUCCESS, VOTE_FOR_TOPIC_SUCCESS} from '../actions/TopicsActions';

let state = { topics: [] };
export class TopicsStore extends EventEmitter {
    constructor() {
        super();
        this.register();
    }

    getState() {
        return state;
    }

    addChangeListener(callback) {
        this.on('change', callback);
    }

    removeChangeListener(callback) {
        this.removeListener('change', callback);
    }

    emitChange() {
        this.emit('change');
    }

    register() {
        AppDispatcher.register((action) => {
            switch (action.actionType) {
                case LOAD_TOPICS_SUCCESS:
                    state = { topics: action.data.topics };
                    this.emitChange();
                    break;
                case CREATE_TOPIC_SUCCESS: 
                    state.topics.push(action.data);
                    this.emitChange();
                    break;
                
                case VOTE_FOR_TOPIC_SUCCESS: 
                    const topic = state.topics.find(t => t.Id === action.data.topicId);
                    topic.Votes = topic.Votes + 1;
                    this.emitChange();
                    break;  
            }
        })
    }
}