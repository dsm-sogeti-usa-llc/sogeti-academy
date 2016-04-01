import {PresentationDetailController} from './controllers/PresentationDetailController';

export class PresentationDetailDirective implements angular.IDirective {
    scope = {};
    controller = PresentationDetailController;
    controllerAs = '$detail';
    template = require('./templates/presentationDetail');
    
    static factory(): angular.IDirectiveFactory {
        return () => new PresentationDetailDirective();
    }
}

angular.module('sogeti-academy')
    .directive('presentationDetail', PresentationDetailDirective.factory());