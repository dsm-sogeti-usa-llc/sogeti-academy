import {EventEmitter} from 'events'

const topics = [];
export class TopicsStore extends EventEmitter {
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
}