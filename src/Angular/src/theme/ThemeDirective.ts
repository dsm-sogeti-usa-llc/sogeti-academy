import {ThemeController} from './controllers/ThemeController';


export class ThemeDirective implements angular.IDirective {
    scope = {};
    controller = ThemeController;
    controllerAs = '$theme';
    template = require('./templates/theme');
    
    static factory(): angular.IDirectiveFactory {
        return () => new ThemeDirective();
    }
}

angular.module('sogeti-academy')
    .directive('theme', ThemeDirective.factory());