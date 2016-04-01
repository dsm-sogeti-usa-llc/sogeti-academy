import {PresentationDetailState} from './PresentationDetailState';
import {PresentationsState} from '../PresentationsState';

describe('PresentationDetailState', () => {
    let $state: angular.ui.IStateService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$state_) => {
        $state = _$state_;
    }));
    
    it('should specify name', () => {
        expect(PresentationDetailState.name).toBe('presentations.detail');
    });
    
    it('should specify url', () => {
        expect(PresentationDetailState.url).toBe('/presentations/:id');
    });
    
    it('should specify template', () => {
        expect(PresentationDetailState.template).toBe('<presentation-detail></presentation-detail>');
    });
    
    it('should specify parent', () => {
        expect(PresentationDetailState.parent).toBe(PresentationsState.name);
    })
    
    it('should register state', () => {
        expect($state.get(PresentationDetailState)).toEqual(PresentationDetailState);
    })
});