import {TopicsService} from '../services/TopicsService';
import {Topic} from '../models/Topic';

import '../services/TopicsService';
export class TopicsController {
    static $inject = ['$scope', '$mdDialog', 'TopicsService'];
    topics: Topic[];
    
    constructor(private $scope: angular.IScope,
        private $mdDialog: angular.material.IDialogService,
        private topicsService: TopicsService) {
            
        this.topics = [];
        this.topicsService.getAll().then(topics => {
            this.topics = topics;
            this.$scope.$apply();
        });    
    }
    
    createTopic(): void {
        const options: angular.material.IDialogOptions = {
            template: '<create-topic></create-topic>',
            hasBackdrop: true,
            clickOutsideToClose: false,
            escapeToClose: false,
            autoWrap: false
        }
        this.$mdDialog.show(options)
            .then(topic => this.topics.push(topic));
    }
    
    voteForTopic(topic: Topic): void {
        const dialogScope = this.$scope.$new();
        dialogScope['topic'] = topic;
        const options: angular.material.IDialogOptions = {
            template: '<vote-for-topic topic="topic"></vote-for-topic>',
            hasBackdrop: true,
            clickOutsideToClose: false,
            escapeToClose: false,
            autoWrap: false,
            scope: dialogScope
        };
        this.$mdDialog.show(options)
            .then(() => topic.Votes++);
    }
}