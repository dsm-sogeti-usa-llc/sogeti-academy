import {ToolbarController} from './ToolbarController';
import {INavigationState} from '../../core/INavigationState';

describe('ToolbarController', () => {
    let createController: () => ToolbarController;
    let $state: angular.ui.IStateService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$state_, _$mdMedia_, _$mdSidenav_) => {
        this.$mdMedia = _$mdMedia_;
        this.$mdSidenav = _$mdSidenav_;
        $state = _$state_;
        
        createController = () => {
            return _$controller_(ToolbarController, {
                $state: $state, 
                $mdMedia: this.$mdMedia,
                $mdSidenav: this.$mdSidenav
            });
        };
    }));
    
    it('should use title of current state', () => {
        $state.current = <INavigationState>{
            title: 'Something'
        };
        
        const controller = createController();
        expect(controller.title).toBe('Something');
    });
    
    it('should show nav toggle if less than large', () => {
         spyOn(this, '$mdMedia').and.callFake(size => {
             return size !== 'gt-md'
         });
         
         const controller = createController();
         expect(controller.showNavToggle).toBeTruthy();
    });
    
    it('should hide nav toggle if greater than medium', () => {
        spyOn(this, '$mdMedia').and.callFake(size => {
             return size === 'gt-md'
         });
         
         const controller = createController();
         expect(controller.showNavToggle).toBeFalsy();
    });
    
    it('should toggle nav', () => {
        const sideNavObject = jasmine.createSpyObj<angular.material.ISidenavObject>('sideNavObject', ['toggle']);
        spyOn(this, '$mdSidenav').and.returnValue(sideNavObject);
        
        const controller = createController();
        controller.toggleNav();
        
        expect(this.$mdSidenav).toHaveBeenCalledWith('left');
        expect(sideNavObject.toggle).toHaveBeenCalledWith();
    })
});