import {SideNavService} from './SideNavService';
import {WelcomeState} from '../../welcome/states/WelcomeState';

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
});