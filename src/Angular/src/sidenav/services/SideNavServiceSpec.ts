import {SidenavService} from './SidenavService';
import {WelcomeState} from '../../welcome/states/WelcomeState';
import {TopicsState} from '../../topics/states/TopicsState';
import {PresentationsState} from '../../presentations/PresentationsState';

describe('SideNavService', () => {
    let sidenavService: SidenavService
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_SidenavService_) => {
        sidenavService = _SidenavService_;
    }))
    
    it('should include WelcomeState', () => {
         const states = sidenavService.getAllStates();
         expect(states.indexOf(WelcomeState)).toBeGreaterThan(-1);
    });
    
    it('should include TopicsState', () => {
       const states = sidenavService.getAllStates();
       expect(states.indexOf(TopicsState)).toBeGreaterThan(-1); 
    });
    
    it('should include PresentationsState', () => {
        const states = sidenavService.getAllStates();
        expect(states.indexOf(PresentationsState)).toBeGreaterThan(-1);
    })
});