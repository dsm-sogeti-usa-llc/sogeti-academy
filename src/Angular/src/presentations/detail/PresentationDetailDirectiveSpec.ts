import {PresentationDetailDirective} from './PresentationDetailDirective';
import {PresentationDetailController} from './controllers/PresentationDetailController';

describe('PresentationDetailDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: PresentationDetailDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new PresentationDetailDirective(); 
    }));   
    
    it('should register with angular', () => {
        expect($injector.has('presentationDetailDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(PresentationDetailController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$detail');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/presentationDetail'));
    })
});