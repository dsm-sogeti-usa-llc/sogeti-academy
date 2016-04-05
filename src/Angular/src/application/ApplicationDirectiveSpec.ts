import {ApplicationDirective} from './ApplicationDirective';
import {ApplicationController} from './controllers/ApplicationController';

describe('ApplicationDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: ApplicationDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new ApplicationDirective();
    }));

    it('should register with angular', () => {
        expect($injector.has('applicationDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
       expect(directive.controller).toBe(ApplicationController); 
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$app');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/application'));
    });
});