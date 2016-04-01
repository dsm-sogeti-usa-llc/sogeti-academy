import {PresentationsController} from './controllers/PresentationsController';

import '../list/PresentationListDirective';
import '../add/AddPresentationDirective';
import '../detail/PresentationDetailDirective';
export class PresentationsDirective implements angular.IDirective {
    scope = {};
    controller = PresentationsController;
    controllerAs = '$pres';
    template = require('./templates/presentations');
       
    static factory(): angular.IDirectiveFactory {
        return () => new PresentationsDirective();
    }
}

angular.module('sogeti-academy')
    .directive('presentations', PresentationsDirective.factory());