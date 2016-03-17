import {VoteForTopicController} from './VoteForTopicController';

describe('VoteForTopicController', () => {
    let $mdDialog: angular.material.IDialogService;
    let createController: () => VoteForTopicController;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$mdDialog_, _$controller_) => {
        $mdDialog = _$mdDialog_;
        
        createController = () => {
            return _$controller_(VoteForTopicController, {
                $mdDialog: $mdDialog
            });
        };
    }));
    
    it('should hide dialog', () => {
        spyOn($mdDialog, 'hide').and.callThrough();
        
        const controller = createController();
        controller.cancel();
        expect($mdDialog.hide).toHaveBeenCalled(); 
    });
})