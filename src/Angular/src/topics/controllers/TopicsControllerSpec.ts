import {TopicsController} from './TopicsController';
import {TopicsService} from '../services/TopicsService';
import {Topic} from '../models/Topic';

describe('TopicsController', () => {
    let $mdDialog: angular.material.IDialogService;
    let $scope: angular.IScope;
    let $q: angular.IQService;
    let topicsService: TopicsService;
    let topics: Topic[];
    let topicsPromise: Promise<Topic[]>;
    let createController: () => TopicsController;

    beforeEach(angular.mock.module('sogeti-academy'));

    beforeEach(angular.mock.inject((_$controller_, _$mdDialog_, _$rootScope_, _$q_, _TopicsService_) => {
        $q = _$q_;
        $mdDialog = _$mdDialog_;
        $scope = _$rootScope_.$new();

        topics = [];
        topicsService = _TopicsService_;
        spyOn(topicsService, 'getAll').and.callFake(() => {
            topicsPromise = Promise.resolve(topics);
            return topicsPromise;
        });

        createController = () => {
            return _$controller_(TopicsController, {
                $mdDialog: $mdDialog,
                $scope: $scope,
                TopicsService: topicsService
            });
        };
    }));

    it('should show create topic dialog', () => {
        let dialogOptions: angular.material.IDialogOptions;
        spyOn($mdDialog, 'show').and.callFake(o => {
            dialogOptions = o;
            return $q.resolve({});
        });

        const controller = createController();
        controller.createTopic();
        expect($mdDialog.show).toHaveBeenCalledWith(dialogOptions);
        expect(dialogOptions.template).toBe('<create-topic></create-topic>');
        expect(dialogOptions.hasBackdrop).toBeTruthy();
        expect(dialogOptions.clickOutsideToClose).toBeFalsy();
        expect(dialogOptions.escapeToClose).toBeFalsy();
        expect(dialogOptions.autoWrap).toBeFalsy();
    });

    it('should add created topic to topics', (done) => {
        const newTopic: Topic = { name: '2342', votes: 0 };
        spyOn($mdDialog, 'show').and.callFake(() => $q.resolve(newTopic));

        const controller = createController();
        topicsPromise.then(() => {
            controller.createTopic();
            $scope.$digest();

            expect(controller.topics.indexOf(newTopic)).toBeGreaterThan(-1);
            done();
        });
    });
    
    it('should get topics', (done) => {
        spyOn($scope, '$apply').and.callThrough();

        topics.push({ name: 'one', votes: 4 });
        topics.push({ name: 'two', votes: 24 });
        topics.push({ name: 'three', votes: 41 });

        const controller = createController();
        topicsPromise.then(() => {
            expect(controller.topics).toEqual(topics);
            expect($scope.$apply).toHaveBeenCalled();
            done();
        });
    });

    it('should show vote for topic dialog', () => {
        let dialogOptions: angular.material.IDialogOptions;
        spyOn($mdDialog, 'show').and.callFake(o => {
            dialogOptions = o;
            return $q.resolve();
        });

        const topic: Topic = { id: 'not now', name: 'something', votes: 6 };
        const controller = createController();
        controller.voteForTopic(topic);

        expect($mdDialog.show).toHaveBeenCalledWith(dialogOptions);
        expect(dialogOptions.template).toBe('<vote-for-topic topic="topic"></vote-for-topic>');
        expect(dialogOptions.hasBackdrop).toBeTruthy();
        expect(dialogOptions.clickOutsideToClose).toBeFalsy();
        expect(dialogOptions.escapeToClose).toBeFalsy();
        expect(dialogOptions.autoWrap).toBeFalsy();
        expect(dialogOptions.scope['topic']).toEqual(topic);
    });

    it('should add one to topic', () => {
        spyOn($mdDialog, 'show').and.callFake(() => {
           return $q.resolve(); 
        });
        
        const topic: Topic = { name: 'something', votes: 5 };
        const controller = createController();
        
        controller.voteForTopic(topic);
        $scope.$digest();
        
        expect(topic.votes).toBe(6);
    });
})