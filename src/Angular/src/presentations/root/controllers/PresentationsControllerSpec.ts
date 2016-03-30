import {PresentationsController} from './PresentationsController';

describe('PresentationsController', () => {
    let createController: () => PresentationsController;
    let $mdDialog: angular.material.IDialogService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$mdDialog_) => {
        $mdDialog = _$mdDialog_;
        
        createController = () => {
            return _$controller_(PresentationsController, {
                $mdDialog: $mdDialog
            });
        };
    }));
    
    it('should open add presentation dialog', () => {
        spyOn($mdDialog, 'show').and.callThrough();
        
        const controller = createController();
        controller.addPresentation();
        expect($mdDialog.show).toHaveBeenCalledWith({
            template: '<add-presentation></add-presentation>',
            hasBackdrop: true,
            clickOutsideToClose: false,
            escapeToClose: false,
            autoWrap: false
        })
    });
});