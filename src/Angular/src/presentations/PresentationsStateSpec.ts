import {PresentationsState} from './PresentationsState';

describe('PresentationsState', () => {
    let $state: angular.ui.IStateService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$state_) => {
        $state = _$state_;
    }));
    
    it('should register state', () => {
        expect($state.get(PresentationsState)).toEqual(PresentationsState); 
    });
    
    it('should specify name', () => {
        expect(PresentationsState.name).toBe('presentations');
    });
    
    it('should specify url', () => {
        expect(PresentationsState.url).toBe('/presentations');
    });
    
    it('should specify template', () => {
        expect(PresentationsState.template).toBe('<presentations></presentations>');
    });
    
    it('should specify title', () => {
        expect(PresentationsState.title).toBe('Presentations');
    });
    
    it('should specify order', () => {
        expect(PresentationsState.order).toBe(3);
    })
})