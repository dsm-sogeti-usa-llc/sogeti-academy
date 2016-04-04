import {PresentationsController} from './PresentationsController';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';
import {PresentationDetailState} from '../../detail/PresentationDetailState';
import {createFile} from '../../../createFile';

describe('PresentationsController', () => {
    let createController: () => PresentationsController;
    let $state: angular.ui.IStateService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$mdMedia_, _$state_) => {
        $state = _$state_;
        this.$mdMedia = _$mdMedia_;
        
        createController = () => {
            return _$controller_(PresentationsController, {
                $mdMedia: this.$mdMedia,
                $state: $state
            });
        };
    }));
    
    it('should show list and detail', () => {
        spyOn(this, '$mdMedia').and.returnValue(true);
        
        const controller = createController();
        expect(controller.showList).toBeTruthy();
        expect(controller.showDetail).toBeTruthy();
        expect(this.$mdMedia).toHaveBeenCalledWith('gt-sm');
    });
    
    it('should show only list', () => {
        spyOn(this, '$mdMedia').and.returnValue(false);
        spyOn($state, 'is').and.returnValue(false);
        
        const controller = createController();
        expect(controller.showDetail).toBeFalsy();
        expect(controller.showList).toBeTruthy();
        expect($state.is).toHaveBeenCalledWith(PresentationDetailState);
        expect(this.$mdMedia).toHaveBeenCalledWith('gt-sm');
    });
    
    it('should show only detail', () => {
         spyOn(this, '$mdMedia').and.returnValue(false);
        spyOn($state, 'is').and.returnValue(true);
        
        const controller = createController();
        expect(controller.showDetail).toBeTruthy();
        expect(controller.showList).toBeFalsy();
        expect($state.is).toHaveBeenCalledWith(PresentationDetailState);
        expect(this.$mdMedia).toHaveBeenCalledWith('gt-sm');
    });
});