import {CreateTopicController} from './CreateTopicController';
import {TopicsService} from '../../../services/TopicsService';

describe('CreateTopicController', () => {
    let $mdDialog: angular.material.IDialogService;
    let topicsService: TopicsService;
    let form: angular.IFormController;
    let createController: () => CreateTopicController; 
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$mdDialog_, _TopicsService_) => {
        $mdDialog = _$mdDialog_;
        topicsService = _TopicsService_;
        form = jasmine.createSpyObj<angular.IFormController>('form', ['$valid']);
        
        createController = () => {
            const controller = _$controller_(CreateTopicController, {
                $mdDialog: $mdDialog
            });
            controller.form = form;
            return controller;
        };
    }));
    
    it('should hide dialog', () => {
        spyOn($mdDialog, 'cancel').and.callFake(() => {});
        
        const controller = createController();
        controller.cancel();
        expect($mdDialog.cancel).toHaveBeenCalled(); 
    });
    
    it('should not allow save', () => {
        const controller = createController();
        controller.form = undefined;
        expect(controller.canSave).toBeFalsy();
        
        controller.form = form;
        form.$valid = false;
        expect(controller.canSave).toBeFalsy();
        
        form.$valid = true;
        expect(controller.canSave).toBeTruthy(); 
    });
    
    it('should not create topic', () => {
        spyOn(topicsService, 'createTopic').and.callFake(() => {});
        spyOn($mdDialog, 'hide').and.callThrough();
        form.$valid = false;
        
        const controller = createController();
        controller.save();
        expect(topicsService.createTopic).not.toHaveBeenCalled();
        expect($mdDialog.hide).not.toHaveBeenCalled();
    });
    
    it('should create topic', (done) => {
        let createTopicPromise: Promise<string>;
        spyOn(topicsService, 'createTopic').and.callFake(() => {
            createTopicPromise = Promise.resolve('asdfasdf');
            return createTopicPromise; 
        });
        spyOn($mdDialog, 'hide').and.callThrough();
        form.$valid = true;
        
        const controller = createController();
        controller.topic.name = 'my Name';
        
        controller.save();
        expect(controller.isSaving).toBeTruthy();
        createTopicPromise.then(id => {
            expect(controller.isSaving).toBeFalsy();
            expect($mdDialog.hide).toHaveBeenCalledWith({
                name: 'my Name',
                id: id,
                votes: 0   
            });
            done();
        });
    });
});