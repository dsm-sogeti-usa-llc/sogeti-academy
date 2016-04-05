import {TopicsController} from './controllers/TopicsController';

import './styles/topics';
import './directives/createTopic/CreateTopicDirective';
import './directives/voteForTopic/VoteForTopicDirective';
export class TopicsDirective implements angular.IDirective {
    scope = {};
    controller = TopicsController;
    controllerAs = '$topics';
    template = require('./templates/topics');
    
    static factory(): angular.IDirectiveFactory {
        return () => new TopicsDirective();
    }
}

angular.module('sogeti-academy')
    .directive('topics', TopicsDirective.factory());