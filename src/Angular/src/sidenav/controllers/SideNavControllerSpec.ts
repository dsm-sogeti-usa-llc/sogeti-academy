import {SidenavController} from './SidenavController';
import {SidenavService} from '../services/SidenavService';
import {INavigationState} from '../../core/INavigationState';
import {WelcomeState} from '../../welcome/states/WelcomeState';

describe('SidenavController', () => {
    let $state: angular.ui.IStateService;
    let createController: () => SidenavController;
    let sidenavService: SidenavService;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$controller_, _$mdMedia_, _$state_, _SidenavService_) => {
        this.$mdMedia = _$mdMedia_;
        $state = _$state_;
        sidenavService = _SidenavService_;

        createController = () => {
            return _$controller_(SidenavController, {
                $mdMedia: this.$mdMedia,
                $state: $state,
                SidenavService: sidenavService
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
        spyOn(sidenavService, 'getAllStates').and.returnValue([
            {},
            {},
            {}
        ]);

        const controller = createController();
        expect(controller.states.length).toBe(3);
    });

    it('should have default state', () => {
        spyOn(sidenavService, 'getAllStates').and.returnValue([
            {},
            { isDefault: true },
            {}
        ]);

        const controller = createController();
        expect(controller.defaultState).toEqual({ isDefault: true });
    });

    it('should navigate to state', () => {
        const newState: INavigationState = {
            name: 'bobo'
        };
        spyOn($state, 'go').and.callFake(() => {});

        const controller = createController();
        controller.isSidenavOpen = true;
        controller.navigate(newState);
        
        expect($state.go).toHaveBeenCalledWith(newState);
        expect(controller.isSidenavOpen).toBeFalsy();
    });
});