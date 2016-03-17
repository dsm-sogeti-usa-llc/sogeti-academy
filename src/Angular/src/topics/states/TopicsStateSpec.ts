import {TopicsState} from './TopicsState';

describe('TopicsState', () => {
    let $state: angular.ui.IStateService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$state_) => {
        $state = _$state_;
    }));
    
    it('should register state', () => {
        expect($state.get(TopicsState)).toBeTruthy();
    });
    
    it('should specify url', () => {
        expect(TopicsState.url).toBe('/topics');
    });
    
    it('should specify template', () => {
        expect(TopicsState.template).toBe('<topics></topics>');
    });
    
    it('should specify title', () => {
        expect(TopicsState.title).toBe('Topics');
    })
});