import {PresentationsDirective} from './PresentationsDirective';
import {PresentationsController} from './controllers/PresentationsController';

describe('PresentationsDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: PresentationsDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new PresentationsDirective(); 
    }));
    
    it('should register directive', () => {
        expect($injector.has('presentationsDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(PresentationsController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$pres');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/presentations'));
    })
})