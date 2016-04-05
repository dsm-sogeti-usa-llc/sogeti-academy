import {PresentationListDirective} from './PresentationListDirective';
import {PresentationListController} from './controllers/PresentationListController';

describe('PresentationListDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: PresentationListDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new PresentationListDirective();
    }));
    
    it('should register with angular', () => {
        expect($injector.has('presentationListDirective')).toBeTruthy(); 
    });
    
    it('should isolage scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(PresentationListController);
    });
    
    it('should specify controller as', () => {
       expect(directive.controllerAs).toBe('$list'); 
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/presentationList'));
    })
});