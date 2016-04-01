import {PresentationDetailController} from './PresentationDetailController';
import {PresentationDetailService} from '../services/PresentationDetailService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';

describe('PresentationDetailController', () => {
    let $state: angular.ui.IStateService;
    let $scope: angular.IScope;
    let presentationDetailService: PresentationDetailService;
    let createController: () => PresentationDetailController; 
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$state_, _$rootScope_, _PresentationDetailService_) => {
        $state = _$state_;
        $scope = _$rootScope_.$new();
        presentationDetailService = _PresentationDetailService_;
        
        createController = () => {
            return _$controller_(PresentationDetailController, {
                $state: $state,
                $scope: $scope,
                PresentationDetailService: presentationDetailService
            });
        };
    }));
    
    it('should get presentation detail', (done) => {
        spyOn($scope, '$applyAsync').and.callFake(cb => cb());
        const viewModel: PresentationDetailViewModel = {
            id: 'presId',
            topic: 'TDD',
            description: 'Don\'t do it.',
            files: [
                { id: 'dfd', name: 'dep', type: 'stuff' },
                { id: 'did', name: 'of', type: 'good' },
                { id: 'dod', name: 'def', type: 'not' }
            ]
        };
        
        let getDetailPromise: Promise<PresentationDetailViewModel>;
        spyOn(presentationDetailService, 'getDetail').and.callFake(() => {
            getDetailPromise = Promise.resolve<PresentationDetailViewModel>(viewModel);
            return getDetailPromise;
        });
        
        $state.params['id'] = '630'
        const controller = createController();
        getDetailPromise.then(() => {
            expect(controller.presentationId).toBe(viewModel.id);
            expect(controller.topic).toBe(viewModel.topic);
            expect(controller.description).toBe(viewModel.description);
            expect(controller.files).toEqual(viewModel.files);
            expect(presentationDetailService.getDetail).toHaveBeenCalledWith('630');
            expect($scope.$applyAsync).toHaveBeenCalledWith(jasmine.any(Function));
            done();
        })
    });
});