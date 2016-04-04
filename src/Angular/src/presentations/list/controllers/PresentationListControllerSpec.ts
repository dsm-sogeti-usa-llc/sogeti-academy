import {Presentation} from '../models/Presentation';
import {PresentationListController} from './PresentationListController';
import {PresentationListService} from '../services/PresentationListService';
import {AddPresentationViewModel} from '../../add/models/AddPresentationViewModel';
import {PresentationDetailState} from '../../detail/PresentationDetailState';
import {createFile} from '../../../createFile';

describe('PresentationListController', () => {
    let $q: angular.IQService;
    let $scope: angular.IScope;
    let $state: angular.ui.IStateService;
    let $rootScope: angular.IRootScopeService;
    let $mdDialog: angular.material.IDialogService;
    let presenationListService: PresentationListService;
    let presentations: Presentation[]; 
    let presentationsPromise: Promise<Presentation[]>
    let createController: () => PresentationListController;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$rootScope_, _$state_, _$mdDialog_, _$q_, _PresentationListService_) => {
        $q = _$q_;
        $state = _$state_;
        $mdDialog = _$mdDialog_;
        $rootScope = _$rootScope_;
        $scope = _$rootScope_.$new();
        
        presentations = [];
        presenationListService = _PresentationListService_;
        spyOn(presenationListService, 'getPresentations').and.callFake(() => {
            presentationsPromise = Promise.resolve(presentations);
            return presentationsPromise;
        });
        
        createController = () => {
            return _$controller_(PresentationListController, {
                $q: $q,
                $scope: $scope,
                $mdDialog: $mdDialog,
                $state: $state,
                PresentationListService: presenationListService
            })
        };
    }));
    
    it('should get presentations from service', (done) => {
        spyOn($scope, '$applyAsync').and.callFake(cb => cb());

        presentations.push({ id: '', topic: '', description: '', filesCount: 0 });
        presentations.push({ id: '', topic: '', description: '', filesCount: 0 });
        presentations.push({ id: '', topic: '', description: '', filesCount: 0 });
        
        const controller = createController();
        expect(controller.isLoading).toBeTruthy();
        presentationsPromise.then(() => {
            expect(controller.presentations).toEqual(presentations);
            expect(controller.isLoading).toBeFalsy();
            done();
        });
    });
    
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
    
    it('should add presentation to presentations', () => {
        const viewModel: AddPresentationViewModel = {
            id: 'asdfasdf',
            topic: 'stuff',
            description: 'desc',
            files: [
                createFile(),
                createFile(),
                createFile()
            ]
        };
        spyOn($mdDialog, 'show').and.callFake(() => $q.resolve(viewModel)); 
        
        const controller = createController();
        controller.addPresentation();
        $rootScope.$digest();
        expect(controller.presentations[0].description).toBe(viewModel.description);    
        expect(controller.presentations[0].filesCount).toBe(viewModel.files.length);    
        expect(controller.presentations[0].topic).toBe(viewModel.topic);    
        expect(controller.presentations[0].id).toBe(viewModel.id);    
    });
    
    it('should go to detail', () => {
        spyOn($state, 'go').and.callFake(() => {});
        
        const viewModel: Presentation = {
            id: 'somethingid',
            topic: 'not good',
            description: 'des',
            filesCount: 5  
        };
        
        const controller = createController();
        controller.goToDetail(viewModel);
        expect($state.go).toHaveBeenCalledWith(PresentationDetailState, {
            id: viewModel.id
        });
    })
});