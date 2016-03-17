import {VoteForTopicController} from './controllers/VoteForTopicController';

export class VoteForTopicDirective implements angular.IDirective {
    scope = {};
    controller = VoteForTopicController;
    controllerAs = '$vote';
    template = require('./templates/voteForTopic');
    
    static factory(): angular.IDirectiveFactory {
        return () => new VoteForTopicDirective();
    }
}

angular.module('sogeti-academy')
    .directive('voteForTopic', VoteForTopicDirective.factory());