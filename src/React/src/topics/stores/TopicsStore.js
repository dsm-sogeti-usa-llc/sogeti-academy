import {EventEmitter} from 'events'
import {AppDispatcher} from '../../core/AppDispatcher';
import {LOAD_TOPICS_SUCCESS} from '../actions/TopicsActions';

let topics = [];
export class TopicsStore extends EventEmitter {
    constructor() {
        super();
        this.register();
    }

    getAll() {
        return topics;
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
                    topics = action.data.topics;
                    this.emitChange();
                    break;
            }
        })
    }
}