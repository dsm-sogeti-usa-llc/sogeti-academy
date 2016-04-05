import {WelcomeDirective} from './WelcomeDirective';
import {WelcomeController} from './controllers/WelcomeController';

describe('WelcomeDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: WelcomeDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new WelcomeDirective();
    }));
    
    it('should register with angular', () => {
        expect($injector.has('welcomeDirective')).toBeTruthy(); 
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(WelcomeController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$welcome');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/welcome'));
    })
});