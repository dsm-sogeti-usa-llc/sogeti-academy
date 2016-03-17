import {WelcomeController} from './WelcomeController';
import {TopicsState} from '../../topics/states/TopicsState';

describe('WelcomeController', () => {
    let createController: () => WelcomeController;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_) => {
        createController = () => {
            return _$controller_(WelcomeController, {
                
            });
        }
    }));
    
    it('should have topics state', () => {
       const controller = createController();
       expect(controller.topicsState).toEqual(TopicsState); 
    });
});