import {PresentationsController} from './PresentationsController';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';
import {createFile} from '../../../createFile';

describe('PresentationsController', () => {
    let createController: () => PresentationsController;
    let $mdDialog: angular.material.IDialogService;
    let $q: angular.IQService;
    let $rootScope: angular.IRootScopeService;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$rootScope_, _$mdDialog_, _$q_) => {
        $q = _$q_;
        $mdDialog = _$mdDialog_;
        $rootScope = _$rootScope_;
        
        createController = () => {
            return _$controller_(PresentationsController, {
                $mdDialog: $mdDialog,
                $q: _$q_
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
        });
    });
    
    it('should broadcast added presentation', () => {
        let viewModel: AddPresentationViewModel = {
            topic: 'stuff',
            description: 'desc',
            files: [
                createFile(),
                createFile(),
                createFile()
            ]
        }
        spyOn($mdDialog, 'show').and.callFake(() => $q.resolve(viewModel)); 
        spyOn($rootScope, '$broadcast').and.callThrough();
        
        const controller = createController();
        controller.addPresentation();
        $rootScope.$digest();
        
        expect($rootScope.$broadcast).toHaveBeenCalledWith('$presentation-added', viewModel);
    });
});