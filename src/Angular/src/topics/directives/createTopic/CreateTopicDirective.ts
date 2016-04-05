import {CreateTopicController} from './controllers/CreateTopicController';

export class CreateTopicDirective implements angular.IDirective {
    scope = {};
    controller = CreateTopicController;
    controllerAs = '$create';
    template = require('./templates/createTopic');
    
    static factory(): angular.IDirectiveFactory {
        return () => new CreateTopicDirective();
    }
}

angular.module('sogeti-academy')
    .directive('createTopic', CreateTopicDirective.factory());