import {VoteForTopicDirective} from './VoteForTopicDirective';
import {VoteForTopicController} from './controllers/VoteForTopicController';

describe('VoteForTopicDirective', () => {
    let $injector: angular.auto.IInjectorService;
    let directive: VoteForTopicDirective;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$injector_) => {
        $injector = _$injector_;
        directive = new VoteForTopicDirective();
    }));
    
    it('should register with angular', () => {
        expect($injector.has('voteForTopicDirective')).toBeTruthy();
    });
    
    it('should isolate scope', () => {
       expect(directive.scope).toEqual({});
    });
    
    it('should specify controller', () => {
        expect(directive.controller).toBe(VoteForTopicController);
    });
    
    it('should specify controller as', () => {
        expect(directive.controllerAs).toBe('$vote');
    });
    
    it('should bind to controller', () => {
        expect(directive.bindToController.topic).toBe('=');
    })
    
    it('should specify template', () => {
        expect(directive.template).toBe(require('./templates/voteForTopic'));
    });
});