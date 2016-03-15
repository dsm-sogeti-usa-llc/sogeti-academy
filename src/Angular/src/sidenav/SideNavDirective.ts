import {SideNavController} from './controllers/SideNavController';

export class SideNavDirective implements angular.IDirective {
    scope = {};
    controller = SideNavController;
    controllerAs = '$sideNav';
    template = require('./templates/sideNav');
    
    static factory(): angular.IDirectiveFactory {
        return () => new SideNavDirective();
    }
}

angular.module('sogeti-academy')
    .directive('sideNav', SideNavDirective.factory());