import {SideNavDirective} from './SideNavDirective';
import {SideNavController} from './controllers/SideNavController';

describe('SideNavDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: SideNavDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;    
        directive = new SideNavDirective();
    }));
    
    it('should register with angular', () => {
        expect($injector.has('sideNavDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(SideNavController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$sideNav');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/sideNav'));
    })
});