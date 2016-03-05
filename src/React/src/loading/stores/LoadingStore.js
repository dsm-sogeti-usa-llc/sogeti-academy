import {EventEmitter} from 'events';
import {AppDispatcher} from '../../core/AppDispatcher';
import {LOADING_START, LOADING_END} from '../actions/LoadingActions';

let state = {
    isLoading: false
};

export class LoadingStore extends EventEmitter {
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
        this.removeListener(callback);
    }
    
    emitChange() {
        this.emit('change');
    }
    
    register() {
        AppDispatcher.register((action) => {
            switch (action.actionType) {
                case LOADING_START:
                    state = { isLoading: true };
                    this.emitChange();
                    break;
                    
                case LOADING_END:
                    state = { isLoading: false };
                    this.emitChange();
                    break;
            }
        })
    }
}