import '../sidenav/SideNavDirective';
import '../toolbar/ToolbarDirective';

import {ApplicationController} from './controllers/ApplicationController';

export class ApplicationDirective implements angular.IDirective {
    scope = {};
    controller = ApplicationController;
    controllerAs = '$app';
    template = require('./templates/application');
    
    static factory(): angular.IDirectiveFactory {
        return () => new ApplicationDirective();
    }
}

angular.module('sogeti-academy')
    .directive('application', ApplicationDirective.factory());