import {CreateTopicDirective} from './CreateTopicDirective';
import {CreateTopicController} from './controllers/CreateTopicController';

describe('CreateTopicDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: CreateTopicDirective;

    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new CreateTopicDirective();
    }));
    
    it('should register with angular', () => {
        expect($injector.has('createTopicDirective')).toBeTruthy(); 
    });
    
    it('should isolate scope', () => {
        expect(directive.scope).toEqual({}); 
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(CreateTopicController); 
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$create');
    });
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/createTopic')); 
    });
});