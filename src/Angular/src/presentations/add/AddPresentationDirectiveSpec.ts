import {AddPresentationDirective} from './AddPresentationDirective';
import {AddPresentationController} from './controllers/AddPresentationController';

describe('AddPresentationDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: AddPresentationDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new AddPresentationDirective();
    }));   
    
    it('should register with angular', () => {
        expect($injector.has('addPresentationDirective')).toBeTruthy(); 
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(AddPresentationController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$add');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/addPresentation'));
    })
});