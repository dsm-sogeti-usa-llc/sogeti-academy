import {WelcomeState} from './WelcomeState';

describe('WelcomeState', () => {
    let $state: angular.ui.IStateService;
    let $rootScope: angular.IRootScopeService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$state_, _$rootScope_) => {
        $state = _$state_;    
        $rootScope = _$rootScope_;
    }));
    
    it('should add state to provider', () => {
        expect($state.get(WelcomeState)).toBeTruthy(); 
    });
    
    it('should specify state name', () => {
        expect(WelcomeState.name).toBe('welcome');
    });
    
    it('should specify template', () => {
        expect(WelcomeState.template).toBe('<welcome></welcome>');
    });
    
    it('should specify url', () => {
        expect(WelcomeState.url).toBe('/welcome');
    });
    
    it('should specify title', () => {
        expect(WelcomeState.title).toBe('Welcome'); 
    });
    
    it('should be default', () => {
        expect(WelcomeState.isDefault).toBeTruthy();
    })
});