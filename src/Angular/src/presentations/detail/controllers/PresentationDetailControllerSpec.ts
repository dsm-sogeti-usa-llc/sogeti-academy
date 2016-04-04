import {PresentationDetailController} from './PresentationDetailController';
import {PresentationDetailService} from '../services/PresentationDetailService';
import {PresentationDetailViewModel} from '../models/PresentationDetailViewModel';
import {FileDetailViewModel} from '../models/FileDetailViewModel';
import {PresentationsState} from '../../PresentationsState';

describe('PresentationDetailController', () => {
    let $state: angular.ui.IStateService;
    let $scope: angular.IScope;
    let form: angular.IFormController;
    let viewModel: PresentationDetailViewModel;
    let getDetailPromise: Promise<PresentationDetailViewModel>;
    let savePromise: Promise<void>;
    let presentationDetailService: PresentationDetailService;
    let createController: () => PresentationDetailController;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$controller_, _$state_, _$rootScope_, _PresentationDetailService_) => {
        $state = _$state_;

        $scope = _$rootScope_.$new();
        spyOn($scope, '$applyAsync').and.callFake(cb => cb());

        form = jasmine.createSpyObj<angular.IFormController>('form', ['$valid']);

        viewModel = {
            id: 'presId',
            topic: 'TDD',
            description: 'Don\'t do it.',
            files: [
                { id: 'dfd', name: 'dep', type: 'stuff', size: 234 },
                { id: 'did', name: 'of', type: 'good', size: 234 },
                { id: 'dod', name: 'def', type: 'not', size: 234 }
            ]
        };

        presentationDetailService = _PresentationDetailService_;
        spyOn(presentationDetailService, 'getDetail').and.callFake(() => {
            getDetailPromise = Promise.resolve<PresentationDetailViewModel>(viewModel);
            return getDetailPromise;
        });
        spyOn(presentationDetailService, 'save').and.callFake(() => {
            savePromise = Promise.resolve<void>();
            return savePromise;
        });

        createController = () => {
            const controller = _$controller_(PresentationDetailController, {
                $state: $state,
                $scope: $scope,
                PresentationDetailService: presentationDetailService
            });
            controller.form = form;
            return controller;
        };
    }));

    it('should get presentation detail', (done) => {
        $state.params['id'] = '630'
        const controller = createController();
        expect(controller.isLoading).toBeTruthy();
        getDetailPromise.then(() => {
            expect(controller.presentationId).toBe(viewModel.id);
            expect(controller.topic).toBe(viewModel.topic);
            expect(controller.description).toBe(viewModel.description);
            expect(controller.files).toEqual(viewModel.files);
            expect(controller.isLoading).toBeFalsy();
            expect(presentationDetailService.getDetail).toHaveBeenCalledWith('630');
            expect($scope.$applyAsync).toHaveBeenCalledWith(jasmine.any(Function));
            done();
        });
    });

    it('should save presentation detail', (done) => {
        const controller = createController();
        getDetailPromise.then(() => {
            controller.form.$valid = true;

            controller.save();
            expect(controller.isSaving).toBeTruthy();
            savePromise.then(() => {
                expect(controller.isSaving).toBeFalsy();
                expect(presentationDetailService.save).toHaveBeenCalledWith(viewModel);
                expect($scope.$applyAsync).toHaveBeenCalledWith(jasmine.any(Function));
                done();
            });
        });
    });

    it('should not save presentation detail', (done) => {
        form.$valid = false;

        const controller = createController();
        getDetailPromise.then(() => {
            controller.save();
            expect(controller.isSaving).toBeFalsy();
            expect(presentationDetailService.save).not.toHaveBeenCalled();
            done();
        })
    });

    it('should download file', (done) => {
        spyOn(presentationDetailService, 'downloadFile').and.callFake(() => Promise.resolve<void>());

        const file: FileDetailViewModel = {
            id: '2344',
            type: '',
            name: '',
            size: 8234
        }
        const controller = createController();
        getDetailPromise.then(() => {
            controller.downloadFile(file);
            expect(presentationDetailService.downloadFile).toHaveBeenCalledWith(file, viewModel);
            done();
        });
    });

    it('should go back to list', () => {
        spyOn($state, 'go').and.callFake(() => { });

        const controller = createController();
        controller.goBack();
        expect($state.go).toHaveBeenCalledWith(PresentationsState);
    });

    it('should not be able to save', (done) => {
        form.$valid = false;

        const controller = createController();
        getDetailPromise.then(() => {
            expect(controller.canSave).toBeFalsy();
            done();
        });
    });
    
    it('should not be able to save if loading', () => {
        form.$valid = true;

        const controller = createController();
        expect(controller.canSave).toBeFalsy();
    });
    
    it('should be able to save', (done) => {
        form.$valid = true;

        const controller = createController();
        getDetailPromise.then(() => {
            expect(controller.canSave).toBeTruthy();
            done(); 
        });
    });
    
    it('should not be able to save if already saving', (done) => {
        form.$valid = true;
        
        const controller = createController();
        getDetailPromise.then(() => {
            controller.save();
            expect(controller.canSave).toBeFalsy(); 
            done();
        });
    });
    
    it('should be able to save if done saving', (done) => {
        form.$valid = true;
        
        const controller = createController();
        getDetailPromise.then(() => {
            controller.save();
            savePromise.then(() => {
                expect(controller.canSave).toBeTruthy();
                done();
            });
        });
    });
    
    it('should not be able to save if form is undefined', () => {
        const controller = createController();
        controller.form = undefined;
        expect(controller.canSave).toBeFalsy();
    });
    
    it('should have default values if view model is not loaded', () => {
        const controller = createController(); 
        expect(controller.topic).toBe('Loading...');
        expect(controller.description).toBe('');
        expect(controller.files).toEqual([]);
    });
});