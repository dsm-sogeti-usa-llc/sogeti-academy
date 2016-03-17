import {TopicsController} from './TopicsController';
import {TopicsService} from '../services/TopicsService';
import {Topic} from '../models/Topic';

describe('TopicsController', () => {
    let $mdDialog: angular.material.IDialogService;
    let $scope: angular.IScope;
    let topicsService: TopicsService;
    let topics: Topic[];
    let topicsPromise: Promise<Topic[]>;
    let createController: () => TopicsController;
    
    beforeEach(angular.mock.module('sogeti-academy'));
    
    beforeEach(angular.mock.inject((_$controller_, _$mdDialog_, _$rootScope_, _TopicsService_) => {
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
        spyOn($mdDialog, 'show').and.callFake(o => dialogOptions = o);
        
        const controller = createController();
        controller.createTopic();
        expect($mdDialog.show).toHaveBeenCalledWith(dialogOptions);
        expect(dialogOptions.template).toBe('<create-topic></create-topic>');
        expect(dialogOptions.hasBackdrop).toBeTruthy();
        expect(dialogOptions.clickOutsideToClose).toBeFalsy();
        expect(dialogOptions.escapeToClose).toBeFalsy();
        expect(dialogOptions.autoWrap).toBeFalsy();
    });
    
    it('should get topics', (done) => {
        spyOn($scope, '$apply').and.callThrough();
        
        topics.push({ Name: 'one', Votes: 4 });
        topics.push({ Name: 'two', Votes: 24 });
        topics.push({ Name: 'three', Votes: 41 });
        
        const controller = createController();
        topicsPromise.then(() => {
            expect(controller.topics).toEqual(topics);
            expect($scope.$apply).toHaveBeenCalled();
            done();    
        });
    });
    
    it('should show vote for topic dialog', () => {
        let dialogOptions: angular.material.IDialogOptions;
        spyOn($mdDialog, 'show').and.callFake(o => dialogOptions = o);
        
        const topic: Topic = {Id: 'not now', Name: 'something', Votes: 6 };
        const controller = createController();
        controller.voteForTopic(topic);
        
        expect($mdDialog.show).toHaveBeenCalledWith(dialogOptions);
        expect(dialogOptions.template).toBe('<vote-for-topic></vote-for-topic>');
        expect(dialogOptions.hasBackdrop).toBeTruthy();
        expect(dialogOptions.clickOutsideToClose).toBeFalsy();
        expect(dialogOptions.escapeToClose).toBeFalsy();
        expect(dialogOptions.autoWrap).toBeFalsy();
        expect(dialogOptions.locals).toEqual({
            topic: topic
        });
    })
})