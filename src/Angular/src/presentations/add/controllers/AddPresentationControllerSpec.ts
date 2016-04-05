import {AddPresentationController} from './AddPresentationController';
import {AddPresentationService} from '../services/AddPresentationService';
import {AddPresentationViewModel} from '../models/AddPresentationViewModel';
import {createFile} from '../../../createFile';

import '../services/AddPresentationService';
describe('AddPresentationController', () => {
    let createController: () => AddPresentationController;
    let addPresentationService: AddPresentationService;
    let form: angular.IFormController;
    let $mdDialog: angular.material.IDialogService;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$controller_, _$mdDialog_, _AddPresentationService_) => {
        $mdDialog = _$mdDialog_;
        addPresentationService = _AddPresentationService_;
        form = jasmine.createSpyObj<angular.IFormController>('form', ['$valid']);

        createController = () => {
            const controller = _$controller_(AddPresentationController, {
                $mdDialog: $mdDialog,
                AddPresentationService: addPresentationService
            });
            controller.form = form;
            return controller;
        };
    }));

    it('should save add presentation view model', (done) => {
        let savePromise: Promise<string>;
        let savedViewModel: AddPresentationViewModel;
        spyOn(addPresentationService, 'save').and.callFake((saved) => {
            savedViewModel = angular.copy<AddPresentationViewModel>(saved);
            savePromise = Promise.resolve('something');
            return savePromise;
        });

        const controller = createController();

        controller.topic = 'one';
        controller.description = 'two';
        controller.files = [createFile(), createFile(), createFile()];
        
        form.$valid = true;
        controller.save();
        expect(controller.isSaving).toBeTruthy();
        savePromise.then(() => {
            expect(addPresentationService.save).toHaveBeenCalled();
            expect(savedViewModel).toEqual({
                topic: 'one',
                description: 'two',
                files: controller.files
            });
            expect(controller.isSaving).toBeFalsy();
            done();
        });
    });

    it('should not save if form is not valid', () => {
        spyOn(addPresentationService, 'save').and.callThrough();

        const controller = createController();
        form.$valid = false;

        controller.save();
        expect(addPresentationService.save).not.toHaveBeenCalled();
    });

    it('should close modal when save finishes', (done) => {
        let savePromise: Promise<string>;
        spyOn(addPresentationService, 'save').and.callFake(() => {
            savePromise = Promise.resolve('someId');
            return savePromise;
        });
        spyOn($mdDialog, 'hide').and.callThrough();

        const controller = createController();
        form.$valid = true;
        controller.topic = 'something';
        controller.description = 'desc something';

        controller.save();
        savePromise.then(() => {
            expect($mdDialog.hide).toHaveBeenCalledWith({
                id: 'someId',
                topic: 'something',
                description: 'desc something',
                files: []
            });
            done();
        });
    });

    it('should cancel dialog', () => {
        spyOn($mdDialog, 'cancel').and.callThrough();

        const controller = createController();
        controller.cancel();
        expect($mdDialog.cancel).toHaveBeenCalled();
    });

    it('should not be able to save', () => {
        form.$valid = false;

        const controller = createController();
        expect(controller.canSave).toBeFalsy();

        form.$valid = true;
        expect(controller.canSave).toBeTruthy();
    });
    
    it('should not be able to save', () => {
        const controller = createController();
        controller.form = undefined;
        expect(controller.canSave).toBeFalsy();
    });
    
    it('should set files', () => {
        const files: File[] = [
            createFile(),
            createFile(),
            createFile()
        ];
        
        const controller = createController();
        controller.selectFiles(files);
        expect(controller.files.length).toBe(files.length);
    });
    
    it('should remove file from files', () => {
        const files: File[] = [
            createFile(),
            createFile(),
            createFile()
        ];
        
        const controller = createController();
        controller.selectFiles(files);
        
        const removedFile = files[2];
        controller.removeFile(removedFile);
        expect(controller.files.indexOf(removedFile)).toBe(-1);
    });
})