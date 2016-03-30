import {PresentationListController} from './controllers/PresentationListController';

export class PresentationListDirective implements angular.IDirective {
    scope = {};
    controller = PresentationListController;
    controllerAs = '$presList';
    template = require('./templates/presentationList');
    
    static factory(): angular.IDirectiveFactory {
        return () => new PresentationListDirective();
    }
}

angular.module('sogeti-academy')
    .directive('presentationList', PresentationListDirective.factory());
