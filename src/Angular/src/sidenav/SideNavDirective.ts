import {SidenavController} from './controllers/SidenavController';

import './styles/sidenav';
export class SidenavDirective implements angular.IDirective {
    scope = {};
    controller = SidenavController;
    controllerAs = '$sidenav';
    template = require('./templates/sidenav');
    
    static factory(): angular.IDirectiveFactory {
        return () => new SidenavDirective();
    }
}

angular.module('sogeti-academy')
    .directive('sidenav', SidenavDirective.factory());