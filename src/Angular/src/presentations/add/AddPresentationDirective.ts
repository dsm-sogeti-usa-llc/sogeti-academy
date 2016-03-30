import {AddPresentationController} from './controllers/AddPresentationController';

export class AddPresentationDirective implements angular.IDirective {
    scope = {};
    controller = AddPresentationController;
    controllerAs = '$add';
    template = require('./templates/addPresentation');
    
    static factory(): angular.IDirectiveFactory {
        return () => new AddPresentationDirective();
    }   
}

angular.module('sogeti-academy')
    .directive('addPresentation', AddPresentationDirective.factory());