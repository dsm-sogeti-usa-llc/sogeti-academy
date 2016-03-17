import {WelcomeController} from './controllers/WelcomeController';

import './styles/welcome';
export class WelcomeDirective implements angular.IDirective {
    scope = {};
    controller = WelcomeController;
    controllerAs = '$welcome';
    template = require('./templates/welcome');
    
    static factory(): angular.IDirectiveFactory {
        return () => new WelcomeDirective();
    }
}

angular.module('sogeti-academy')
    .directive('welcome', WelcomeDirective.factory());