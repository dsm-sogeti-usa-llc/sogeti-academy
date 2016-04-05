import {ToolbarController} from './controllers/ToolbarController';

import './styles/toolbar';
export class ToolbarDirective implements angular.IDirective {
    scope = {};
    controller = ToolbarController;
    controllerAs = '$toolbar';
    template = require('./templates/toolbar');
    
    static factory(): angular.IDirectiveFactory {
        return () => new ToolbarDirective();
    }
}

angular.module('sogeti-academy')
    .directive('toolbar', ToolbarDirective.factory());