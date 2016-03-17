import {CreateTopicController} from './CreateTopicController';

describe('CreateTopicController', () => {
    let createController: () => CreateTopicController; 
    let $mdDialog: angular.material.IDialogService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$mdDialog_) => {
        $mdDialog = _$mdDialog_;
        
        createController = () => {
            return _$controller_(CreateTopicController, {
                $mdDialog: $mdDialog
            })
        };
    }));
    
    it('should hide dialog', () => {
        spyOn($mdDialog, 'hide').and.callFake(() => {});
        
        const controller = createController();
        controller.cancel();
        expect($mdDialog.hide).toHaveBeenCalled(); 
    });
});