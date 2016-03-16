import {SideNavController} from './SideNavController';
import {SideNavService} from '../services/SideNavService';
import {WelcomeState} from '../../welcome/states/WelcomeState';

describe('SideNavController', () => {
    let createController: () => SideNavController;
    let sideNavService: SideNavService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$mdMedia_, _SideNavService_) => {
        this.$mdMedia = _$mdMedia_;
        sideNavService = _SideNavService_;
        
        createController = () => {
            return _$controller_(SideNavController, {
                $mdMedia: this.$mdMedia,
                SideNavService: sideNavService
            });
        };
    }));
    
    it('should be docked open', () => {
        spyOn(this, '$mdMedia').and.callFake(size => {
            return size === 'gt-md';
        });
        
        const controller = createController();
        expect(controller.isDockedOpen).toBeTruthy();
    });
    
    it('should get all states', () => {
        spyOn(sideNavService, 'getAllStates').and.returnValue([
            {},
            {},
            {}
        ]);
        
         const controller = createController();
         expect(controller.states.length).toBe(3);
    });
    
    it('should have default state', () => {
        spyOn(sideNavService, 'getAllStates').and.returnValue([
            {},
            { isDefault: true },
            {}
        ]);
        
        const controller = createController();
        expect(controller.defaultState).toEqual({ isDefault: true }); 
    });
});