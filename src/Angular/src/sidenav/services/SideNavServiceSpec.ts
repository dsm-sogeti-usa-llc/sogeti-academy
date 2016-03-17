import {SideNavService} from './SideNavService';
import {WelcomeState} from '../../welcome/states/WelcomeState';
import {TopicsState} from '../../topics/states/TopicsState';

describe('SideNavService', () => {
    let sideNavService: SideNavService
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_SideNavService_) => {
        sideNavService = _SideNavService_;
    }))
    
    it('should include WelcomeState', () => {
         const states = sideNavService.getAllStates();
         expect(states.indexOf(WelcomeState)).toBeGreaterThan(-1);
    });
    
    it('should include TopicsState', () => {
       const states = sideNavService.getAllStates();
       expect(states.indexOf(TopicsState)).toBeGreaterThan(-1); 
    });
});