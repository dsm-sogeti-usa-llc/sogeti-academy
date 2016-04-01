import {VoteForTopicController} from './VoteForTopicController';
import {TopicsService} from '../../../services/TopicsService';
import {Topic} from '../../../models/Topic';
import {Vote} from '../../../models/Vote';

describe('VoteForTopicController', () => {
    let $mdDialog: angular.material.IDialogService;
    let topic: Topic;
    let topicsService: TopicsService;
    let createController: () => VoteForTopicController;
    let form: angular.IFormController;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$mdDialog_, _$controller_, _TopicsService_) => {
        $mdDialog = _$mdDialog_;
        topicsService = _TopicsService_;
        form = jasmine.createSpyObj<angular.IFormController>('form', ['$valid']);
        topic = { name: 'Top', votes: 5, id: 'asdgsad' };

        createController = () => {
            return _$controller_(VoteForTopicController, {
                $mdDialog: $mdDialog,
                TopicsService: topicsService
            });
        };
    }));

    it('should hide dialog', () => {
        spyOn($mdDialog, 'cancel').and.callThrough();

        const controller = createController();
        controller.cancel();
        expect($mdDialog.cancel).toHaveBeenCalled();
    });

    it('should have topic name', () => {
        const controller = createController();
        controller.topic = topic;
        expect(controller.topic.name).toBe(topic.name);
    });

    it('should vote for topic', (done) => {
        let voteForTopicPromise: Promise<void>;
        spyOn(topicsService, 'voteForTopic').and.callFake((vote: Vote) => {
            voteForTopicPromise = Promise.resolve<void>();
            return voteForTopicPromise;
        });
        spyOn($mdDialog, 'hide').and.callThrough();

        const controller = createController();
        controller.topic = topic;
        controller.email = 'bryce.klinker@us.sogeti.com';
        
        controller.save();
        expect(controller.isSaving).toBeTruthy();
        voteForTopicPromise.then(() => {
            expect(controller.isSaving).toBeFalsy();
            expect(topicsService.voteForTopic).toHaveBeenCalledWith({ topicId: topic.id, email: controller.email });
            expect($mdDialog.hide).toHaveBeenCalledWith(true);
            done();
        });
    });
    
    it('should not be able to save', () => {
        const controller = createController();
        expect(controller.canSave).toBeFalsy();
        
        form.$valid = false;
        controller.form = form;
        expect(controller.canSave).toBeFalsy();
        
        form.$valid = true;
        expect(controller.canSave).toBeTruthy();
    });
});