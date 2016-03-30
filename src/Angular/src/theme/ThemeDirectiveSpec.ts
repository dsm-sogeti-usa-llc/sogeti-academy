import {ThemeDirective} from './ThemeDirective';
import {ThemeController} from './controllers/ThemeController';

describe('ThemeDirective', () => {
    let directive: ThemeDirective;
    let $injector: angular.auto.IInjectorService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;  
        directive = new ThemeDirective();  
    }));
    
    it('should register with angular', () => {
        expect($injector.has('themeDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(ThemeController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$theme');
    });
    
    it('should specify template', () => {
       expect(directive.template).toBe(require('./templates/theme')); 
    });
});