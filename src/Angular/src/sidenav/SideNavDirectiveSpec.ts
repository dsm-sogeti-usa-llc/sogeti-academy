import {SidenavDirective} from './SidenavDirective';
import {SidenavController} from './controllers/SidenavController';

describe('SideNavDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: SidenavDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;    
        directive = new SidenavDirective();
    }));
    
    it('should register with angular', () => {
        expect($injector.has('sidenavDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(SidenavController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$sidenav');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/sidenav'));
    })
});