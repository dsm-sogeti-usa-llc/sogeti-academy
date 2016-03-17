import {TopicsDirective} from './TopicsDirective';
import {TopicsController} from './controllers/TopicsController';

describe('TopicsDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: TopicsDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new TopicsDirective();
    }));
    
    it('should register directive with angular', () => {
       expect($injector.has('topicsDirective')).toBeTruthy(); 
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(TopicsController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$topics');
    });
    
    it('should specify template', () => {
       expect(directive.template).toBe(require('./templates/topics')); 
    });
});