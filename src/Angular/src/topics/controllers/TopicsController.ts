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
        this.$mdDialog.show(options);
    }
    
    voteForTopic(topic: Topic): void {
        const options: angular.material.IDialogOptions = {
            template: '<vote-for-topic></vote-for-topic>',
            hasBackdrop: true,
            clickOutsideToClose: false,
            escapeToClose: false,
            autoWrap: false,
            locals: {
                topic: topic
            }
        }
        this.$mdDialog.show(options);
    }
}